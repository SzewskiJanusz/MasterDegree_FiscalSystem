using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Fiscal_Management_System.model;

namespace Fiscal_Management_System.viewmodels
{
    public class EntityViewModel<T>
    {
        /// <summary>
        /// Context of EF
        /// </summary>
        public IDbContext Context;

        private EntitySearcher<T> _entitySearcher;
        public EntitySearcher<T> EntitySearcher
        {
            get { return _entitySearcher; }
            set { _entitySearcher = value; }
        }

        private T _entity;
        public T Entity { get { return _entity; } set { _entity = value; } }

        public Func<UserControl, int> UserControlSwitcher;

        public EntityViewModel(Func<UserControl, int> ucSetMethod)
        {
            UserControlSwitcher = ucSetMethod;
            EntitySearcher = new EntitySearcher<T>();
        }

        public EntityViewModel(Func<UserControl, int> ucSetMethod, IDbContext context)
        {
            this.Context = context;
            EntitySearcher = new EntitySearcher<T>();
        }
    }
}
