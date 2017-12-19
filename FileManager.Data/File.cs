using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FileManager.Data
{
    public class File
    {
        public int FileId { get; set; }
        [Required]
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public byte[] FileContent { get; set; }
        public DateTime? CreationDate { get; set; }
        public ICollection<FileComment> FileComments { get; set; }
        public ICollection<FileNameChange> FileNameChanges { get; set; }

        [Required]
        public int FolderId { get; set; }
        public virtual Folder Folder { get; set; }
    }
}
