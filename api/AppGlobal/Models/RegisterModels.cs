using System;
using System.Collections.Generic;
using System.Text;

namespace AppGlobal.Models
{
    public class AuthEntity
    {
        public string UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string Phone { get; set; }
        public List<UserRole> Roles { get; set; }
    }

    public class UserRole
    {
        public string ClinicID { get; set; }
        public string ClinicName { get; set; }
        public byte? RoleType { get; set; }
    }

    public class LoginResult
    {
        public string Token { get; set; }
        public AuthEntity User { get; set; }
    }

    public class LoginEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterModelCreate
    {
        // Required fields for validation!
        public string FullName { get; set; }
        public DateTime Dob { get; set; }
        public bool GenderType { get; set; }
        public string Phone { get; set; }
        // Oauth, can to Password or OAuthToken
        // Password can come with email!
        public string Password { get; set; }
        public string Email { get; set; }
        // If Auth Token is provided => Email is not valid
        public string Token { get; set; }
    }
    public class RegisterModelOAuth2
    {
        public string Phone { get; set; }
        public string Token { get; set; }
    }

    public class ProfileModelUpdate
    {
        public bool? GenderType { get; set; }
        public DateTime? Dob { get; set; }
    }

    public class CheckAccount
    {
        public string Phone { get; set; }
    }
}
