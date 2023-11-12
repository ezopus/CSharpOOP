using Vehicles.Core.Interfaces;
using Vehicles.Factories.Interfaces;
using Vehicles.IO.Interfaces;
using Vehicles.Models.Interfaces;

namespace Vehicles.Core
{
    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IVehicleFactory vehicleFactory;
        private readonly ICollection<IVehicle> vehicles;

        public Engine(IReader reader, IWriter writer, IVehicleFactory vehicleFactory)
        {
            this.reader = reader;
            this.writer = writer;
            this.vehicleFactory = vehicleFactory;
            vehicles = new List<IVehicle>();
        }
        public void Run()
        {
            vehicles.Add(CreateVehicle());
            vehicles.Add(CreateVehicle());
            vehicles.Add(CreateVehicle());

            int numberOfCommands = int.Parse(reader.ReadLine());

            for (int i = 0; i < numberOfCommands; i++)
            {
                try
                {
                    ProcessCommand();
                }
                catch (Exception e)
                {
                    writer.WriteLine(e.Message);
                }
            }

            foreach (var vehicle in vehicles)
            {
                writer.WriteLine(vehicle.ToString());
            }

        }

        private IVehicle CreateVehicle()
        {
            string[] tokens = reader.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return vehicleFactory.Create(tokens[0], double.Parse(tokens[1]), double.Parse(tokens[2]), double.Parse(tokens[3]));

        }

        private void ProcessCommand()
        {
            string[] tokens = reader.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string command = tokens[0];
            string vehicleType = tokens[1];

            IVehicle current = vehicles.FirstOrDefault(x => x.GetType().Name == vehicleType);

            if (command == "Drive")
            {
                writer.WriteLine(current.Drive(double.Parse(tokens[2])));
            }
            else if (command == "DriveEmpty")
            {
                writer.WriteLine(current.Drive(double.Parse(tokens[2]), false));
            }
            else if (command == "Refuel")
            {
                current.Refuel(double.Parse(tokens[2]));
            }
            else
            {
                writer.WriteLine("Invalid vehicle");
            }
        }
    }
}



