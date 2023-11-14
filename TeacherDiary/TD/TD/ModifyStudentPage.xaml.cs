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
    public partial class ModifyStudentPage : ContentPage
    {
        private TDModel _model;
        private Student _student;
        public ModifyStudentPage(Student student,TDModel model)
        {
            InitializeComponent();

            _model = model;
            _student = student;
        }

        private async void modifyStudentBtn_Clicked(object sender, EventArgs e)
        {
            _student.Name=nameEntry.Text;
            await Navigation.PopAsync();
        }

        async void OnPreviousPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}