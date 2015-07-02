using AppCampus.Signboard.Enums;
using System;
using System.Windows;
using System.Windows.Media;
using TextWidget;

namespace AppCampus.Signboard.DataStructures.Widgets
{
    public class CenteredTextWidget : Widget
    {
        private Color DefaultColour = Color.FromRgb(0, 0, 0);

        public int FontSize { get; set; }

        public string Text { get; set; }

        public string TextColour { get; set; }

        public CenteredTextWidget(string text, int fontSize, string textColour)
            : base()
        {
            Text = text;
            FontSize = fontSize;

            TextColour = textColour;

            Color testForValidTextColour;
            try
            {
                testForValidTextColour = (Color)ColorConverter.ConvertFromString(textColour);
            }
            catch (Exception)
            {
                TextColour = "#000000";
            }
        }

        public override Widget Copy()
        {
            return new CenteredTextWidget(this.Text, this.FontSize, this.TextColour);
        }

        public override bool Equals(Widget otherWidget)
        {
            if (this.Type != otherWidget.Type)
            {
                return false;
            }

            CenteredTextWidget other = (CenteredTextWidget)otherWidget;

            return Text == other.Text;
        }

        public override UIElement GetUiElement()
        {
            var textWidget = new CenteredTextControl();
            textWidget.DataContext = new TextWidgetBindingModel() { Text = this.Text, FontSize = this.FontSize, TextColour = this.TextColour };

            return textWidget;
        }

        public override void OnStart()
        {
            if (String.IsNullOrWhiteSpace(Text))
            {
                this.State = WidgetState.Error;
            }
            else
            {
                State = WidgetState.Ready;
            }
        }

        protected class TextWidgetBindingModel
        {
            public int FontSize { get; set; }

            public string Text { get; set; }

            public string TextColour { get; set; }
        }
    }
}