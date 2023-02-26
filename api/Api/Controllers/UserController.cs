using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Business;
using AppGlobal.Constants;
using AppGlobal.Entities;
using AppGlobal.Filters;
using AppGlobal.Models;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserBusiness _UserBusiness;
        private readonly AuthenBusiness _AuthenService;
        private readonly AppointmentBusiness _AppointmentBusiness;
        private readonly EpisodeBusiness _EpisodeBusiness;
        private readonly ReportBusiness _ReportBusiness;
        private readonly ActivityBusiness _ActivityBusiness;

        public UserController(AuthenBusiness AuthenService, UserBusiness UserBusiness, AppointmentBusiness AppointmentBusiness, EpisodeBusiness EpisodeBusiness, ReportBusiness ReportBusiness, ActivityBusiness ActivityBusiness)
        {
            _UserBusiness = UserBusiness;
            _AuthenService = AuthenService;
            _AppointmentBusiness = AppointmentBusiness;
            _AppointmentBusiness = AppointmentBusiness;
            _EpisodeBusiness = EpisodeBusiness;
            _ReportBusiness = ReportBusiness;
            _ActivityBusiness = ActivityBusiness;
        }
        #region Authen APIs
        [HttpPost("Register")]
        public LoginResult RegisterUser([FromBody] RegisterModelCreate dataBody)
        {
            return _AuthenService.RegisterUser(dataBody);
        }

        [HttpPost("Login")]
        public LoginResult Login([FromBody]LoginEntity data)
        {
            return _AuthenService.Login(data);
        }

        [HttpPost("Login/OAuth2")]
        public LoginResult Login([FromHeader]string token)
        {
            return _AuthenService.LoginOAuth(token);
        }

        [Authorize]
        [HttpGet("AuthData")]
        public AuthEntity GetAuthData()
        {
            var token = (string)HttpContext.Items["Token"];
            return _AuthenService.GetAuthData(token);
        }

        [Authorize]
        [HttpPut("ChangePassword")]
        public bool ChangePassword([FromBody]UserModelChangePassword data)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _AuthenService.ChangePassword(user.UserID, data);
        }

        [Authorize]
        [HttpGet("AuthData/OAuth2")]
        public FirebaseToken OAuthData([FromHeader]string token)
        {
            return _AuthenService.GetOAuthData(token);
        }

        [Authorize]
        [HttpGet("RefreshToken")]
        public LoginResult RefreshAuthToken()
        {
            var token = (string)HttpContext.Items["Token"];
            return _AuthenService.RefreshAuthToken(token);
        }
        #endregion

        #region Utilities APIs
        [HttpPost("CheckPhone")]
        public bool CheckPhone([FromBody] CheckAccount data)
        {
            return _AuthenService.CheckPhone(data.Phone);
        }

        [HttpPost("GetByPhone")]
        public UserModel GetByPhone([FromBody] CheckAccount data)
        {
            return _UserBusiness.GetByPhone(data.Phone);
        }

        [Authorize]
        [HttpGet("Profile")]
        public UserModelDetail GetProfile(string clinicID = "")
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _UserBusiness.GetProfile(user.UserID, clinicID);
        }

        [Authorize]
        [HttpPut("Profile")]
        public bool UpdateProfile([FromBody] UserModelUpdate data, string clinicID = "")
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _UserBusiness.UpdateProfile(user.UserID, data, clinicID);
        }
        #endregion

        #region User's Booking APIs
        [Authorize]
        [HttpGet("Appointments")]
        public SearchResult<AppointmentModel> SearchAppointments(string keySearch, DateTime? timeStart, DateTime? timeEnd, int? statusID, [FromHeader] int page = 1, [FromHeader] int limit = 10)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _AppointmentBusiness.Search(user.UserID, keySearch, timeStart, timeEnd, statusID, page, limit);
        }
        [Authorize]
        [HttpGet("Appointments/{appointmentID}")]
        public AppointmentDetail GetAppointment(string appointmentID)
        {
            return _AppointmentBusiness.GetByID(appointmentID);
        }
        [Authorize]
        [HttpPost("Appointments")]
        public AppointmentDetail CreateAppointment([FromBody] AppointmentPatientCreate data)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _AppointmentBusiness.Create(data, user.UserID);
        }
        [Authorize]
        [HttpPut("Appointments/{appointmentID}")]
        public AppointmentDetail UpdateAppointment(string appointmentID, [FromBody] AppointmentUpdate data)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _AppointmentBusiness.Update(appointmentID, data, user.UserID);
        }
        [Authorize]
        [HttpDelete("Appointments/{appointmentID}")]
        public bool DeleteAppointment(string appointmentID)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _AppointmentBusiness.Delete(appointmentID, user.UserID);
        }
        #endregion

        #region User's Episode APIs
        [Authorize]
        [HttpGet("Episodes")]
        public SearchResult<EpisodeModel> SearchEpisodes(string keySearch, bool deleted = false, [FromHeader] int page = 1, [FromHeader] int limit = 10)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            var clinicIDs = user.Roles.Select(r => r.ClinicID).ToList();
            return _EpisodeBusiness.Search(user.UserID, clinicIDs, keySearch, deleted, page, limit);
        }
        [Authorize]
        [HttpGet("Episodes/{episodeID}")]
        public EpisodeDetail GetEpisode(string episodeID)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _EpisodeBusiness.GetByIDWithPermission(episodeID, user.UserID);
        }
        #endregion

        #region User Report APIs
        [Authorize]
        [HttpGet("Report/Statistics")]
        public UserStatistics GetUserStats()
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _ReportBusiness.GetUserStats(user.UserID);
        }
        #endregion

        #region User Activity APIs
        [Authorize]
        [HttpGet("Activities")]
        public SearchResult<ActivityModel> SearchActivities([FromHeader] int page = 1, [FromHeader] int limit = 10)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _ActivityBusiness.Search(user.UserID, page, limit);
        }

        [Authorize]
        [HttpGet("Activities/CountNotRead")]
        public int CountNotRead()
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _ActivityBusiness.CountNotRead(user.UserID);
        }

        [Authorize]
        [HttpPut("Activities/{activityID}")]
        public bool UpdateIsRead(string activityID)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _ActivityBusiness.UpdateIsRead(user.UserID, activityID);
        }
        #endregion

        #region Notification APIs
        [Authorize]
        [HttpPost("Notifications/Subscribe")]
        public bool SubscribeNotification([FromHeader] string fcmToken)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _AuthenService.SubscribeNotification(user.UserID, fcmToken);
        }

        [Authorize]
        [HttpPost("Notifications/Unsubscribe")]
        public bool UnSubscribeNotification([FromHeader] string fcmToken)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _AuthenService.UnSubscribeNotification(user.UserID, fcmToken);
        }
        #endregion

        [HttpGet("Ping")]
        public string Ping()
        {
            return "pong";
        }
    }
}
