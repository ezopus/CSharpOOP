using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirthdayCelebration.Models.Interfaces;

namespace BirthdayCelebration.Models
{
    public class Pet : IHabitant, IBirthable
    {
        public Pet(string name, string birthdate)
        {
            Name = name;
            Birthdate = birthdate;
        }
        public string Name { get; set; }
        public string Birthdate { get; private set; }
        public string Id { get; private set; }
    }
}
