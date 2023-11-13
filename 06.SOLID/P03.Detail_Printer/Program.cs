using P03.Detail_Printer.Interfaces;
using System.Collections.Generic;

namespace P03.Detail_Printer
{
    class Program
    {
        static void Main()
        {

            IEmployee employee = new Employee("Ivo");
            IEmployee manager = new Manager("Gosho", new List<string> { "1", "2", "3" });

            List<IEmployee> list = new();

            list.Add(employee);
            list.Add(manager);

            DetailsPrinter printer = new DetailsPrinter(list);

            printer.PrintDetails();

        }
    }
}
