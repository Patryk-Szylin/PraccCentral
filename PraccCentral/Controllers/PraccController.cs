using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PraccCentral.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace PraccCentral.Controllers
{
    public class PraccController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        //private ApplicationSignInManager _signInManager;
        //private ApplicationUserManager _userManager;

        //public PraccController()
        //{

        //}

        //public PraccController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        //{
        //    UserManager = userManager;
        //    SignInManager = signInManager;
        //}

        //public ApplicationSignInManager SignInManager
        //{
        //    get
        //    {
        //        return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
        //    }
        //    private set
        //    {
        //        _signInManager = value;
        //    }
        //}

        //public ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}



        public ActionResult DisplayUser(string id)
        {
            var praccs = db.Users.FirstOrDefault(u => u.Id == id).UserPraccs;

            //var user = db.Users.FirstOrDefault(u => u.Id == id);
            return View(praccs);

        }


        public ActionResult DisplayAll()
        {
            var praccs = db.AllPraccs.ToList();
            return View(praccs);
        }

        //[HttpPost]
        //public ActionResult DisplayAll(ApplicationUser userRequest, int praccId)
        //{
        //    //var praccs = db.Praccs.ToList();
        //    var pracc = db.Praccs.FirstOrDefault(p => p.PraccId == praccId);

        //    if (pracc != null)
        //    {
        //        //pracc.Requests.Add(userRequest);
        //    }

        //    return View(pracc);
        //}

        public ActionResult DisplayForUser(string id)
        {
            //var user = db.Users.FirstOrDefault(u => u.Id == userId);
            var userpraccs = db.AllPraccs.Where(p => p.User.Id == id);


            if(userpraccs != null)
            {
                return View(userpraccs);
            }
            else
            {
                ViewBag.UsersPracc = "There's no such user";
                return View();
            }            
        }



        // GET: Pracc
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(Pracc pracc)
        {


            if (System.Web.HttpContext.Current.User.Identity.GetUserId() == null)
            {
                ViewBag.ResultInfo = "There's no user logged in";
                return View();
            }
            else
            {
                ViewBag.ResultInfo = "The user : " + System.Web.HttpContext.Current.User.Identity.GetUserId().ToString() + " is logged in, proceed to add the pracc";
                //db.Users.FirstOrDefault(u => u.Id == ).Praccs.Add(pracc);
                //var user = db.Users.First(u => u.Id == System.Web.HttpContext.Current.User.Identity.GetUserId());

                var currentuser = db.Users.AsEnumerable().First(u => u.Id == System.Web.HttpContext.Current.User.Identity.GetUserId());
                //pracc.Owner = currentuser;
                currentuser.UserPraccs.Add(pracc);

                // AllPraccs should be updated when praccs are created by users
                // The way it would be done would be to populute AllPraccs through fetching data from looping through all users
                // However this would probably result in longer request times if there is loads of praccs
                db.AllPraccs.Add(pracc);

                db.SaveChanges();

                return View();
            }

        }
    }
}