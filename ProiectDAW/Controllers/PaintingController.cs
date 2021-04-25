using ProiectDAW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace ProiectDAW.Controllers
{
    public class PaintingController : Controller
    {   
        private DbCtx db = new DbCtx();
        
        [HttpGet]
        public ActionResult Index()
        {
            List<Painting> paintings = db.Paintings;
                //.Include("Curator").ToList();
            ViewBag.Paintings = paintings;
            return View();
        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Painting painting = db.Paintings.Find(id);
                if (painting != null)
                {
                    return View(painting);
                }
                return HttpNotFound("Couldn't find the painting with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing painting id parameter!");
        }
        [HttpGet] 
        public ActionResult New()
        {
            Painting painting = new Painting();
            painting.CuratorsList = GetAllCurators();
            painting.Techniques = new List<Technique>();
            return View(painting);
        }
        [HttpPost]
        public ActionResult New(Painting paintingRequest)
        {
            try
            {
                paintingRequest.CuratorsList = GetAllCurators();
                if (ModelState.IsValid)
                {
                    paintingRequest.Curator = db.Curators.FirstOrDefault(p => p.CuratorId.Equals(1));
                    db.Paintings.Add(paintingRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(paintingRequest);
            }
            catch (Exception e)
            {
                return View(paintingRequest);
            }
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Painting painting = db.Paintings.Find(id);
                if (painting == null)
                {
                    return HttpNotFound("Couldn't find the painting with id " + id.ToString());
                }
                painting.CuratorsList = GetAllCurators();
                return View(painting);
            }
            return HttpNotFound("Missing painting id parameter!");
        }
        [HttpPut]
        public ActionResult Edit(int id, Painting paintingRequest)
        {
            try
            {
                paintingRequest.CuratorsList = GetAllCurators();
                if (ModelState.IsValid)
                {
                    Painting painting = db.Paintings
                   //.Include("Curator")
                    .SingleOrDefault(b => b.PaintingId.Equals(id));
                    if (TryUpdateModel(painting))
                    {
                        painting.Title = paintingRequest.Title;
                        painting.Artist = paintingRequest.Artist;
                        painting.Description = paintingRequest.Description;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(paintingRequest);
            }
            catch (Exception e)
            {
                return View(paintingRequest);
            }
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Painting painting = db.Paintings.Find(id);
            if (painting != null)
            {
                db.Paintings.Remove(painting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Couldn't find the painting with id " + id.ToString());
        }
        [NonAction] 
        public IEnumerable<SelectListItem> GetAllCurators()
        {
            var selectList = new List<SelectListItem>();
            foreach (var curator in db.Curators.ToList())
            {
                // adaugam in lista elementele necesare pt dropdown
                selectList.Add(new SelectListItem
                {
                    Value = curator.CuratorId.ToString(),
                    Text = curator.Name
                });
            }
            // returnam lista pentru dropdown
            return selectList;
        }
    }
    
    
}