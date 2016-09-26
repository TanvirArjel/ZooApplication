using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZooApp.Models;
using ZooApp.Services;

namespace ZooApp.MvcClient.Controllers
{
    public class AnimalFoodController : Controller
    {
        private ZooContext db = new ZooContext();
        AnimalFoodService animalFoodService = new AnimalFoodService();

        // GET: AnimalFood
        public ActionResult Index()
        {
            IEnumerable<AnimalFood> animalFoods = animalFoodService.GetAllAnimalFood();
            return View(animalFoods);
        }

        // GET: AnimalFood/Details/5
        public ActionResult Details(int id)
        {
            AnimalFood animalFood = animalFoodService.GetAnimalFoodById(id);
            return View(animalFood);
        }

        // GET: AnimalFood/Create
        public ActionResult Create()
        {
            AnimalDropDownList();
            FoodDropDownList();
            return View();
        }
        private void AnimalDropDownList(object selectedObject = null)
        {
            ViewBag.AnimalList = new SelectList(db.Animals.OrderBy(x => x.AnimalName), "Id", "AnimalName", selectedObject);
        }

        private void FoodDropDownList(object selectedObject = null)
        {
            ViewBag.FoodList = new SelectList(db.Foods.OrderBy(x => x.FoodName), "Id", "FoodName", selectedObject);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AnimalId,FoodId,FoodQuantity")] AnimalFood animalFood)
        {
            if (ModelState.IsValid)
            {
                animalFoodService.Save(animalFood);
                return RedirectToAction("Index");
            }

            AnimalDropDownList();
            FoodDropDownList();
            return View(animalFood);
        }

        // GET: AnimalFood/Edit/5
        public ActionResult Edit(int id)
        {
            AnimalFood animalFoodToBeEdited = animalFoodService.GetAnimalFoodById(id);
            AnimalDropDownList(animalFoodToBeEdited.AnimalId);
            FoodDropDownList(animalFoodToBeEdited.FoodId);
            return View(animalFoodToBeEdited);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AnimalId,FoodId,FoodQuantity")] AnimalFood animalFood)
        {
            if (ModelState.IsValid)
            {
                animalFoodService.Update(animalFood);
                return RedirectToAction("Index");
            }
            AnimalDropDownList(animalFood.AnimalId);
            FoodDropDownList(animalFood.FoodId);
            return View(animalFood);
        }

        // GET: AnimalFood/Delete/5
        public ActionResult Delete(int id)
        {
            AnimalFood animalFoodToBeDeleted = animalFoodService.GetAnimalFoodById(id);
            return View(animalFoodToBeDeleted);
        }

        // POST: AnimalFood/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(AnimalFood animalFood)
        {
            animalFoodService.Delete(animalFood);
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
    }
}
