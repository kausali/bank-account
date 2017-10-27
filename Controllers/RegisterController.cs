using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using bank.Models;
using System.Linq;

namespace bank.Controllers
{
    public class RegisterController : Controller
    {
        private UserContext _context;
 
        public RegisterController(UserContext context)
        {
            _context = context;
        }
        //GET: HOME
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.errors = new List<string>();    
            return View();
        }
        //POST: Register User here!
        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterViewModel anything)
        {
            //if user email exist in db then throw an error asking user to login!!

            if(ModelState.IsValid)
            {
                //anything that we are sending into our db goes from here!!
                User newUser = new User
                {
                    first_name = anything.first_name,
                    last_name = anything.last_name,
                    email = anything.email,
                    password = anything.password,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now
                };
                _context.Add(newUser);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("currUserID", newUser.userID);
                return RedirectToAction("Account");                
            }
            // Handle success
            ViewBag.errors = ModelState.Values;
            return View("Index");
            
        }
        //GET: Direct user to account page!
        [HttpGet]
        [Route("loginpage")]
        public IActionResult Loginpage()
        { 
            return View("Login");
        }

        [HttpGet]
        [Route("account")]
        public IActionResult Account()
        { 
            ViewBag.error = TempData["moneyError"];
            ViewBag.ID = _context.users.SingleOrDefault(u =>u.userID == HttpContext.Session.GetInt32("currUserID"));
            ViewBag.AllTrans = _context.transactions.Where(u=> u.userID == HttpContext.Session.GetInt32("currUserID"));
            return View("Account");
        }

        [HttpPost]
            [Route("login")]
            public IActionResult Login(string email, string password)
            {
                //if user email exist in db then throw an error asking user to login!!
                User info = _context.users.Where(u => u.email == email).SingleOrDefault();
                if (info.password == password){
                HttpContext.Session.SetInt32("currUserID", info.userID);
                return RedirectToAction("Account");                
                }
                return View("Index");
                }
        [HttpPost]
        [Route("process")]
        public IActionResult Process(int amount)
        { 
            User thisUser = _context.users.SingleOrDefault(u => u.userID == HttpContext.Session.GetInt32("currUserID"));
            if (amount <0){
                if (-1 *(amount) >thisUser.balance){
                    TempData["moneyError"] = "No enough money to withdraw";
                    return RedirectToAction("Account"); 
                }
            }
            Transaction trans = new Transaction{
                amount = amount,
                created_at = DateTime.Now,
                updated_at = DateTime.Now,
                userID = (int)HttpContext.Session.GetInt32("currUserID")
            };

            
            if (amount >0){
                trans.type = "Deposit";
            }
            else{
                trans.type = "Withdraw";
            }
            _context.Add(trans);
            thisUser.balance +=amount;
            _context.SaveChanges();
            return RedirectToAction("Account");   
        }
    }
}
