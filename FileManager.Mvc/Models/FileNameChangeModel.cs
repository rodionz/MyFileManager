using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileManager.Data;

namespace FileManager.Mvc.Models
{
    public class FileNameChangeModel
    {
        public int Id { get; set; }    
        public string OldName { get; set; }  
        public string NewName { get; set; }
        public int FileId { get; set; }
        public string Date { get; set; }

       public FileNameChangeModel(FileNameChange domain)
        {
            this.Id = domain.Id;
            this.OldName = domain.OldName;
            this.NewName = domain.NewName;
            this.FileId = domain.FileId;
            this.Date = domain.FileChangeDate.ToShortDateString();
        }
    }
}