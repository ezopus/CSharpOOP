using PrototypePattern;

SandwichMenu sandwichMenu = new SandwichMenu();

sandwichMenu["BLT"] = new Sandwich("Wheat", "Bacon", "", "Lettuce, Tomato");
sandwichMenu["PBJ"] = new Sandwich("White", "", "", "Peanut butter, Jelly");
sandwichMenu["Turkey"] = new Sandwich("Rye", "Turkey", "Swiss", "Lettuce, Onion, Tomato");

sandwichMenu["LoadedBLT"] = new Sandwich("Wheat", "Turkey, Bacon", "American", "Lettuce, Tomato, Onion, Olives");
sandwichMenu["Vegetarian"] = new Sandwich("Wholewheat", "", "", "Lettuce, Tomato, Onion, Olives, Spinach");

Sandwich? sandwichOne = sandwichMenu["BLT"].Clone() as Sandwich;
Sandwich? sandwichTwo = sandwichMenu["Vegetarian"].Clone() as Sandwich;
Sandwich? sandwichThree = sandwichMenu["LoadedBLT"].Clone() as Sandwich;