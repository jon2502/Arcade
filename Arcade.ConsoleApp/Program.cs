
namespace Blackjack {
    public class Program {

        public static void Playerinput(Func<string, Results> myMethodName) {
             while (true){
                string input = Console.ReadLine();
                Results result = myMethodName(input);
                if (result.IsSuccess == true) {
                    if (!string.IsNullOrWhiteSpace(result.Message)){Console.WriteLine(result.Message);}
                    break;
                } else {
                    Console.WriteLine(result.Message);
                }
            }
        }

        public static BlackJack CreateGame(){
            Deck blackjackdeck = new Deck();
            BlackJack game = new BlackJack(blackjackdeck);

            Console.WriteLine($"Hello how may players are you: from {game.MinPlayers} - {game.MaxPlayers-game.PlayerCount}");
            Playerinput(game.SetPlayerCount);

            for (int i = game.players.Count; i < game.PlayerCount; i++) {
                Console.WriteLine($"player {i+1}: write your name");
                Playerinput(game.SetPlayerName);
            }
            return game;
        }

        public static void BlackJackSetup(BlackJack game){
            Console.WriteLine("select deck size");
            Playerinput(game.blackjackdeck.SetDecksize);
            game.blackjackdeck.CreateDeck();
            foreach (Player player in game.players) {
                Console.WriteLine($"{player.Name} place your bet");
                Playerinput(player.SetBet);
            }
            game.GetStartingHands();
        }

        public static void BlackjackEnd(BlackJack game) {
                game.Results();
                game.CanPlayersContinue();
                game.DoPlayersWantContinue();
                game.CheckIfnewPlayersJoin();
                game.CheckIfNewRoundBegins();
        }

        static void Main(string[] args) {
            BlackJack game = CreateGame();

            /*while (game.Playing == true) {

                BlackJackSetup(game);

                game.DoPlayersWantToSurrender();

                game.DoPlayersWantToSplit();

                game.DoubleDownOption();

                game.CheckInsurrance();

                game.DealerHitLoop();

                game.TurnRotation();
            }*/
        }
    }
}