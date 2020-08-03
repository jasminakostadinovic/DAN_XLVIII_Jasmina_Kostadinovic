using Pizza_app.Command;
using Pizza_app.Model;
using Pizza_app.View.Employees;
using Pizza_app.View.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Pizza_app.ViewModel.Employees
{
    class EmployeeViewModel : ViewModelBase
    {
        #region Fields
        private readonly EmployeeView view;
        private tblOrder order;
        private List<tblOrder> orders;
        private readonly string ordererPersonalNo;
        private readonly DataAccess db = new DataAccess();
        private int orderID;
        #endregion

        #region Constructors
        internal EmployeeViewModel(EmployeeView view, string ordererPersonalNo)
        {
            this.view = view;
            this.ordererPersonalNo = ordererPersonalNo;
            Orders = LoadOrders();
            Order = new tblOrder();

        }

        private List<tblOrder> LoadOrders()
        {
            try
            {
                return db.LoadAllOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        #endregion

        #region Properties 
        public tblOrder Order
        {
            get
            {
                return order;
            }
            set
            {
                order = value;
                OnPropertyChanged(nameof(Order));
            }
        }
        public List<tblOrder> Orders
        {
            get
            {
                return orders;
            }
            set
            {
                orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }
        #endregion

        #region Commands
        //removing order
        private ICommand delete;
        public ICommand Delete
        {
            get
            {
                if (delete == null)
                {
                    delete = new RelayCommand(param => DeleteOrderExecute(), param => CanDeleteOrder());
                }
                return delete;
            }
        }

        private bool CanDeleteOrder()
        {
            if (Order == null)
                return false;
            return true;
        }

        private void DeleteOrderExecute()
        {
            try
            {
                if (Order != null)
                {
                    orderID = Order.OrderID;
                    bool isExistingOrder = db.IsExistingOrder(orderID);

                    if (isExistingOrder == true)
                    {
                        DeleteOrderView deleteOrder = new DeleteOrderView();
                        deleteOrder.ShowDialog();
                        if ((deleteOrder.DataContext as DeleteEmployeeViewModel).ShouldDelete == true)
                        {
                            var orderedMeals = db.LoadOrderedMeals(orderID);
                            foreach (var item in orderedMeals)
                            {
                                db.DeleteOrderedMeal(item);
                            }
                            db.DeleteOrder(orderID);
                            MessageBox.Show("You have successfully deleted the order.");
                            Orders = LoadOrders();
                        }
                    }
                    else
                    {
                        MessageBox.Show("[ERROR]");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //approving the order

        private ICommand approve;
        public ICommand Approve
        {
            get
            {
                if (approve == null)
                {
                    approve = new RelayCommand(param => ApproveOrderExecute(), param => CanApproveOrder());
                }
                return approve;
            }
        }

        private bool CanApproveOrder()
        {
            if (Order == null || Order.IsApproved == "approved" || Order.IsApproved == "rejected")
                return false;
            return true;
        }

        private void ApproveOrderExecute()
        {
            try
            {
                if (Order != null)
                {
                    orderID = Order.OrderID;
                    bool isExistingOrder = db.IsExistingOrder(orderID);

                    if (isExistingOrder == true)
                    {
                        var orderToUpdate = Orders.First(x => x.OrderID == orderID);
                        orderToUpdate.IsApproved = "approved";
                        db.UpdateOrder(orderToUpdate);
                        MessageBox.Show("You have successfully approved the order.");
                        Orders = LoadOrders();
                    }
                    else
                    {
                        MessageBox.Show("[ERROR]");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //rejecting the order

        private ICommand reject;
        public ICommand Reject
        {
            get
            {
                if (reject == null)
                {
                    reject = new RelayCommand(param => RejectOrderExecute(), param => CanRejectOrder());
                }
                return reject;
            }
        }

        private bool CanRejectOrder()
        {
            if (Order == null || Order.IsApproved == "approved" || Order.IsApproved == "rejected")
                return false;
            return true;
        }

        private void RejectOrderExecute()
        {
            try
            {
                if (Order != null)
                {
                    orderID = Order.OrderID;
                    bool isExistingOrder = db.IsExistingOrder(orderID);

                    if (isExistingOrder == true)
                    {
                        var orderToUpdate = Orders.First(x => x.OrderID == orderID);
                        orderToUpdate.IsApproved = "rejected";
                        db.UpdateOrder(orderToUpdate);
                        MessageBox.Show("You have successfully rejected the order.");
                        Orders = LoadOrders();
                    }
                    else
                    {
                        MessageBox.Show("[ERROR]");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //logging out

        private ICommand logout;
        public ICommand Logout
        {
            get
            {
                if (logout == null)
                {
                    logout = new RelayCommand(param => ExitExecute(), param => CanExitExecute());
                }
                return logout;
            }
        }

        private bool CanExitExecute()
        {
            return true;
        }

        private void ExitExecute()
        {
            MainWindow loginWindow = new MainWindow();
            view.Close();
            loginWindow.Show();
        }
        #endregion
    }
}
