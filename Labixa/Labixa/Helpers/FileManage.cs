using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Labixa.Helpers
{
    public static class FileManage
    {
        public static void CreateFile(string fileName)
        {
            FileStream fs = null;
            if (!File.Exists(HttpContext.Current.Server.MapPath("~/images/"+fileName)))
            {
                using (fs = File.Create(HttpContext.Current.Server.MapPath("~/images/" + fileName)))
                {

                }
            }
        }
        
        public static void writeFile(string fileName,string value)
        {
            if (File.Exists(HttpContext.Current.Server.MapPath("~/images/" + fileName)))
            {
                using (StreamWriter sw = new StreamWriter(HttpContext.Current.Server.MapPath("~/images/" + fileName),true))
                {
                    sw.WriteLine(value);
                    sw.Close();
                }
            }
        }

        public static string readFile(string fileName)
        {
            if (File.Exists(HttpContext.Current.Server.MapPath("~/images/" + fileName)))
            {
                using (TextReader tr = new StreamReader(HttpContext.Current.Server.MapPath("~/images/" + fileName)))
                {
                    return tr.ReadLine();
                }
            }
            else
            {
                return null;
            }
        }

        public static void deleteFile(string fileName)
        {
            if (File.Exists(HttpContext.Current.Server.MapPath("~/images/" + fileName)))
            {
                File.Delete(HttpContext.Current.Server.MapPath("~/images/" + fileName));
            }
        }
    }
}