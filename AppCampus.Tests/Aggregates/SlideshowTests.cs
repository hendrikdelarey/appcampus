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
    [Category("SlideShow")]
    class SlideshowTests
    {
        public Slideshow make_Slideshow(string name)
        {
            return new Slideshow(name, Guid.NewGuid());
        }

        [TestCase("Test")]
        public void Create_Slideshow_NewSlideshow(string name)
        {
            var slideshow = make_Slideshow(name);

            Assert.AreEqual(slideshow.Name, name);
        }

        [TestCase(null)]
        [TestCase("")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_InvalidSlideshow_ThrowException(string name)
        {
            make_Slideshow(name);
        }

        [TestCase]
        public void AddSlide_ValidSlide_SlideAdded() 
        {
            Slide slide = new Slide(Duration.From(10), Transition.From(TransitionType.None), "Untitled");

            Slideshow slideshow = make_Slideshow("Test");
            slideshow.Insert(slide);

            Assert.AreEqual(slideshow.Slides.Count(), 1);
        }

        [TestCase]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddSlide_InvalidSlide_ArgumentNullException()
        {
            Slideshow slideshow = make_Slideshow("Test");
            slideshow.Insert(null);
        }

        [TestCase]
        public void RemoveSlide_ValidSlide_RemoveSlide()
        {
            Slide slide = new Slide(Duration.From(10), Transition.From(TransitionType.None), "Untitled");

            Slideshow slideshow = make_Slideshow("Test");
            slideshow.Insert(slide);
            slideshow.RemoveSlide(slide);
            Assert.AreEqual(slideshow.Slides.Count(), 0);
        }

        [TestCase]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RemoveSlide_InvalidSlide_SlideRemoved()
        {
            Slide slide = new Slide(Duration.From(10), Transition.From(TransitionType.None), "Untitled");

            Slideshow slideshow = make_Slideshow("Test");
            slideshow.RemoveSlide(slide);
        }

        [TestCase]
        public void MoveBefore_ValidMove_MovedBefore() 
        {
            Slide slide1 = new Slide(Duration.From(10), Transition.From(TransitionType.None), "Untitled");
            Slide slide2 = new Slide(Duration.From(20), Transition.From(TransitionType.None), "Untitled");

            Slideshow slideshow = make_Slideshow("Test");
            slideshow.Insert(slide1);
            slideshow.Insert(slide2);

            slideshow.MoveBefore(slide2, slide1);

            Assert.AreEqual(slideshow.Slides.First(), slide2);
        }

        [TestCase]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MoveBefore_InvalidMove_MovedBefore()
        {
            Slide slide1 = new Slide(Duration.From(10), Transition.From(TransitionType.None), "Untitled");
            Slide slide2 = new Slide(Duration.From(20), Transition.From(TransitionType.None), "Untitled");

            Slideshow slideshow = make_Slideshow("Test");
            slideshow.Insert(slide1);

            slideshow.MoveBefore(slide2, slide1);
        }

        [TestCase]
        public void MoveAfter_ValidMove_MovedAfter()
        {
            Slide slide1 = new Slide(Duration.From(10), Transition.From(TransitionType.None), "Untitled");
            Slide slide2 = new Slide(Duration.From(20), Transition.From(TransitionType.None), "Untitled");

            Slideshow slideshow = make_Slideshow("Test");
            slideshow.Insert(slide1);
            slideshow.Insert(slide2);

            slideshow.MoveAfter(slide1, slide2);

            Assert.AreEqual(slideshow.Slides.First(), slide2);
        }

        [TestCase]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MoveAfter_InvalidMove_MovedAfter()
        {
            Slide slide1 = new Slide(Duration.From(10), Transition.From(TransitionType.None), "Untitled");
            Slide slide2 = new Slide(Duration.From(20), Transition.From(TransitionType.None), "Untitled");

            Slideshow slideshow = make_Slideshow("Test");
            slideshow.Insert(slide1);

            slideshow.MoveAfter(slide1, slide2);
        }

        [TestCase]
        public void InsertBefore_ValidInsert_InsertedBefore()
        {
            Slide slide1 = new Slide(Duration.From(10), Transition.From(TransitionType.None), "Untitled");
            Slide slide2 = new Slide(Duration.From(20), Transition.From(TransitionType.None), "Untitled");

            Slideshow slideshow = make_Slideshow("Test");
            slideshow.Insert(slide1);

            slideshow.InsertBefore(slide2, slide1);

            Assert.AreEqual(slideshow.Slides.First(), slide2);
        }

        [TestCase]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InsertBefore_InvalidInsert_InsertedBefore()
        {
            Slide slide1 = new Slide(Duration.From(10), Transition.From(TransitionType.None), "Untitled");
            Slide slide2 = new Slide(Duration.From(20), Transition.From(TransitionType.None), "Untitled");

            Slideshow slideshow = make_Slideshow("Test");

            slideshow.InsertBefore(slide2, slide1);
        }

        [TestCase]
        public void InsertAfter_ValidInsert_InsertedAfter()
        {
            Slide slide1 = new Slide(Duration.From(10), Transition.From(TransitionType.None), "Untitled");
            Slide slide2 = new Slide(Duration.From(20), Transition.From(TransitionType.None), "Untitled");

            Slideshow slideshow = make_Slideshow("Test");
            slideshow.Insert(slide1);

            slideshow.InsertAfter(slide2, slide1);

            Assert.AreEqual(slideshow.Slides.First(), slide1);
        }

        [TestCase]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InsertAfter_InvalidInsert_InsertedAfter()
        {
            Slide slide1 = new Slide(Duration.From(10), Transition.From(TransitionType.None), "Untitled");
            Slide slide2 = new Slide(Duration.From(20), Transition.From(TransitionType.None), "Untitled");

            Slideshow slideshow = make_Slideshow("Test");

            slideshow.InsertAfter(slide1, slide1);
        }
    }
}
