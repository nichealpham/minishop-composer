using AppGlobal.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppGlobal.Models
{
    public class SearchResult<TClass> where TClass : class
    {
        public int Totals { get; set; }
        public List<TClass> Items { get; set; }
    }

    public class ApiError : Exception
    {
        public int ErrorCode { get; set; }
        public string ErrorName { get; set; }
        public string ErrorMessage { get; set; }

        public ApiError(int errorCode)
        {
            ErrorCode = errorCode;
            ErrorName = Enum.GetName(typeof(ErrorCodes), errorCode);
        }
    }
}
