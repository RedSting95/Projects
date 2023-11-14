using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TD.Model
{
    public class Student : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public List<Mark> Marks { get; set; }
        public ObservableCollection<Missing> Missings { get; set; }

        public Student(int id, string name, string className)
        {
            Id = id;
            Name = name;
            ClassName = className;
            Marks = new List<Mark>();
            Missings = new ObservableCollection<Missing>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
