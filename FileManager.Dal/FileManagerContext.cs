using System.Data.Entity;
using FileManager.Data;

namespace FileManager.Dal
{
   public class FileManagerContext : DbContext
    {
        public virtual DbSet<File> Files { get; set; }

        public virtual DbSet<Folder> Folders { get; set; }

        public virtual DbSet<FileNameChange> FileNameChanges { get; set; }

        public virtual DbSet<FileComment> FileComments { get; set; }
    }
}
