using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DrawerSample.Models;

namespace DrawerSample.ViewModels {
    public class MainViewModel: INotifyPropertyChanged {
        private const int UnselectedIndex = -1;
        private static readonly IReadOnlyList<Vehicle> allVehicles = new List<Vehicle> {
            new Vehicle("Mercedes-Benz", "SL500 Roadster"),
            new Vehicle("Mercedes-Benz", "CLK55 AMG Cabriolet"),
            new Vehicle("Mercedes-Benz", "C230 Kompressor Sport Coupe"),
            new Vehicle("BMW", "530i"),
            new Vehicle("Rolls-Royce", "Corniche"),
            new Vehicle("Jaguar", "S-Type 3.0"),
            new Vehicle("Cadillac", "Seville"),
            new Vehicle("Cadillac", "DeVille"),
            new Vehicle("Lexus", "LS430"),
            new Vehicle("Lexus", "GS430"),
            new Vehicle("Ford", "Ranger FX-4"),
            new Vehicle("Dodge", "RAM 1500"),
            new Vehicle("GMC", "Siera Quadrasteer"),
            new Vehicle("Nissan", "Crew Cab SE"),
            new Vehicle("Toyota", "Tacoma S-Runner"),
        };

        public IReadOnlyList<GroupedVehiclesViewModel> VehiclesByMake { get; }

        int selectedIndex = UnselectedIndex;
        public int SelectedIndex {
            get => selectedIndex;
            set {
                if (selectedIndex == value) return;
                if (selectedIndex != UnselectedIndex) {
                    VehiclesByMake[selectedIndex].IsSelected = false;
                }
                selectedIndex = value;
                if (selectedIndex != UnselectedIndex) {
                    VehiclesByMake[selectedIndex].IsSelected = true;
                }
                RaisePropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel() {
            List<GroupedVehiclesViewModel> groupedVehiclesVMs = new List<GroupedVehiclesViewModel>();
            groupedVehiclesVMs.Add(new GroupedVehiclesViewModel("All", allVehicles));

            IEnumerable<IGrouping<string, Vehicle>> groupedVehicles = allVehicles.GroupBy(v => v.MakeName);
            foreach (IGrouping<string, Vehicle>  vehiclesGroup in groupedVehicles) {
                groupedVehiclesVMs.Add(new GroupedVehiclesViewModel(vehiclesGroup.Key, vehiclesGroup));
            }

            VehiclesByMake = groupedVehiclesVMs;
        }

        private void RaisePropertyChanged([CallerMemberName] string caller = "") {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler.Invoke(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}
