using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileManager.Data;

namespace FileManager.Mvc.Models
{
    public class FileModel
    {
        #region Fields
        public int FileId { get; set; }

        public string FileName { get; set; }

        public HttpPostedFileBase FileContent { get; set; }

    

        public int FolderId { get; set; }

        #endregion

        #region methods

        public FileModel() { }

        public FileModel(File domainFile) 
        {
            this.FileId = domainFile.FileId;
            this.FileName = domainFile.FileName;
            this.FolderId = domainFile.FolderId;
        }

        public File ToDomain()
        {
            return new File
            {
                FileId = FileId,
                FileName = FileName,
                FolderId = FolderId
            };
        }

        #endregion
    }
}