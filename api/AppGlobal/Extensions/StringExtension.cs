using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Security.Cryptography;
using System.IO;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Resources;

namespace AppGlobal.Extensions
{
    public static class StringExtension
    {
        private static readonly string[] VietnameseSigns = new string[]
        {

            "aAeEoOuUiIdDyY",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ"
        };

        public static string Base64Encode(this string plainText)
        {
            Base64UrlEncoder.Encode(plainText);
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64UrlEncode(this string plainText)
        {
            return Base64UrlEncoder.Encode(plainText);
        }

        public static string Base64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string Base64UrlDecode(this string base64EncodedData)
        {
            return Base64UrlEncoder.Decode(base64EncodedData);
        }

        public static string RemoveAccents(this string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    text = text.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return text;

            //var normalizedString = text.ToString().Normalize(NormalizationForm.FormD);
            //var stringBuilder = new StringBuilder();

            //foreach (var c in normalizedString)
            //{
            //    var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            //    if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            //    {
            //        stringBuilder.Append(c);
            //    }
            //}
            //return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string Standardizing(this string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            return text.RemoveAccents().ToLower();
        }

        public static string RemoveWhitespace(this string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            return Regex.Replace(text, @"\s+", "");
        }

        public static string HashMD5(this string text)
        {
            StringBuilder sBuilder = new StringBuilder();
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(text));

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
            }
            return sBuilder.ToString();
        }

        public static bool Match(this string text, string compareText)
        {
            string text1 = text.Standardizing().Trim();
            string text2 = compareText.Standardizing().Trim();
            return text1.Contains(text2) || text2.Contains(text1);
        }

        public static string RemoveExtraSpaces(this string s)
        {
            if (s == null)
                return null;
            return string.Join(" ", s.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
        }

        public static int ConvertToInt(this string text)
        {
            int result;
            int.TryParse(text, out result);
            return result;
        }

        public static DateTime ConvertToDateTime(this string text) 
        {
            DateTime result;
            if (!DateTime.TryParse(text, out result))
            {
                result = DateTime.FromOADate(double.Parse(text));
            }
            return result;
        }
    }
}
