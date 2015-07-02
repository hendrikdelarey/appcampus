using AppCampus.Signboard.DataStructures.Slideshow;
using AppCampus.Signboard.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCampus.Signboard.Models.InputModels
{
    [Serializable]
    public class SignboardHealth
    {
        public string ScreenState { get; set; }

        public bool IsShowingScreensaver { get; set; }

        public float FontFactor { get; set; }

        public List<SlideHealth> SlideHealth { get; set; }

        public static SignboardHealth From(SignboardState state, bool isShowingScreensaver, Slideshow slideshow, float fontFactor) 
        {
            return new SignboardHealth()
            {
                ScreenState = state.ToString(),
                IsShowingScreensaver = isShowingScreensaver,
                FontFactor = fontFactor,
                SlideHealth = slideshow == null ? null : slideshow.Slides.Select(x =>
                    new SlideHealth()
                    {
                        SlideIndex = slideshow.Slides.IndexOf(x),
                        WidgetHealth = x.Widgets.Select(y => 
                            new WidgetHealth() 
                            { 
                                WidgetState = y.State.ToString(),
                                WidgetType = y.Type.ToString()
                            }).ToList()
                    }).ToList()
            };
        }
    }
}
