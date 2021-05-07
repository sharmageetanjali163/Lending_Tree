using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using System.Windows;
using System.Windows.Forms;
using LendingTree.Models;

namespace LendingTree.Controllers
{
    public class UserController : Controller
    {
        private LendingContext db = new LendingContext();

        private EncryptPassword encryptPassword = new EncryptPassword();

        //GET: Users1
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }


        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,DoB,Gender,ContactNumber,Email,UserId,Password,ConfirmPassword,Answer1,Answer2,Answer3")] User user)
        {
            if (ModelState.IsValid)          
            {
                var confrmpasskey = encryptPassword.Encode(user.ConfirmPassword);
                user.ConfirmPassword = confrmpasskey;
                var passkey = encryptPassword.Encode(user.Password);
                user.Password = passkey;
                db.Users.Add(user);
                try
                {
                    db.SaveChanges();
                    MessageBox.Show("New User Created Successfully");
                    return View(user);
                    //return RedirectToAction("UserHome", "Home");
                }
                catch (Exception e)
                {
                    ViewBag.Message = e.Message;
                    return View("Error");
                }
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult UserLogin()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserLogin([Bind(Include = "UserId,Password")] User user)
        {
            //if (ModelState.IsValid)
            //{
            if (user.UserId != null)
            {
                if (user.Password != null)
                {
                    string password = encryptPassword.Encode(user.Password);

                    if (db.Users.Any(b => b.UserId.Equals(user.UserId, StringComparison.InvariantCultureIgnoreCase) && b.Password.Equals(password, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        //ViewBag.Username = User.Identity.Name;
                        FormsAuthentication.SetAuthCookie(user.UserId, false);

                        return RedirectToAction("UserAccount", new { UserId = user.UserId });
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


        public ActionResult UserAccount(string UserId)
        {
            var entity = db.Users.Find(UserId);
            ViewBag.Gender = entity.Gender;
            ViewBag.Message = entity.FirstName + entity.LastName;
            return View();
        }

        public ActionResult UserLogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("UserLogin");
        }


        // GET: Users1/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: Users1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FirstName,LastName,Dob,Gender,ContactNumber,Email,UserId,password,Category,Q1,Q2,Q3,A1,A2,A3")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users1/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: Users1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult ForgotUserId()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotUserId(ForgotUserId ob)
        {
            string message = "";
            string Status = "false";
            if (ModelState.IsValid)
            {
                var data = db.Users.FirstOrDefault(x => x.ContactNumber == ob.ContactNumber);
                if (data != null)
                {
                    if (string.Compare(ob.Answer1, data.Answer1) == 0 && string.Compare(ob.Answer2, data.Answer2) == 0 && string.Compare(ob.Answer3, data.Answer3) == 0)
                    {
                        Status = "true";
                        message = $"User ID is {data.UserId} ";
                    }
                    else
                    {
                        message = "Wrong Answers to the Questions";
                    }
                }
                else
                {
                    message = "Wrong Contact Number";
                }
            }
            ViewBag.Status = Status;
            ViewBag.Message = message;

            return View(ob);
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPassword ob)
        {
            string message = "";

            if (ModelState.IsValid)
            {

                var data = db.Users.FirstOrDefault(x => x.UserId == ob.UserId);

                if (data != null)
                {
                    if (string.Compare(ob.Ques1, data.Answer1) == 0 && string.Compare(ob.Ques2, data.Answer2) == 0 && string.Compare(ob.Ques3, data.Answer3) == 0)
                    {
                        return RedirectToAction("ResetPassword", new { UserId = data.UserId });
                    }
                    else
                    {
                        message = "Wrong Answers to the Questions";
                    }
                }
                else
                {
                    message = "User ID does not Exist";
                }
            }
            ViewBag.Message = message;

            return View(ob);
        }

        [HttpGet]
        public ActionResult ResetPassword(string UserId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(string UserId, ResetPassword ob)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                var data = db.Users.Find(UserId);
                data.Password = encryptPassword.Encode(ob.NewPassword);
                data.ConfirmPassword = encryptPassword.Encode(ob.ConfirmPassword);
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();

                message = "Password Reset Sucessfull !!!";
            }
            ViewBag.Message = message;

            return View(ob);
        }

        public ActionResult ApplyLoan(Loan loan) {

            if (ModelState.IsValid)
            {
                db.Loans.Add(loan);
                var col1 = db.Loans.Where(w => w.PANNo.Equals(loan.PANNo));
                foreach (var item in col1)
                {
                    item.Status = "0" ;
                }


                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Loan Request Submitted Successfully");
                    return View(loan);
                    //return RedirectToAction("UserHome", "Home");
                }
                catch (Exception e)
                {
                    ViewBag.Message = e.Message;
                    return View("Error");
                }
            }
            return View(loan);
        }




    



    }
}