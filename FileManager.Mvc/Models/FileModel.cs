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

        public string Comment { get; set; }

        public int FolderId { get; set; }

        #endregion

        #region constructors

        public FileModel() { }

        public FileModel(File domainFile) 
        {
            this.FileId = domainFile.FileId;
            this.FileName = domainFile.FileName;
            // TODO File Content
        
        }

        #endregion
    }
}