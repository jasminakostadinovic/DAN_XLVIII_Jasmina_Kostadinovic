using Pizza_app.ViewModel.Employees;
using System.Windows;

namespace Pizza_app.View.Orders
{
    /// <summary>
    /// Interaction logic for DeleteOrderView.xaml
    /// </summary>
    public partial class DeleteOrderView : Window
    {
        public DeleteOrderView()
        {
            InitializeComponent();
            this.DataContext = new DeleteEmployeeViewModel(this);
        }
    }
}
