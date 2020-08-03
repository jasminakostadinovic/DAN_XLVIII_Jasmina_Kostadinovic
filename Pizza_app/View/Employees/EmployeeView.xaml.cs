using Pizza_app.ViewModel.Employees;
using System.Windows;
using System.Windows.Controls;

namespace Pizza_app.View.Employees
{
    /// <summary>
    /// Interaction logic for EmployeeView.xaml
    /// </summary>
    public partial class EmployeeView : Window
    {
        public EmployeeView(string ordererPersonalNo)
        {
            InitializeComponent();
            this.DataContext = new EmployeeViewModel(this, ordererPersonalNo);
        }
        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //hiding id columns
            if (e.Column.Header.ToString() == "OrderID"
                 || e.Column.Header.ToString() == "tblMealOrders")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
    }
}
