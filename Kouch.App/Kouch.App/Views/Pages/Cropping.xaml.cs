using Kouch.App.ImageCropper;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kouch.App.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cropping : ContentPage
    {
        PhotoCropperCanvasView photoCropper;
        SKBitmap croppedBitmap;

        public Cropping()
        {
            InitializeComponent();
            Init();
           
        }
        public async void Init()
        {
            try
            {
                var customFileType =
                new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.Android, new[] { "*/*" } }
                });
                var options = new PickOptions
                {
                    PickerTitle = "Please select a comic file",
                    FileTypes = customFileType,
                };

                var result = await FilePicker.PickAsync(options);
                if (result != null)
                {
                    var Text = $"File Name: {result.FileName}";
                    if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                    {

                        var stream = await result.OpenReadAsync();
                        var Image = ImageSource.FromStream(() => stream);

                        SKBitmap bitmap = SKBitmap.Decode(stream);

                        photoCropper = new PhotoCropperCanvasView(bitmap);
                        canvasViewHost.Children.Add(photoCropper);
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
        void OnDoneButtonClicked(object sender, EventArgs args)
        {
            croppedBitmap = photoCropper.CroppedBitmap;

            SKCanvasView canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasViewPaintSurface;
            Content = canvasView;
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
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