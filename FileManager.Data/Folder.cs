using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Data
{
     public class Folder
    {
        public int FolderId { get; set; }

        public string FolderName { get; set; }

        public  ICollection<File> Files { get; set; }


    }
}
