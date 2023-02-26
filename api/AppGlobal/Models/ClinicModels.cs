using AppGlobal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppGlobal.Models
{
    public class ClinicModel
    {
        public Guid ClinicID { get; set; }
        public string ClinicName { get; set; }
        public string ShortDescription { get; set; }
        public string Logo { get; set; }
        public string Phone { get; set; }
        public string TaxNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateUpdated { get; set; }
        public byte StatusID { get; set; }
        public bool PublicStatus { get; set; }
        public string PublicNameUrl { get; set; }
        public static ClinicModel Convert(Clinic clinic)
            => clinic != null ? new ClinicModel()
            {
                ClinicID = clinic.ClinicID,
                ClinicName = clinic.ClinicName,
                ShortDescription = clinic.ShortDescription,
                Logo = clinic.Logo,
                Phone = clinic.Phone,
                TaxNumber = clinic.TaxNumber,
                Address = clinic.Address,
                DateUpdated = clinic.DateUpdated,
                StatusID = (byte)clinic.StatusID,
                PublicStatus = clinic.PublicStatus ?? false,
                PublicNameUrl = clinic.PublicNameUrl,
            } : null;
    }
    public class ClinicModelDetail
    {
        public Guid ClinicID { get; set; }
        public string ClinicName { get; set; }
        public string ShortDescription { get; set; }
        public string Logo { get; set; }
        public string Phone { get; set; }
        public string TaxNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateUpdated { get; set; }
        public byte StatusID { get; set; }
        public List<ClinicService> Services { get; set; }
        public bool PublicStatus { get; set; }
        public string PublicNameUrl { get; set; }
        public static ClinicModelDetail Convert(Clinic clinic)
            => new ClinicModelDetail()
            {
                ClinicID = clinic.ClinicID,
                ClinicName = clinic.ClinicName,
                ShortDescription = clinic.ShortDescription,
                Logo = clinic.Logo,
                Phone = clinic.Phone,
                TaxNumber = clinic.TaxNumber,
                Address = clinic.Address,
                DateUpdated = clinic.DateUpdated,
                StatusID = (byte)clinic.StatusID,
                PublicStatus = clinic.PublicStatus ?? false,
                PublicNameUrl = clinic.PublicNameUrl,
            };
    }

    public class ClinicService
    {
        public string ServiceID { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
    }

    public class ClinicModelCreate
    {
        public string ClinicName { get; set; }
        public string ShortDescription { get; set; }
        public string Logo { get; set; }
        public string Phone { get; set; }
        public string TaxNumber { get; set; }
        public string Address { get; set; }
    }
    public class ClinicModelUpdate
    {
        public string ClinicName { get; set; }
        public string ShortDescription { get; set; }
        public string Logo { get; set; }
        public string Phone { get; set; }
        public string TaxNumber { get; set; }
        public string Address { get; set; }
        public string PublicNameUrl { get; set; }
    }
}
