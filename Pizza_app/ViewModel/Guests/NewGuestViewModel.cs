using Pizza_app.Command;
using Pizza_app.Model;
using Pizza_app.View.Guests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Pizza_app.ViewModel.Guests
{
    class NewGuestViewModel : ViewModelBase
    {
        #region Fields
        private readonly NewGuestView view;
        private tblMeal meal;
        private List<tblMeal> meals;
        private tblMeal selectedMeal;
        private readonly DataAccess db = new DataAccess();
        private decimal totalAmount;
        private tblOrder newOrder;
        private string ordererPersonalNo;
        int mealID;
        private List<tblMealOrder> orderedMeals;
        #endregion

        #region Constructors
        internal NewGuestViewModel(NewGuestView view, string ordererPersonalNo)
        {
            this.view = view;
            Meals = LoadMeals();
            selectedMeal = new tblMeal();
            Meal = new tblMeal();
            newOrder = new tblOrder();
            orderedMeals = new List<tblMealOrder>();
            this.ordererPersonalNo = ordererPersonalNo;
        }
        #endregion

        #region Properties            

        public decimal TotalAmount
        {
            get
            {
                return totalAmount;
            }
            set
            {
                totalAmount = value;
                OnPropertyChanged(nameof(TotalAmount));
            }
        }
        public tblMeal Meal
        {
            get
            {
                return meal;
            }
            set
            {
                meal = value;
                OnPropertyChanged(nameof(Meal));
            }
        }
        public List<tblMeal> Meals
        {
            get
            {
                return meals;
            }
            set
            {
                meals = value;
                OnPropertyChanged(nameof(Meals));
            }
        }
        #endregion

        #region Methods
        private List<tblMeal> LoadMeals()
        {
            try
            {
                return db.LoadMeals();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        #endregion

        #region Commands
        //increasing the number of meals
        private ICommand addOneItem;
        public ICommand AddOneItem
        {
            get
            {
                if (addOneItem == null)
                {
                    addOneItem = new RelayCommand(param => AddOneItemExecute(), param => CanAddOneItem());
                }
                return addOneItem;
            }
        }

        private bool CanAddOneItem()
        {
            if (Meal == null)
                return false;
            return true;
        }

        private void AddOneItemExecute()
        {
            try
            {
                if (Meal != null)
                {
                    mealID = Meal.MealID;
                    bool isExistingMeal = db.IsExistingMeal(mealID);

                    if (isExistingMeal == true)
                    {
                        if (!orderedMeals.Any(x => x.MealID == mealID))
                        {
                            var orderedMeal = new tblMealOrder();
                            orderedMeal.Count++;
                            orderedMeal.MealID = Meal.MealID;
                            TotalAmount += (decimal)Meal.Price;
                            orderedMeals.Add(orderedMeal);
                        }
                        else
                        {
                            orderedMeals.First(x => x.MealID == mealID).Count++;
                            TotalAmount += (decimal)Meal.Price;
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

        //reduce the number of meals

        private ICommand removeOneItem;
        public ICommand RemoveOneItem
        {
            get
            {
                if (removeOneItem == null)
                {
                    removeOneItem = new RelayCommand(param => RemoveOneItemExecute(), param => CanRemoveOneItem());
                }
                return removeOneItem;
            }
        }

        private bool CanRemoveOneItem()
        {
            if (Meal == null || !orderedMeals.Any(x => x.MealID == mealID))
                return false;
            return true;
        }

        private void RemoveOneItemExecute()
        {
            try
            {
                if (Meal != null)
                {
                    mealID = Meal.MealID;
                    bool isExistingMeal = db.IsExistingMeal(mealID);

                    if (isExistingMeal == true)
                    {
                        orderedMeals.First(x => x.MealID == mealID).Count--;
                        TotalAmount -= (decimal)Meal.Price;
                        if (orderedMeals.First(x => x.MealID == mealID).Count == 0)
                        {
                            orderedMeals.Remove(orderedMeals.First(x => x.MealID == mealID));
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

        //submiting the order

        private ICommand submitOrder;
        public ICommand SubmitOrder
        {
            get
            {
                if (submitOrder == null)
                {
                    submitOrder = new RelayCommand(param => SubmitOrderExecute(), param => CanSubmitOrderEmployee());
                }
                return submitOrder;
            }
        }

        private void SubmitOrderExecute()
        {
            try
            {
                newOrder.DateOfOrder = DateTime.Now;
                newOrder.IsApproved = "on hold";
                newOrder.OrdererPersonalNo = ordererPersonalNo;
                db.AddNewOrder(newOrder);

                var orders = db.LoadOrders();
                var idOrder = orders.Last().OrderID;
                foreach (var item in orderedMeals)
                {
                    item.OrderID = idOrder;
                    db.AddNewOrderedMeal(item);
                }
                MessageBox.Show("Your order is successfully created!");
                MainWindow loginWindow = new MainWindow();
                view.Close();
                loginWindow.Show();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSubmitOrderEmployee()
        {
            if (orderedMeals.Any())
                return true;
            return false;
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
