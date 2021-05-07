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
        public ActionResult Account(Agent agent)
        {
            if (agent.DepartmentId == 1)
            {
                return View("Admin");
            }
            else if (agent.DepartmentId == 2)
            {
                return View("PhysicalVerification");
            }
            else if (agent.DepartmentId == 3)
            {
                return View("ApprovalAgency");
            }
            else if (agent.DepartmentId == 4)
            {
                return View("PickUp");
            }
            else
            {
                return View("LegalDept");
            }
        }

        public ActionResult Notification()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {

            /*List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Action", Value = "0" });

            items.Add(new SelectListItem { Text = "Drama", Value = "1" });

            items.Add(new SelectListItem { Text = "Comedy", Value = "2" });

            items.Add(new SelectListItem { Text = "Science Fiction", Value = "3" });*/

            Agent agent = new Agent
            {
                DepartmentList = new SelectList(db.Departments, "DepartmentId", "DepartmentName")
            };

            return View(agent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName, LastName, DoB, Gender, ContactNumber, DepartmentId, AgentId, Password, ConfirmPassword")] Agent agent)
        {
            if (ModelState.IsValid)
            {
                if (!db.Agents.Any(x => x.AgentId == agent.AgentId))
                {
                    //var passkey = encryptPassword.Encode(agent.Password);
                    //agent.Password = passkey;
                    //db.Agents.Add(agent);
                    //db.SaveChanges();

                    var confrmpasskey = encryptPassword.Encode(agent.ConfirmPassword);
                    agent.ConfirmPassword = confrmpasskey;

                    var passkey = encryptPassword.Encode(agent.Password);
                    agent.Password = passkey;

                    db.Agents.Add(agent);

                    try
                    {
                        db.SaveChanges();

                        System.Windows.Forms.MessageBox.Show("New Agent Created Successfully");

                        return View(agent);
                        //return RedirectToAction("UserHome", "Home");
                    }
                    catch (Exception e)
                    {
                        ViewBag.Message = e.Message;

                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Agent ID already exists");

                    return View(agent);
                }
            }

            return View(agent);
        }

        public ActionResult Login()
        {
            return View();
        }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "AgentId, Password")] Agent agent)
        {
            if (agent.AgentId != null)
            {
                if (agent.Password != null)
                {
                    string password = encryptPassword.Encode(agent.Password);

                    if (db.Agents.Any(b => b.AgentId == agent.AgentId && b.Password == password))
                    {
                        FormsAuthentication.SetAuthCookie(agent.AgentId.ToString(), false);

                        return RedirectToAction("AgentAccount", agent);
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

        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "AgentId, Password")] Agent agent)
        {
            //if (ModelState.IsValid)
            //{
            if (agent.AgentId != null)
            {
                if (agent.Password != null)
                {
                    string password = encryptPassword.Encode(agent.Password);

                    if (db.Agents.Any(b => b.AgentId.Equals(agent.AgentId, StringComparison.InvariantCultureIgnoreCase) && b.Password.Equals(password, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        //ViewBag.Username = User.Identity.Name;
                        FormsAuthentication.SetAuthCookie(agent.AgentId, false);

                        return RedirectToAction("UserAccount", new { UserId = agent.AgentId });
                    }
                    else
                    {
                        ModelState.AddModelError("", "User Name / Password is Incorrect");

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

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("AgentHome", "Home");
        }

    }
}
