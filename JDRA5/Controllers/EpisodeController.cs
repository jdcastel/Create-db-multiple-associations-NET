using JDRA5.Controllers;
using JDRA5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JDRA5.Controllers
{
    [Authorize(Roles = "Coordinator,Clerk,Executive,Admin")]
    public class EpisodeController : Controller
    {
        private Manager manager = new Manager();

        // GET: Episode
        public ActionResult Index()
        {
            return View(manager.EpisodesGetAll());
        }

        // GET: Episode/Details/5
        public ActionResult Details(int? id)
        {
            var episode = manager.EpisodeGetById(id.GetValueOrDefault());

            if (episode == null)
                return HttpNotFound();
            else
                return View(episode);
        }

        // GET: Episode/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Episode/Create
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
        [Authorize(Roles = "Clerk")]
        [Route("Shows/{id}/AddEpisode")]
        // GET: Show/Create
        public ActionResult AddEpisode(int? id)
        {
            var show = manager.ShowGetById(id.GetValueOrDefault());

            if (show == null)
            {
                return HttpNotFound();
            }

            var addEpisodeViewModel = new EpisodeAddFormViewModel
            {
                ShowId = show.Id,
                ShowName = show.Name
            };

            return View(addEpisodeViewModel);
        }

        [Authorize(Roles = "Clerk")]
        [Route("Shows/{id}/AddEpisode")]
        // POST: Show/Create
        [HttpPost]
        public ActionResult AddEpisode(EpisodeAddViewModel newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            newItem.Clerk = User.Identity.Name;

            var addedItem = manager.EpisodeAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("details", new { id = addedItem.Id });
            }
        }
        //// GET: Episode/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Episode/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Episode/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Episode/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
