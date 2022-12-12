using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Strongly_Example.Models;


namespace Strongly_Example.Controllers
{
    public class HomeController : Controller
    {
        EF_DBEntities db = new EF_DBEntities();
        // GET: Home
        public ActionResult Index()
        {


            return View(db.People.Select(p => new PersonViewModel()
            {
                ID = p.ID,
                Name = p.Name,
                Email = p.Email,
                Website = p.Website,
                CarName = p.PersonCar.CarName,
                CarPlak = p.PersonCar.CarPlak
            }));

        }
        public ActionResult Detail(int id)
        {

            return View(db.People.Where(p=> p.ID==id).Select(p=> new PersonViewModel() {
                ID = p.ID,
                Name = p.Name,
                Email = p.Email,
                Website = p.Website,
                CarName = p.PersonCar.CarName,
                CarPlak = p.PersonCar.CarPlak

            }).First());
        }
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(PersonViewModel person)
        {
            People p = new People()
            {
                Name = person.Name,
                Email = person.Email,
                Website = person.Website,
            };
            db.People.Add(p);
            PersonCar pc = new PersonCar() { 
                PersonID=p.ID,
                CarName=person.CarName,
                CarPlak=person.CarPlak
            };
            db.PersonCar.Add(pc);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {

            return View(db.People.Where(p => p.ID == id).Select(p => new PersonViewModel()
            {
                ID = p.ID,
                Name = p.Name,
                Email = p.Email,
                Website = p.Website,
                CarName = p.PersonCar.CarName,
                CarPlak = p.PersonCar.CarPlak

            }).First());
        }
        [HttpPost]
        public ActionResult Edit(PersonViewModel person)
        {
            var p = db.People.Find(person.ID);
            p.Name = person.Name;
            p.Email = person.Email;
            p.Website = person.Website;
            var car = db.PersonCar.Find(person.ID);
            car.CarName = person.CarName;
            car.CarPlak = person.CarPlak;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var p = db.People.Find(id);
            var car = db.PersonCar.Find(id);
            db.PersonCar.Remove(car);
            db.People.Remove(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}