using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using FileManager.Data;
using FileManager.Dal;

namespace FileManager.Logic
{
   public class FilesLogic
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
               context.Files.Attach(file);
               context.Entry(file).State = EntityState.Added;
               context.SaveChanges();
           }
       }

       public void UpdateFile(File file)
       {
           using (var context = new FileManagerContext())
           {
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

    }
}
