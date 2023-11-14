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
    public partial class MissingDetailPage : ContentPage
    {
        private TDModel _model;
        private Missing _missing;
        private Student _student;
        public string Description { get; set; }
        public MissingDetailPage(Missing missing, TDModel model)
        {
            InitializeComponent();

            _missing = missing;
            _model = model;
            Description= _missing.Description;
            _student = _model.findStudentById(_missing.StudentId);

            BindingContext = this;

        }


        private async void clearBtn_Clicked(object sender, EventArgs e)
        {
            _model.missings.Remove(_missing);
            
            await Navigation.PopAsync();

        }

        private async void modifyBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ModifyMissingPage(_missing,_model));
        }

        async void OnPreviousPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}