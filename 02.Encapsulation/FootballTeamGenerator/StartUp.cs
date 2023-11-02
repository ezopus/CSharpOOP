using FootballTeamGenerator.Models;

namespace FootballTeamGenerator
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            List<Team> teams = new List<Team>();

            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                try
                {
                    string[] tokens = input.Split(';');
                    switch (tokens[0])
                    {
                        case "Team":
                            Team currentTeam = new Team(tokens[1]);
                            teams.Add(currentTeam);
                            break;
                        case "Add":
                            AddPlayer(tokens[1], tokens[2], int.Parse(tokens[3]), int.Parse(tokens[4]), int.Parse(tokens[5]), int.Parse(tokens[6]), int.Parse(tokens[7]), teams);
                            break;
                        case "Remove":
                            RemovePlayer(tokens[1], tokens[2], teams);
                            break;
                        case "Rating":
                            PrintTeamRating(tokens[1], teams);
                            break;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static void AddPlayer(string teamName, string playerName, int endurance, int sprint, int dribble, int passing, int shooting,
            List<Team> teams)
        {
            Team team = teams.FirstOrDefault(t => t.Name == teamName);
            if (team == null)
            {
                throw new ArgumentException($"Team {teamName} does not exist.");
            }
            Player player = new Player(playerName, endurance, sprint, dribble, passing, shooting);
            team.AddPlayer(player);
            
        }

        static void RemovePlayer(string teamName, string playerName, List<Team> teams)
        {
            Team team = teams.FirstOrDefault(t => t.Name == teamName);
            if (team == null)
            {
                throw new ArgumentException($"Team {teamName} does not exist.");
            }

            team.RemovePlayer(playerName);
        }

        static void PrintTeamRating(string teamName, List<Team> teams)
        {
            Team team = teams.FirstOrDefault(t => t.Name == teamName);
            if (team == null)
            {
                throw new ArgumentException($"Team {teamName} does not exist.");
            }

            Console.WriteLine($"{team.Name} - {team.Rating:f0}");
        }
    }
}