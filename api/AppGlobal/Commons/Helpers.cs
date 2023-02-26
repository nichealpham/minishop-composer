using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using AppGlobal.Constants;
using AppGlobal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using AppGlobal.Extensions;
using System.Security.Cryptography;
using System.Text;
using System.Reflection;
using System.IO;
using System.IO.Compression;
using System.Xml.Serialization;
using System.Xml;

namespace AppGlobal.Commons
{
    public static class Validation
    {
        public static bool StatusID(int statusID)
        {
            if (!Enum.IsDefined(typeof(StatusType), statusID))
                return false;
            return true; 
        }

        public static bool IsNotLetter(char c) 
            => !IsLetter(c);
        public static bool IsLetter(char c) 
            => Regex.IsMatch(c.ToString(), "[a-zA-Z]", RegexOptions.IgnoreCase);        
    }

    public static class Helper
    {
        private static readonly string DEFAULT_REGION = "en-us";
        private static readonly CultureInfo DEFAULT_PROVIDER = new CultureInfo(DEFAULT_REGION);

        public static string PadZeros(int number, int padding, IFormatProvider provider = null)
        {
            provider ??= DEFAULT_PROVIDER;
            var format = padding.ToString("D02", provider);     // format padding has to be 2 digits
            return number.ToString($"D{format}", provider);     // return padded number
        }

        public static bool InRange<T>(T value, T min, T max) where T : IComparable<T>
        {
            // Assumption: min < max
            if (max.CompareTo(min) < 0)
                Swap(ref min, ref max);      // pass variables by reference (e.g. not by value/copy)
            return min.CompareTo(value) <= 0 && value.CompareTo(max) <= 0;
        }

        /* Verifies if segments A and B overlap or not */
        public static bool Overlap<T>(T aLeft, T aRight, T bLeft, T bRight) where T : IComparable<T>
        {
            /* Our assumption is that aLeft and aRight represent the left and right sides respectively.
             * So if arguments are passed in the wrong order, swap them. */
            if (aRight.CompareTo(aLeft) < 0)
                Swap(ref aLeft, ref aRight);
            if (bRight.CompareTo(bLeft) < 0)
                Swap(ref bLeft, ref bRight);
            return aLeft.CompareTo(bRight) <= 0 && bLeft.CompareTo(aRight) <= 0;
        }

        /* Swaps two variables of the same type */
        public static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
    }

    public static class EventFlags
    {
        public static int[] MINIMUM_BITS = { 7, 4, 12, 31 };
        public static int
            DAY_OF_WEEK_SELECTOR = 0,
            WEEK_OF_MONTH_SELECTOR = 1,
            DAY_OF_MONTH_SELECTOR = 2,
            MONTH_OF_YEAR_SELECTOR = 3;

        public static List<int> Explode(int selector, int? optionalCombinedFlags)
        {
            var list = new List<int>();
            if (!optionalCombinedFlags.HasValue)
                return list;
            var combinedFlags = optionalCombinedFlags.Value;
            var max = MINIMUM_BITS[selector];
            for (int i = 0, flag = 1; i < max; flag = 1 << ++i) // flag iterates powers of 2
                if ((combinedFlags & flag) == flag)
                    list.Add(i + 1);  // revert to 1-based numbers
            return list;
        }
        public static int Merge(List<int> numbers)    // 1-based numbers
        {
            int combinedFlags = 0;
            if (numbers == null)
                return 0;
            foreach (var i in numbers)
                combinedFlags |= 1 << (i - 1);
            return combinedFlags;
        }

        private static bool Matches(int combinedFlags, int flag)
            => combinedFlags == 0 ||                // If not set, always return true
                (combinedFlags & flag) == flag;     // or has been set + flag matches

        public static bool MatchesWeekOfMonth(this DateTime date, int recurrence)
            => Matches(recurrence, 1 << (date.Day / 8));       // get week number from day
        public static bool MatchesDayOfMonth(this DateTime date, int recurrence)
            => Matches(recurrence, 1 << (date.Day - 1));        // since day is [1,31], becomes [0,30]
        public static bool MatchesMonthOfYear(this DateTime date, int recurrence)
            => Matches(recurrence, 1 << (date.Month - 1));      // since month is [1,12]
    }

    public static class Transactions
    {
        // Convenience methods for transactions
        public static List<IDbContextTransaction> From(params DbContext[] contexts)
            => contexts.Select(context => context.Database.BeginTransaction()).ToList();
        public static void Commit(this List<IDbContextTransaction> committables)
            => committables.ForEach(committable => committable.Commit());
        public static void Rollback(this List<IDbContextTransaction> rollbackables)
            => rollbackables.ForEach(rollbackable => rollbackable.Rollback());
        public static void Dispose<T>(this List<T> disposables) where T : IDisposable
            => disposables.ForEach(disposable => disposable.Dispose());

