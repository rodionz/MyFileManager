using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Data
{
     public class FileChange
    {
         public int FileChangeId { get; set; }

         public string FileChangeText { get; set; }

         public DateTime FileChangeDate { get; set; }

         public int FileId { get; set; }

         public virtual File File { get; set; }

    }
}
