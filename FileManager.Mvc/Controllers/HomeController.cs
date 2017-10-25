using System.Web.Mvc;
using System.Linq;
using FileManager.Logic;
using FileManager.Mvc.Models;

namespace FileManager.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly FoldersService _folderservice;
        private readonly FilesService _fileservice;

        public HomeController() 
        {
            _folderservice = new FoldersService();
            _fileservice = new FilesService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllFolders()
        {
            var folders = _folderservice.GetAllFolders().Select(f => new FolderModel(f));

            return Json(folders, JsonRequestBehavior.AllowGet);
        }

      
    }
}