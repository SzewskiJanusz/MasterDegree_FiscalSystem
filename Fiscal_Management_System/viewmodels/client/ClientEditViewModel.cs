using Fiscal_Management_System.model;
using Fiscal_Management_System.model.client;

namespace Fiscal_Management_System.viewmodels.client
{
    /// <summary>
    /// Viewmodel of client edition view
    /// </summary>
    public class ClientEditViewModel : AddEditOperationViewModel<Client>
    {
        private StateManager _stateManager;
        public StateManager StateManager { get { return _stateManager; } set { _stateManager = value; } }

        private RevenueManager _revenueManager;
        public RevenueManager RevenueManager { get { return _revenueManager; } set { _revenueManager = value; } }

        public override void Operation(Client entity)
        {
            using (var repo = new ClientRepository(new FiscalDbContext()))
            {
                repo.Edit(repo.Get(entity.ID), entity);
                repo.Save();
            }

            
        }

        public ClientEditViewModel(Client c)
        {
            StateManager = new StateManager();
            RevenueManager = new RevenueManager();
            Entity = c;
            ButtonText = "Edytuj";
            WindowTitle = "Edytowanie kontrahenta";
        }
    }
}
