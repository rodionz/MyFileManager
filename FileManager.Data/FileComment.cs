using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FileManager.Data
{
   public class FileComment
    {

       [Key]
       public int CommentId { get; set; }

       public int FileId { get; set; }

       public virtual File File { get; set;}

       public DateTime CommentDate { get; set; }

    }
}
