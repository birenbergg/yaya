using System.Linq;
using System.Web.Mvc;

namespace YaYaAssignment.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();

        public ActionResult Index()
        {
            return View(db.Candidates.ToList());
        }
    }
}