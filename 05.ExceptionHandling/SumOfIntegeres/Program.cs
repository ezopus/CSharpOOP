
string[] tokens = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
int sum = 0;

foreach (string token in tokens)
{
    try
    {
        sum += int.Parse(token);
    }
    catch (FormatException)
    {
        Console.WriteLine($"The element '{token}' is in wrong format!");
    }
    catch (OverflowException)
    {
        Console.WriteLine($"The element '{token}' is out of range!");
    }
    finally
    {
        Console.WriteLine($"Element '{token}' processed - current sum: {sum}");
    }
}

Console.WriteLine($"The total sum of all integers is: {sum}");