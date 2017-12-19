using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Data
{

    public class FileComment
    {
        public int Id { get; set; }
        public string CommentText { get; set; }
        public int FileId { get; set; }
        public virtual File File { get; set; }
    }
}
