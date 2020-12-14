﻿using Meetup.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Meetup.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _db;

        public HomeController(ILogger<HomeController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        [Route("participants", Name = "index")]
        public IActionResult Index()
        {
            return View(_db.Participants.ToList());
        }
        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            //Participant p = new Participant() { Name = "Rohan Joshi", Age = 34, DoB = DateTime.Parse("Jan 1, 1986"), Address = "Steet 3, Worli,\nMumbai", Locality = "South Mumbai", NumberOfGuests = 1, Profession = Profession.Employed };
            //_db.Participants.Add(p);
            //_db.SaveChanges();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("participants", Name ="create")]
        public IActionResult Create( Participant participant)
        {
            if (ModelState.IsValid)
            {
                _db.Participants.Add(participant);
                _db.SaveChanges();
                //return RedirectToAction(nameof(Index));
                return RedirectToRoute("index");
            }
            return View(participant);
        }

        [HttpGet]
        [Route("Edit")]
        public IActionResult Edit(int id)
        {
            //Participant p = new Participant() { Name = "Rohan Joshi", Age = 34, DoB = DateTime.Parse("Jan 1, 1986"), Address = "Steet 3, Worli,\nMumbai", Locality = "South Mumbai", NumberOfGuests = 1, Profession = Profession.Employed };
            //_db.Participants.Add(p);
            //_db.SaveChanges();
            Participant p = _db.Participants.FirstOrDefault(x => x.ParticipantId == id);
            return View(p);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public IActionResult Edit(Participant participant)
        {
            if (ModelState.IsValid)
            {
                var p = _db.Participants.FirstOrDefault(x=>x.ParticipantId == participant.ParticipantId);
                p.UpdateProperties(participant);
                _db.SaveChanges();
                return RedirectToRoute("index");
            }
            return View(participant);
        }
        [HttpPut]
        [Route("participants")]
        public IActionResult Update(Participant participant)
        {
            if (ModelState.IsValid)
            {
                var p = _db.Participants.FirstOrDefault(x => x.ParticipantId == participant.ParticipantId);
                p.UpdateProperties(participant);
                _db.SaveChanges();
                return RedirectToRoute("index");
            }
            return View(participant);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
