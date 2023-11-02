using ShoppingSpree.Models;

string[] peopleInput = Console
    .ReadLine()
    .Split(new[] { '=', ';'}, StringSplitOptions.RemoveEmptyEntries);
string[] productInput = Console
    .ReadLine()
    .Split(new[] { '=', ';' }, StringSplitOptions.RemoveEmptyEntries);

List<Person> people = new List<Person>();
List<Product> products = new List<Product>();

try
{
    for (int i = 0; i < peopleInput.Length - 1; i += 2)
    {
        Person person = new Person(peopleInput[i], decimal.Parse(peopleInput[i + 1]));
        people.Add(person);
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    return;
}

try
{
    for (int i = 0; i < productInput.Length - 1; i += 2)
    {
        Product product = new Product(productInput[i], decimal.Parse(productInput[i+1]));
        products.Add(product);
    }
}
catch (Exception e)
{
    Console.WriteLine(e);
    return;
}

string input;
while ((input = Console.ReadLine()) != "END")
{
    string[] tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    string buyer = tokens[0];
    string item = tokens[1];

    Person currentBuyer = people.Find(x => x.Name == buyer);
    Product currentItem = products.Find(x => x.Name == item);
    if (currentBuyer.Money >= currentItem.Cost)
    {
        currentBuyer.Products.Add(currentItem);
        currentBuyer.Money -= currentItem.Cost;
    }
    else
    {
        Console.WriteLine($"{currentBuyer.Name} can't afford {currentItem.Name}");
    }
}

foreach (var person in people)
{
    if (person.Products.Count > 0)
    {
        Console.WriteLine(person);
    }
    else
    {
        Console.WriteLine($"{person.Name} - Nothing bought");
    }
}

