using AppGlobal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppGlobal.Entities;
using AppGlobal.Commons;
using AppGlobal.Models;
using AppGlobal.DBContext;
using Microsoft.EntityFrameworkCore;
using AppGlobal.Extensions;
using FirebaseAdmin.Auth;
using AppGlobal.Constants;

namespace Api.Business
{
    public class AttachmentBusiness
    {
        private readonly ERPContext _Context;

        public AttachmentBusiness(ERPContext context)
        {
            _Context = context;
        }

        

        public SearchResult<AttachmentModel> Search(int? attachmentType, int? targetItemType, string clinicID = "", string targetItemID = "", 
            string keySearch = "", int page = 1, int limit = 10)
        {
            var query = _Context.Attachments.AsNoTracking();

            if (attachmentType.HasValue)
            {
                query = query.Where(a => a.AttachmentType == attachmentType.Value);
            }

            if (targetItemType.HasValue)
            {
                query = query.Where(a => a.TargetItemType == targetItemType.Value);
            }

            if (!string.IsNullOrEmpty(clinicID))
            {
                var cid = Guid.Parse(clinicID);
                query = query.Where(a => a.ClinicID == cid);
            }

            if (!string.IsNullOrEmpty(targetItemID))
            {
                var tid = Guid.Parse(targetItemID);
                query = query.Where(a => a.TargetItemID == tid);
            }

            if (!string.IsNullOrEmpty(keySearch))
            {
                keySearch = keySearch.Standardizing();
                query = query.Where(a => a.AttachmentName != null && ApplicationDBContext.Standardizing(a.AttachmentName).Contains(keySearch));
            }

            var totals = query.Count();
            var items = query
                .OrderBy(c => c.DateCreated)
                .Skip((page - 1) * limit)
                .Take(limit)
                .Select(AttachmentModel.Convert)
                .ToList();

            return new SearchResult<AttachmentModel>()
            {
                Totals = totals,
                Items = items
            };
        }

        public AttachmentModel Get(string attachmentID)
        {
            var id = Guid.Parse(attachmentID);

            return _Context.Attachments.AsNoTracking()
                .Where(c => c.AttachmentID == id)
                .Select(AttachmentModel.Convert)
                .FirstOrDefault();
        }

        public AttachmentModel Create(string clinicID, string targetItemID, int targetItemType, AttachmentModelCreate body)
        {
            var cid = Guid.Parse(clinicID);
            var data = AttachmentModelCreate.Convert(body);
            data.ClinicID = cid;
            data.TargetItemID = Guid.Parse(targetItemID);
            data.TargetItemType = (byte)targetItemType;

            _Context.Attachments.Add(data);
            _Context.SaveChanges();

            return AttachmentModel.Convert(data);
        }

        public bool Delete(string clinicID, string attachmentID)
        {
            var cid = Guid.Parse(clinicID);
            var aid = Guid.Parse(attachmentID);

            var data = _Context.Attachments
                .FirstOrDefault(c => c.AttachmentID == aid && c.ClinicID == cid);

            if (data == null)
            {
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);
            }

            _Context.Attachments.Remove(data);
            _Context.SaveChanges();

            return true;
        }
    }
}
