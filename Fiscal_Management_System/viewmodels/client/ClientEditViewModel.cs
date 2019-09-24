using Fiscal_Management_System.model;
using Fiscal_Management_System.model.client;
using Fiscal_Management_System.model.revenue;

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

        public override void OperateOnDatabase(Client entity)
        {
            Context.Entry(Context.Set<Client>().Find(entity.ID)).CurrentValues.SetValues(entity);
        }

        public ClientEditViewModel(Client c)
        {
            StateManager = new StateManager();
            RevenueManager = new RevenueManager();
            Entity = new Client(c);
            ButtonText = "Edytuj";
            WindowTitle = "Edytowanie kontrahenta";
        }

        public ClientEditViewModel(IDbContext context, Client c) : this(c)
        {
            Context = context;
        }
    }
}
