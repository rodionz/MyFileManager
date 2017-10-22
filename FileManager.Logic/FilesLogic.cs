using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using FileManager.Data;
using FileManager.Dal;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;

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





        public byte[] GetFileContent(int fileId)
        {
            using(var context = new FileManagerContext())
            {
                var content = (from c in context.Files
                               where c.FileId == fileId
                               select c.FileContent).FirstOrDefault();

                return content;
            }
        }




        public static void CreateMessageWithAttachment(string server)
        {
            // Specify the file to be attached and sent. 
            // This example assumes a file named Data.xls exists in the 
            // 
            //currentWorkingDirectory . 
            string file = "data.xls";
            // Create a message and set up the recipients. 
            MailMessage message = new MailMessage(
                "jane@contoso.com",
                "ben@contoso.com",
                "Quarterly data report.",
                "See the attached spreadsheet.");

            // Create the file attachment for this e-mail message. 
            Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
            // Add timestamp information for the file.
            ContentDisposition disposition = data.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(file);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
            // Add the file attachment to this e-mail message.
            message.Attachments.Add(data);

            // Send the message. 
            SmtpClient client = new SmtpClient(server);
            // Add credentials if the SMTP server requires them.
            client.Credentials = CredentialCache.DefaultNetworkCredentials;

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {

            }

        }
    }
}
