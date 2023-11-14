using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TD
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModifyClassPage : ContentPage
    {
        private TDModel _model;
        private SchoolClass _school;
        public ModifyClassPage(SchoolClass school,TDModel model)
        {
            InitializeComponent();

            _model = model;
            _school = school;
        }

        private async void modifySchoolBtn_Clicked(object sender, EventArgs e)
        {
            _school.Name = nameEntry.Text;
            await Navigation.PopAsync();
        }

        async void OnPreviousPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}