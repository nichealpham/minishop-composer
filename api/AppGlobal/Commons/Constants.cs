using System;
using System.Collections.Generic;
using System.Text;

namespace AppGlobal.Constants
{
    public enum RoleType
    {
        Patient = 1,
        Doctor,
        Receptionist,
        Owner,
    }
    public enum StatusType
    {
        Deleted = 0,
        Active = 1,
    }

    public enum PatientStatusType
    {
        // Soft delete == Deactivated account
        Deactivated = 0,
        // Activated == Patient owns the account
        Activated = 1,
        // Owned == Clinic owns the account until patient activates it!
        Owned = 2,
    }

    public enum RecurrenceType
    {
        Daily = 1,
        Weekly = 2,
        Monthly = 3,
        Yearly = 4
    }

    public enum EpisodeStatusType
    {
        // Soft delete
        Deleted = 0,
        // Checked-in => Doctor or Owner checkin patient
        CheckedIn,
        // Success => Finised exam
        Success,
        // Failed => Cancel
        Canceled,
    }

    public enum AppointmentStatusType
    {
        // Soft delete
        Deleted = 0,
        Active,
        CheckedIn,
        Canceled,
    }

    public enum AdmissionType
    {
        CheckedIn = 1,
    }

    public enum ServiceCode
    {
        User = 0,
        Booking,
        Episode,
        Doctor,
        Service,
        Clinic,
    }

    public enum ActionCode
    {
        Deleted = 0,
        Created,
        Booked,
        Invited,
        Accepted,
        Checkin,
        Success,
    }

    public enum StatusCode
    {
        Danger = 0,
        Info,
        Warning,
        Success,
    }

    public enum AssetType
    {
        Asset = 1,
        Consumable,
    }

    public enum CategoryType
    {
        Service = 1,
        Asset,
        Episode,
    }

    public enum AttachmentType
    {
        Image = 1,
    }
}