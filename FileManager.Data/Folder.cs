using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FileManager.Data
{
     public class Folder
    {   
        [Key]
        public int FolderId { get; set; }
        [Required]
        [StringLength(30)]
        public string FolderName { get; set; }
        public ICollection<File> Files { get; set; }
    }
}
