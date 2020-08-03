using System.Collections.Generic;
using System.Linq;

namespace Pizza_app.Model
{
	class DataAccess
    {
		internal List<string> LoadPersonalNumbers()
		{
			using (var context = new PizzaRestaurantEntities())
			{
				var personalNumbers = new List<string>();
				if (context.tblOrders.Any())
				{
					foreach (var item in context.tblOrders)
					{
						personalNumbers.Add(item.OrdererPersonalNo);
					}
					return personalNumbers;
				}
				return personalNumbers;
			}
		}

		internal List<tblMeal> LoadMeals()
		{
			using (var context = new PizzaRestaurantEntities())
			{
				var meals = new List<tblMeal>();

				if (context.tblMeals.Any())
				{
					foreach (var item in context.tblMeals)
					{
						meals.Add(item);
					}
					return meals;
				}
				return meals;
			}
		}

		internal bool IsExistingMeal(int mealID)
		{
			using (var context = new PizzaRestaurantEntities())
			{
				return context.tblMeals.Any(e => e.MealID == mealID);
			}
		}

		internal List<tblOrder> LoadAllOrders()
		{
			using (var context = new PizzaRestaurantEntities())
			{
				var orders = new List<tblOrder>();

				if (context.tblOrders.Any())
				{
					foreach (var item in context.tblOrders)
					{
						orders.Add(item);
					}
					return orders;
				}
				return orders;
			}
		}

		internal List<tblOrder> LoadOrders(string ordererPersonalNo)
		{
			using (var context = new PizzaRestaurantEntities())
			{
				var orders = new List<tblOrder>();

				if (context.tblOrders.Any(x => x.OrdererPersonalNo == ordererPersonalNo))
				{
					foreach (var item in context.tblOrders.Where(x => x.OrdererPersonalNo == ordererPersonalNo))
					{
						orders.Add(item);
					}
					return orders;
				}
				return orders;
			}
		}

		internal void AddNewOrder(tblOrder newOrder)
		{
			using (var conn = new PizzaRestaurantEntities())
			{
				conn.tblOrders.Add(newOrder);
				conn.SaveChanges();
			}
		}

		internal List<tblOrder> LoadOrders()
		{
			using (var conn = new PizzaRestaurantEntities())
			{
				var orders = new List<tblOrder>();
				if (conn.tblOrders.Any())
					return conn.tblOrders.ToList();
				return orders;
			}
		}

		internal void AddNewOrderedMeal(tblMealOrder mealOrder)
		{
			using (var conn = new PizzaRestaurantEntities())
			{
				conn.tblMealOrders.Add(mealOrder);
				conn.SaveChanges();
			}
		}

		internal bool IsExistingOrder(int orderID)
		{
			using (var context = new PizzaRestaurantEntities())
			{
				return context.tblOrders.Any(e => e.OrderID == orderID);
			}
		}

		internal void DeleteOrder(int orderID)
		{
			using (var context = new PizzaRestaurantEntities())
			{
				var orderToRemove = context.tblOrders.FirstOrDefault(e => e.OrderID == orderID);
				if (orderToRemove != null)
				{
					context.tblOrders.Remove(orderToRemove);
					context.SaveChanges();
				}
			}
		}

		internal List<tblMealOrder> LoadOrderedMeals(int orderID)
		{
			using (var conn = new PizzaRestaurantEntities())
			{
				var orderedMeals = new List<tblMealOrder>();
				if (conn.tblOrders.Any(x => x.OrderID == orderID))
				{
					foreach (var item in conn.tblMealOrders.Where(x => x.OrderID == orderID))
					{
						orderedMeals.Add(item);
					}
					return orderedMeals;
				}

				return orderedMeals;
			}
		}

		internal void DeleteOrderedMeal(tblMealOrder mealOrder)
		{
			using (var context = new PizzaRestaurantEntities())
			{
				var orderToRemove = context.tblMealOrders.FirstOrDefault(e => e.MealOrderID == mealOrder.MealOrderID);
				if (orderToRemove != null)
				{
					context.tblMealOrders.Remove(orderToRemove);
					context.SaveChanges();
				}
			}
		}

		internal void UpdateOrder(tblOrder orderToUpdate)
		{
			using (var context = new PizzaRestaurantEntities())
			{
				context.tblOrders.First(e => e.OrderID == orderToUpdate.OrderID).IsApproved = orderToUpdate.IsApproved;

				context.SaveChanges();
			}
		}
	}
}
