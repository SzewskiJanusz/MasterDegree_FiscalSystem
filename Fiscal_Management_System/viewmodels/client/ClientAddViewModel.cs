﻿using Fiscal_Management_System.model;
using Fiscal_Management_System.model.client;
using Fiscal_Management_System.model.revenue;
using System.Data.Entity;
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

        public override void OperateOnDatabase(Client entity)
        {
            if (Context.Set<Revenue>() != null)
            {
                Revenue r;
                r = Context.Set<Revenue>().Where(x => x.ID == entity.Revenue.ID).FirstOrDefault();
                entity.Revenue = r;
            }
            Client c = new Client(entity);
            Context.Set<Client>().Add(c);
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

        public ClientAddViewModel(IDbContext context) : this()
        {
            Context = context;
        }
    }
}
