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
