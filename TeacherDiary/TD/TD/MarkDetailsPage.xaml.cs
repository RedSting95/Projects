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
    public partial class MarkDetailsPage : ContentPage
    {
        private TDModel _model;
        private Mark _mark;
        
        public int Value { get; }
        public double Weight { get; }

        public MarkDetailsPage(Mark mark,TDModel model)
        {
            InitializeComponent();

            _model = model;
            _mark = mark;
            Value = _mark.Value;
            Weight = _mark.Weight;

            BindingContext = this;
        }

        private async void clearBtn_Clicked(object sender, EventArgs e)
        {
            _model.marks.Remove(_mark);
            await Navigation.PopAsync();
            
        }

        private async void modifyBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ModifyMarkPage(_mark, _model));
        }

        async void OnPreviousPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}