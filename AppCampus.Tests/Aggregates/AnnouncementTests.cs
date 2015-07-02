using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.Enums;
using AppCampus.Domain.Models.ValueObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCampus.Tests.Aggregates
{
    [TestFixture]
    [Category("Announcement")]
    class AnnouncementTests
    {
        public Announcement make_Announcement(string message, Severity severity, DateTime startDate, DateTime endDate)
        {
            return new Announcement(Guid.NewGuid(), message, "Untitled", severity, startDate, endDate);
        }

        [TestCase("message", Severity.General)]
        public void Create_ValidAnnouncement_NewAnnouncement(string message, Severity severity)
        {
            DateTime startDate = new DateTime(2014, 1, 1);
            DateTime endDate = new DateTime(2014, 1, 2);

            var announcement = make_Announcement(message, severity, startDate, endDate);

            Assert.AreEqual(announcement.Message, message);
            Assert.AreEqual(announcement.Severity, severity);
            Assert.AreEqual(announcement.StartDate, startDate);
            Assert.AreEqual(announcement.EndDate, endDate);
        }

        [TestCase(null, Severity.General)]
        [TestCase("", Severity.General)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_InvalidAnnouncement_ThrowArgumentNullException(string message, Severity severity)
        {
            DateTime startDate = new DateTime(2014, 1, 1);
            DateTime endDate = new DateTime(2014, 1, 2);
            make_Announcement(message, severity, startDate, endDate);
        }

        [TestCase("message", Severity.General)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Create_InvalidAnnouncement_StartDateAfterEndDate_ThrowArgumentOutOfRangeException(string message, Severity severity)
        {
            DateTime startDate = new DateTime(2014, 5, 5);
            DateTime endDate = new DateTime(2014, 1, 2);

            make_Announcement(message, severity, startDate, endDate);
        }

        [TestCase("message", Severity.General)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Create_InvalidAnnouncement_StartDateEqualsEndDate_ThrowArgumentOutOfRangeException(string message, Severity severity)
        {
            DateTime date = new DateTime(2014, 5, 5);

            make_Announcement(message, severity, date, date);
        }

        public void AssignSignboard_ValidSignboard_AddSignboard() 
        {
            Signboard signboard = new Signboard("test", Guid.NewGuid(), Guid.NewGuid(), "0.0.0", 1);

            Announcement announcement = make_Announcement("test", Severity.General, new DateTime(2014, 1, 1), new DateTime(2014, 2, 1));
            announcement.AssignToSignboard(signboard);

            Assert.AreEqual(announcement.SignboardIds.Count(), 1);
        }

        [TestCase]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AssignSignboard_InvalidSignboard_AddSignboard()
        {
            Signboard signboard = new Signboard("test", Guid.NewGuid(), Guid.NewGuid(), "0.0.0", 1);

            Announcement announcement = make_Announcement("test", Severity.General, new DateTime(2014, 1, 1), new DateTime(2014, 2, 1));
            announcement.AssignToSignboard(signboard);
            announcement.AssignToSignboard(signboard);
        }

        [TestCase]
        public void UnassignSignboard_UnassignValidSignboard_RemoveSignboard()
        {
            Signboard signboard = new Signboard("test", Guid.NewGuid(), Guid.NewGuid(), "0.0.0", 1);

            Announcement announcement = make_Announcement("test", Severity.General, new DateTime(2014, 1, 1), new DateTime(2014, 2, 1));
            announcement.AssignToSignboard(signboard);
            announcement.UnassignSignboard(signboard);

            Assert.AreEqual(announcement.SignboardIds.Count(), 0);
        }

        [TestCase]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UnassignSignboard_UnassignInvalidSignboard_RemoveSignboard()
        {
            Signboard signboard = new Signboard("test", Guid.NewGuid(), Guid.NewGuid(), "0.0.0", 1);

            Announcement announcement = make_Announcement("test", Severity.General, new DateTime(2014, 1, 1), new DateTime(2014, 2, 1));
            announcement.UnassignSignboard(signboard);
        }

        [TestCase]
        public void AssignSignboards_ValidSignboards_AddSignboards()
        {
            List<Signboard> signboards = new List<Signboard>();

            Signboard signboard1 = new Signboard("test", Guid.NewGuid(), Guid.NewGuid(), "0.0.0", 1);
            Signboard signboard2 = new Signboard("test", Guid.NewGuid(), Guid.NewGuid(), "0.0.0", 1);

            signboards.Add(signboard1);
            signboards.Add(signboard2);

            Announcement announcement = make_Announcement("test", Severity.General, new DateTime(2014, 1, 1), new DateTime(2014, 2, 1));
            announcement.AssignToSignboards(signboards);

            Assert.AreEqual(announcement.SignboardIds.Count(), 2);
        }

        [TestCase]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AssignSignboards_InvalidSignboards_AlreadyExists()
        {
            List<Signboard> signboards = new List<Signboard>();

            Signboard signboard1 = new Signboard("test", Guid.NewGuid(), Guid.NewGuid(), "0.0.0", 1);
            Signboard signboard2 = new Signboard("test", Guid.NewGuid(), Guid.NewGuid(), "0.0.0", 1);

            signboards.Add(signboard1);
            signboards.Add(signboard2);

            Announcement announcement = make_Announcement("test", Severity.General, new DateTime(2014, 1, 1), new DateTime(2014, 2, 1));
            announcement.AssignToSignboard(signboard1);
            announcement.AssignToSignboards(signboards);

            Assert.AreEqual(announcement.SignboardIds.Count(), 2);
        }

        [TestCase]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AssignSignboards_InvalidSignboards_DuplicatesInList()
        {
            List<Signboard> signboards = new List<Signboard>();

            Signboard signboard = new Signboard("test", Guid.NewGuid(), Guid.NewGuid(), "0.0.0", 1);

            signboards.Add(signboard);
            signboards.Add(signboard);

            Announcement announcement = make_Announcement("test", Severity.General, new DateTime(2014, 1, 1), new DateTime(2014, 2, 1));
            announcement.AssignToSignboards(signboards);
        }
    }
}
