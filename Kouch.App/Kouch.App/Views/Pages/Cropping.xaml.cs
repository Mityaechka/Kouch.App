using Kouch.App.ImageCropper;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kouch.App.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cropping : ContentPage
    {
        private PhotoCropperCanvasView photoCropper;
        private SKBitmap croppedBitmap;

        public Cropping()
        {
            InitializeComponent();
            Init();

        }
        public async void Init()
        {
            try
            {
                FilePickerFileType customFileType =
                new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.Android, new[] { "*/*" } }
                });
                PickOptions options = new PickOptions
                {
                    PickerTitle = "Please select a comic file",
                    FileTypes = customFileType,
                };

                FileResult result = await FilePicker.PickAsync(options);
                if (result != null)
                {
                    string Text = $"File Name: {result.FileName}";
                    if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                    {

                        System.IO.Stream stream = await result.OpenReadAsync();
                        ImageSource Image = ImageSource.FromStream(() => stream);

                        SKBitmap bitmap = SKBitmap.Decode(stream);

                        photoCropper = new PhotoCropperCanvasView(bitmap);
                        canvasViewHost.Children.Add(photoCropper);
                    }
                }

            }
            catch (Exception)
            {

            }
        }

        private void OnDoneButtonClicked(object sender, EventArgs args)
        {
            croppedBitmap = photoCropper.CroppedBitmap;

            SKCanvasView canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasViewPaintSurface;
            Content = canvasView;
        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();
            //canvas.DrawBitmap(croppedBitmap, info.Rect, BitmapStretch.Uniform);
        }

        private void TouchEffect_TouchAction(object sender, TouchActionEventArgs args)
        {

        }
    }
}