using Pizza_app.Command;
using Pizza_app.Model;
using Pizza_app.View;
using Pizza_app.View.Employees;
using Pizza_app.View.Guests;
using Pizza_app.View.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Validations;

namespace Pizza_app.ViewModel
{
    class MainWindowViewModel : ViewModelBase
	{
		#region Fields
		private string userName;
		readonly MainWindow loginView;
		#endregion

		#region Constructor
		internal MainWindowViewModel(MainWindow view)
		{
			this.loginView = view;
		}
		#endregion

		#region Properties
		public string UserName
		{
			get
			{
				return userName;
			}
			set
			{
				userName = value;
				OnPropertyChanged(nameof(UserName));
			}
		}
		#endregion

		private ICommand submitCommand;
		public ICommand SubmitCommand
		{
			get
			{
				if (submitCommand == null)
				{
					submitCommand = new RelayCommand(Submit);
					return submitCommand;
				}
				return submitCommand;
			}
		}

		void Submit(object obj)
		{
			string password = (obj as PasswordBox).Password;
			var validate = new Validations.Validations();
			if (UserName == "Zaposleni" && password == "Zaposleni")
			{
				EmployeeView employeeView = new EmployeeView(UserName);
				loginView.Close();
				employeeView.Show();
				return;
			}

			else if (validate.IsValidPersonalNoFormat(UserName) && password == "Gost")
			{

				DataAccess dataAccess = new DataAccess();
				if (validate.IsPersonalNoInDb(UserName, dataAccess.LoadPersonalNumbers()))
				{
					OrderStatusView oldQuestView = new OrderStatusView(UserName);

					//loginView.Close();
					oldQuestView.Show();
					var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
					timer.Start();
					timer.Tick += (sender, args) =>
					{
						timer.Stop();
						NewGuestView guestView = new NewGuestView(UserName);
						loginView.Close();
						oldQuestView.Close();
						guestView.Show();
					};

					return;
				}
				NewGuestView newGuestView = new NewGuestView(UserName);
				loginView.Close();
				newGuestView.Show();
				return;
			}
			else
			{
				WarningView warning = new WarningView(loginView);
				warning.Show("User name or password are not correct!");
				return;
			}
		}
	}
}
