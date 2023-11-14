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
    public partial class MarksPage : ContentPage
    {
        private TDModel _model;
        private Student _student;
        
        
        public MarksPage(Student student, TDModel model )
        {
            InitializeComponent();

            _model=model;
            _student = student;
            _student.Marks=_model.findMarksByStudentId(_student.Id);
            _student.Missings=_model.findMissingsByStudentId(_student.Id);

            marksList.ItemsSource=_student.Marks;
            missingsList.ItemsSource=_student.Missings;

            average.Text=calculateAverage().ToString("F");
            
        }

        private double calculateAverage()
        {
            double _average = 0;
            double totalWeight = 0;
            foreach (var mark in _student.Marks)
            {
                _average += mark.Value * mark.Weight;
                totalWeight += mark.Weight;
            }
            _average = _average / totalWeight;
            return _average;
        }

        private async void marksList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Mark mark = e.SelectedItem as Mark;
            await Navigation.PushAsync(new MarkDetailsPage(mark, _model));
        }

        private async void missingsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Missing missing = e.SelectedItem as Missing;
            await Navigation.PushAsync(new MissingDetailPage(missing, _model));
        }

        private void addBtn_Clicked(object sender, EventArgs e)
        {
            Missing missing = new Missing(_student.Id, dscEntry.Text);
            _model.missings.Add(missing);
        }

        private void addMarkBtn_Clicked(object sender, EventArgs e)
        {
            Mark mark = new Mark(_student.Id, Int32.Parse(valueEntry.Text), Double.Parse(weightEntry.Text));
            _model.marks.Add(mark);
            average.Text = calculateAverage().ToString("F");
        }

        async void OnPreviousPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}