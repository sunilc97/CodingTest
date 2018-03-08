using Xamarin.Forms;

namespace WhetherPOC
{
    public partial class App : Application
    {
        ILocation loc;
        public App()
        {
            InitializeComponent();


            MainPage =new NavigationPage( new WhetherPOCPage());
        }

        protected override void OnStart()
        {

            loc = DependencyService.Get<ILocation>();
            loc.locationObtained += (object sender,
                ILocationEventArgs e) =>
            {

                var lat = e.lat;
                var lng = e.lng;

                Application.Current.Properties["Latitude"] = lat.ToString();
                Application.Current.Properties["Longitude"] = lng.ToString();
            };
            loc.ObtainMyLocation();
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
