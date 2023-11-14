using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Model;
using Xamarin.Forms;

namespace TD
{
    public partial class MainPage : ContentPage
    {
        private TDModel _model;
        
        public MainPage(TDModel model)
        {
            InitializeComponent();
            
            
            _model = model;
        }

        private async void _classes_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Classes(_model));
        }

        

        private async void _calendar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CalendarPage(_model));
        }
    }
}
