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
    public partial class ModifyMissingPage : ContentPage
    {
        private TDModel _model;
        private Missing _missing;
        public ModifyMissingPage(Missing missing,TDModel model)
        {
            InitializeComponent();

            _missing = missing;
            _model = model;
        }

        

        private async void modifyBtn_Clicked(object sender, EventArgs e)
        {
            _missing.Description = dscEntry.Text;
            await Navigation.PopAsync();
        }

        async void OnPreviousPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}