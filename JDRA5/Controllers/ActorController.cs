
using Antlr.Runtime.Misc;
using JDRA5.Controllers;
using JDRA5.Data;
using JDRA5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JDRA5.Controllers
{
    [Authorize(Roles = "Coordinator,Clerk,Executive,Admin")]
    public class ActorController : Controller
    {
        private Manager manager = new Manager();
        // GET: Actor
        public ActionResult Index()
        {
            return View(manager.ActorsGetAll());
        }

        // GET: Actor/Details/5
        public ActionResult Details(int? id)
        {
            var actor = manager.ActorGetById(id.GetValueOrDefault());

            if (actor == null)
                return HttpNotFound();
            else
                return View(actor);
        }

        // GET: Actor/Create
        [Authorize(Roles = "Executive")]
        public ActionResult Create()
        {
            var form = new ActorAddFormViewModel();

            return View(form);
        }

        // POST: Actor/Create
        [Authorize(Roles = "Executive")]
        [HttpPost]
        public ActionResult Create(ActorAddViewModel newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = manager.ActorAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("details", new { id = addedItem.Id });
            }
        }

        [Authorize(Roles = "Coordinator")]
        [Route("Actor/{id}/AddShow")]
        // GET: Show/Create
        public ActionResult AddShow(int? id)
        {
            // Attempt to get the associated object
            var actorId = manager.ActorGetById(id.GetValueOrDefault());

            if (actorId == null)
            {
                return HttpNotFound();
            }
            else
            {
                var actorSelected = new List<int> { actorId.Id };

                var addShowViewModel = new ShowAddFormViewModel
                {
                    GenreList = new SelectList(manager.GenreGetAll(), "Id", "Name"),
                    ActorsSelectionList = new MultiSelectList(manager.ActorsGetAll(), "Id", "Name", actorSelected),
                    ShowId = actorId.Id,
                    ShowName = actorId.Name
                };

                //addVehicleWithId.ShowId = actorId.Id;
                //addVehicleWithId.ShowName = actorId.Name;

                return View(addShowViewModel);
            }
        }

        [Authorize(Roles = "Coordinator")]
        [Route("Actor/{id}/AddShow")]
        // POST: Show/Create
        [HttpPost]
        public ActionResult AddShow(ShowAddViewModel newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }
            //var actorId = manager.ActorGetById(id.GetValueOrDefault());

            // Process the input
            var addedItem = manager.ShowAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {

                return RedirectToAction("details", "show", new { id = addedItem.Id });
            }
        }


        //    // GET: Actor/Edit/5
        //    public ActionResult Edit(int id)
        //    {
        //        return View();
        //    }

        //    // POST: Actor/Edit/5
        //    [HttpPost]
        //    public ActionResult Edit(int id, FormCollection collection)
        //    {
        //        try
        //        {
        //            // TODO: Add update logic here

        //            return RedirectToAction("Index");
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }

        //    // GET: Actor/Delete/5
        //    public ActionResult Delete(int id)
        //    {
        //        return View();
        //    }

        //    // POST: Actor/Delete/5
        //    [HttpPost]
        //    public ActionResult Delete(int id, FormCollection collection)
        //    {
        //        try
        //        {
        //            // TODO: Add delete logic here

        //            return RedirectToAction("Index");
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }
    }
}
