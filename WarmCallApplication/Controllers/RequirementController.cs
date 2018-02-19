using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WarmCallApplication.Models;
using AutoMapper;

namespace WarmCallApplication.Controllers
{
    public class RequirementController : Controller
    {
        ApplicationDbContext dbContext = ApplicationDbContext.Create();
        // GET: Requirement
        public ActionResult Index()
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = dbContext.Users.FirstOrDefault(x => x.Id == currentUserId);

            List<RequirementViewModel> MyRequirements = Mapper.Map<List<RequirementViewModel>>(currentUser.MyRequirements);
            return View(MyRequirements);
        }

        // GET: Requirement/Details/5
        public ActionResult Details(int id)
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = dbContext.Users.FirstOrDefault(x => x.Id == currentUserId);
            Requirement req = currentUser.MyRequirements.FirstOrDefault(x => x.Id == id);

            RequirementViewModel MyRequirement = Mapper.Map<RequirementViewModel>(req);
            return View(MyRequirement);
        }

        // GET: Requirement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Requirement/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = dbContext.Users.FirstOrDefault(x => x.Id == currentUserId);

                currentUser.MyRequirements.Add(new Requirement()
                {
                    Title = collection["Title"],
                    Description = collection["Description"],
                    Cost = Convert.ToInt32(collection["Cost"]),
                    CallPrice = Convert.ToInt32(collection["CallPrice"]),
                    Status = (collection["Status"] == "true")
                });
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Requirement/Edit/5
        public ActionResult Edit(int id)
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = dbContext.Users.FirstOrDefault(x => x.Id == currentUserId);
            Requirement req = currentUser.MyRequirements.FirstOrDefault(x => x.Id == id);

            RequirementViewModel MyRequirement = Mapper.Map<RequirementViewModel>(req);
            return View(MyRequirement);
        }

        // POST: Requirement/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = dbContext.Users.FirstOrDefault(x => x.Id == currentUserId);
                Requirement req = currentUser.MyRequirements.FirstOrDefault(x => x.Id == id);
 
                req.Id = id;
                req.Title = collection["Title"];
                req.Description = collection["Description"];
                req.Cost = Convert.ToInt32(collection["Cost"]);
                req.CallPrice = Convert.ToInt32(collection["CallPrice"]);
                req.Status = (collection["Status"] == "true");
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Requirement/Delete/5
        public ActionResult Delete(int id)
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = dbContext.Users.FirstOrDefault(x => x.Id == currentUserId);
            Requirement req = currentUser.MyRequirements.FirstOrDefault(x => x.Id == id);

            RequirementViewModel MyRequirement = Mapper.Map<RequirementViewModel>(req);
            return View(req);
        }

        // POST: Requirement/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = dbContext.Users.FirstOrDefault(x => x.Id == currentUserId);
                Requirement req = currentUser.MyRequirements.FirstOrDefault(x => x.Id == id);

                currentUser.MyRequirements.Remove(req);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
