using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using System.Web.Mvc;
using ZooApp.Models;
using ZooApp.ViewModels;
using PagedList;
using PagedList.Mvc;

namespace ZooApp.Services
{
    public class AnimalService
    {
        ZooContext db = new ZooContext();
        public IEnumerable<AnimalViewModel> GetAnimals(string searchString, string sortOrder, int? page)
        {
            IQueryable<Animal> animals = db.Animals;

            if (!String.IsNullOrEmpty(searchString))
            {
                animals = animals.Where(x => x.AnimalName.Contains(searchString) || x.Origin.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Name_Desc":
                    animals = animals.OrderByDescending(x => x.AnimalName);
                    break;
                case "Origin_Asc":
                    animals = animals.OrderBy(x => x.Origin);
                    break;
                case "Origin_Desc":
                    animals = animals.OrderByDescending(x => x.Origin);
                    break;
                case "Quantity_Asc":
                    animals = animals.OrderBy(x => x.AnimalQuantity);
                    break;
                case "Quantity_Desc":
                    animals = animals.OrderByDescending(x => x.AnimalQuantity);
                    break;

                default:
                    animals = animals.OrderBy(x => x.AnimalName);
                    break;
            }

            IQueryable<AnimalViewModel> viewAnimals = animals.Select(animal => new AnimalViewModel()
            {
                Id = animal.Id,
                AnimalName = animal.AnimalName,
                Origin = animal.Origin,
                AnimalQuantity = animal.AnimalQuantity
            });

            return viewAnimals.ToList().ToPagedList(page ?? 1,6);
        }

        public AnimalViewModel GetAnimalById(int id)
        {
           Animal animal = db.Animals.Find(id);
           return new AnimalViewModel(animal);
        }

        public void Save(AnimalViewModel animal)
        {
            Animal animalToBeCreated = new Animal()
            {
                Id = animal.Id,
                AnimalName = animal.AnimalName,
                Origin = animal.Origin,
                AnimalQuantity = animal.AnimalQuantity

            };
            db.Animals.Add(animalToBeCreated);
            db.SaveChanges();
        }

        public void Update(AnimalViewModel animal)
        {
            Animal animalToBeUpdated = new Animal()
            {
                Id = animal.Id,
                AnimalName = animal.AnimalName,
                Origin = animal.Origin,
                AnimalQuantity = animal.AnimalQuantity

            };
            db.Entry(animalToBeUpdated).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Animal animal)
        {
           Animal deleteAnimal=  db.Animals.Find(animal.Id);
            db.Animals.Remove(deleteAnimal);
            db.SaveChanges();
        }

    }
}
