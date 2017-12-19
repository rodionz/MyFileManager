using System;
using System.ComponentModel.DataAnnotations;

namespace FileManager.Data
{
     public class FileNameChange
    {
         public int Id { get; set; }
         [Required]
         public string OldName { get; set; }
         [Required]
         public string NewName { get; set; }
         public DateTime FileChangeDate { get; set; }

         [Required]
         public int FileId { get; set; }
         public virtual File File { get; set; }
    }
}
