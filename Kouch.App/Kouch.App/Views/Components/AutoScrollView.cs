using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Kouch.App.Views.Components
{
    public class AutoScrollView : ScrollView
    {
        public AutoScrollView()
        {
            
        }

        protected override async void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == "ContentSize")
            {
                    await ScrollToAsync(10000,0,false);
            }
            base.OnPropertyChanged(propertyName);
        }
    }

    
}
