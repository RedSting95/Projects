using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TD.Model
{

    public class SchoolClass : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public List<Student> Students { get; set; }


        public SchoolClass(string name)
        {
            Name = name;
            Students = new List<Student>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
