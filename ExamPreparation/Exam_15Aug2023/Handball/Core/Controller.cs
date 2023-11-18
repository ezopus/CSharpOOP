using Handball.Core.Contracts;
using Handball.Models;
using Handball.Models.Contracts;
using Handball.Repositories;
using Handball.Repositories.Contracts;
using System.Linq;
using System.Text;

namespace Handball.Core
{
    public class Controller : IController
    {
        private IRepository<ITeam> teams;
        private IRepository<IPlayer> players;

        public Controller()
        {
            teams = new TeamRepository();
            players = new PlayerRepository();
        }
        public string NewTeam(string name)
        {
            Team currentTeam = new Team(name);

            if (teams.ExistsModel(name))
            {
                return $"{name} is already added to the {nameof(TeamRepository)}.";
            }

            teams.AddModel(currentTeam);
            return $"{name} is successfully added to the {nameof(TeamRepository)}.";
        }

        public string NewPlayer(string typeName, string name)
        {

            if (typeName != "Goalkeeper" && typeName != "CenterBack" && typeName != "ForwardWing")
            {
                return $"{typeName} is invalid position for the application.";
            }

            if (players.ExistsModel(name))
            {
                IPlayer existing = players.GetModel(name);
                return $"{name} is already added to the {nameof(PlayerRepository)} as {existing.GetType().Name}.";
            }

            if (typeName == "Goalkeeper")
            {
                players.AddModel(new Goalkeeper(name));
            }
            else if (typeName == "CenterBack")
            {
                players.AddModel(new CenterBack(name));
            }
            else if (typeName == "ForwardWing")
            {
                players.AddModel(new ForwardWing(name));
            }

            return $"{name} is filed for the handball league.";
        }

        public string NewContract(string playerName, string teamName)
        {
            //check if player exists
            if (!players.ExistsModel(playerName))
            {
                return $"Player with the name {playerName} does not exist in the {nameof(PlayerRepository)}.";
            }
            //check if team exists
            if (!teams.ExistsModel(teamName))
            {
                return $"Team with the name {teamName} does not exist in the {nameof(TeamRepository)}.";
            }
            //check if player has already signed a contract
            if (players.GetModel(playerName).Team != null)
            {
                return $"Player {playerName} has already signed with {players.GetModel(playerName).Team}.";
            }

            //get existing player
            IPlayer currentPlayer = players.GetModel(playerName);

            //player signs contract
            currentPlayer.JoinTeam(teamName);

            //add player to existing team
            teams.GetModel(teamName).SignContract(currentPlayer);

            return $"Player {playerName} signed a contract with {teamName}.";
        }

        public string NewGame(string firstTeamName, string secondTeamName)
        {
            ITeam firstTeam = teams.GetModel(firstTeamName);
            ITeam secondTeam = teams.GetModel(secondTeamName);

            if (firstTeam.OverallRating > secondTeam.OverallRating)
            {
                firstTeam.Win();
                secondTeam.Lose();
                return $"Team {firstTeamName} wins the game over {secondTeamName}!";
            }
            else if (firstTeam.OverallRating < secondTeam.OverallRating)
            {
                firstTeam.Lose();
                secondTeam.Win();
                return $"Team {secondTeamName} wins the game over {firstTeamName}!";
            }
            else
            {
                firstTeam.Draw();
                secondTeam.Draw();
                return $"The game between {firstTeamName} and {secondTeamName} ends in a draw!";
            }
        }

        public string PlayerStatistics(string teamName)
        {
            ITeam currentTeam = teams.GetModel(teamName);

            StringBuilder sb = new();

            sb.AppendLine($"***{teamName}***");

            foreach (var player in currentTeam.Players.OrderByDescending(p => p.Rating).ThenBy(p => p.Name))
            {
                sb.AppendLine(player.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public string LeagueStandings()
        {
            StringBuilder sb = new();

            sb.AppendLine($"***League Standings***");
            foreach (var team in teams.Models.OrderByDescending(t => t.PointsEarned).ThenByDescending(t => t.OverallRating).ThenBy(t => t.Name))
            {
                sb.AppendLine(team.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
