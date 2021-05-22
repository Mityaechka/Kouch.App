using Xamarin.Forms;

namespace Kouch.App.Views.Components
{
    public class VisibleToolbarItem : ToolbarItem
    {
        public static readonly BindableProperty IsVisibleProperty = BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(VisibleToolbarItem), true, BindingMode.TwoWay, propertyChanged: OnIsVisibleChanged);

        public bool IsVisible
        {
            get => (bool)GetValue(IsVisibleProperty);
            set => SetValue(IsVisibleProperty, value);
        }

        private static void OnIsVisibleChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            VisibleToolbarItem item = bindable as VisibleToolbarItem;

            if (item == null || item.Parent == null)
            {
                return;
            }

            System.Collections.Generic.IList<ToolbarItem> toolbarItems = ((ContentPage)item.Parent).ToolbarItems;

            if ((bool)newvalue && !toolbarItems.Contains(item))
            {
                Device.BeginInvokeOnMainThread(() => { toolbarItems.Add(item); });
            }
            else if (!(bool)newvalue && toolbarItems.Contains(item))
            {
                Device.BeginInvokeOnMainThread(() => { toolbarItems.Remove(item); });
            }
        }
    }
}
