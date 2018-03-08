using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using WhetherPOC.Models;
using WhetherPOC.ServiceAccess;
using Xamarin.Forms;

namespace WhetherPOC
{
    public partial class WhetherPOCPage : ContentPage
    {
        public WhetherPOCPage()
        {
            InitializeComponent();
        }

        async void submit_Tapped(object sender, System.EventArgs e)
        {
            try
            {
                var zipcode = txtzipCode.Text;
                ExploreSL objSl = new ExploreSL();
                UserDialogs.Instance.ShowLoading("Loading", MaskType.Black);

                Xamarin.Forms.Device.BeginInvokeOnMainThread(async() =>
                {
                    var data = await objSl.GetWhetherData(zipcode);
                    UserDialogs.Instance.HideLoading();
                    if (data != null)
                    {
                        Navigation.PushAsync(new MapPage(data));
                    }
                });
               
            }
            catch(Exception ex){
                
            }

        }
    }
}
