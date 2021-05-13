using Kouch.App.ImageCropper;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kouch.App.Views.Modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageCropperModal : PopupPage
    {
        private readonly Action<byte[]> onSelect;
        private PhotoCropperCanvasView photoCropperCanvasView;
        public ImageCropperModal(byte[] bitmap,Action<byte[]> onSelect)
        {
            InitializeComponent();
            photoCropperCanvasView = new PhotoCropperCanvasView(SKBitmap.Decode(bitmap));
            canvasViewHost.Children.Add(photoCropperCanvasView);
            this.onSelect = onSelect;
        }

        private async void CancelClick(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }
        private async void SelectClick(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();

            SKData data = SKImage.FromBitmap(photoCropperCanvasView.CroppedBitmap).Encode(SKEncodedImageFormat.Png, 100);
            
            onSelect?.Invoke(data.ToArray());
        }
    }
}