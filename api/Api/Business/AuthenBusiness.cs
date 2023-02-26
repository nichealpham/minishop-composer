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
    public static class AuthenHelper
    {
        public static AuthEntity GetAuthEntity(this IQueryable<User> query)
        {
            return query.Select(u => new AuthEntity()
            {
                UserID = u.UserID.ToString(),
                Email = u.Email,
                FullName = u.FullName,
                Avatar = u.Avatar,
                Phone = u.Phone,
                Roles = u.Roles
                    // Only get roles that is not type Patient
                    .Where(r => r.RoleType != (int)RoleType.Patient)
                    .OrderByDescending(r => r.DateCreated)
                    .Select(r => new UserRole()
                    {
                        ClinicID = r.ClinicID.ToString(),
                        ClinicName = r.Clinic.ClinicName,
                        RoleType = r.RoleType,
                    }).ToList(),
            })
            .FirstOrDefault();
        }

        public static string PreparePhone(this string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                throw new ApiError((int)ErrorCodes.CredentialsInvalid);
            }
            var result = phone.Standardizing().Replace("+", "").Replace("-", "").RemoveWhitespace();
            result = "+" + result;
            return result;
        }

        public static bool CheckPhone(this IQueryable<User> query, string phone)
        {
            var result = phone.PreparePhone().GetLast9Digits();
            if (result.Length < 9) return false;
            return query.Any(u => u.Phone.Contains(result));
        }

        public static string GetLast9Digits(this string phone)
        {
            return phone.Substring(Math.Max(0, phone.Length - 9));
        }
    }
    public class AuthenBusiness
    {
        private readonly FirebaseTokenValidator _FirebaseTokenService;
        private readonly AccessTokenService _TokenService;
        private readonly ERPContext _Context;
        private readonly FirebaseCloudMessaging _FirebaseCloudMessaging;
        public AuthenBusiness(AccessTokenService token, FirebaseTokenValidator firebase, ERPContext context, FirebaseCloudMessaging FirebaseCloudMessaging)
        {
            _FirebaseTokenService = firebase;
            _TokenService = token;
            _Context = context;
            _FirebaseCloudMessaging = FirebaseCloudMessaging;
        }

        // Login using password + (Email || Phone)
        public LoginResult Login(LoginEntity data)
        {
            var password = data.Password.HashMD5();
            var email = data.Email.ToLower();
            var phone = data.Email.PreparePhone().GetLast9Digits();
            if (phone.Length < 9)
            {
                throw new ApiError((int)ErrorCodes.CredentialsInvalid);
            }

            var user = _Context.Users
                .Include(u => u.Roles)
                    .ThenInclude(r => r.Clinic)
                .AsNoTracking()
                .Where(u => u.Password == password)
                .Where(u => u.Email.ToLower() == email || u.Phone.Contains(phone))
                .GetAuthEntity();

            if (user == null)
            {
                throw new ApiError((int)ErrorCodes.CredentialsInvalid);
            }

            var token = _TokenService.GenerateJwtToken(user);

            return new LoginResult()
            {
                Token = token,
                User = user,
            };
        }

        public LoginResult LoginOAuth(string token)
        {
            var auth = GetOAuthData(token);
            var email = auth.Claims.First(c => c.Key == "email").Value.ToString().ToLower();

            var user = _Context.Users
                .Include(u => u.Roles)
                    .ThenInclude(r => r.Clinic)
                .AsNoTracking()
                .Where(u => u.Email.ToLower() == email.ToLower())
                .GetAuthEntity();

            if (user == null)
            {
                throw new ApiError((int)ErrorCodes.CredentialsInvalid);
            }

            token = _TokenService.GenerateJwtToken(user);

            return new LoginResult()
            {
                Token = token,
                User = user,
            };
        }

        public AuthEntity GetAuthData(string token)
        {
            return _TokenService.ParseJwtToken(token);
        }

        public FirebaseToken GetOAuthData(string token)
        {
            return _FirebaseTokenService.VerifyTokenAsync(token);
        }

        public LoginResult RegisterUser(RegisterModelCreate data)
        {
            var password = data.Password;
            var token = data.Token;
            var phone = data.Phone.PreparePhone();
            data.Email = data.Email ?? "";
            // if both password and token are null!!!
            if (string.IsNullOrEmpty(password) && string.IsNullOrEmpty(token))
            {
                throw new ApiError((int)ErrorCodes.CredentialsInvalid);
            }
            // if phone already existed => return error!
            if (_Context.Users.CheckPhone(phone))
            {
                throw new ApiError((int)ErrorCodes.DuplicatedDataEntry);
            }
            var fullName = data.FullName;
            if (!string.IsNullOrEmpty(token))
            {
                var auth = GetOAuthData(token);
                fullName = auth.Claims.First(c => c.Key == "name").Value?.ToString() ?? null;
            }
            // Check if profile existed
            var existed = _Context.Users
                .Where(u =>
                    u.FullName.ToLower() == fullName.ToLower() &&
                    u.GenderType == data.GenderType &&
                    u.Dob.HasValue &&
                    EF.Functions.DateDiffDay(u.Dob.Value, data.Dob) == 0)
                .Any();

            // Dont allow create if profile already existed
            if (existed)
            {
                throw new ApiError((int)ErrorCodes.DuplicatedDataEntry);
            }

            var user = new User()
            {
                FullName = fullName,
                GenderType = data.GenderType,
                Phone = data.Phone,
                Dob = data.Dob,
                Email = data.Email,
            };

            // check if user register using password 
            if (!string.IsNullOrEmpty(password))
            {
                user.Password = password.HashMD5();
            }
            // or using oautoken
            else
            {
                // verify autoken
                var auth = GetOAuthData(token);
                var email = auth.Claims.First(c => c.Key == "email").Value?.ToString() ?? "";
                var picture = auth.Claims.First(c => c.Key == "picture").Value?.ToString() ?? "";
                user.Email = email;
                user.Avatar = picture;
            }

            _Context.Users.Add(user);
            _Context.SaveChanges();

            var entity = _Context.Users
                .Include(u => u.Roles)
                    .ThenInclude(r => r.Clinic)
                .AsNoTracking()
                .Where(u => u.UserID == user.UserID)
                .GetAuthEntity();

            return new LoginResult()
            {
                Token = _TokenService.GenerateJwtToken(entity),
                User = entity,
            };
        }

        public LoginResult RefreshAuthToken(string token)
        {
            var auth = _TokenService.ParseJwtToken(token);
            var phone = auth.Phone.PreparePhone().GetLast9Digits();
            if (phone.Length < 9)
            {
                throw new ApiError((int)ErrorCodes.CredentialsInvalid);
            }

            var user = _Context.Users
                .Include(u => u.Roles)
                    .ThenInclude(r => r.Clinic)
                .AsNoTracking()
                .Where(u => u.Phone.Contains(phone))
                .GetAuthEntity();

            if (user == null)
            {
                throw new ApiError((int)ErrorCodes.CredentialsInvalid);
            }

            token = _TokenService.GenerateJwtToken(user);

            return new LoginResult()
            {
                Token = token,
                User = user,
            };
        }

        public bool CheckPhone(string phone)
        {
            return _Context.Users.CheckPhone(phone);
        }

        public bool SubscribeNotification(string userID, string fcmToken)
        {
            return _FirebaseCloudMessaging.SubscribeToChannel(userID.ToLower(), fcmToken);
        }

        public bool UnSubscribeNotification(string userID, string fcmToken)
        {
            return _FirebaseCloudMessaging.UnSubscribeFromChannel(userID.ToLower(), fcmToken);
        }

        public bool ChangePassword(string userID, UserModelChangePassword data)
        {
            var uid = Guid.Parse(userID);
            var oldPassword = data.OldPassword.HashMD5();
            var newPassword = data.NewPassword.HashMD5();

            var user = _Context.Users
                .Where(u =>
                    u.UserID == uid &&
                    u.Password == oldPassword)
                .FirstOrDefault();

            if (user == null)
            {
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);
            }

            user.Password = newPassword;
            _Context.SaveChanges();

            return true;
        }
    }
}

