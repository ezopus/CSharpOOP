using CompositePattern;

CompositeGift rootBox = new CompositeGift("Root box", 0);
SingleGift truck = new SingleGift("Truck", 200);
SingleGift car = new SingleGift("Car", 100);
rootBox.Add(truck);
rootBox.Add(car);

CompositeGift childBox = new CompositeGift("Child box", 0);
SingleGift plane = new SingleGift("Plane", 300);
childBox.Add(plane);

rootBox.Add(childBox);

Console.WriteLine($"Total price of this composite present is: {rootBox.CalculateTotalPrice()}");