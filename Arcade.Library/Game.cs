public class Game {
    public List<Player> players = new List<Player>();

    public int MinPlayers{get; set;}

    public int MaxPlayers{get; set;}
    
    public int PlayerCount {get; set;} = 0;

    public bool Playing {get; set;} = true;

    public Game(int max, int min)
    {
        MaxPlayers = max;
        MinPlayers = min;
    }

    public void SelectGame(string indput) {
        switch (indput)
        {
            case "B":
            break;
            case "M":
            break;
            default:
            break;
        }
    }


    public Results SetPlayerCount(string input){
        Results results = new Results();
            try {
                int output = Int32.Parse(input);
                if(output >= 1 && output <= 7-PlayerCount){
                    PlayerCount += output; 
                    results.IsSuccess = true;
                } else {
                    results.IsSuccess = false;
                    results.Message = $"Please select a number between {MinPlayers} - {MaxPlayers-PlayerCount}";
                }
            } catch {
                results.IsSuccess = false;
                results.Message = $"Please select a number between {MinPlayers} - {MaxPlayers-PlayerCount}";
            }
        return results;
        }

    public Results SetPlayerName(string name){
        Results results = new Results();
        Player player = new Player();

        if (!string.IsNullOrWhiteSpace(name)){
            player.Name = name;
        }
        players.Add(player);
        results.IsSuccess = true;
        return results;
    }
}