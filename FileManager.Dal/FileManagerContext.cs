using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using FileManager.Data;

namespace FileManager.Dal
{
   public class FileManagerContext : DbContext
    {
        public virtual DbSet<File> Files { get; set; }

        public virtual DbSet<Folder> Folders { get; set; }

        public virtual DbSet<FileComment> FileComments { get; set; }

        public virtual DbSet<FileChange> FileChanges { get; set; }
    }
}
