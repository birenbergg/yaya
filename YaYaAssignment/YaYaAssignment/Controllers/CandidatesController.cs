using System.Linq;
using System.Web.Mvc;

namespace YaYaAssignment.Controllers
{
    public class CandidatesController : Controller
    {
        private readonly DatabaseEntities db = new DatabaseEntities();

        public ActionResult Index()
        {
            return Json(db.Candidates.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}
