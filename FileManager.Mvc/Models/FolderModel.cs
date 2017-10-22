using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileManager.Data;

namespace FileManager.Mvc.Models
{
    public class FolderModel
    {
        public int FolderId { get; set; }

        public string FolderName { get; set; }



        public FolderModel() { }

        public FolderModel(Folder domainFolder) 
        {
            this.FolderId = domainFolder.FolderId;

            this.FolderName = domainFolder.FolderName;
        }

        public Folder ToDomain() 
        {
            return new Folder
            {
                FolderId = FolderId,
                FolderName = FolderName
            };
        }
    }
}