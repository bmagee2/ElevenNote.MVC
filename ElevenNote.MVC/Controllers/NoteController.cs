using ElevenNote.Data;
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

        private readonly ApplicationDbContext _db = new ApplicationDbContext();  // ADDED

        // GET: Note/Index
        public ActionResult Index()
        {
            //var userId = Guid.Parse(User.Identity.GetUserId());
            //var service = new NoteService(userId);
            var service = CreateNoteService();
            var model = service.GetNotes();
            

            return View(model);

            //var model = new NoteListItem[0];    // initializing a new instance of the NoteListItem as an IEnumerable with the [0] syntax
            //return View(model);
        }

        // GET -- making a request to get the Create View
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
            if (!ModelState.IsValid) return View(model);

            var service = CreateNoteService();

            if (service.CreateNote(model))
            {
                TempData["SaveResult"] = "Your note was created.";  // TempData removes information after it's accessed
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Note could not be created."); // ERROR MESSAGE

            return View(model);
        }

        //public ActionResult Details(int id)
        //{
        //    var svc = CreateNoteService();
        //    var model = svc.GetNoteById(id);

        //    return View(model);
        //}

        public ActionResult ModalDetails(int id)
        {
            var svc = CreateNoteService();
            var model = svc.GetNoteById(id);
            return PartialView("_Details", model);
        }

        //public ActionResult Details(int Id)
        //{
        //    NoteDetail note = new NoteDetail();
        //    note = _db.NoteDetail.
        //    return PartialView("_Details", note);
        //}

        public ActionResult Edit(int id)
        {
            var service = CreateNoteService();
            var detail = service.GetNoteById(id);
            var model =
                new NoteEdit
                {
                    NoteId = detail.NoteId,
                    Title = detail.Title,
                    Content = detail.Content
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, NoteEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.NoteId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateNoteService();

            if (service.UpdateNote(model))
            {
                TempData["SaveResult"] = "Your note was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your note could not be updated.");
            return View(model);
        }


        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateNoteService();
            var model = svc.GetNoteById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateNoteService();

            service.DeleteNote(id);

            TempData["SaveResult"] = "Your note was deleted";

            return RedirectToAction("Index");
        }

        // EXTRACTED METHOD
        private NoteService CreateNoteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
            return service;
        }

        public ActionResult ModalPopUp()
        {
            return View();
        }

       
    }
}