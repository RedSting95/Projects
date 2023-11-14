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
    public partial class CalendarPage : ContentPage
    {
        private TDModel _model;
        public CalendarPage(TDModel model)
        {
            InitializeComponent();

            _model = model;

            calendarList.ItemsSource=_model.calendarItems;
        }

        private void selected_DateSelected(object sender, DateChangedEventArgs e)
        {
            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            
            CalendarItem ci = new CalendarItem(dscEntry.Text, dpDate.Date.ToShortDateString());
            _model.calendarItems.Add(ci);
        }

        private void clearButton_Clicked(object sender, EventArgs e)
        {
            var item = (Button)sender;
            CalendarItem listItem = _model.calendarItems.Where(c => c.Description == item.CommandParameter.ToString()).FirstOrDefault();

            _model.calendarItems.Remove(listItem);
        }

        async void OnPreviousPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}