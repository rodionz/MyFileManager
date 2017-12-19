using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManager.Data;
using FileManager.Dal;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Data.Entity;

namespace FileManager.Logic
{
    public class FilesService
    {
        public IEnumerable<File> GetAllFiles()
        {
            using (var context = new FileManagerContext())
            {
                var allfiles = (from f in context.Files
                                select f).ToArray();
                return allfiles;
            }
        }

        public IEnumerable<File> GetAllFilesFromFolder(int folderId)
        {
            using (var context = new FileManagerContext())
            {
                var files = (from f in context.Files
                             where f.Folder.FolderId == folderId
                             select f).ToArray();
                return files;
            }
        }

        public File GetFileById(int fileId)
        {
            using (var context = new FileManagerContext())
            {
                var file = (from f in context.Files
                            where f.FileId == fileId
                            select f).FirstOrDefault();
                return file;
            }
        }

        public void AddFile(File file)
        {
            using (var context = new FileManagerContext())
            {
                file.CreationDate = DateTime.Now;
                context.Files.Attach(file);
                context.Entry(file).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void RenameFile(File file)
        {
            File oldFile;

            using (var context = new FileManagerContext())
            {
                oldFile = (from f in context.Files
                           where f.FileId == file.FileId
                           select f).FirstOrDefault();
            }
            var nameChange = new FileNameChange();

            if (oldFile.FileName != file.FileName)
            {

                nameChange.FileChangeDate = DateTime.Now;
                nameChange.FileId = file.FileId;
                nameChange.OldName = oldFile.FileName;
                nameChange.NewName = file.FileName;

                if (file.FileNameChanges == null)
                {
                    file.FileNameChanges = new List<FileNameChange>();
                }

                file.FileNameChanges.Add(nameChange);
            }
            file.FileContent = oldFile.FileContent;

            using (var context = new FileManagerContext())
            {
                context.FileNameChanges.Add(nameChange);
                context.SaveChanges();
                context.Files.Attach(file);
                context.Entry(file).State = EntityState.Modified;
                context.SaveChanges();
            }    
        }

        public IEnumerable<FileComment> GetFileComments(int fileId) {
        
            using(var context = new FileManagerContext())
            {               
                var comments = (from c in context.FileComments
                               where c.FileId == fileId
                               select c).ToArray();
                return comments;
            }       
        }

        public IEnumerable<FileNameChange> GetNamesHistory(int fileid) { 
          
            using(var context = new FileManagerContext())
            {
                var history = (from h in context.FileNameChanges
                              where h.FileId == fileid
                               select h).ToArray();
                return history;
            }
        }

        public void UpdateFileComments(int fileId, string comment) 
        {
            File file;

            using (var context = new FileManagerContext())
            {
                file = (from f in context.Files
                         where f.FileId == fileId
                         select f).FirstOrDefault();

                var newComment = new FileComment();
                newComment.CommentText = comment;
                newComment.FileId = file.FileId;
                context.FileComments.Add(newComment);
                context.SaveChanges();
                context.Files.Attach(file);
                context.Entry(file).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteFile(int fileId)
        {
            using (var context = new FileManagerContext())
            {
                var file = (from f in context.Files
                            where f.FileId == fileId
                            select f).FirstOrDefault();
                context.Files.Attach(file);
                context.Entry(file).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }


        public File GetFileWhithContent(int fileId)
        {
            using(var context = new FileManagerContext())
            {
                var file = (from c in context.Files
                               where c.FileId == fileId
                               select c).FirstOrDefault();
                return file;
            }
        }
    }
}
