using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using Harlem.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Security.Claims;
using System.Linq;

namespace Harlem.Core.Tools
{
   public static class  Extensions
    {
        public static (string FileName,string PhsicalPath) WriteFile(this IFormFile formFile, string location )
        {
                FileInfo fileInfo = new FileInfo(formFile.FileName);
                var fileExtension = fileInfo.Extension;
                string fileName = Guid.NewGuid().ToString() + fileExtension;
                if (!Directory.Exists(location)) Directory.CreateDirectory(location);
                var phsicalPath = location + "\\" + fileName;
                using (var stream = System.IO.File.Create(phsicalPath))
                {
                     formFile.CopyTo(stream);
                }
                return (fileName,phsicalPath);
        }
        public static T DeepClone<T>(this T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }
        public static string Md5Hash(this string rawData)
        {
            MD5CryptoServiceProvider cryptoGrapher = new MD5CryptoServiceProvider();
            byte[] dizi = Encoding.UTF8.GetBytes(rawData);
            dizi = cryptoGrapher.ComputeHash(dizi);
            StringBuilder sb = new StringBuilder();
            foreach (byte ba in dizi)
            {
                sb.Append(ba.ToString("x2").ToLower());
            }
            return sb.ToString();
        }
      

    }
}

