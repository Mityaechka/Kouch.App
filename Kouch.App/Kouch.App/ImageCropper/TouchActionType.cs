using System.IO;
using System.Reflection;
using System.Text;
using Xamarin.Essentials;

namespace Kouch.App.ImageCropper
{
    public delegate void TouchActionEventHandler(object sender, TouchActionEventArgs args);
    public enum TouchActionType
    {
        Entered,
        Pressed,
        Moved,
        Released,
        Exited,
        Cancelled
    }
}
