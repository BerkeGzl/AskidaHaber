using System;
using System.IO;
using System.Reflection;
using System.Web;

namespace Project.TOOLUI.MyTools
{
    public static class ImageUploader
    {
        public static string UploadImage(string serverPath, HttpPostedFileBase file)
        {
            if (file != null)
            {
                Guid uniqueName = Guid.NewGuid();
                serverPath = serverPath.Replace("~", string.Empty); 
                string[] fileArray = file.FileName.Split('.'); 
                string extension = fileArray[fileArray.Length - 1].ToLower(); 
                string fileName = $"{uniqueName}.{extension}";

                if (extension == "jpg" || extension == "gif" || extension == "png" || extension == "jpeg")
                {
                    if (File.Exists(HttpContext.Current.Server.MapPath(serverPath + fileName)))
                    {
                        return "Dosya zaten var";
                    }
                    else
                    {
                        string filePath = HttpContext.Current.Server.MapPath(serverPath + fileName);
                        file.SaveAs(filePath);
                        return serverPath + fileName;
                    }
                }
                else
                {
                    return "Geçerli Bir Resim Seçiniz!";
                }
            }
            else
            {
                return "Dosya Boş";
            }
        }
    }
}
