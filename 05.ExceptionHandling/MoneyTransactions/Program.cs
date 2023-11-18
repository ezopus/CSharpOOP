string input = Console.ReadLine();

string[] splitInput = input.Split(new char[] { '-', ',' }, StringSplitOptions.RemoveEmptyEntries);

Dictionary<int, double> accounts = new();

for (int i = 0; i < splitInput.Length - 1; i += 2)
{
    accounts.Add(int.Parse(splitInput[i]), double.Parse(splitInput[i + 1]));
}

while ((input = Console.ReadLine()) != "End")
{
    try
    {
        string[] tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string command = tokens[0];
        int inputAccount = int.Parse(tokens[1]);
        double inputAmount = double.Parse(tokens[2]);

        switch (command)
        {
            case "Deposit":
                CheckAccount(inputAccount, accounts);
                accounts[inputAccount] += inputAmount;
                Console.WriteLine($"Account {inputAccount} has new balance: {accounts[inputAccount]:f2}");
                break;
            case "Withdraw":
                CheckAccount(inputAccount, accounts);
                if (accounts[inputAccount] < inputAmount)
                {
                    throw new ArgumentException("Insufficient balance!");
                }

                accounts[inputAccount] -= inputAmount;
                Console.WriteLine($"Account {tokens[1]} has new balance: {accounts[inputAccount]:f2}");
                break;

            default:
                throw new ArgumentException("Invalid command!");
        }
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine(ex.Message);
    }
    finally
    {
        Console.WriteLine("Enter another command");
    }
}
void CheckAccount(int account, Dictionary<int, double> accounts)
{
    if (!accounts.ContainsKey(account))
    {
        throw new ArgumentException("Invalid account!");
    }
}
