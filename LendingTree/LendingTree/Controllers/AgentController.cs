using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Windows;
using LendingTree.Models;

namespace LendingTree.Controllers
{
    public class AgentController : Controller
    {
        private LendingContext db = new LendingContext();
        private EncryptPassword encryptPassword = new EncryptPassword();
        //public JsonResult IsUserExists(string agentId)
        //{
        //    //check if any of the UserName matches the UserName specified in the Parameter using the ANY extension method.  
        //    return Json(!db.Agents.Any(x => x.AgentId == AgentId), JsonRequestBehavior.AllowGet);
        //}

        [Authorize(Roles = "Agent")]
        public ActionResult AgentAccount()
        {
            return View();
        }
        public ActionResult Notification()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AgentCreate()
        {

            IEnumerable<SelectListItem> list = new SelectList(db.Departments, "DepartmentId");
            ViewBag.DepartmentId = list ;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgentCreate([Bind(Include = "FirstName,LastName,DoB,Gender,ContactNumber,DepartmentId,AgentId,Password,ConfirmPassword")] Agent agent)
        {
            if (ModelState.IsValid)
            {
                if (!db.Agents.Any(x => x.AgentId == agent.AgentId))
                {
                    var passkey = encryptPassword.Encode(agent.Password);
                    agent.Password = passkey;
                    var confkey = encryptPassword.Encode(agent.ConfirmPassword);
                    agent.ConfirmPassword = confkey;
                    db.Agents.Add(agent);
                    db.SaveChanges();

                    System.Windows.Forms.MessageBox.Show("New Agent Created Successfully");

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Agent Name / ID already exists");

                    return View(agent);
                }
            }

            return View(agent);
        }

        public ActionResult AgentLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgentLogin([Bind(Include = "AgentId,Password")] Agent agent)
        {
            if (agent.AgentId != null)
            {
                if (agent.Password != null)
                {
                    string password = encryptPassword.Encode(agent.Password);

                    if (db.Agents.Any(b => b.AgentId == agent.AgentId && b.Password == agent.Password))
                    {
                        FormsAuthentication.SetAuthCookie(agent.AgentId.ToString(), false);

                        return RedirectToAction("AgentAccount", "Agent");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Agent Name / Password is Incorrect");

                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Please enter Log In credentials");

                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Please enter Log In credentials");

                return View();
            }

        }

        public ActionResult AgentLogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("AgentHome", "Home");
        }

    }
}
