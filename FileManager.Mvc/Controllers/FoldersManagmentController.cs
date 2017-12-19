using FileManager.Logic;
using FileManager.Mvc.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileManager.Mvc.Controllers
{
    public class FoldersManagmentController : Controller
    {

        private readonly FoldersService _folderservice;

        public FoldersManagmentController()
        {
            _folderservice = new FoldersService();
        }

        public ActionResult Index()
        {
            return View("Folder");
        }

        [HttpPost]
        public ActionResult CreateNewFolder(FolderModel newFolder)
        {
            if (ModelState.IsValid)
            {
                _folderservice.AddFolder(newFolder.ToDomain());
                return Json(newFolder, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Response.StatusCode = 400;
                var error = new ErrorModel();
                error.ErrorID = 5;
                error.Message = "Creation failed";
                return Json(error, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult UpdateFolder(FolderModel updatedfolder)
        {
            if (ModelState.IsValid)
            {
                _folderservice.UpdateFolder(updatedfolder.ToDomain());
                return Json(updatedfolder, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Response.StatusCode = 400;
                var error = new ErrorModel();
                error.ErrorID = 6;
                error.Message = "Update failed";
                return Json(error, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DeleteFolder(FolderModel folder)
        {
            if (ModelState.IsValid)
            {
                _folderservice.DeleteFolder(folder.ToDomain());
                return Json(string.Empty, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Response.StatusCode = 400;
                var error = new ErrorModel();
                error.ErrorID = 7;
                error.Message = "Delete failed";
                return Json(error, JsonRequestBehavior.AllowGet);
            }
        }
    }
}