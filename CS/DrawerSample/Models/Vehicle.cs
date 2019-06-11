using System;
namespace DrawerSample.Models {
    public class Vehicle {
        public string MakeName { get; }
        public string ModelName { get; }

        public string FullName => $"{MakeName} {ModelName}";

        public Vehicle(string make, string model) {
            this.MakeName = make;
            this.ModelName = model;
        }
    }
}
