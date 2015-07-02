
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCampus.SignboardApi.Models.QueryModels
{
    /// <summary>
    /// The object containing the Health states of the screen
    /// </summary>
    public class SignboardHealthModel
    {
        /// <summary>
        /// The State of the Signboard
        /// </summary>
        public string ScreenState { get; set; }

        /// <summary>
        /// Is the option true for showing the screensaver on the Signboard.
        /// </summary>
        public bool IsShowingScreensaver { get; set; }

        /// <summary>
        /// List of all slides and health
        /// </summary>
        public List<SlideHealthModel> SlideHealth { get; set; }
    }
}
