using Meetup.Controllers;
using Meetup.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Meetup.Tests
{
    public class MeetupApiTests
    {
        //DbContextOptionsBuilder<AppDbContext> _builder;
        AppDbContext _db;
        public MeetupApiTests()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            _db = new AppDbContext(builder.Options);
            _db.Participants.Add(new Participant { ParticipantId = 1, Name = "Vishwas Kumar", Age = 25, DoB = DateTime.Parse("Jan 21, 1995"), Locality = "Suburbs", Address = "Andheri, Mumbai 400061", NumberOfGuests = 2, Profession = Profession.Employed });
            _db.SaveChanges();
        }
        [Fact]
        public void ShouldBeAbleToRegisterParticipant()
        {
            int countBeforeAddingParticipant = _db.Participants.Count();
            var controller = new HomeController(null, _db);
            var result = controller.Create(new Participant { ParticipantId = 2, Name = "Rahul Kedia", Age = 24, DoB = DateTime.Parse("Feb 21, 1996"), Locality = "Town", Address = "Colaba, Mumbai 400001", NumberOfGuests = 1, Profession = Profession.Student });
            Assert.Equal(countBeforeAddingParticipant + 1, _db.Participants.Count());

        }
        [Fact]
        public void ShouldBeAbleToEditParticipant()
        {
            var editedParticipant = _db.Participants.FirstOrDefault(x => x.ParticipantId == 1);
            editedParticipant.NumberOfGuests = (editedParticipant.NumberOfGuests + 1) % 3;
            editedParticipant.Profession = (editedParticipant.Profession == Profession.Student) ? Profession.Employed : Profession.Student;
            var controller = new HomeController(null, _db);
            controller.Update(editedParticipant);
            var participant = _db.Participants.FirstOrDefault(x => x.ParticipantId == editedParticipant.ParticipantId);
            Assert.Equal(participant.NumberOfGuests, editedParticipant.NumberOfGuests);
            Assert.Equal(participant.Profession, editedParticipant.Profession);
        }
        [Fact]
        public void ShouldBeAbleToListAllParticipant()
        {
            var controller = new HomeController(null, _db);
            var result = controller.Index();
            Assert.True((result as ViewResult).Model.GetType() == typeof(List<Participant>));
        }
    }
}
