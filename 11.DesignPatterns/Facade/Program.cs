using FacadePattern;

Car car = new CarBuilderFacade()
    .Info
        .WithType("VW")
        .WithColor("Black")
        .WithNumberOfDoors(5)
    .Built
        .InCity("Wolfsburg")
        .AtAddress("Heinzstrasse 69")
    .Build();

Console.WriteLine(car);

Car car2 = new CarBuilderFacade()
    .Info
        .WithType("Opel")
    .Built
        .InCity("Bayern")
    .Build();

Console.WriteLine(car2);