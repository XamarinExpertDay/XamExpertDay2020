using Xamarin.Forms; 
using UiDrawing.Views;

namespace UiDrawing
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
             
            MainPage = new SliderSample();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
