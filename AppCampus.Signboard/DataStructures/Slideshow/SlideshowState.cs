using AppCampus.Signboard.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCampus.Signboard.DataStructures.Slideshow
{
    public class SlideshowState
    {
        public float FontFactor = 1.0f;
        public bool HardReload = false;

        private int StructureFailCounter = 0;
        private const int FAIL_THRESHHOLD = 10;

        public SignboardState SignboardState { get; set; }

        public bool IsShowScreensaver { get; set; }

        public Slideshow Slideshow { get; set; }

        public TimeSpan NewPollingInterval { get; set; }

        public bool IsNewSlideshow { get; set; }

        public List<Announcement> Announcements { get; set; }

        public bool IsNewSlideshowToStart { get; set; }

        public void Update(Structure structure) 
        {
            IsNewSlideshowToStart = false;
            NewPollingInterval = TimeSpan.MinValue;

            if (structure != null)
            {
                NewPollingInterval = TimeSpan.FromSeconds(structure.PollingIntervalInSeconds);
            }

            if (IsStructureError(structure))
            {
                SignboardState = SignboardState.Error;
            }
            else if (StructureFailCounter > 0)
            {
                // keep state what it was last time as it could be small part time network failure
            }
            else if (structure.Slideshow == null || structure.Slideshow.Slides == null || structure.Slideshow.Slides.Count == 0)
            {
                SignboardState = SignboardState.NoSlideshow;
                Slideshow = null;
                return;
            }

            if (structure.IsShowScreensaver)
            {
                SignboardState = SignboardState.ShowingScreensaver;
            }
            else if (IsShowScreensaver) 
            {
                SignboardState = SignboardState.LoadingSlideshow;
            }

            if (IsNewSlideshowToDisplay(structure.Slideshow))
            {
                IsNewSlideshow = true;
                Slideshow = structure.Slideshow.Copy();
                IsNewSlideshowToStart = true;
                SignboardState = SignboardState.LoadingWidgets;
            }

            if (structure.Announcements != null) 
            {
                Announcements = structure.Announcements;
            }
            else if (Announcements != null) 
            {
                Announcements = new List<Announcement>();
            }
        }

        private bool IsNewSlideshowToDisplay(Slideshow newSlideshow)
        {
            if (Slideshow == null)
            {
                return true;
            }
            bool isNewSlideshowToDisplay = !Slideshow.Equals(newSlideshow);
            return isNewSlideshowToDisplay;
        }

        private bool IsStructureError(Structure structure)
        {
            if (structure == null)
            {
                StructureFailCounter++;

                if (StructureFailCounter >= FAIL_THRESHHOLD)
                {
                    return true;
                }
            }
            else
            {
                StructureFailCounter = 0;
            }

            return false;
        }

    }
}
