using AppCampus.Signboard.Components.Logging;
using AppCampus.Signboard.Enums;
using AppCampus.Signboard.Helpers;
using AppCampus.Signboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace AppCampus.Signboard.DataStructures.Widgets
{
    public abstract class Widget
    {
        private static readonly int DefaultPollingIntervalInSeconds = 30;
        private static readonly int DefaultNumResults = 6;
        private static readonly int DefaultFontSize = 96;

        public float FontFactor { get; private set; }
        public Type Type { get; private set; }

        public WidgetState State { get; set; }

        public Widget(float fontFactor = 1.0f) 
        {
            State = WidgetState.Initialising;
            Type = GetType();
            FontFactor = fontFactor;
        }

        public abstract UIElement GetUiElement();

        public abstract void OnStart();

        public abstract bool Equals(Widget otherWidget);

        public abstract Widget Copy();

        public void UpdateFontFactor(float fontFactor) 
        {
            if (fontFactor <= 0) 
            {
                Logger.Instance.Write("UpdateFontFactor", LogLevel.Medium, "Invalid fontFactor '" + fontFactor + "'");
                return;
            }

            FontFactor = fontFactor;
        }

        public static Widget From(WidgetModel widget)
        {
            var t = (WidgetType)Enum.Parse(typeof(WidgetType), widget.Type);
            if (t == WidgetType.Image){
                var imageId = widget.GetValueByType(ParameterType.ImageIdentifier);
                var imageFillValue = widget.GetValueByType(ParameterType.ImageFill);
                var imageFill = (ImageFill) Enum.Parse(typeof(ImageFill),imageFillValue);
                return new ImageWidget(
                   imageId,
                   imageFill);

            } else if (t == WidgetType.Timetable) {
                var operatorName = widget.GetValueByType(ParameterType.OperatorName);
                var displayNameValue = widget.GetValueByType(ParameterType.OperatorDisplayName);
                var stopId = widget.GetValueByType(ParameterType.StopIdentifier);
                var numResultsValue = widget.GetValueByType(ParameterType.NumResults);
                var pollingIntervalInSeconds = widget.GetValueByType(ParameterType.PollingIntervalInSeconds);
                var walkingDistanceToStopInSeconds = widget.GetValueByType(ParameterType.WalkingDistanceToStopInSeconds);
                if (String.IsNullOrWhiteSpace(displayNameValue)){
                   displayNameValue = operatorName;
                }
                int numResultsInt =DefaultNumResults;
                Int32.TryParse(numResultsValue, out numResultsInt);
                int pollingInt = DefaultPollingIntervalInSeconds;
                Int32.TryParse(pollingIntervalInSeconds, out pollingInt);
                int walkingInt = 0;
                Int32.TryParse(walkingDistanceToStopInSeconds, out walkingInt);
                return new TimetableWidget(
                     operatorName,
                     displayNameValue,
                     stopId,
                     numResultsInt,
                     pollingInt,
                     walkingInt);
            } else if (t == WidgetType.Text){
                var text = widget.GetValueByType(ParameterType.Text);
                var fontsizeString = widget.GetValueByType(ParameterType.FontSize);
                var textColour = widget.GetValueByType(ParameterType.TextColour);
                var fontSizeInt = DefaultFontSize;
                Int32.TryParse(fontsizeString, out fontSizeInt);
                return new CenteredTextWidget(
                    text,
                    fontSizeInt,
                    textColour);
                            


            } else {return new ErrorWidget();
            }
          
                    
            
        }
    }
}
