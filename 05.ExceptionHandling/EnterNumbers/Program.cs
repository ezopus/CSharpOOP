

int start = 1;
List<int> numbers = new();

while (numbers.Count < 10)
{
    try
    {
        if (numbers.Count != 0)
        {
            start = numbers[numbers.Count - 1];
        }
        numbers.Add(ReadNumber(start, 100));
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

Console.WriteLine(string.Join(", ", numbers));


int ReadNumber(int start, int end)
{
    int number;
    try
    {
        number = int.Parse(Console.ReadLine());
    }
    catch (FormatException ex)
    {
        throw new FormatException("Invalid Number!");
    }

    if (number > start && number < end)
    {
        return number;
    }
    else
    {
        throw new ArgumentException($"Your number is not in range {start} - {end}!");
    }
}