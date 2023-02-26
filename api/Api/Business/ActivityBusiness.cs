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
    public class ActivityBusiness
    {
        private readonly ERPContext _Context;
        private readonly FirebaseCloudMessaging _FirebaseCloudMessaging;

        public ActivityBusiness(ERPContext context, FirebaseCloudMessaging FirebaseCloudMessaging)
        {
            _Context = context;
            _FirebaseCloudMessaging = FirebaseCloudMessaging;
        }

        public SearchResult<ActivityModel> Search(string userID, int page = 1, int limit = 10)
        {
            var guid = Guid.Parse(userID);

            var query = _Context.Activities
                .AsNoTracking()
                .Include(a => a.UserTargeted)
                .Where(a => a.UserTargetedID == guid);

            var items = query
                .OrderByDescending(a => a.DateCreated)
                .Skip((page - 1) * limit)
                .Take(limit)
                .Select(ActivityModel.Convert)
                .ToList();

            var totals = query.Count();

            return new SearchResult<ActivityModel>()
            {
                Totals = totals,
                Items = items
            };
        }

        public int CountNotRead(string userID)
        {
            var guid = Guid.Parse(userID);

            return _Context.Activities
                .AsNoTracking()
                .Where(a => a.UserTargetedID == guid && a.IsRead == false)
                .Count();
        }

        public bool Create(string userID, ActivityCreate data)
        {
            RemoveOverload(userID);

            var activity = new Activity()
            {
                ServiceCode = data.ServiceCode,
                ActionCode = data.ActionCode,
                StatusCode = data.StatusCode,
                ClinicID = data.ClinicID,
                TargetItemID = data.TargetItemID,
                Metadata = data.Metadata,
                UserTargetedID = Guid.Parse(userID),
                DateCreated = DateTime.Now.ToUniversalTime(),
            };

            _Context.Activities.Add(activity);
            _Context.SaveChanges();

            _ = _FirebaseCloudMessaging.SendMessageAsync(userID.ToLower(), data);

            return true;
        }

        public bool UpdateIsRead(string userID, string activityID) {
            var uguid = Guid.Parse(userID);
            var aguid = Guid.Parse(activityID);

            var item = _Context.Activities
                .Where(a => a.UserTargetedID == uguid && a.ActivityID == aguid)
                .FirstOrDefault();

            if (item == null)
            {
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);
            }

            item.IsRead = true;
            _Context.SaveChanges();

            return true;
        }

        public bool RemoveOverload(string userID)
        {
            var guid = Guid.Parse(userID);

            var overload = 50;
            var activities = _Context.Activities
                .Where(a => a.UserTargetedID == guid)
                .Skip(overload)
                .Take(1)
                .ToList();

            if (activities.Count() == 0)
            {
                return true;
            }

            _Context.Activities.RemoveRange(activities);
            _Context.SaveChanges();
            return true;
        }
    }
}
