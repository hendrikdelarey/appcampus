using AppCampus.Signboard.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace AppCampus.Signboard.DataStructures.Widgets
{
    public class ErrorWidget : Widget
    {
        public ErrorWidget() : base()
        {
            State = WidgetState.Error;
        }

        public override UIElement GetUiElement() 
        {
            return null;
        }

        public override void OnStart() 
        {
        }

        public override bool Equals(Widget otherWidget) 
        {
            return (otherWidget.Type == typeof(ErrorWidget));
        }

        public override Widget Copy()
        {
            return new ErrorWidget();
        }
    }
}
