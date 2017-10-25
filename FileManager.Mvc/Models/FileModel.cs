using System.Web;
using FileManager.Data;
using System.ComponentModel.DataAnnotations;

namespace FileManager.Mvc.Models
{
    public class FileModel
    {
        #region Fields
        public int FileId { get; set; }
        [Required]
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileContent { get; set; }
        public int FolderId { get; set; }

        #endregion

        #region methods

        public FileModel() { }

        public FileModel(File domainFile) 
        {
            this.FileId = domainFile.FileId;
            this.FileName = domainFile.FileName;
            this.ContentType = domainFile.ContentType;
            this.FolderId = domainFile.FolderId;
        }

        public FileModel(File domainFile, bool bringContetn)
        {
            this.FileId = domainFile.FileId;
            this.FileName = domainFile.FileName;
            this.ContentType = domainFile.ContentType;
            this.FolderId = domainFile.FolderId;
            this.FileContent = domainFile.FileContent;
        }

        public File ToDomainWhithContent()
        {
            return new File
            {
                FileId = FileId,
                FileName = FileName,
                ContentType = ContentType,
                FolderId = FolderId,
                FileContent = FileContent
            };
        }

        public File ToDomainWhithoutContent()
        {
            return new File
            {
                FileId = FileId,
                FileName = FileName,
                ContentType = ContentType,
                FolderId = FolderId        
            };
        }
        #endregion
    }
}