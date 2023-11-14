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
    public partial class ModifyMarkPage : ContentPage
    {
        TDModel _model;
        Mark _mark;
        public ModifyMarkPage(Mark mark, TDModel model)
        {
            InitializeComponent();

            _model = model;
            _mark = mark;
        }


        private async void modifyMarkBtn_Clicked(object sender, EventArgs e)
        {
            _mark.Value = Int32.Parse(valueEntry.Text);
            _mark.Weight = Double.Parse(weightEntry.Text);
            await Navigation.PopAsync();
        }

        async void OnPreviousPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}