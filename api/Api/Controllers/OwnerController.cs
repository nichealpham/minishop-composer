using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Business;
using AppGlobal.Constants;
using AppGlobal.Filters;
using AppGlobal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OwnerController : Controller
    {
        private readonly ClinicBusiness _ClinicService;
        private readonly ClinicServiceBusiness _ServiceService;
        private readonly ClinicUserBusiness _PatientBusiness;
        private readonly AppointmentBusiness _AppointmentBusiness;
        private readonly EpisodeBusiness _EpisodeBusiness;
        private readonly ReportBusiness _ReportBusiness;
        private readonly AssetBusiness _AssetBusiness;
        private readonly CategoryBusiness _CategoryBusiness;
        private readonly AttachmentBusiness _AttachmentBusiness;

        public OwnerController(ClinicServiceBusiness ServiceBusiness, ClinicBusiness ClinicBusiness, ClinicUserBusiness PatientBusiness, AppointmentBusiness AppointmentBusiness, EpisodeBusiness EpisodeBusiness, ReportBusiness ReportBusiness, AssetBusiness AssetBusiness, CategoryBusiness CategoryBusiness, AttachmentBusiness AttachmentBusiness)
        {
            _ServiceService = ServiceBusiness;
            _ClinicService = ClinicBusiness;
            _PatientBusiness = PatientBusiness;
            _AppointmentBusiness = AppointmentBusiness;
            _EpisodeBusiness = EpisodeBusiness;
            _ReportBusiness = ReportBusiness;
            _AssetBusiness = AssetBusiness;
            _AssetBusiness = AssetBusiness;
            _CategoryBusiness = CategoryBusiness;
            _AttachmentBusiness = AttachmentBusiness;
        }
        
        #region Owner Clinics API
        // Search clinics
        [Authorize]
        [HttpGet("Clinics")]
        public SearchResult<ClinicModel> SearchClinics(string search = "", int statusID = (int)StatusType.Active, [FromHeader] int page = 1, [FromHeader] int limit = 10)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _ClinicService.Search(user.UserID, statusID, search, page, limit);
        }

        // GetDetail clinics
        [Authorize]
        [HttpGet("Clinics/{clinicID}")]
        public ClinicModelDetail GetClinicByID(string clinicID)
        {
            return _ClinicService.GetByID(clinicID);
        }

        // Create clinics
        [Authorize]
        [HttpPost("Clinics")]
        public ClinicModelDetail CreateClinic([FromBody] ClinicModelCreate dataBody)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _ClinicService.Create(user.UserID, dataBody);
        }

        // Update clinics
        [Authorize]
        [ClinicOwner]
        [HttpPut("Clinics/{clinicID}")]
        public ClinicModelDetail Update(string clinicID, [FromBody] ClinicModelUpdate dataBody)
        {
            return _ClinicService.Update(clinicID, dataBody);
        }

        [Authorize]
        [HttpGet("Clinics/{clinicID}/Attachments")]
        public SearchResult<AttachmentModel> GetClinicAttachments(string clinicID, [FromHeader] int page = 1, [FromHeader] int limit = 10,
            string keySearch = "", int? attachmentType = null)
        {
            return _AttachmentBusiness.Search(attachmentType, null, clinicID, "", keySearch, page, limit);
        }

        // Delete clinics
        [Authorize]
        [ClinicOwner]
        [HttpDelete("Clinics/{clinicID}")]
        public bool Delete(string clinicID)
        {
            return _ClinicService.Delete(clinicID);
        }
        // Search Clinics Services
        [Authorize]
        [HttpGet("Clinics/{clinicID}/Services")]
        public SearchResult<ServiceModel> SearchServices(string clinicID, string search, int statusID = (int)StatusType.Active, [FromHeader] int page = 1, [FromHeader] int limit = 10)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _ServiceService.Search(user.UserID, clinicID, statusID, search, page, limit);
        }
        // GetDetail Clinic Service
        [Authorize]
        [HttpGet("Clinics/{clinicID}/Services/{serviceID}")]
        public ServiceModelDetail GetServiceByID(string clinicID, string serviceID)
        {
            return _ServiceService.GetByID(serviceID);
        }

        // Create Clinic's Service
        [Authorize]
        [ClinicStaff]
        [HttpPost("Clinics/{clinicID}/Services")]
        public ServiceModelDetail CreateService(string clinicID, [FromBody] ServiceModelCreate dataBody)
        {
            dataBody.ClinicID =Guid.Parse(clinicID);
            return _ServiceService.Create(dataBody);
        }

        // Update Clinic's Service
        [Authorize]
        [ClinicStaff]
        [HttpPut("Clinics/{clinicID}/Services/{serviceID}")]
        public ServiceModelDetail UpdateService(string clinicID, string serviceID, [FromBody] ServiceModelUpdate bodyData)
        {
            bodyData.ClinicID =Guid.Parse(clinicID);
            return _ServiceService.Update(serviceID, bodyData);
        }

        [Authorize]
        [HttpGet("Clinics/{clinicID}/Services/Attachments")]
        public SearchResult<AttachmentModel> GetServiceAttachments(string clinicID, [FromHeader] int page = 1, [FromHeader] int limit = 10,
            string keySearch = "", int? attachmentType = null)
        {
            return _AttachmentBusiness.Search(attachmentType, (byte)CategoryType.Service, clinicID, "", keySearch, page, limit);
        }

        [Authorize]
        [HttpGet("Clinics/{clinicID}/Services/{serviceID}/Attachments")]
        public SearchResult<AttachmentModel> GetServiceDetailAttachments(string clinicID, string serviceID, [FromHeader] int page = 1, [FromHeader] int limit = 10,
            string keySearch = "", int? attachmentType = null)
        {
            return _AttachmentBusiness.Search(attachmentType, (byte)CategoryType.Service, clinicID, serviceID, keySearch, page, limit);
        }

        [Authorize]
        [ClinicStaff]
        [HttpPost("Clinics/{clinicID}/Services/{serviceID}/Attachments")]
        public AttachmentModel CreateServiceAttachment(string clinicID, string serviceID, [FromBody] AttachmentModelCreate data)
        {
            return _AttachmentBusiness.Create(clinicID, serviceID, (byte)CategoryType.Service, data);
        }

        [Authorize]
        [ClinicStaff]
        [HttpDelete("Clinics/{clinicID}/Services/{serviceID}/Attachments/{attachmentID}")]
        public bool DeleteServiceAttachment(string clinicID, string serviceID, string attachmentID)
        {
            return _AttachmentBusiness.Delete(clinicID, attachmentID);
        }

        // Delete Clinic's Service
        [Authorize]
        [ClinicOwner]
        [HttpDelete("Clinics/{clinicID}/Services/{serviceID}")]
        public bool DeleteService(string clinicID, string serviceID)
        {
            return _ServiceService.Delete(serviceID);
        }
        #endregion

        #region Clinic Category APIs
        [Authorize]
        [HttpGet("Clinics/{clinicID}/Categories")]
        public SearchResult<CategoryModel> SearchCategories(string clinicID, [FromHeader] int page = 1, [FromHeader] int limit = 10,
            string keySearch = "", string targetItemID = "", int? targetItemType = null, bool? deleted = null)
        {
            return _CategoryBusiness.Search(clinicID, keySearch, targetItemID, targetItemType, deleted, page, limit);
        }

        [Authorize]
        [HttpGet("Clinics/{clinicID}/Categories/{categoryID}")]
        public CategoryModel GetCategoryByID(string categoryID)
        {
            return _CategoryBusiness.Get(categoryID);
        }

        [Authorize]
        [ClinicStaff]
        [HttpPost("Clinics/{clinicID}/Categories")]
        public CategoryModel CreateCategory(string clinicID, [FromBody] CategoryModelCreate data)
        {
            return _CategoryBusiness.Create(clinicID, data);
        }

        [Authorize]
        [ClinicStaff]
        [HttpPut("Clinics/{clinicID}/Categories/{categoryID}")]
        public bool UpdateCategory(string clinicID, string categoryID, [FromBody] CategoryModelUpdate data)
        {
            return _CategoryBusiness.Update(clinicID, categoryID, data);
        }

        [Authorize]
        [ClinicStaff]
        [HttpDelete("Clinics/{clinicID}/Categories/{categoryID}")]
        public bool DeleteCategory(string clinicID, string categoryID)
        {
            return _CategoryBusiness.Delete(clinicID, categoryID);
        }

        [Authorize]
        [ClinicStaff]
        [HttpPost("Clinics/{clinicID}/Categories/{categoryID}/AttachItem")]
        public bool AttachCategoryItem(string clinicID, string categoryID, [FromBody] CategoryItemUpdate data)
        {
            return _CategoryBusiness.AttachItem(clinicID, categoryID, data.TargetItemID, data.TargetItemType);
        }

        [Authorize]
        [ClinicStaff]
        [HttpDelete("Clinics/{clinicID}/Categories/{categoryID}/DetachItem")]
        public bool DetachCategoryItem(string clinicID, string categoryID, [FromBody] CategoryItemUpdate data)
        {
            return _CategoryBusiness.DetachItem(clinicID, categoryID, data.TargetItemID, data.TargetItemType);
        }
        #endregion

        #region Owner Clinic Patient APIs
        [Authorize]
        [HttpGet("Clinics/{clinicID}/Patients")]
        public SearchResult<UserModel> Search(string clinicID, [FromHeader] int page = 1, [FromHeader] int limit = 10, 
            string keySearch = "", bool? gender = null, DateTime? dob = null, bool? deactivated = null)
        {
            var clinicIDs = new List<string>() { clinicID };
            return _PatientBusiness.Search(clinicIDs, keySearch, null, gender, dob, deactivated, page, limit);
        }

        [Authorize]
        [HttpGet("Clinics/{clinicID}/Patients/{patientID}")]
        public UserModelDetail GetByID(string clinicID, string patientID)
        {
            return _PatientBusiness.GetByID(patientID, clinicID);
        }

        [Authorize]
        [HttpGet("Clinics/{clinicID}/Patients/{patientID}/LastVisits")]
        public SearchResult<EpisodeModel> GetLastVisits(string clinicID, string patientID, [FromHeader] int page = 1, [FromHeader] int limit = 10)
        {
            return _PatientBusiness.GetLastVisits(patientID, clinicID, page, limit);
        }

        [Authorize]
        [ClinicStaff]
        [HttpPost("Clinics/{clinicID}/Patients")]
        public UserModelDetail Create(string clinicID, [FromBody] UserModelCreate data)
        {
            return _PatientBusiness.Create(clinicID, data);
        }

        [Authorize]
        [ClinicStaff]
        [HttpPut("Clinics/{clinicID}/Patients/{patientID}")]
        public UserModelDetail Update(string clinicID, string patientID, [FromBody] UserModelUpdate data)
        {
            return _PatientBusiness.Update(clinicID, patientID, data);
        }

        [Authorize]
        //[ClinicStaff]
        [HttpPut("Clinics/{clinicID}/Patients/{patientID}/Invite")]
        public UserModelDetail InvitePatient(string clinicID, string patientID)
        {
            return _PatientBusiness.Invite(clinicID, patientID, (byte)RoleType.Patient);
        }

        [Authorize]
        [ClinicStaff]
        [HttpDelete("Clinics/{clinicID}/Patients/{patientID}")]
        public bool Deactivate(string clinicID, string patientID)
        {
            return _PatientBusiness.Deactivate(clinicID, patientID);
        }

        [Authorize]
        [ClinicStaff]
        [HttpPut("Clinics/{clinicID}/Patients/{patientID}/Reactivate")]
        public bool Reactivate(string clinicID, string patientID)
        {
            return _PatientBusiness.Reactivate(clinicID, patientID);
        }
        #endregion

        #region Owner Clinic Doctor APIs
        [Authorize]
        [HttpGet("Clinics/{clinicID}/Doctors")]
        public SearchResult<UserModel> SearchDoctors(string clinicID, [FromHeader] int page = 1, [FromHeader] int limit = 10,
            string keySearch = "", bool? gender = null, DateTime? dob = null, bool? deactivated = null)
        {
            var clinicIDs = new List<string>() { clinicID };
            var roleType = (byte)RoleType.Doctor;
            return _PatientBusiness.Search(clinicIDs, keySearch, roleType, gender, dob, deactivated, page, limit);
        }

        [Authorize]
        [HttpGet("Clinics/{clinicID}/Doctors/{doctorID}")]
        public UserModelDetail GetDoctorByID(string clinicID, string doctorID)
        {
            return _PatientBusiness.GetByID(doctorID, clinicID);
        }

        [Authorize]
        [ClinicStaff]
        [HttpPost("Clinics/{clinicID}/Doctors")]
        public UserModelDetail CreateDoctor(string clinicID, [FromBody] UserModelCreate data)
        {
            var roleType = (byte)RoleType.Doctor;
            return _PatientBusiness.Create(clinicID, data, roleType);
        }

        [Authorize]
        [ClinicStaff]
        [HttpPut("Clinics/{clinicID}/Doctors/{doctorID}")]
        public UserModelDetail UpdateDoctor(string clinicID, string doctorID, [FromBody] UserModelUpdate data)
        {
            return _PatientBusiness.Update(clinicID, doctorID, data);
        }

        [Authorize]
        [ClinicStaff]
        [HttpPut("Clinics/{clinicID}/Doctors/{doctorID}/Invite")]
        public UserModelDetail InviteDoctor(string clinicID, string doctorID)
        {
            return _PatientBusiness.Invite(clinicID, doctorID);
        }

        [Authorize]
        [ClinicStaff]
        [HttpDelete("Clinics/{clinicID}/Doctors/{doctorID}")]
        public bool DeactivateDoctor(string clinicID, string doctorID)
        {
            return _PatientBusiness.Deactivate(clinicID, doctorID);
        }

        [Authorize]
        [ClinicStaff]
        [HttpPut("Clinics/{clinicID}/Doctors/{doctorID}/Reactivate")]
        public bool ReactivateDoctor(string clinicID, string doctorID)
        {
            return _PatientBusiness.Reactivate(clinicID, doctorID);
        }
        #endregion

        #region Owner Appointment APIs
        [Authorize]
        [ClinicStaff]
        [HttpGet("Clinics/{clinicID}/Appointments")]
        public SearchResult<AppointmentModel> SearchAppointments(string clinicID, string keySearch, DateTime? timeStart, DateTime? timeEnd, int? statusID, [FromHeader] int page = 1, [FromHeader] int limit = 10)
        {
            var clinicIDs = new List<string>() { clinicID };
            return _AppointmentBusiness.Search(clinicIDs, keySearch, timeStart, timeEnd, statusID, page, limit);
        }
        [Authorize]
        [ClinicStaff]
        [HttpGet("Clinics/{clinicID}/Appointments/{appointmentID}")]
        public AppointmentDetail GetAppointment(string clinicID, string appointmentID)
        {
            return _AppointmentBusiness.GetByID(appointmentID);
        }
        [Authorize]
        [ClinicStaff]
        [HttpPost("Clinics/{clinicID}/Appointments")]
        public AppointmentDetail CreateAppointment(string clinicID, [FromBody] AppointmentClinicCreate data)
        {
            return _AppointmentBusiness.Create(clinicID, data);
        }
        [Authorize]
        [ClinicStaff]
        [HttpPost("Clinics/{clinicID}/Appointments/{appointmentID}/CheckIn")]
        public EpisodeDetail CheckIn(string clinicID, string appointmentID)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _EpisodeBusiness.Create(appointmentID);
        }
        [Authorize]
        [ClinicStaff]
        [HttpPut("Clinics/{clinicID}/Appointments/{appointmentID}")]
        public AppointmentDetail UpdateAppointment(string clinicID, string appointmentID, [FromBody] AppointmentUpdate data)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _AppointmentBusiness.Update(appointmentID, data, user.UserID);
        }
        [Authorize]
        [ClinicStaff]
        [HttpDelete("Clinics/{clinicID}/Appointments/{appointmentID}")]
        public bool DeleteAppointment(string clinicID, string appointmentID)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _AppointmentBusiness.Delete(appointmentID, user.UserID);
        }
        #endregion

        #region Owner Clinic Episode APIs
        [Authorize]
        [ClinicStaff]
        [HttpGet("Clinics/{clinicID}/Episodes")]
        public SearchResult<EpisodeModel> SearchEpisodes(string clinicID, string keySearch, bool deleted = false, [FromHeader] int page = 1, [FromHeader] int limit = 10)
        {
            var clinicIDs = new List<string>() { clinicID };
            return _EpisodeBusiness.Search(clinicIDs, keySearch, deleted, page, limit);
        }
        [Authorize]
        [ClinicStaff]
        [HttpGet("Clinics/{clinicID}/Episodes/{episodeID}")]
        public EpisodeDetail GetEpisode(string clinicID, string episodeID)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _EpisodeBusiness.GetByIDWithPermission(episodeID, user.UserID);
        }

        [Authorize]
        [HttpPut("Clinics/{clinicID}/Episodes/{episodeID}/Public")]
        public string PublicEpisode(string clinicID, string episodeID, [FromBody] EpisodePublicStatus body)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _EpisodeBusiness.PublicEpisode(clinicID, episodeID, user.UserID, body);
        }

        [Authorize]
        [HttpPut("Clinics/{clinicID}/Episodes/{episodeID}/Records/{recordID}")]
        public bool UpdateRecordResult(string clinicID, string episodeID, string recordID, [FromBody] RecordUpdate data)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _EpisodeBusiness.UpdateRecordResult(clinicID, user.UserID, episodeID, recordID, data);
        }
        [Authorize]
        [ClinicStaff]
        [HttpPost("Clinics/{clinicID}/Episodes/{episodeID}/Complete")]
        public bool Complete(string clinicID, string episodeID)
        {
            return _EpisodeBusiness.Complete(clinicID, episodeID);
        }
        [Authorize]
        [ClinicStaff]
        [HttpPut("Clinics/{clinicID}/Episodes/{episodeID}/Invoice")]
        public bool UpdateInvoice(string clinicID, string episodeID, [FromBody] InvoiceUpdate data)
        {
            return _EpisodeBusiness.UpdateInvoice(episodeID, data);
        }

        // Asset Comsume Section
        [Authorize]
        [HttpGet("Clinics/{clinicID}/Episodes/{episodeID}/AssetConsumes")]
        public SearchResult<AssetConsumedModel> SearchConsumeAsset(string clinicID, string episodeID, string keySearch, [FromHeader] int page = 1, [FromHeader] int limit = 10)
        {
            return _AssetBusiness.SearchConsumeAsset(episodeID, keySearch, page, limit);
        }
        [Authorize]
        [HttpGet("Clinics/{clinicID}/Episodes/{episodeID}/AssetConsumes/{assetConsumeID}")]
        public AssetConsumedModel GetConsumeAsset(string clinicID, string episodeID, string assetConsumeID)
        {
            return _AssetBusiness.GetConsumeAsset(assetConsumeID);
        }
        [Authorize]
        [HttpPost("Clinics/{clinicID}/Episodes/{episodeID}/AssetConsumes")]
        public AssetConsumedModel GetConsumeAsset(string clinicID, string episodeID, [FromBody] AssetConsumedCreate data)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _AssetBusiness.CreateConsumeAsset(user.UserID, data);
        }
        [Authorize]
        [HttpPut("Clinics/{clinicID}/Episodes/{episodeID}/AssetConsumes/{assetConsumeID}")]
        public AssetConsumedModel UpdateConsumeAsset(string clinicID, string episodeID, string assetConsumeID, [FromBody] AssetConsumedUpdate data)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _AssetBusiness.UpdateConsumeAsset(assetConsumeID, user.UserID, data);
        }
        [Authorize]
        [HttpDelete("Clinics/{clinicID}/Episodes/{episodeID}/AssetConsumes/{assetConsumeID}")]
        public bool DeleteConsumeAsset(string clinicID, string episodeID, string assetConsumeID)
        {
            return _AssetBusiness.DeleteConsumeAsset(assetConsumeID);
        }
        // end of Asset Comsume Section

        [Authorize]
        [ClinicStaff]
        [HttpPost("Clinics/{clinicID}/Episodes")]
        public EpisodeDetail CreateEpisode(string clinicID, [FromBody] EpisodeCreate data)
        {
            return _EpisodeBusiness.Create(clinicID, data);
        }
        [Authorize]
        //[ClinicStaff]
        [HttpDelete("Clinics/{clinicID}/Episodes/{episodeID}")]
        public bool DeleteEpisode(string clinicID, string episodeID)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _EpisodeBusiness.Delete(episodeID, user.UserID);
        }
        #endregion

        #region Owner Clinic Asset APIs
        [Authorize]
        [HttpGet("Clinics/{clinicID}/Assets")]
        public SearchResult<AssetModel> GetAssets(string clinicID, [FromHeader] int page = 1, [FromHeader] int limit = 10,
            string keySearch = "", int? statusID = (int)StatusType.Active)
        {
            return _AssetBusiness.GetAssets(clinicID, keySearch, statusID, page, limit);
        }

        [Authorize]
        [HttpGet("Clinics/{clinicID}/Assets/{assetID}")]
        public AssetModel GetAssetDetail(string clinicID, string assetID)
        {
            return _AssetBusiness.GetAssetDetail(assetID);
        }

        [Authorize]
        [HttpGet("Clinics/{clinicID}/Assets/Attachments")]
        public SearchResult<AttachmentModel> GetAssetAttachments(string clinicID, [FromHeader] int page = 1, [FromHeader] int limit = 10,
            string keySearch = "", int? attachmentType = null)
        {
            return _AttachmentBusiness.Search(attachmentType, (byte)CategoryType.Asset, clinicID, "", keySearch, page, limit);
        }

        [Authorize]
        [HttpGet("Clinics/{clinicID}/Assets/{assetID}/Attachments")]
        public SearchResult<AttachmentModel> GetAssetDetailAttachments(string clinicID, string assetID, [FromHeader] int page = 1, [FromHeader] int limit = 10,
            string keySearch = "", int? attachmentType = null)
        {
            return _AttachmentBusiness.Search(attachmentType, (byte)CategoryType.Asset, clinicID, assetID, keySearch, page, limit);
        }

        [Authorize]
        [ClinicStaff]
        [HttpPost("Clinics/{clinicID}/Assets/{assetID}/Attachments")]
        public AttachmentModel CreateAssetAttachment(string clinicID, string assetID, [FromBody] AttachmentModelCreate data)
        {
            return _AttachmentBusiness.Create(clinicID, assetID, (byte)CategoryType.Asset, data);
        }

        [Authorize]
        [ClinicStaff]
        [HttpDelete("Clinics/{clinicID}/Assets/{assetID}/Attachments/{attachmentID}")]
        public bool DeleteAssetAttachment(string clinicID, string assetID, string attachmentID)
        {
            return _AttachmentBusiness.Delete(clinicID, attachmentID);
        }

        [Authorize]
        [ClinicStaff]
        [HttpPost("Clinics/{clinicID}/Assets")]
        public AssetModel CreateAsset(string clinicID, [FromBody] AssetModelCreate data)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _AssetBusiness.CreateAsset(clinicID, user.UserID, data);
        }

        [Authorize]
        [ClinicStaff]
        [HttpPut("Clinics/{clinicID}/Assets/{assetID}")]
        public AssetModel UpdateAsset(string clinicID, string assetID, [FromBody] AssetModelUpdate data)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _AssetBusiness.UpdateAsset(assetID, user.UserID, data);
        }

        [Authorize]
        [ClinicStaff]
        [HttpDelete("Clinics/{clinicID}/Assets/{assetID}")]
        public bool DeleteAsset(string clinicID, string assetID)
        {
            return _AssetBusiness.DeleteAsset(assetID);
        }

        [Authorize]
        [ClinicStaff]
        [HttpPost("Clinics/{clinicID}/Assets/{assetID}/Import")]
        public AssetImportModel ImportAsset(string clinicID, string assetID, [FromBody] AssetImportCreate data)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _AssetBusiness.ImportAsset(assetID, user.UserID, data);
        }
        #endregion

        #region Owner Clinic Report APIs
        [Authorize]
        [ClinicStaff]
        [HttpGet("Clinics/{clinicID}/Report/Revenue/Stats")]
        public RevenueStatistics GetRevenueStats(string clinicID)
        {
            return _ReportBusiness.GetRevenueStats(clinicID);
        }

        [Authorize]
        [ClinicStaff]
        [HttpGet("Clinics/{clinicID}/Report/Revenue/Excel")]
        public FileResult GetRevenueExcel(string clinicID, DateTime? timeStart, DateTime? timeEnd)
        {
            var dataFile = _ReportBusiness.GetRevenueExcel(clinicID, timeStart, timeEnd);
            return File(dataFile.Bytes, dataFile.ContentType, dataFile.Name);
        }
        #endregion

        #region TargetItem Attachment APIs
        [Authorize]
        [HttpGet("Clinics/{clinicID}/TargetItems/{targetItemID}/Attachments")]
        public SearchResult<AttachmentModel> GetTargetItemAttachments(string clinicID, string targetItemID, [FromHeader] int page = 1, [FromHeader] int limit = 10,
            string keySearch = "", int? attachmentType = null, int? targetItemType = null)
        {
            return _AttachmentBusiness.Search(attachmentType, targetItemType, clinicID, targetItemID, keySearch, page, limit);
        }

        [Authorize]
        [ClinicStaff]
        [HttpPost("Clinics/{clinicID}/TargetItems/{targetItemID}/Attachments")]
        public AttachmentModel CreateTargetItemAttachment(string clinicID, string targetItemID, int targetItemType, [FromBody] AttachmentModelCreate data)
        {
            return _AttachmentBusiness.Create(clinicID, targetItemID, targetItemType, data);
        }
        [Authorize]
        [ClinicStaff]
        [HttpDelete("Clinics/{clinicID}/TargetItems/{targetItemID}/Attachments/{attachmentID}")]
        public bool DeleteTargetItemAttachment(string clinicID, string attachmentID)
        {
            return _AttachmentBusiness.Delete(clinicID, attachmentID);
        }
        #endregion

        #region TargetItem Category APIs
        [Authorize]
        [HttpGet("Clinics/{clinicID}/TargetItems/{targetItemID}/Categories")]
        public SearchResult<CategoryModel> GetTargetItemCategories(string clinicID, string targetItemID, [FromHeader] int page = 1, [FromHeader] int limit = 10,
            string keySearch = "", int? targetItemType = null)
        {
            var deleted = false;
            return _CategoryBusiness.Search(clinicID, keySearch, targetItemID, targetItemType, deleted, page, limit);
        }

        [Authorize]
        [ClinicStaff]
        [HttpPost("Clinics/{clinicID}/TargetItems/{targetItemID}/AttachCategory/{categoryID}")]
        public bool AttachTargetItemCategory(string clinicID, string targetItemID, string categoryID, int targetItemType)
        {
            return _CategoryBusiness.AttachItem(clinicID, categoryID, targetItemID, targetItemType);
        }

        [Authorize]
        [ClinicStaff]
        [HttpPut("Clinics/{clinicID}/TargetItems/{targetItemID}/ModifyCategories")]
        public bool ModifyTargetItemCategories(string clinicID, string targetItemID, int targetItemType, [FromBody] CategoryModify body)
        {
            return _CategoryBusiness.ModifyMultiple(clinicID, targetItemID, targetItemType, body.CategoryIDs);
        }

        [Authorize]
        [ClinicStaff]
        [HttpDelete("Clinics/{clinicID}/TargetItems/{targetItemID}/DetachCategory/{categoryID}")]
        public bool DetachTargetItemCategory(string clinicID, string targetItemID, string categoryID, int targetItemType)
        {
            return _CategoryBusiness.DetachItem(clinicID, categoryID, targetItemID, targetItemType);
        }
        #endregion

        #region Owner Services + Patients + Doctors + Appointments + Episodes API
        [Authorize]
        [HttpGet("Services")]
        public SearchResult<ServiceModel> SearchServices(string search, int statusID = (int)StatusType.Active, [FromHeader] int page = 1, [FromHeader] int limit = 10)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _ServiceService.Search(user.UserID, null, statusID, search, page, limit);
        }
        [Authorize]
        [HttpGet("Services/{serviceID}")]
        public ServiceModelDetail GetServiceByID(string serviceID)
        {
            return _ServiceService.GetByID(serviceID);
        }
        [Authorize]
        [HttpGet("Patients")]
        public SearchResult<UserModel> Search([FromHeader] int page = 1, [FromHeader] int limit = 10,
            string keySearch = "", bool? gender = null, DateTime? dob = null, bool? deactivated = null)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            var clinicIDs = user.Roles.Select(r => r.ClinicID).ToList();
            return _PatientBusiness.Search(clinicIDs, keySearch, null, gender, dob, deactivated, page, limit);
        }
        [Authorize]
        [HttpGet("Patients/{patientID}")]
        public UserModelDetail GetByID(string patientID)
        {
            return _PatientBusiness.GetByID(patientID, "");
        }
        [Authorize]
        [HttpGet("Doctors")]
        public SearchResult<UserModel> SearchDoctors([FromHeader] int page = 1, [FromHeader] int limit = 10,
            string keySearch = "", bool? gender = null, DateTime? dob = null, bool? deactivated = null)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            var clinicIDs = user.Roles.Select(r => r.ClinicID).ToList();
            var doctorRole = (byte)RoleType.Doctor;
            return _PatientBusiness.Search(clinicIDs, keySearch, doctorRole, gender, dob, deactivated, page, limit);
        }
        [Authorize]
        [HttpGet("Doctors/{doctorID}")]
        public UserModelDetail GetDoctorByID(string doctorID)
        {
            return _PatientBusiness.GetByID(doctorID, "");
        }
        [Authorize]
        [HttpGet("Appointments")]
        public SearchResult<AppointmentModel> SearchAppointments(string keySearch, DateTime? timeStart, DateTime? timeEnd, int? statusID, [FromHeader] int page = 1, [FromHeader] int limit = 10)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            var clinicIDs = user.Roles.Select(r => r.ClinicID).ToList();
            return _AppointmentBusiness.Search(clinicIDs, keySearch, timeStart, timeEnd, statusID, page, limit);
        }
        [Authorize]
        [HttpGet("Appointments/{appointmentID}")]
        public AppointmentDetail GetAppointment(string appointmentID)
        {
            return _AppointmentBusiness.GetByID(appointmentID);
        }
        [Authorize]
        [HttpGet("Episodes")]
        public SearchResult<EpisodeModel> SearchEpisodes(string keySearch, bool deleted = false, [FromHeader] int page = 1, [FromHeader] int limit = 10)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            var clinicIDs = user.Roles.Select(r => r.ClinicID).ToList();
            return _EpisodeBusiness.Search(clinicIDs, keySearch, deleted, page, limit);
        }
        [Authorize]
        [HttpGet("Episodes/{episodeID}")]
        public EpisodeDetail getEpisode(string episodeID)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _EpisodeBusiness.GetByIDWithPermission(episodeID, user.UserID);
        }
        //[Authorize]
        [HttpGet("Assets/{assetID}")]
        public AssetModel GetAssetDetail(string assetID)
        {
            return _AssetBusiness.GetAssetDetail(assetID);
        }
        #endregion
    }
}