        // Wrapper for each method signature type
        public static R TryExecuting<R>(Func<R> func, params DbContext[] contexts)
            => Wrap(contexts, () => func.Invoke());
        public static R TryExecuting<T, R>(Func<T, R> func, T parameter, params DbContext[] contexts)
            => Wrap(contexts, () => func.Invoke(parameter));
        public static R TryExecuting<T1, T2, R>(Func<T1, T2, R> func, T1 p1, T2 p2, params DbContext[] contexts)
            => Wrap(contexts, () => func.Invoke(p1, p2));
        public static R TryExecuting<T1, T2, T3,  R>(Func<T1, T2, T3, R> func, T1 p1, T2 p2, T3 p3, params DbContext[] contexts)
            => Wrap(contexts, () => func.Invoke(p1, p2, p3));
        public static R TryExecuting<T1, T2, T3, T4, R>(Func<T1, T2, T3, T4, R> func, T1 p1, T2 p2, T3 p3, T4 p4, params DbContext[] contexts)
            => Wrap(contexts, () => func.Invoke(p1, p2, p3, p4));
        public static R TryExecuting<T1, T2, T3, T4, T5, R>(Func<T1, T2, T3, T4, T5, R> func, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, params DbContext[] contexts)
            => Wrap(contexts, () => func.Invoke(p1, p2, p3, p4, p5));
        public static R TryExecuting<T1, T2, T3, T4, T5, T6, R>(Func<T1, T2, T3, T4, T5, T6, R> func, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, params DbContext[] contexts)
            => Wrap(contexts, () => func.Invoke(p1, p2, p3, p4, p5, p6));
        public static R TryExecuting<T1, T2, T3, T4, T5, T6, T7, R>(Func<T1, T2, T3, T4, T5, T6, T7, R> func, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, params DbContext[] contexts)
            => Wrap(contexts, () => func.Invoke(p1, p2, p3, p4, p5, p6, p7));
        public static R TryExecuting<T1, T2, T3, T4, T5, T6, T7, T8, R>(Func<T1, T2, T3, T4, T5, T6, T7, T8, R> func, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, params DbContext[] contexts)
            => Wrap(contexts, () => func.Invoke(p1, p2, p3, p4, p5, p6, p7, p8));
        public static R TryExecuting<T1, T2, T3, T4, T5, T6, T7, T8, T9, R>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, R> func, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, params DbContext[] contexts)
            => Wrap(contexts, () => func.Invoke(p1, p2, p3, p4, p5, p6, p7, p8, p9));
        public static R TryExecuting<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R> func, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, params DbContext[] contexts)
            => Wrap(contexts, () => func.Invoke(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10));
        // I dont think we have functions with more than 10 arguments ...

        private static R Wrap<R>(DbContext[] contexts, Func<R> func)
        {
            if (contexts.Length == 0)
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);  // Why call these methods just to not use transactions =v
            var transactions = From(contexts);
            try
            {
                R result = func.Invoke();
                transactions.Commit();
                return result;
            }
            catch
            {
                transactions.Rollback();
                throw new ApiError((int)ErrorCodes.FailedValidationData);
            }
            finally
            {
                transactions.Dispose();
            }
        }
    }

    public static class Transform
    {
        public static string ToHexString(this string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in str) 
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }
    }


    public static class Encryption
    {
        public static class TripleDES
        {
            private static TripleDESCryptoServiceProvider CreateProvider(string key)
                => new TripleDESCryptoServiceProvider
                {
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7,
                    Key = Encoding.UTF8.GetBytes(key)
                };

            public static string Encrypt(string clearText, string encryptKey, bool toReadable = true)
            {
                using var cryptProvider = CreateProvider(encryptKey);           // auto-close to prevent memory leaks
                byte[] byteBuff = UTF8Encoding.UTF8.GetBytes(clearText),        // data bytes buffer
                    encrypted = cryptProvider.CreateEncryptor()                 // uses pre-specified key + generated IV
                        .TransformFinalBlock(byteBuff, 0, byteBuff.Length);     // write & return byte buffer cipher text
                string encoded = Convert.ToBase64String(encrypted);
                if (toReadable)
                    return encoded.ToHexString();
                return encoded;
            }
        }

        public static class RSA
        {
            private static RSACryptoServiceProvider CreateProvider(string publicKey, int keySize = 2048)
            {
                var rsa = new RSACryptoServiceProvider(keySize);
                byte[] publicKeyByte = Convert.FromBase64String(publicKey);
                rsa.ImportSubjectPublicKeyInfo(publicKeyByte, out int read);
                if (read == 0)
                    throw new ArgumentException("Verify public key, read 0 bytes from it");
                return rsa;
            }

            public static string Encrypt(string dataString, string publicKey)
            {
                byte[] dataByte = Encoding.UTF8.GetBytes(dataString);
                using var rsa = CreateProvider(publicKey);
                try
                {
                    var encryptedData = rsa.Encrypt(dataByte, true);
                    var base64Encrypted = Convert.ToBase64String(encryptedData);
                    return base64Encrypted;
                }
                finally
                {   // delete key from provider only AFTER everything finished
                    rsa.PersistKeyInCsp = false;
                }
            }
        }
    }

    public static class ReflectionHelperGetterSetter
    {
        public static object GetNested(this object @object, string name)
            => @object.SearchNested(name);
        public static object SetNested<T>(this object @object, string name, T value)
            => @object.SearchNested(name, (obj, propertyInfo) => propertyInfo.SetValue(obj, value));

        private static object SearchNested(
            this object @object,                                // object to search property inside of
            string propertyPath,                                // absolute path/name of property
            params Action<object, PropertyInfo>[] actions)      // actions to apply when property is found
        {
            // Check property name is given
            if (propertyPath == null || propertyPath.Trim().Length == 0)
                return null;
            // Search for (potentially nested) property
            var names = propertyPath.Split('.');
            for (int i = 0; i < names.Length; i++)
            {
                if (@object == null)        // if object/property value is null
                    return null;
                var propertyInfo = @object.GetType().GetProperty(names[i]);
                if (propertyInfo == null)   // if can't find property
                    return null;
                if (i == names.Length - 1)  // if found element
                    foreach (var action in actions)
                        action.Invoke(@object, propertyInfo);   // give lambdas the property + info
                @object = propertyInfo.GetValue(@object, null);
            }
            return @object;
        }
    }


    public static class ConvertXML
    {
        public static string ConvertObjectToXML(this Object objectDta)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlSerializer xmlDta = new XmlSerializer(objectDta.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                xmlDta.Serialize(ms, objectDta);
                ms.Position = 0;
                xmlDoc.Load(ms);
                return xmlDoc.InnerXml;
            }
        }

        public static Object ConvertXmlToObject(this string xmlString, Object objectData)
        {
            XmlSerializer oXmlSerializer = new XmlSerializer(objectData.GetType());
            objectData = oXmlSerializer.Deserialize(new StringReader(xmlString));
            return objectData;
        }
    }

    public static class ConvertBase64
    {
        public static string Encode64(this string data)
        {
            var valueBytes = Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(valueBytes);
        }
        public static string Decode64(this string data)
        {
            var valueBytes = System.Convert.FromBase64String(data);
            return Encoding.UTF8.GetString(valueBytes);
        }
    }


    public static class CompressDeCompressString
    {
        public static string CompressGzipString(this string data)
        {
            byte[] InputBytes = Encoding.UTF8.GetBytes(data);
            ;
            using (var outPutStream = new MemoryStream())
            {
                using (var gZipStream = new GZipStream(outPutStream, CompressionMode.Compress))
                    gZipStream.Write(InputBytes, 0, InputBytes.Length);
                var outputBytes = outPutStream.ToArray();

                var outPutStr = Encoding.UTF8.GetString(outputBytes);
                return outPutStr;
            }
        }

        public static string DecompressGzipString(this string data)
        {
            byte[] inputBytes = Convert.FromBase64String(data);

            using (var inputStream = new MemoryStream(inputBytes))
            using (var gZipStream = new GZipStream(inputStream, CompressionMode.Decompress))
            using (var outputStream = new MemoryStream())
            {
                gZipStream.CopyTo(outputStream);
                var outputBytes = outputStream.ToArray();

                string decompressed = Encoding.UTF8.GetString(outputBytes);
                return decompressed;
            }
        }
    }


    public static class EnscryptDescryptSSLCipher
    {
        public static string EncryptSSLCBC(this string data, string key, string iv)
        {
            byte[] encrypted;
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.Key = Convert.FromBase64String(key);
                aes.IV = Convert.FromBase64String(iv);
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform enc = aes.CreateEncryptor(aes.Key, aes.IV);

                using(MemoryStream ms = new MemoryStream())
                {
                    using(CryptoStream cs = new CryptoStream(ms, enc, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.WriteLine(data);
                        }

                        encrypted = ms.ToArray();
                    }
                }
            }
            return Encoding.ASCII.GetString(encrypted);
        }


        public static string DecryptSSLCBC(this string data, string key, string iv)
        {
            string decrypted = null;
            byte[] cipher = Encoding.ASCII.GetBytes(data);

            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.Key = Convert.FromBase64String(key);
                aes.IV = Convert.FromBase64String(iv);
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform dec = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(cipher))
                {
                    using (CryptoStream cs = new CryptoStream(ms, dec, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            decrypted = sr.ReadToEnd();
                        }
                    }
                }
            }

            return decrypted;
        }
    }
}
