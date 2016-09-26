﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZooApp.Models;
using ZooApp.Services;
using ZooApp.ViewModels;

namespace ZooApp.MvcClient.Controllers
{
    public class AnimalController : Controller
    {
        ZooContext db = new ZooContext();
        AnimalService animalService = new AnimalService();
        public ActionResult Index(string sortOrder, string searchString, int? page) 
        {
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "Name_Desc" : "";
            ViewBag.OriginSortParam = sortOrder == "Origin_Asc" ? "Origin_Desc" : "Origin_Asc";
            ViewBag.QuantitySortParam = sortOrder == "Quantity_Asc" ? "Quantity_Desc" : "Quantity_Asc";

            IEnumerable<AnimalViewModel> animals = animalService.GetAnimals(searchString, sortOrder, page);
      
            return View(animals);
        }
        public ActionResult Details(int id)
        {
            AnimalViewModel animal = animalService.GetAnimalById(id);
            return View(animal);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(AnimalViewModel animal)
        {
            if (ModelState.IsValid)
            {
                animalService.Save(animal);
                return RedirectToAction("Index");
            }

            return View(animal);
        }

       

        [HttpGet]
        public ActionResult Edit(int id)
        {
            AnimalViewModel animalToBeEdited = animalService.GetAnimalById(id);
            return View(animalToBeEdited);
        }


        [HttpPost]
        public ActionResult Edit(AnimalViewModel animal)
        {
            if (ModelState.IsValid)
            {
                animalService.Update(animal);
                return RedirectToAction("Index");
            }
            return View(animal);
                     
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            AnimalViewModel animalToBeDeleted = animalService.GetAnimalById(id);
            return View(animalToBeDeleted);
        }
        [HttpPost]
        public ActionResult Delete(Animal animal)
        {
            animalService.Delete(animal);
            return RedirectToAction("Index");
        }
    }
}