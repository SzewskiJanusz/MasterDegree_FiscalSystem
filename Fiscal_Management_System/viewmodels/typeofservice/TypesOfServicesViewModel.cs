using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Fiscal_Management_System.model;
using Fiscal_Management_System.model.service;
using Fiscal_Management_System.views.typeofservice;

namespace Fiscal_Management_System.viewmodels.typeofservice
{
    public class TypesOfServicesViewModel : EntityViewModel<TypeOfService>
    {
        public TypesOfServicesViewModel(Func<UserControl, int> ucSetMethod) : base(ucSetMethod)
        {
            EntitySearcher.Collection = GetAllTypesOfServices();
        }

        public TypesOfServicesViewModel(Func<UserControl, int> ucSetMethod, IDbContext context) : base(ucSetMethod, context)
        {
        }

        public ObservableCollection<TypeOfService> GetAllTypesOfServices()
        {
            ObservableCollection<TypeOfService> allTypeOfServices;
            using (var ctx = (Context == null ? new FiscalDbContext() : Context))
            {
                allTypeOfServices = new ObservableCollection<TypeOfService>(ctx.Set<TypeOfService>().Include("Services").ToList());
            }

            return allTypeOfServices;
        }

        private ICommand _goToAddTypeOfServiceButtonCommand;
        public ICommand GoToAddTypeOfServiceButtonCommand
        {
            get
            {
                _goToAddTypeOfServiceButtonCommand = new RelayCommand(o =>
                {
                    AddTypeOfService addTosWindow = new AddTypeOfService();
                    if ((bool)addTosWindow.ShowDialog())
                    {
                        MessageBox.Show("Dodano typ usługi!");
                        EntitySearcher.Collection = GetAllTypesOfServices();
                    }

                }, o => true);

                return _goToAddTypeOfServiceButtonCommand;
            }
        }

        private ICommand _goToRemoveTypeOfServiceButtonCommand;
        public ICommand GoToRemoveTypeOfServiceButtonCommand
        {
            get
            {
                _goToRemoveTypeOfServiceButtonCommand = new RelayCommand(o =>
                {
                    TypeOfService r = new TypeOfService((TypeOfService)o);
                    MessageBoxResult result =
                        MessageBox.Show(string.Format("Czy na pewno chcesz usunąć {0}", r.Name),
                            "Ostrzeżenie",
                            MessageBoxButton.YesNoCancel
                        );

                    if (result == MessageBoxResult.Yes)
                    {
                        if (r.Services.Count > 0)
                        {
                            MessageBox.Show("Ten typ usługi jest przypisany do usług. " +
                                            "Najpierw usuń usługi przypisanych do tego typu usługi");
                        }
                        else
                        {
                            using (var ctx = Context == null ? new FiscalDbContext() : Context)
                            {
                                ctx.Set<TypeOfService>().Attach(r);
                                ctx.Set<TypeOfService>().Remove(r);
                                ctx.SaveChanges();
                                EntitySearcher.Collection = GetAllTypesOfServices();
                            }
                        }
                    }
                }, o => true);

                return _goToRemoveTypeOfServiceButtonCommand;
            }
        }
    }
}
