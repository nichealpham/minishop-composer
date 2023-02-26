using AppGlobal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppGlobal.Models
{
    public class UserModel
    {
        public Guid UserID { get; set; }
        public string FullName { get; set; }
        public bool? GenderType { get; set; }
        public DateTime? Dob { get; set; }
        public string Avatar { get; set; }
        public string Phone { get; set; }
        public byte StatusID { get; set; }
        public string ProfileUrl { get; set; }
        public static UserModel Convert(User user) => user != null ? new UserModel()
        {
            UserID = user.UserID,
            FullName = user.FullName,
            GenderType = user.GenderType,
            Dob = user.Dob,
            Avatar = user.Avatar,
            Phone = user.Phone,
            StatusID = user.StatusID,
            ProfileUrl = user.Roles?.FirstOrDefault()?.ProfileUrl,
        } : null;
    }

    public class UserModelDetail
    {
        public Guid UserID { get; set; }
        public string FullName { get; set; }
        public bool? GenderType { get; set; }
        public DateTime? Dob { get; set; }
        public string Avatar { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string IdentityID { get; set; }
        public string Address { get; set; }
        public string Occupation { get; set; }
        public string Country { get; set; }
        public byte StatusID { get; set; }
        public string ProfileUrl { get; set; }
        public List<EpisodeModel> LastVisits { get; set; }
        public static UserModelDetail Convert(User user) => new UserModelDetail()
        {
            UserID = user.UserID,
            FullName = user.FullName,
            GenderType = user.GenderType,
            Dob = user.Dob,
            Avatar = user.Avatar,
            Phone = user.Phone,
            Email = user.Email,
            IdentityID = user.IdentityID,
            Address = user.Address,
            Occupation = user.Occupation,
            Country = user.Country,
            StatusID = user.StatusID,
            ProfileUrl = "",
            LastVisits = new List<EpisodeModel>(),
        };
    }

    public class UserModelCreate
    {
        public string FullName { get; set; }
        public bool? GenderType { get; set; }
        public DateTime? Dob { get; set; }
        public string Avatar { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string IdentityID { get; set; }
        public string Address { get; set; }
        public string Occupation { get; set; }
        public string Country { get; set; }
        public string ProfileUrl { get; set; }
    }

    public class UserModelUpdate
    {
        public string FullName { get; set; }
        public bool? GenderType { get; set; }
        public DateTime? Dob { get; set; }
        public string Avatar { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string IdentityID { get; set; }
        public string Address { get; set; }
        public string Occupation { get; set; }
        public string Country { get; set; }
        public string ProfileUrl { get; set; }
    }

    public class UserModelChangePassword
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
