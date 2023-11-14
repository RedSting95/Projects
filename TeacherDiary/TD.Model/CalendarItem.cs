using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TD.Model
{
    public class CalendarItem : INotifyPropertyChanged
    {
        public string Description { get; set; }
        public string Date { get; set; }

        public CalendarItem(string Description, string Date)
        {
            this.Description = Description;
            this.Date=Date;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
