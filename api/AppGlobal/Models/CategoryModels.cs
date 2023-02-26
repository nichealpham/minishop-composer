using AppGlobal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppGlobal.Models
{
    public class CategoryModel
    {
        public Guid CategoryID { get; set; }
        public string CategoryName { get; set; }
        public Guid ClinicID { get; set; }
        public int ItemCounts { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public byte StatusID { get; set; }

        public static CategoryModel Convert(Category c) => c == null ? null : new CategoryModel()
        {
            CategoryID = c.CategoryID,
            CategoryName = c.CategoryName,
            ClinicID = c.ClinicID,
            StatusID = c.StatusID,
            DateCreated = c.DateCreated.ToLocalTime(),
            DateUpdated = c.DateUpdated.ToLocalTime(),
            ItemCounts = c.CategoryItems?.Count ?? 0,
        };
    }

    public class CategoryModelCreate
    {
        public string CategoryName { get; set; }
    }

    public class CategoryModelUpdate
    {
        public string CategoryName { get; set; }
    }

    public class CategoryItemUpdate
    {
        public string TargetItemID { get; set; }
        public int TargetItemType { get; set; }
    }

    public class CategoryModify
    {
        public List<string> CategoryIDs { get; set; }
    }
}
