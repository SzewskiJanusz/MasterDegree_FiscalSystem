using Fiscal_Management_System.model;
using Fiscal_Management_System.model.client;
using Fiscal_Management_System.model.revenue;
using System.Linq;

namespace Fiscal_Management_System.viewmodels.client
{
    /// <summary>
    /// Viewmodel of client addition view
    /// </summary>
    public class ClientAddViewModel : AddEditOperationViewModel<Client>
    {
        private StateManager _stateManager;
        public StateManager StateManager { get { return _stateManager; } set { _stateManager = value; } }

        private RevenueManager _revenueManager;
        public RevenueManager RevenueManager { get { return _revenueManager; } set { _revenueManager = value; } }

        public override void Operation(Client entity)
        {
            Revenue r;
            using (var ctx = new FiscalDbContext())
            {
                if (entity.Revenue == null)
                {
                    r = ctx.Revenues.Where(x => x.ID == entity.Revenue.ID).FirstOrDefault();
                    entity.Revenue = r;
                }
                Client c = new Client(entity);
                ctx.Clients.Add(c);
                ctx.SaveChanges();
            }
        }

        public ClientAddViewModel()
        {
            StateManager = new StateManager();
            // Default
            Entity = new Client();
            Entity.State = "zachodniopomorskie";

            RevenueManager = new RevenueManager();

            ButtonText = "Dodaj";
            WindowTitle = "Dodawanie kontrahenta";
        }
    }
}
