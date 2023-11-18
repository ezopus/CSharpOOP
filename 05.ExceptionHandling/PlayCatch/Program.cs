using PlayCatch;

int[] array = Console
    .ReadLine()
    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse)
    .ToArray();

int numberOfExceptions = 0;

while (numberOfExceptions < 3)
{
    string[] tokens = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
    string command = tokens[0];

    try
    {
        switch (command)
        {
            case "Replace":
                int indexToReplace = int.Parse(tokens[1]);
                int newElement = int.Parse(tokens[2]);
                array[indexToReplace] = newElement;
                break;
            case "Print":
                int startIndex = int.Parse(tokens[1]);
                int endIndex = int.Parse(tokens[2]);
                int length = endIndex - startIndex + 1;
                int[] rangedArray = new int[length];
                for (int i = 0; i < length; i++)
                {
                    rangedArray[i] = array[startIndex++];
                }
                Console.WriteLine(string.Join(", ", rangedArray));
                break;
            case "Show":
                Console.WriteLine(array[int.Parse(tokens[1])]);
                break;
        }
    }
    catch (FormatException)
    {
        numberOfExceptions++;
        Console.WriteLine(ExceptionMessages.VariableIncorrectFormat);
    }
    catch (IndexOutOfRangeException)
    {
        numberOfExceptions++;
        Console.WriteLine(ExceptionMessages.IndexDoesNotExist);
    }

}

Console.WriteLine(string.Join(", ", array));
