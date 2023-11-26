using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;
using System.Text;

namespace EDriveRent.Models
{
    public abstract class Vehicle : IVehicle
    {
        private string brand;
        private string model;
        private string licensePlate;
        private double maxMileage;

        public Vehicle(string brand, string model, double maxMileage, string licensePlate)
        {
            Brand = brand;
            Model = model;
            MaxMileage = maxMileage;
            LicensePlateNumber = licensePlate;
            BatteryLevel = 100;
            IsDamaged = false;
        }
        public string Brand
        {
            get => brand;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.BrandNull);
                }
                brand = value;
            }
        }
        public string Model
        {
            get => model;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.ModelNull);
                }
                model = value;
            }
        }
        public double MaxMileage
        {
            get => maxMileage;
            private set => maxMileage = value;
        }
        public string LicensePlateNumber
        {
            get => licensePlate;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.LicenceNumberRequired);
                }
                licensePlate = value;
            }
        }
        public int BatteryLevel { get; private set; }
        public bool IsDamaged { get; private set; }
        public void Drive(double mileage)
        {
            if (this.GetType().Name == nameof(CargoVan))
            {
                mileage *= 1.05;
            }

            BatteryLevel -= (int)Math.Round(mileage * 100 / MaxMileage);
        }

        public void Recharge()
        {
            BatteryLevel = 100;
        }

        public void ChangeStatus()
        {
            if (IsDamaged)
            {
                IsDamaged = false;
            }
            else
            {
                IsDamaged = true;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Brand} {Model} License plate: {LicensePlateNumber} Battery: {BatteryLevel}% Status: ");
            if (IsDamaged)
            {
                sb.Append("damaged");
            }
            else
            {
                sb.Append("OK");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
