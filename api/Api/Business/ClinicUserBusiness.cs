using AppGlobal.Commons;
using AppGlobal.Constants;
using AppGlobal.DBContext;
using AppGlobal.Entities;
using AppGlobal.Extensions;
using AppGlobal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Business
{
    public static class ClinicUserHelper
    {
        public static SearchResult<UserModel> GetSearchResult(this IQueryable<User> query, 
            string keySearch = "", bool? gender = null, DateTime? dob = null, 
            bool? deactivated = null, int page = 1, int limit = 10)
        {
            if (!string.IsNullOrEmpty(keySearch))
            {
                keySearch = keySearch.Standardizing();
                var phoneSearch = keySearch.PreparePhone().Replace("+", "");
                query = query.Where(c => 
                    ApplicationDBContext.Standardizing(c.FullName).Contains(keySearch) ||
                    c.Phone.Contains(phoneSearch));
            }
            if (gender.HasValue)
            {
                query = query.Where(c => c.GenderType == gender.Value);
            }
            if (dob.HasValue)
            {
                dob.Value.StartOfDay();
                query = query
                    .Where(c => c.Dob.HasValue)
                    .Where(c => EF.Functions.DateDiffDay(c.Dob.Value, dob) == 0);
            }
            if (deactivated == true)
            {
                query = query.Where(c => c.StatusID == (int)PatientStatusType.Deactivated);
            }
            else
            {
                query = query.Where(c => c.StatusID != (int)PatientStatusType.Deactivated);
            }

            var totals = query.Count();
            var items = query
                .OrderBy(c => c.FullName)
                .Skip((page - 1) * limit)
                .Take(limit)
                .Select(UserModel.Convert)
                .ToList();

            return new SearchResult<UserModel>()
            {
                Totals = totals,
                Items = items,
            };
        }
    }
    public class ClinicUserBusiness
    {
        private readonly ERPContext _Context;
        public ClinicUserBusiness(ERPContext context)
        {
            _Context = context;
        }

        // Clinics search their patients
        public SearchResult<UserModel> Search(List<string> clinicIDs, string keySearch = "", int? roleType = null, bool ? gender = null, DateTime? dob = null, 
            bool? deactivated = null, int page = 1, int limit = 10)
        {
            var guids = clinicIDs.Select(c => Guid.Parse(c)).ToList();
            var query = _Context.Users.AsNoTracking();
            if (clinicIDs.Count() > 0)
            {
                query = query
                    .Include(u => u.Roles)
                    .Where(u => u.Roles.Any(r => 
                        (guids.Contains(r.ClinicID)) &&
                        (!roleType.HasValue || r.RoleType == roleType.Value)));
            }
            var result = query.GetSearchResult(keySearch, gender, dob, deactivated, page, limit);

            var userIDs = result.Items.Select(i => i.UserID).ToList();

            var roles = _Context.Roles.AsNoTracking().Where(r =>
                    userIDs.Contains(r.UserID) &&
                    guids.Contains(r.ClinicID) &&
                    !String.IsNullOrEmpty(r.ProfileUrl))
                .ToList();

            result.Items.ForEach(item =>
            {
                item.ProfileUrl = roles.Where(p => p.UserID == item.UserID).Select(p => p.ProfileUrl).FirstOrDefault();
            });

            return result;
        }

        public UserModelDetail GetByID(string userID, string clinicID)
        {
            var guid = Guid.Parse(userID);
            var user = _Context.Users
                .AsNoTracking()
                .Where(u => u.UserID == guid)
                .Select(UserModelDetail.Convert)
                .FirstOrDefault();

            if (!string.IsNullOrEmpty(clinicID))
            {
                var cid = Guid.Parse(clinicID);
                user.LastVisits = new List<EpisodeModel>();
                var url = _Context.Roles
                    .AsNoTracking()
                    .Where(r => r.UserID == guid && r.ClinicID == cid)
                    .Select(r => r.ProfileUrl)
                    .FirstOrDefault();
                user.ProfileUrl = url;
            }

            return user;
        }

        public SearchResult<EpisodeModel> GetLastVisits(string userID, string clinicID, int page = 1, int limit = 10)
        {
            var uid = Guid.Parse(userID);
            var cid = Guid.Parse(clinicID);
            return _Context
                .GetEpisodeQueryable()
                .Where(e => e.UserAdmittedID == uid && e.ClinicID == cid)
                .GetEpisodeResults("", false, page, limit);
        }

        // Clinic create a patient profile
        // ==> Clinic owns the patient profile
        // ==> Until patient login into their account for the 1st time!
        public UserModelDetail Create(string clinicID, UserModelCreate data, byte roleType = (byte)RoleType.Patient)
        {
            var phone = data.Phone.PreparePhone();
            if (_Context.Users.CheckPhone(phone))
            {
                throw new ApiError((int)ErrorCodes.DuplicatedDataEntry);
            }

            var guid = Guid.Parse(clinicID);
            // Check if profile existed
            var user = _Context.Users
                .Include(u => u.Roles)
                .Where(u =>
                    u.FullName.ToLower() == data.FullName.ToLower() &&
                    u.GenderType == data.GenderType &&
                    u.Dob.HasValue &&
                    EF.Functions.DateDiffDay(u.Dob.Value, data.Dob) == 0)
                .FirstOrDefault();

            // If not existed => create a new user profile
            if (user == null)
            {
                user = new User()
                {
                    FullName = data.FullName,
                    GenderType = data.GenderType,
                    Dob = data.Dob,
                    Avatar = data.Avatar,
                    Phone = phone,
                    Email = data.Email,
                    IdentityID = data.IdentityID,
                    Address = data.Address,
                    Occupation = data.Occupation,
                    Country = data.Country,
                    StatusID = (int)PatientStatusType.Owned,
                };
                _Context.Users.Add(user);
                _Context.SaveChanges();
            }
            // Then, add this user into clinic by role = user
            if (!user.Roles.Any(r => r.UserID == user.UserID && r.ClinicID == guid))
            {
                var role = new Role()
                {
                    ClinicID = guid,
                    RoleType = roleType,
                    ProfileUrl = data.ProfileUrl,
                };
                user.Roles.Add(role);
                _Context.SaveChanges();
            }

            return UserModelDetail.Convert(user);
        }

        // Clinic update patient profile
        // ==> Only applicable for patient created by clinic!
        // ==> Which mean: they have role Patient in Clinic +
        // ==> Their account type is Owned by Clinic!
        public UserModelDetail Update(string clinicID, string patientID, UserModelUpdate data)
        {
            var guid = Guid.Parse(clinicID);
            var guidu = Guid.Parse(patientID);
            var user = _Context.Users
                .Include(u => u.Roles)
                .Where(u => u.UserID == guidu)
                .Where(u => u.StatusID == (int)PatientStatusType.Owned)
                .Where(u => u.Roles.Any(r => r.ClinicID == guid))
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

            var roles = user.Roles.Where(r => r.ClinicID == Guid.Parse(clinicID)).ToList();
            roles.ForEach(role =>
            {
                role.ProfileUrl = data.ProfileUrl;
            });

            _Context.SaveChanges();
            return UserModelDetail.Convert(user);
        }

        // Clinic deactivate patient profile
        // ==> Only applicable for patient created by clinic!
        // ==> Which mean: they have role Patient in Clinic +
        // ==> Their account type is Owned by Clinic!
        public bool Deactivate(string clinicID, string patientID)
        {
            var guid = Guid.Parse(clinicID);
            var guidu = Guid.Parse(patientID);
            var user = _Context.Users
                .Include(u => u.Roles)
                .Where(u => u.UserID == guidu)
                .Where(u => u.StatusID == (int)PatientStatusType.Owned)
                .Where(u => u.Roles.Any(r => r.ClinicID == guid))
                .FirstOrDefault();

            if (user == null)
            {
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);
            }

            user.StatusID = (int)PatientStatusType.Deactivated;
            _Context.SaveChanges();
            return true;
        }

        // Clinic reactivate patient profile
        // ==> Only applicable for patient created by clinic!
        // ==> Which mean: they have role Patient in Clinic +
        // ==> Their account type is Owned by Clinic!
        public bool Reactivate(string clinicID, string patientID)
        {
            var guid = Guid.Parse(clinicID);
            var guidu = Guid.Parse(patientID);
            var user = _Context.Users
                .Include(u => u.Roles)
                .Where(u => u.UserID == guidu)
                .Where(u => u.StatusID == (int)PatientStatusType.Deactivated)
                .Where(u => u.Roles.Any(r => r.ClinicID == guid))
                .FirstOrDefault();

            if (user == null)
            {
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);
            }

            user.StatusID = (int)PatientStatusType.Owned;
            _Context.SaveChanges();
            return true;
        }

        // Clinic invite doctor to join
        public UserModelDetail Invite(string clinicID, string doctorID, byte roleType = (byte)RoleType.Doctor)
        {
            var doctorGID = Guid.Parse(doctorID);
            var clinicGID = Guid.Parse(clinicID);
            // Check if profile existed
            var user = _Context.Users
                .Include(u => u.Roles)
                .Where(u => u.UserID == doctorGID)
                .FirstOrDefault();

            if (user == null)
            {
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);
            }

            // Then, add this user into clinic if not role before
            var role = user.Roles.FirstOrDefault(r => r.ClinicID == clinicGID);
            if (role == null)
            {
                user.Roles.Add(new Role()
                {
                    ClinicID = clinicGID,
                    RoleType = roleType,
                });
                _Context.SaveChanges();
            }
            // If role added, check if current role == patient
            else if (role.RoleType == (byte)RoleType.Patient)
            {
                // if yes, then update role
                // because if role = doctor or receptionist, nothing required
                role.RoleType = roleType;
                _Context.SaveChanges();
            }

            return UserModelDetail.Convert(user);
        }
    }
}
