using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using ZooApp.Models;
using ZooApp.ViewModels;
using ZooApp.Services;

namespace ZooApp.MvcClient.Controllers
{
    public class FoodSummaryController : Controller
    {
        AnimalFoodService animalFoodService = new AnimalFoodService();
        public ActionResult Index(int? id)
        {
            var summaryData = new SummaryData();
            summaryData.FoodSummaryViewModels = animalFoodService.GetFoodSummary();
            ViewBag.TotalCost1 = summaryData.FoodSummaryViewModels.Sum(x => x.TotalPrice);

            if (id != null)
            {
                summaryData.FoodSummayByAnimalViewModels = animalFoodService.GetFoodSummaryByAnimal(id);
                ViewBag.TotalCost2 = summaryData.FoodSummayByAnimalViewModels.Sum(x => x.TotalPrice);
                ViewBag.FoodId = id;
            }

            return View(summaryData);
        }      
    }
}