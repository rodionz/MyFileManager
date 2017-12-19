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

        public ActionResult Index()
        {
            return View();
        }

     
        public ActionResult GetAllFilesFromFolder(int folderId)
        {
            var files = _fileservice.GetAllFilesFromFolder(folderId).Select(f => new FileModel(f));
            return Json(files, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFileComments(int fileid) 
        {
            var fileComments = _fileservice.GetFileComments(fileid).Select(c => new FileCommentModel(c));
            return Json(fileComments, JsonRequestBehavior.AllowGet);          
        }

        public ActionResult GetNamesHistory(int fileid)
        {
            var hisory = _fileservice.GetNamesHistory(fileid).Select(c => new FileNameChangeModel(c));
            return Json(hisory, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateFileComments(FileModel file)
        {
            _fileservice.UpdateFileComments(file.FileId, file.LastComment);
            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UploadFile()
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

                return Json(string.Empty, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Response.StatusCode = 400;
                var error = new ErrorModel();
                error.ErrorID = 4;
                error.Message = "Upload Faile";
                return Json(error, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult RenameFile(FileModel file)
        {
            if (ModelState.IsValid)
            {
                _fileservice.RenameFile(file.ToDomainWhithoutContent());
                return Json(file, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Response.StatusCode = 400;
                var error = new ErrorModel();
                error.ErrorID = 4;
                error.Message = "Rename Failed";
                return Json(error, JsonRequestBehavior.AllowGet);
            }
        }

       
        public ActionResult DeleteFile(FileModel file)
        {
            if (ModelState.IsValid)
            {
                _fileservice.DeleteFile(file.FileId);
                return Json(file, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Response.StatusCode = 400;
                var error = new ErrorModel();
                error.ErrorID = 1;
                error.Message = "Delete Failed";
                return Json(error, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DownloadFile(int fileid)
        {
            var fileWhithContent = new FileModel(_fileservice.GetFileWhithContent(fileid), true);
            return File(fileWhithContent.FileContent, fileWhithContent.ContentType, fileWhithContent.FileName);
        }

        public ActionResult SendFileByEmail(EmailSendModel emailModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var messageAttachment = new FileModel(_fileservice.GetFileWhithContent(emailModel.FileId), true);
                    string from = "rodion@combix.co.il";
                    using (MailMessage mail = new MailMessage(from, emailModel.RecipientsEmail))
                    using (var stream = new MemoryStream(messageAttachment.FileContent))
                    {
                        mail.Subject = emailModel.Subject;
                        mail.Attachments.Add(new Attachment(stream, messageAttachment.FileName, messageAttachment.ContentType));
                        NetworkCredential networkCredential = new NetworkCredential(from, "r321616351z");
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Credentials = networkCredential;
                        smtp.Port = 587;
                        smtp.Send(mail);
                        return Json(string.Empty, JsonRequestBehavior.AllowGet);
                    }
                }
                catch
                {
                    Response.StatusCode = 400;
                    var error = new ErrorModel();
                    error.ErrorID = 123;
                    error.Message = "Mail is invalid";
                    return Json(error, JsonRequestBehavior.AllowGet);
                }
            }

            else
            {
                Response.StatusCode = 400; // Replace .AddHeader
                var error = new ErrorModel();  // Create class Error() w/ prop
                error.ErrorID = 2;
                error.Message = "Something went wrong!";
                return Json(error, JsonRequestBehavior.AllowGet);
            }
        }
    }
}