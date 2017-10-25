using FileManager.Logic;
using FileManager.Mvc.Models;
using System.Linq;
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
            return View();
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
                return null;
            }
        }

        [HttpPost]
        public ActionResult UpdateFolder(FolderModel folder)
        {
            if (ModelState.IsValid)
            {
                _folderservice.UpdateFolder(folder.ToDomain());
                return Json(folder, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
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
                return null;
            }
        }
    }
}