using JDRA5.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JDRA5.Controllers
{
    [Authorize(Roles = "Coordinator,Clerk,Executive,Admin")]
    public class ShowController : Controller
    {
        private Manager manager = new Manager();

        // GET: Show
        public ActionResult Index()
        {
            return View(manager.ShowGetAll());

        }

        // GET: Show/Details/5
        public ActionResult Details(int? id)
        {
            var show = manager.ShowGetById(id.GetValueOrDefault());

            if (show == null)
                return HttpNotFound();
            else
                return View(show);
        }

        // GET: Show/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Show/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Show/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Show/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Show/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Show/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
