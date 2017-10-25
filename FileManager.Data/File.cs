using System;
using System.Collections.Generic;

namespace FileManager.Data
{
    public class File
    {
        public int FileId { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string ContentType { get; set; }

        public byte[] FileContent { get; set; }

        public DateTime? CreationDate { get; set; }

        public  ICollection<string> FileComments { get; set; }

        public ICollection<FileNameChange> FileNameChanges { get; set; }

        public int FolderId { get; set; }

        public virtual Folder Folder { get; set; }


    }
}
