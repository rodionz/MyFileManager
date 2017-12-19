using System.Web.Mvc;
using System.Linq;
using FileManager.Logic;
using FileManager.Mvc.Models;

namespace FileManager.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly FoldersService _folderservice;
   
        public HomeController() 
        {
            _folderservice = new FoldersService();
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