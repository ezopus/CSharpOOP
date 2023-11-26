using EDriveRent.Core.Contracts;
using EDriveRent.Models;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories;
using EDriveRent.Utilities.Messages;
using System.Linq;
using System.Text;

namespace EDriveRent.Core
{
    public class Controller : IController
    {
        private UserRepository users;
        private VehicleRepository vehicles;
        private RouteRepository routes;

        public Controller()
        {
            users = new UserRepository();
            vehicles = new VehicleRepository();
            routes = new RouteRepository();
        }
        public string RegisterUser(string firstName, string lastName, string drivingLicenseNumber)
        {
            if (users.GetAll().Any(u => u.DrivingLicenseNumber == drivingLicenseNumber))
            {
                return string.Format(OutputMessages.UserWithSameLicenseAlreadyAdded, drivingLicenseNumber);
            }

            IUser currentUser = new User(firstName, lastName, drivingLicenseNumber);
            users.AddModel(currentUser);
            return string.Format(OutputMessages.UserSuccessfullyAdded, firstName, lastName, drivingLicenseNumber);
        }

        public string UploadVehicle(string vehicleType, string brand, string model, string licensePlateNumber)
        {
            if (vehicleType != nameof(CargoVan) && vehicleType != nameof(PassengerCar))
            {
                return string.Format(OutputMessages.VehicleTypeNotAccessible, vehicleType);
            }

            if (vehicles.FindById(licensePlateNumber) != null)
            {
                return string.Format(OutputMessages.LicensePlateExists, licensePlateNumber);
            }

            IVehicle currentVehicle;
            if (vehicleType == nameof(CargoVan))
            {
                currentVehicle = new CargoVan(brand, model, licensePlateNumber);
            }
            else
            {
                currentVehicle = new PassengerCar(brand, model, licensePlateNumber);
            }

            vehicles.AddModel(currentVehicle);
            return string.Format(OutputMessages.VehicleAddedSuccessfully, brand, model, licensePlateNumber);
        }

        public string AllowRoute(string startPoint, string endPoint, double length)
        {
            int routeId = routes.GetAll().Count + 1;
            if (routes.GetAll().Any(r => r.StartPoint == startPoint && r.EndPoint == endPoint && r.Length == length))
            {
                return string.Format(OutputMessages.RouteExisting, startPoint, endPoint, length);
            }

            if (routes.GetAll().Any(r => r.StartPoint == startPoint && r.EndPoint == endPoint && r.Length < length))
            {
                return string.Format(OutputMessages.RouteIsTooLong, startPoint, endPoint);
            }

            IRoute currentRoute = new Route(startPoint, endPoint, length, routeId);
            routes.AddModel(currentRoute);

            IRoute longerRoute = routes.GetAll().FirstOrDefault(r => r.StartPoint == startPoint && r.EndPoint == endPoint && r.Length > length);
            if (longerRoute != null)
            {
                longerRoute.LockRoute();
            }

            return string.Format(OutputMessages.NewRouteAdded, startPoint, endPoint, length);
        }

        public string MakeTrip(string drivingLicenseNumber, string licensePlateNumber, string routeId, bool isAccidentHappened)
        {
            IUser currentUser = users.FindById(drivingLicenseNumber);
            if (currentUser.IsBlocked)
            {
                return string.Format(OutputMessages.UserBlocked, drivingLicenseNumber);
            }

            IVehicle currentVehicle = vehicles.FindById(licensePlateNumber);
            if (currentVehicle.IsDamaged)
            {
                return string.Format(OutputMessages.VehicleDamaged, licensePlateNumber);
            }

            IRoute currentRoute = routes.FindById(routeId);
            if (currentRoute.IsLocked)
            {
                return string.Format(OutputMessages.RouteLocked, routeId);
            }

            currentVehicle.Drive(currentRoute.Length);
            if (isAccidentHappened)
            {
                currentVehicle.ChangeStatus();
                currentUser.DecreaseRating();
            }
            else
            {
                currentUser.IncreaseRating();
            }

            return currentVehicle.ToString();

        }

        public string RepairVehicles(int count)
        {
            var damagedVehicles = vehicles.GetAll().Where(v => v.IsDamaged == true).OrderBy(v => v.Brand).ThenBy(v => v.Model).Take(count);
            int countOfRepairs = 0;
            foreach (var vehicle in damagedVehicles)
            {
                vehicle.ChangeStatus();
                vehicle.Recharge();
                countOfRepairs++;
            }

            return $"{countOfRepairs} vehicles are successfully repaired!";
        }

        public string UsersReport()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("*** E-Drive-Rent ***");
            foreach (var user in users.GetAll()
                         .OrderByDescending(u => u.Rating)
                         .ThenBy(u => u.LastName)
                         .ThenBy(u => u.FirstName))
            {
                sb.AppendLine(user.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
