
namespace PersonsInfo
{
    public class Team
    {
        private string name;
        private List<Person> firstTeam;
        private List<Person> reserveTeam;

        public Team(string name)
        {
            this.name = name;
            this.firstTeam = new List<Person>();
            this.reserveTeam = new List<Person>();
        }
        public string Name
        {
            get => name;
            set => name = value;
        }
        public IReadOnlyCollection<Person> FirstTeam => firstTeam.AsReadOnly();
        public IReadOnlyCollection<Person> ReserveTeam => reserveTeam.AsReadOnly();

        public void AddPlayer(Person person)
        {
            if (person.Age < 40)
            {
                firstTeam.Add(person);
            }
            else
            {
                reserveTeam.Add(person);
            }
        }


    }
}
