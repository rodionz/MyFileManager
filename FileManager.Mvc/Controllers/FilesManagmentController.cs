using FileManager.Mvc.Models;
using System.Web;
using System.Web.Mvc;
using FileManager.Logic;
using System.Linq;
using System;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace FileManager.Mvc.Controllers
{
    public class FilesManagmentController : Controller
    {

        private readonly FilesService _fileservice;

        public FilesManagmentController()
        {
            _fileservice = new FilesService();         
        }

        // GET: Files
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetAllFilesFromFolder(FolderModel folder)
        {
            var files = _fileservice.GetAllFilesFromFolder(folder.FolderId).Select(f => new FileModel(f));
            return Json(files, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void UploadFile()
        {
            var file = Request.Files[0];
            var myFile = new FileModel();
            myFile.FolderId = int.Parse(Request.QueryString.ToString());           
            myFile.FileName = file.FileName;
            myFile.ContentType = file.ContentType;

            if (ModelState.IsValid)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                    myFile.FileContent = array;
                }
                _fileservice.AddFile(myFile.ToDomainWhithContent());
            }           
        }

        

        public ActionResult UpdateFile(FileModel folder)
        {
            if (ModelState.IsValid)
            {
                return Json(folder, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }

        public ActionResult DeleteFile(FileModel folder)
        {
            if (ModelState.IsValid)
            {
                return Json(folder, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }

        public ActionResult DownloadFile(int fileid)
        {
           var fileWhithContent = new FileModel(_fileservice.GetFileWhithContent(fileid), true);
           return File(fileWhithContent.FileContent, fileWhithContent.ContentType, fileWhithContent.FileName);                  
        }

        public void SendFileByEmail(EmailSendModel emailModel) 
        {
            if (ModelState.IsValid)
            {
                var messageAttachment = new FileModel(_fileservice.GetFileWhithContent(emailModel.FileId), true);
                string from = "*****";

                using(MailMessage mail = new MailMessage(from, emailModel.RecipientsEmail))
                using (var stream = new MemoryStream(messageAttachment.FileContent))
                {
                    mail.Subject = emailModel.Subject;                  
                    mail.Attachments.Add(new Attachment(stream, messageAttachment.FileName, messageAttachment.ContentType));
                    NetworkCredential networkCredential = new NetworkCredential(from, "******");
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;               
                    smtp.UseDefaultCredentials = false;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = networkCredential;
                    smtp.Port = 587;
                    smtp.Send(mail);
                }
            }
        }
    }
}
