using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Data
{
    public class File
    {
        public int FileId { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public byte[] FileContent { get; set; }

        public DateTime CreationDate { get; set; }

        public  ICollection<FileComment> FileComments { get; set; }

        public int FolderId { get; set; }

        public virtual Folder Folder { get; set; }


    }
}
