using System;
using System.Net;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace YaYaAssignment.Controllers
{
    public class UsersController : Controller
    {
        private readonly DatabaseEntities db = new DatabaseEntities();
        private readonly Random random = new Random();
        private Dictionary<string, string> result = null;

        [HttpPost]
        public ActionResult CheckTel(string tel)
        {
            result = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(tel))
            {
                result.Add("statusCode", "400");
                return Json(result);
            }

            User user = db.Users.FirstOrDefault(u => u.Phone == tel);

            if (user == null)
            {
                result.Add("statusCode", "401");
                return Json(result);
            }

            string verificationCode = random.Next(0, 1000000).ToString("000000");

            user.VerificationCode = verificationCode;
            db.SaveChanges();

            result.Add("statusCode", "200");
            result.Add("userName", user.Name);
            result.Add("userId", user.Id.ToString());
            result.Add("verificationCode", verificationCode);

            return Json(result);
        }

        [HttpPost]
        public ActionResult CheckVerificationCode(string userId, string verificationCode)
        {
            result = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(verificationCode))
            {
                result.Add("statusCode", "400");
                return Json(result);
            }

            var id = int.Parse(userId);

            User user = db.Users.FirstOrDefault(u => u.Id == id && u.VerificationCode == verificationCode);

            if (user == null)
            {
                result.Add("statusCode", "401");
                return Json(result);
            }

            result.Add("statusCode", "200");

            return Json(result);
        }
    }
}
