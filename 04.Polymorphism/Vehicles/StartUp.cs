
using Vehicles.Core;
using Vehicles.Core.Interfaces;
using Vehicles.Factories;
using Vehicles.Factories.Interfaces;
using Vehicles.IO;
using Vehicles.IO.Interfaces;

IReader reader = new Reader();
IWriter writer = new Writer();
IVehicleFactory vehicleFactory = new VehicleFactory();

IEngine engine = new Engine(reader, writer, vehicleFactory);
engine.Run();