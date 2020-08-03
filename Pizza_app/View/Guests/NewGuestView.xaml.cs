using Pizza_app.ViewModel.Guests;
using System.Windows;
using System.Windows.Controls;

namespace Pizza_app.View.Guests
{
    /// <summary>
    /// Interaction logic for NewGuestView.xaml
    /// </summary>
    public partial class NewGuestView : Window
    {
        public NewGuestView(string ordererPersonalNo)
        {
            InitializeComponent();
            this.DataContext = new NewGuestViewModel(this, ordererPersonalNo);
        }
        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //hiding id columns
            if (e.Column.Header.ToString() == "MealID"
                || e.Column.Header.ToString() == "tblMealOrders")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
    }
}
