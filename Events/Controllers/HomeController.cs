using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Events.Models;
namespace Events.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetEvents()
        {
            using (MyDataEntities dc = new MyDataEntities())
            {
                var events = dc.Events.OrderBy(a => a.EventName).ToList();

                return Json(new { data = events }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Save(int id)
        {
            using (MyDataEntities dc = new MyDataEntities())
            {
                var v = dc.Events.Where(a => a.EventId == id).FirstOrDefault();
                return View(v);
            }
        }
        [HttpPost]
        public ActionResult Save(Event ev)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (MyDataEntities dc = new MyDataEntities())
                {
                    if (ev.EventId>0)
                    {
                        var v = dc.Events.Where(a => a.EventId == ev.EventId).FirstOrDefault();
                        if (v != null)
                        {
                            v.EventName = ev.EventName;
                            v.EventLocation = ev.EventLocation;
                            v.EventStartDate = ev.EventStartDate;
                            v.EventEndDate = ev.EventEndDate;
                        }
                    }
                    else
                    {
                        dc.Events.Add(ev);
                    }
                    dc.SaveChanges();
                    status = true;
                }

            }
            return new JsonResult { Data = new { status = status } };
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (MyDataEntities dc = new MyDataEntities())
            {
                var v = dc.Events.Where(a => a.EventId == id).FirstOrDefault();
                if (v!=null)
                {
                    return View(v);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteEvent(int id)
        {
            bool status = false;
            using (MyDataEntities dc = new MyDataEntities())
            {
                var v = dc.Events.Where(a => a.EventId == id).FirstOrDefault();
                if (v!=null)
                {
                    dc.Events.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}