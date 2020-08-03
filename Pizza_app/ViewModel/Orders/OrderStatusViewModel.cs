using Pizza_app.Model;
using Pizza_app.View.Orders;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Pizza_app.ViewModel.Orders
{
    class OrderStatusViewModel : ViewModelBase
    {
        #region Fields
        private readonly OrderStatusView view;
        private tblOrder order;
        private List<tblOrder> orders;
        private readonly string ordererPersonalNo;
        private readonly DataAccess db = new DataAccess();
        #endregion

        #region Constructors
        internal OrderStatusViewModel(OrderStatusView view, string ordererPersonalNo)
        {
            this.view = view;
            this.ordererPersonalNo = ordererPersonalNo;
            Orders = LoadOrders(ordererPersonalNo);
            Order = new tblOrder();

        }

        private List<tblOrder> LoadOrders(string ordererPersonalNo)
        {
            try
            {
                return db.LoadOrders(ordererPersonalNo);
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

    }
}
