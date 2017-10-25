using System.Collections.Generic;

namespace FileManager.Data
{
     public class Folder
    {
        public int FolderId { get; set; }

        public string FolderName { get; set; }

        public  ICollection<File> Files { get; set; }


    }
}
