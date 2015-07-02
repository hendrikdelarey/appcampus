using AppCampus.Signboard.Components;
using AppCampus.Signboard.Components.Cache;
using AppCampus.Signboard.Enums;
using AppCampus.Signboard.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AppCampus.Signboard.DataStructures.Widgets
{
    class ImageWidget : Widget
    {
        public string ImageId { get; private set; }

        public string Base64Image { get; set; }

        public Image ImageElement { get; private set; }

        public ImageFill ImageFill;

        public ImageWidget(string imageId, ImageFill imageFill)
            : base() 
        {
            ImageId = imageId;
            ImageFill = imageFill;
        }

        private void InitialiseImage() 
        {
            if (ImageCache.Instance.HasImage(new Guid(ImageId)))
            {
                Base64Image = ImageCache.Instance.GetImage(new Guid(ImageId));
            }

            if (ImageId.Length > 0)
            {
                var worker = new BackgroundWorker();
                string myResult = String.Empty;
                worker.DoWork += BackgroundTask;
                worker.RunWorkerCompleted += BackgroundTaskComplete;
                worker.RunWorkerAsync();
            }
            else 
            {
                // user wants an empty slide
                Base64Image = String.Empty;
                State = WidgetState.Ready;
            }
        }

        private void BackgroundTask(object sender, DoWorkEventArgs e) 
        {
            e.Result = new ImageComponent().GetBase64Image(new Guid(ImageId)); 
        }

        private void BackgroundTaskComplete(object sender, RunWorkerCompletedEventArgs e) 
        {
            if (e.Error != null)
            {
                InitialiseImage();
            }
            else if (e.Cancelled)
            {
                InitialiseImage();
            }
            else
            {
                if (e.Result == null) 
                {
                    InitialiseImage();
                    return;
                }

                UpdateImage(e.Result.ToString());
            }
        }

        private void UpdateImage(string base64Image) 
        {
            Base64Image = base64Image;

            if (IsValidImage(Base64Image)) 
            {
                State = WidgetState.Ready;
            }
            else
            {
                State = WidgetState.Error;
                InitialiseImage();
            }
        }

        private bool IsValidImage(string imageString) 
        {
            return imageString.Length > 0;
        }

        public override void OnStart()
        {
            InitialiseImage();
        }

        private Image GetImage()
        {
            if (ImageElement != null) 
            {
                return ImageElement;
            }

            ImageElement = ImageHelper.ConvertBase64StringToImage(Base64Image);
            RenderOptions.SetBitmapScalingMode(ImageElement, BitmapScalingMode.LowQuality);
            return ImageElement;
        }

        public override UIElement GetUiElement() 
        {
            var imageElemet = GetImage();

            if (ImageFill == ImageFill.Stretch) 
            {
                imageElemet.Stretch = Stretch.Fill;
            }

            return imageElemet;
        }

        public override bool Equals(Widget otherWidget)
        {
            if (Type != otherWidget.Type) 
            {
                return false;
            }

            ImageWidget other = (ImageWidget)otherWidget;

            return ImageId == other.ImageId;
        }

        public override Widget Copy() 
        {
            return new ImageWidget(this.ImageId, this.ImageFill);
        }
    }
}
