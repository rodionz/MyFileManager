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
  public class FoldersLogic
    {
      public IEnumerable<Folder> GetAllFolders()
      {
          using (var context = new FileManagerContext())
          {
              var allFolders = (from f in context.Folders
                                select f).ToArray();

              return allFolders;
          }
      }

      public Folder GetFolderById(int folderId)
      {
          using (var context = new FileManagerContext())
          {
              var folder = (from f in context.Folders
                            where f.FolderId == folderId
                            select f).FirstOrDefault();

              return folder;
          }
      }

      public void AddFolder(Folder folder)
      {
          using (var context = new FileManagerContext())
          {
              context.Folders.Attach(folder);
              context.Entry(folder).State = EntityState.Added;
              context.SaveChanges();
          }
      }

      public void UpdateFolder(Folder folder)
      {
          using (var context = new FileManagerContext())
          {
              context.Folders.Attach(folder);
              context.Entry(folder).State = EntityState.Modified;
              context.SaveChanges();
          }
      }

      public void DeleteFolder(int folderId)
      {
          using (var context = new FileManagerContext())
          {
              var folder = (from f in context.Folders
                          where f.FolderId == folderId
                          select f).FirstOrDefault();


              context.Folders.Attach(folder);
              context.Entry(folder).State = EntityState.Deleted;
              context.SaveChanges();
          }
      }
   
    }
}
