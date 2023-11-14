using System;
using TD.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TD
{
    public partial class App : Application
    {
        private TDModel _model;
        public App()
        {
            InitializeComponent();

            _model=new TDModel();

            MainPage = new NavigationPage(new MainPage(_model));
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
