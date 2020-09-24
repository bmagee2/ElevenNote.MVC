using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.MVC.Controllers
{
    // annotation makes it so that the views are accessible only to logged in users
    [Authorize]
    public class NoteController : Controller    // controller name will be the first part of our path --- localhost:xxxxx/Note
    {
        // GET: Note/Index
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
            var model = service.GetNotes();

            return View(model);

            //var model = new NoteListItem[0];    // initializing a new instance of the NoteListItem as an IEnumerable with the [0] syntax
            //return View(model);
        }

        // GET
        public ActionResult Create()
        {
            return View();
        }

        // POST
        // method that will eventually push the data inputted in the view through our service and into the database
        // method makes sure the model is valid, grabs the current userId, calls on CreateNote, and returns the user back to the index view
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);

            service.CreateNote(model);

            return RedirectToAction("Index");
        }
    }
}