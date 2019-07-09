using System;
using System.IO;
using System.Web;

namespace Project.TOOLUI.MyTools
{
    public static class VideoUploader
    {
        public static string UploadVideo(string serverPath, HttpPostedFileBase file)
        {
            if (file != null)
            {
                Guid uniqueName = Guid.NewGuid();
                serverPath = serverPath.Replace("~", string.Empty);
                string[] fileArray = file.FileName.Split('.');
                string extension = fileArray[fileArray.Length - 1].ToLower();
                string fileName = $"{uniqueName}.{extension}";

                if (extension == "mkv" || extension == "mp4" || extension == "flv" || extension == "avi" || extension == "wmv" || extension == "m4v" || extension == "mpg") 
                {
                    if (File.Exists(HttpContext.Current.Server.MapPath(serverPath + fileName)))
                    {
                        return "Dosya zaten mevcut";
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
                    return "Geçerli Bir Video Seçiniz!";
                }
            }
            else
            {
                return "Dosya Boş";
            }
        }
    }
}
