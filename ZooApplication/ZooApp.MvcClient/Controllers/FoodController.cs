using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZooApp.Models;
using ZooApp.Services;
using ZooApp.ViewModels;

namespace ZooApp.MvcClient.Controllers
{
    public class FoodController : Controller
    {
        ZooContext db = new ZooContext();
        FoodService foodService = new FoodService();
        public ActionResult Index()
        {
            List<FoodViewModel> foods = foodService.GetFoods();
            return View(foods);
        }
        
        public ActionResult Details(int id)
        {
            FoodViewModel food = foodService.GetFoodById(id);
            return View(food);
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FoodViewModel food)
        {
            if (ModelState.IsValid)
            {
                foodService.Create(food);
                return RedirectToAction("Index");
            }
          return View(food);
        }

        public JsonResult IsFoodNameExist(string FoodName, int? Id)
        {
            var validateName = db.Foods.FirstOrDefault(x => x.FoodName == FoodName && x.Id != Id);
            if (validateName != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            FoodViewModel foodToBeEdited = foodService.GetFoodById(id);
            return View(foodToBeEdited);
        }

        [HttpPost]
        public ActionResult Edit(FoodViewModel food)
        {
            if (ModelState.IsValid)
            {
                foodService.Update(food);
                return RedirectToAction("Index");
            }
            return View(food);
        }
        
        [HttpGet]
        public ActionResult Delete(int id)
        {
            FoodViewModel foodToBedeleted = foodService.GetFoodById(id);
            return View(foodToBedeleted);
        }

        [HttpPost]
        public ActionResult Delete(Food food)
        {
            foodService.DeleteConfirm(food);
            return RedirectToAction("Index");
        }
    }
}