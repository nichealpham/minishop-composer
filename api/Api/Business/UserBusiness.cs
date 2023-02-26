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

namespace Api.Business
{
    public class UserBusiness
    {
        private readonly ERPContext _Context;
        public UserBusiness(ERPContext context)
        {
            _Context = context;
        }

        public bool UpdateProfile(string userID, UserModelUpdate data, string clinicID = "")
        {
            var guid = Guid.Parse(userID);
            var user = _Context.Users
                .Where(u => u.UserID == guid)
                .FirstOrDefault();

            if (user == null)
            {
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);
            }

            user.FullName = data.FullName;
            user.GenderType = data.GenderType;
            user.Dob = data.Dob;
            user.Avatar = data.Avatar;
            user.Phone = data.Phone;
            user.Email = data.Email;
            user.IdentityID = data.IdentityID;
            user.Address = data.Address;
            user.Occupation = data.Occupation;
            user.Country = data.Country;

            if (!string.IsNullOrEmpty(clinicID))
            {
                var cid = Guid.Parse(clinicID);
                var role = _Context.Roles.FirstOrDefault(r => r.UserID == guid && r.ClinicID == cid);
                if (role != null)
                {
                    role.ProfileUrl = data.ProfileUrl;
                }
            }

            _Context.SaveChanges();
            return true;
        }

        public UserModelDetail GetProfile(string userID, string clinicID = "")
        {
            var guid = Guid.Parse(userID);
            var result = _Context.Users.AsNoTracking()
                .Where(u => u.UserID == guid)
                .Select(UserModelDetail.Convert)
                .FirstOrDefault();

            if (!string.IsNullOrEmpty(clinicID))
            {
                var cid = Guid.Parse(clinicID);
                var role = _Context.Roles.FirstOrDefault(r => r.UserID == guid && r.ClinicID == cid);
                if (role != null)
                {
                    result.ProfileUrl = role.ProfileUrl;
                }
            }

            return result;
        }

        public UserModel GetByPhone(string phone)
        {
            phone = phone.PreparePhone().GetLast9Digits();
            if (phone.Length < 9) return null;
            return _Context.Users.Where(u => u.Phone.Contains(phone)).Select(UserModel.Convert).FirstOrDefault();
        }
    }
}