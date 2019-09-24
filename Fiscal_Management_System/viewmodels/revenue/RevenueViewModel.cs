using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Fiscal_Management_System.model;
using Fiscal_Management_System.model.revenue;
using Fiscal_Management_System.views.revenue;

namespace Fiscal_Management_System.viewmodels.revenue
{
    public class RevenueViewModel : EntityViewModel<Revenue>
    {
        public RevenueViewModel(Func<UserControl, int> ucSetMethod) : base(ucSetMethod)
        {
            EntitySearcher.Collection = GetAllRevenues();
        }

        public RevenueViewModel(Func<UserControl, int> ucSetMethod, IDbContext context) : base(ucSetMethod, context)
        {

        }

        public ObservableCollection<Revenue> GetAllRevenues()
        {
            ObservableCollection<Revenue> allRevenues;
            using (var ctx = (Context == null ? new FiscalDbContext() : Context))
            {
                allRevenues = new ObservableCollection<Revenue>(ctx.Set<Revenue>().Include("Clients").ToList());
            }

            return allRevenues;
        }

        private ICommand _goToAddRevenueButtonCommand;
        public ICommand GoToAddRevenueButtonCommand
        {
            get
            {
                _goToAddRevenueButtonCommand = new RelayCommand(o =>
                {
                    AddRevenue addRevenueWindow = new AddRevenue();
                    if ((bool)addRevenueWindow.ShowDialog())
                    {
                        MessageBox.Show("Dodano urząd skarbowy!");
                        EntitySearcher.Collection = GetAllRevenues();
                    }

                }, o => true);

                return _goToAddRevenueButtonCommand;
            }
        }

        private ICommand _goToRemoveRevenueButtonCommand;
        public ICommand GoToRemoveRevenueButtonCommand
        {
            get
            {
                _goToRemoveRevenueButtonCommand = new RelayCommand(o =>
                {
                    Revenue r = new Revenue((Revenue) o);
                    MessageBoxResult result =
                        MessageBox.Show(string.Format("Czy na pewno chcesz usunąć {0}", r.Name),
                            "Ostrzeżenie",
                            MessageBoxButton.YesNoCancel
                        );

                    if (result == MessageBoxResult.Yes)
                    {
                        if (r.Clients.Count > 0)
                        {
                            MessageBox.Show("Ten urząd skarbowy jest przypisany do użytkowników. " +
                                            "Najpierw usuń użytkowników przypisanych do tego urzędu skarbowego");
                        }
                        else
                        {
                            using (var ctx = Context == null ? new FiscalDbContext() : Context)
                            {
                                ctx.Set<Revenue>().Attach(r);
                                ctx.Set<Revenue>().Remove(r);
                                ctx.SaveChanges();
                                EntitySearcher.Collection = GetAllRevenues();
                            }
                        }
                    }
                }, o => true);

                return _goToRemoveRevenueButtonCommand;
            }
        }
    }
}
