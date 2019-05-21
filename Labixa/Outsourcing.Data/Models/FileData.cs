using System;
using System.IO;

namespace Outsourcing.Data.Models
{
    public class FileData : BaseEntity
    {
        public FileData()
        {
            CreateDate = DateTime.Now;
        }
        public FileData(string path)
        {
            FileName = Path.GetFileName(path);
            Url = path;
            MimeType = Path.GetExtension(path);
            CreateDate = DateTime.Now;
        }

        // public byte[] Data { get; set; }
        public string Title { get; set; }

        public string MimeType { get; set; }
        public string Url { get; set; }
        public string UrlDecode { get; set; }
        public string FileName { get; set; }
        public string FileNameWithoutUnicode { get; set; }
        public string Description { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsMainPicture { get; set; }
        public int DisplayOrder { get; set; }
        public string Owner { get; set; }
        public DateTime CreateDate { get; set; }

        public int? ProductId { get; set; }

    }
}