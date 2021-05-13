using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kouch.App.Views.Components
{
    public class InfiniteListView : ListView
    {
        public static readonly BindableProperty LoadMoreCommandProperty = BindableProperty.Create<InfiniteListView, ICommand>(bp => bp.LoadMoreCommand, default(ICommand));
        public static readonly BindableProperty AutoScrollProperty = BindableProperty.Create<InfiniteListView, bool>(bp => bp.AutoScroll, default(bool));


        public ICommand LoadMoreCommand
        {
            get { return (ICommand)GetValue(LoadMoreCommandProperty); }
            set { SetValue(LoadMoreCommandProperty, value); }
        }
        public bool AutoScroll
        {
            get { return (bool)GetValue(LoadMoreCommandProperty); }
            set { SetValue(LoadMoreCommandProperty, value); }
        }
        public InfiniteListView():base(ListViewCachingStrategy.RecycleElementAndDataTemplate)
        {
            ItemAppearing += InfiniteListView_ItemAppearing;
        }

        void InfiniteListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var items = ItemsSource as List<object>;

            if (items != null && e.Item == items[items.Count - 1])
            {
                if (LoadMoreCommand != null && LoadMoreCommand.CanExecute(null))
                    LoadMoreCommand.Execute(null);
            }
            if (items != null&&e.Item == items.Last())
            {
                ScrollTo(e.Item, ScrollToPosition.MakeVisible, false);
            }
        }
    }

}
