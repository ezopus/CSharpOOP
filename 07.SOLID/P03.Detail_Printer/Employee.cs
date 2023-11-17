using P03.Detail_Printer.Interfaces;
using System;

namespace P03.Detail_Printer
{
    public class Employee : IEmployee
    {
        public Employee(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public virtual void PrintEmployee()
        {
            Console.WriteLine(Name);
        }
    }
}
