using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace YaYaAssignment.Controllers
{
    public class VotesController : Controller
    {
        private readonly DatabaseEntities db = new DatabaseEntities();

        [HttpPost]
        public ActionResult Create(List<int> candidateIds)
        {
            if (candidateIds == null || candidateIds.Count == 0)
            {
                return Json(HttpStatusCode.BadRequest);
            }

            foreach (var candidateId in candidateIds)
            {
                db.Votes.Add(new Vote { CandidateId = candidateId, UserId = 1 });
            }

            db.SaveChanges();
            return Json(HttpStatusCode.OK);
        }
    }
}
