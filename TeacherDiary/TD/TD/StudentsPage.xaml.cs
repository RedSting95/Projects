using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TD
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentsPage : ContentPage
    {
        private TDModel _model;
        private string _school;
        private List<Student> _students;

        public StudentsPage(string school, TDModel model)
        {
            InitializeComponent();

            _school = school;
            _model = model;
            _students = _model.findStudentsByClassName(_school).OrderBy(s=>s.Name).ToList();
            studentList.ItemsSource= _students;
            foreach (var student in _students)
            {
                student.Marks=_model.findMarksByStudentId(student.Id);
            }
            foreach (var student in _students)
            {
                student.Missings=_model.findMissingsByStudentId(student.Id);
            }
        }

        private async void studentList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Student student = e.SelectedItem as Student;
            await Navigation.PushAsync(new MarksPage(student, _model));
        }

        private void clearBtn_Clicked(object sender, EventArgs e)
        {
            var item = (Button)sender;
            Student listItem = _model.students.Where(s => s.Id == Int32.Parse(item.CommandParameter.ToString())).FirstOrDefault();

            _model.students.Remove(listItem);

        }

        private async void modifyBtn_Clicked(object sender, EventArgs e)
        {
            var item = (Button)sender;
            Student listItem = _model.students.Where(s => s.Id == Int32.Parse(item.CommandParameter.ToString())).FirstOrDefault();

            await Navigation.PushAsync(new ModifyStudentPage(listItem, _model));
        }

        private void newBtn_Clicked(object sender, EventArgs e)
        {
            int id = _model.students.Last().Id+1;
            Student student = new Student(id, studentEntry.Text, _school);
            _model.students.Add(student);

        }

        async void OnPreviousPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}