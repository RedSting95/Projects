using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TD.Model
{
    public class Missing : INotifyPropertyChanged
    {
        public int StudentId { get; set; }
        public string Description { get; set; }

        public Missing(int studentId, string description)
        {
            StudentId = studentId;
            Description = description;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
