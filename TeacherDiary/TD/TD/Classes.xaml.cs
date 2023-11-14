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
    public partial class Classes : ContentPage
    {
        private TDModel _model;
        public Classes(TDModel model)
        {
            InitializeComponent();

            _model = model;
            classList.ItemsSource = _model.schools;
        }

        private async void classList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            SchoolClass school = e.SelectedItem as SchoolClass;
            await Navigation.PushAsync(new StudentsPage(school.Name, _model));
        }

        private async void modifyBtn_Clicked(object sender, EventArgs e)
        {
            var item = (Button)sender;
            SchoolClass listItem = _model.schools.Where(s => s.Name.Equals(item.CommandParameter.ToString())).FirstOrDefault();
            await Navigation.PushAsync(new ModifyClassPage(listItem,_model));
        }

        private void clearBtn_Clicked(object sender, EventArgs e)
        {
            var item = (Button)sender;
            SchoolClass listItem = _model.schools.Where(s => s.Name.Equals(item.CommandParameter.ToString())).FirstOrDefault();
            _model.schools.Remove(listItem);
        }

        private void newBtn_Clicked(object sender, EventArgs e)
        {
            SchoolClass schoolClass = new SchoolClass(schoolEntry.Text);
            _model.schools.Add(schoolClass);
        }

        async void OnPreviousPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}