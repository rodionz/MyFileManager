using System;

namespace FileManager.Data
{
     public class FileNameChange
    {
         public int Id { get; set; }

         public string OldName { get; set; }

         public string NewName { get; set; }

         public DateTime FileChangeDate { get; set; }

         public int FileId { get; set; }

         public virtual File File { get; set; }

    }
}
