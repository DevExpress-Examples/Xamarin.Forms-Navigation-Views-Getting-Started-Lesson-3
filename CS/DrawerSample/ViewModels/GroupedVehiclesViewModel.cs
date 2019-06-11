using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DrawerSample.Models;

namespace DrawerSample.ViewModels {
    public class GroupedVehiclesViewModel: INotifyPropertyChanged {
        public string GroupKey { get; }
        public IReadOnlyList<Vehicle> Vehicles { get; }

        private bool isSelected = false;
        public bool IsSelected {
            get => isSelected;
            set {
                if (value == isSelected) return;
                isSelected = value;
                RaisePropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public GroupedVehiclesViewModel(string groupKey, IEnumerable<Vehicle> vehicles) {
            if (String.IsNullOrEmpty(groupKey)) {
                this.GroupKey = String.Empty;
            } else {
                this.GroupKey = groupKey;
            }
            if (vehicles == null) {
                this.Vehicles = new List<Vehicle>();
            } else {
                this.Vehicles = vehicles.ToList();
            }
        }

        private void RaisePropertyChanged([CallerMemberName] string caller = "") {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler.Invoke(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}
