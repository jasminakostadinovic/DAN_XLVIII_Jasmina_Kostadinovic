using Pizza_app.ViewModel.Orders;
using System.Windows;
using System.Windows.Controls;

namespace Pizza_app.View.Orders
{
    /// <summary>
    /// Interaction logic for OrderStatusView.xaml
    /// </summary>
    public partial class OrderStatusView : Window
    {
        public OrderStatusView(string ordererPersonalNo)
        {
            InitializeComponent();
            this.DataContext = new OrderStatusViewModel(this, ordererPersonalNo);
        }
        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //hiding id columns
            if (e.Column.Header.ToString() == "OrdererPersonalNo"
                || e.Column.Header.ToString() == "OrderID"
                 || e.Column.Header.ToString() == "tblMealOrders")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
    }
}
