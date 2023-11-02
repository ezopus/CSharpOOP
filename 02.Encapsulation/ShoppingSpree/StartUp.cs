using ShoppingSpree.Models;

List<Person> people = new List<Person>();
List<Product> products = new List<Product>();

string[] peopleInput = Console
    .ReadLine()
    .Split(new[] { '=', ';' }, StringSplitOptions.RemoveEmptyEntries);
string[] productInput = Console
    .ReadLine()
    .Split(new[] { '=', ';' }, StringSplitOptions.RemoveEmptyEntries);


try
{
    for (int i = 0; i < peopleInput.Length - 1; i += 2)
    {
        Person person = new Person(peopleInput[i], decimal.Parse(peopleInput[i + 1]));
        people.Add(person);
    }
    for (int i = 0; i < productInput.Length - 1; i += 2)
    {
        Product product = new Product(productInput[i], decimal.Parse(productInput[i + 1]));
        products.Add(product);
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    return;
}

string input;
while ((input = Console.ReadLine()) != "END")
{
    string[] tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    string buyer = tokens[0];
    string item = tokens[1];

    Person currentBuyer = people.FirstOrDefault(x => x.Name == buyer);
    Product currentItem = products.FirstOrDefault(x => x.Name == item);

    if (currentBuyer != null && currentItem != null)
    {
        Console.WriteLine(currentBuyer.Add(currentItem));
    }
}

foreach (var person in people)
{
    Console.WriteLine(person);
}

