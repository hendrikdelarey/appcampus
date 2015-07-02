using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace AppCampus.Signboard.Controls
{
    public class AnimatedGifImage : System.Windows.Controls.Image
    {
        private Bitmap bitmap; // Local bitmap member to cache image resource
        private BitmapSource bitmapSource;

        public delegate void FrameUpdatedEventHandler();

        /// <summary>
        /// Delete local bitmap resource
        /// Reference: http://msdn.microsoft.com/en-us/library/dd183539(VS.85).aspx
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass"), DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool DeleteObject(IntPtr hObject);

        /// <summary>
        /// Override the OnInitialized method
        /// </summary>
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.Loaded += new RoutedEventHandler(AnimatedGifImageLoaded);
            this.Unloaded += new RoutedEventHandler(AnimatedGifImageUnloaded);
        }

        /// <summary>
        /// Load the embedded image for the Image.Source
        /// </summary>

        private void AnimatedGifImageLoaded(object sender, RoutedEventArgs e)
        {
            // Get GIF image from Resources
            if (Properties.Resources.RefreshGif != null)
            {
                bitmap = Properties.Resources.RefreshGif;
                Width = bitmap.Width;
                Height = bitmap.Height;

                bitmapSource = GetBitmapSource();
                Source = bitmapSource;
            }
        }

        /// <summary>
        /// Close the FileStream to unlock the GIF file
        /// </summary>
        private void AnimatedGifImageUnloaded(object sender, RoutedEventArgs e)
        {
            StopAnimate();
        }

        /// <summary>
        /// Start animation
        /// </summary>
        public void StartAnimate()
        {
            ImageAnimator.Animate(bitmap, OnFrameChanged);
        }

        /// <summary>
        /// Stop animation
        /// </summary>
        public void StopAnimate()
        {
            ImageAnimator.StopAnimate(bitmap, OnFrameChanged);
        }

        /// <summary>
        /// Event handler for the frame changed
        /// </summary>
        private void OnFrameChanged(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                                   new FrameUpdatedEventHandler(FrameUpdatedCallback));
        }

        private void FrameUpdatedCallback()
        {
            ImageAnimator.UpdateFrames();

            if (bitmapSource != null)
            {
                bitmapSource.Freeze();
            }

            // Convert the bitmap to BitmapSource that can be display in WPF Visual Tree
            bitmapSource = GetBitmapSource();
            Source = bitmapSource;
            InvalidateVisual();
        }

        private BitmapSource GetBitmapSource()
        {
            IntPtr handle = IntPtr.Zero;

            try
            {
                handle = bitmap.GetHbitmap();
                bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                    handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                if (handle != IntPtr.Zero)
                {
                    DeleteObject(handle);
                }
            }

            return bitmapSource;
        }
    }
}