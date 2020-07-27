using Cars.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Cars.Controllers
{
    public class HomeController : Controller
    {
        private carDBContext db = new carDBContext();
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        /// USER LOGIN

        public ActionResult login()
        {
            return View();
        }

        //
        // Post: /login
        [HttpPost]
        public ActionResult login(loginvm user)
        {
            if (user.username == "admin" && user.password == "caradmin")
            {
                return RedirectToAction("index");
            }
            else
            {
                TempData["loginfailed"] = "Incorrect username or password";
                return View();
            }
        }



        /// CAR INDEX

        public ActionResult carindex()
        {
            var allcars = db.cars.OrderByDescending(c => c.carid);
            return View(allcars.ToList());
        }
        
        // Available cars
        public ActionResult availablecars()
        {
            var availablecars = db.cars.OrderByDescending(c => c.carid).Where(c => c.status == "Available");
            return View(availablecars.ToList());
        }

        // Unavailable cars
        public ActionResult unavailablecars()
        {
            var unavailablecars = db.cars.OrderByDescending(c => c.carid).Where(c => c.status == "Unavailable");
            return View(unavailablecars.ToList());
        }


        /// CREATE CAR

        public ActionResult createcar()
        {
            return View();
        }

        // Post: createcar
        [HttpPost]
        public ActionResult createcar(car car)
        {
            // displying and stating the image type
            if (ModelState.IsValid)
            {
                System.Random randomInteger = new System.Random();
                int genNumber = randomInteger.Next(1000000);
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    if (file.ContentLength > 0 && file.ContentType.ToUpper().Contains("JPEG") || file.ContentType.ToUpper().Contains("PNG") || file.ContentType.ToUpper().Contains("JPG") || file.ContentType.ToUpper().Contains("PDF"))
                    {

                        WebImage img = new WebImage(file.InputStream);
                        if (img.Width > 500)
                        {
                            img.Resize(width: 400, height: 400, preserveAspectRatio: true, preventEnlarge: true);
                            // img.Save("~/Images/Items");
                        }
                        string fileName = Path.Combine(Server.MapPath("/Images/"), Path.GetFileName(genNumber + file.FileName));
                        img.Save(fileName);
                        // file.SaveAs(fileName);
                        car.carimage = fileName;
                    }

                }

                car.status = "Available";
                db.cars.Add(car);
                db.SaveChanges();

                TempData["success"] = "New car has been added successfully";
                return RedirectToAction("carindex");
            }
            return View();
        }


        // GET: /editcar

        public ActionResult editcar(int id = 0)
        {
            car car = db.cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(car);
            }

        }

        //
        // POST: /editcar

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult editcar(car car)
        {
            if (ModelState.IsValid)
            {
                System.Random randomInteger = new System.Random();
                int genNumber = randomInteger.Next(1000000);
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    if (file.ContentLength > 0 && file.ContentType.ToUpper().Contains("JPEG") || file.ContentType.ToUpper().Contains("PNG") || file.ContentType.ToUpper().Contains("JPG") || file.ContentType.ToUpper().Contains("PDF"))
                    {

                        WebImage img = new WebImage(file.InputStream);
                        if (img.Width > 500)
                        {
                            img.Resize(width: 400, height: 400, preserveAspectRatio: true, preventEnlarge: true);
                            // img.Save("~/Images/Items");
                        }
                        string fileName = Path.Combine(Server.MapPath("/Images/"), Path.GetFileName(genNumber + file.FileName));
                        img.Save(fileName);
                        // file.SaveAs(fileName);
                        car.carimage = fileName;
                    }
                }
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("carindex");
            }
            else
            {
                return View(car);
            }
        }

        //
        // Leasecar

        public ActionResult leasecar(int id = 0)
        {
            car car = db.cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            else
            {
                car.status = "Unavailable";
                db.SaveChanges();
                return RedirectToAction("carlog", new { id = car.carid });
            }
        }

        //
        // Carlog
        public ActionResult carlog(int id)
        {
            if (id == null)
            {
                return RedirectToAction("index");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult carlog(log carlog, int id)
        {
                var car = db.cars.FirstOrDefault(s => s.carid == id);
                if (car.carid == null)
                {
                    return RedirectToAction("Availablecars");
                }
                else
                {
                    carlog.carid = car.carid;
                    carlog.carname = car.carname;

                    db.logs.Add(carlog);
                    db.SaveChanges();
                    TempData["success"] = "You have successfully leased out" + " " + car.carname + " " + "to" + " " + carlog.customername;
                    return RedirectToAction("Availablecars");
                }

            }

        //Log
        //
        public ActionResult log()
        {
            var logs = db.logs.OrderByDescending(l => l.logid);
            return View(logs.ToList());
        }


        //
        // Returncar

        public ActionResult returncar(int id = 0)
        {
            car car = db.cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            else
            {
                car.status = "Available";
                db.SaveChanges();
                return RedirectToAction("carindex");
            }
        }

        //
        // GET: /deletecar

        public ActionResult Delete(int id = 0)
        {
            car car = db.cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        //
        // POST: /deletecar

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            car car = db.cars.Find(id);
            db.cars.Remove(car);
            db.SaveChanges();
            return RedirectToAction("carindex");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


    }
}
