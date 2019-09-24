using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Fiscal_Management_System.model;
using Fiscal_Management_System.model.devicemodel;
using Fiscal_Management_System.views.devicemodel;

namespace Fiscal_Management_System.viewmodels.devicemodel
{
    public class DeviceModelViewModel : EntityViewModel<DeviceModel>
    {
        public DeviceModelViewModel(Func<UserControl, int> ucSetMethod) : base(ucSetMethod)
        {
            EntitySearcher.Collection = GetAllDeviceModels();
        }

        public DeviceModelViewModel(Func<UserControl, int> ucSetMethod, IDbContext context) : base(ucSetMethod, context)
        {

        }

        public ObservableCollection<DeviceModel> GetAllDeviceModels()
        {
            ObservableCollection<DeviceModel> allDeviceModels;
            using (var ctx = (Context == null ? new FiscalDbContext() : Context))
            {
                allDeviceModels = new ObservableCollection<DeviceModel>(ctx.Set<DeviceModel>().Include("Devices").ToList());
            }

            return allDeviceModels;
        }

        private ICommand _goToAddDeviceModelButtonCommand;
        public ICommand GoToAddDeviceModelButtonCommand
        {
            get
            {
                _goToAddDeviceModelButtonCommand = new RelayCommand(o =>
                {
                    AddDeviceModel addDeviceModelWindow = new AddDeviceModel();
                    if ((bool)addDeviceModelWindow.ShowDialog())
                    {
                        MessageBox.Show("Dodano model urządzenia!");
                        EntitySearcher.Collection = GetAllDeviceModels();
                    }

                }, o => true);

                return _goToAddDeviceModelButtonCommand;
            }
        }

        private ICommand _goToRemoveDeviceModelButtonCommand;
        public ICommand GoToRemoveDeviceModelButtonCommand
        {
            get
            {
                _goToRemoveDeviceModelButtonCommand = new RelayCommand(o =>
                {
                    DeviceModel r = new DeviceModel((DeviceModel)o);
                    MessageBoxResult result =
                        MessageBox.Show(string.Format("Czy na pewno chcesz usunąć {0}", r.Name),
                            "Ostrzeżenie",
                            MessageBoxButton.YesNoCancel
                        );

                    if (result == MessageBoxResult.Yes)
                    {
                        if (r.Devices.Count > 0)
                        {
                            MessageBox.Show("Ten model jest przypisany do urządzeń. " +
                                            "Najpierw usuń urządzenia przypisanego do tego modelu");
                        }
                        else
                        {
                            using (var ctx = Context == null ? new FiscalDbContext() : Context)
                            {
                                ctx.Set<DeviceModel>().Attach(r);
                                ctx.Set<DeviceModel>().Remove(r);
                                ctx.SaveChanges();
                                EntitySearcher.Collection = GetAllDeviceModels();
                            }
                        }
                    }
                }, o => true);

                return _goToRemoveDeviceModelButtonCommand;
            }
        }


    }
}
