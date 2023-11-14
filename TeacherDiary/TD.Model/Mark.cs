using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TD.Model
{
    public class Mark : INotifyPropertyChanged
    {
        public int StudentId { get; set; }
        public int Value { get; set; }
        public double Weight { get; set; }

        public Mark(int studentId, int value, double weight)
        {
            StudentId = studentId;
            Value = value;
            Weight = weight;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
