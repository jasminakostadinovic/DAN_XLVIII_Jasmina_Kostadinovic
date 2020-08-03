using Pizza_app.Command;
using Pizza_app.View.Orders;
using System.Windows.Input;

namespace Pizza_app.ViewModel.Employees
{
    class DeleteEmployeeViewModel : ViewModelBase
    {
        #region Fields
        private DeleteOrderView deleteOrder;
        #endregion

        #region Properties
        public bool ShouldDelete { get; set; }
        #endregion

        #region Constructors
        public DeleteEmployeeViewModel(DeleteOrderView deleteOrder)
        {
            this.deleteOrder = deleteOrder;

        }
        #endregion

        #region Commands
        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }

        private bool CanSaveExecute()
        {
            return true;
        }

        private void SaveExecute()
        {
            ShouldDelete = true;
            deleteOrder.Close();
        }

        private ICommand exit;
        public ICommand Exit
        {
            get
            {
                if (exit == null)
                {
                    exit = new RelayCommand(param => ExitExecute(), param => CanExitExecute());
                }
                return exit;
            }
        }

        private bool CanExitExecute()
        {
            return true;
        }

        private void ExitExecute()
        {
            ShouldDelete = false;
            deleteOrder.Close();
        }
        #endregion
    }
}
