using CommandPattern;

var modifyPrice = new ModifyPrice();
var product = new Product("Phone", 1000);

Execute(product, modifyPrice, new ProductCommand(product, PriceAction.Increase, 100));

Execute(product, modifyPrice, new ProductCommand(product, PriceAction.Increase, 50));

Execute(product, modifyPrice, new ProductCommand(product, PriceAction.Decrease, 25));

Console.WriteLine(product);
void Execute(Product product1, ModifyPrice modifyPrice1, ProductCommand productCommand)
{
    modifyPrice.SetCommand(productCommand);
    modifyPrice.Invoke();
}

