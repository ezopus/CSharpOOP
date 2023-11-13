using System;
using System.Collections.Generic;

namespace P03.Detail_Printer
{
    public class Manager : Employee
    {
        public Manager(string name, ICollection<string> documents)
            : base(name)
        {
            Documents = new List<string>(documents);
        }

        public IReadOnlyCollection<string> Documents { get; set; }

        public override void PrintEmployee()
        {
            Console.WriteLine(Name);
            Console.WriteLine(string.Join(Environment.NewLine, Documents));
        }
    }
}
