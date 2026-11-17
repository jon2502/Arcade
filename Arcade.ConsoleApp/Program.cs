
namespace Blackjack {
    public class Program {

        public static string SimplePlayerinput() {
            Console.WriteLine("y : yes");
            Console.WriteLine("anyother key : no");
            string input = Console.ReadLine();
            return input;
        }
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

        public static void PlayerIndexedInput (Func<string, int, Results> myMethodName, int Index) {
             while (true){
                string input = Console.ReadLine();
                Results result = myMethodName(input, Index);
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
                PlayerIndexedInput(player.SetBet,0);
            }
            game.GetStartingHands();
            
        }

        public static void BlackJackSurrender(BlackJack game) {
            foreach (Player player in game.players) {
                Console.WriteLine($"{player.Name} would you like to surrender and get half your bet back. youyr current hand is");
                string Output = SimplePlayerinput();
                bool Didsurrender = player.PlayerOption(Output);
                if(Didsurrender == true){
                    string response = player.Surrender(0);
                    Console.WriteLine(response);
                }
            }
        }
        public static void BlackJackSplit(BlackJack game) {
            foreach (Player player in game.players) {
                int HandIndex = 0;
                while (HandIndex < player.Hand.Count) { 
                    Results CheckSplit = player.CheckSplit(HandIndex);
                    if(CheckSplit.IsSuccess == true){
                    Console.WriteLine(CheckSplit.Message);}
                    bool DidSplit;
                    if(CheckSplit.Message != $"{player.Name} you have two aces so your hand will be split") {
                        string Output = SimplePlayerinput();
                        DidSplit = player.PlayerOption(Output);
                    } else {DidSplit = true;}
                    player.Split(HandIndex);
                    if (DidSplit) {
                        game.MoveCard(player,HandIndex);
                        game.MoveCard(player, player.Hand.Count - 1);
                        Console.WriteLine($"{player.Name} place bet for new hand");
                        PlayerIndexedInput(player.SetBet, player.Hand.Count - 1);
                        player.PlayerBlackjack(HandIndex);
                        player.PlayerBlackjack(player.Hand.Count - 1);
                    } else {HandIndex++;}
                }
            }
        }

        public static void BlackJackDoubleDown(BlackJack game){
            foreach (Player player in game.players) {
                for (int i = 0; i < player.Hand.Count; i++) {
                    Console.WriteLine($"{player.Name} Would you like to doubledown for your hand of?");
                    Console.WriteLine(player.DisplayHand(i));
                    string Output = SimplePlayerinput();
                    bool DidBouble = player.PlayerOption(Output);
                    if (DidBouble) {
                        Results sucsses = player.DoubleDown(i);
                        if (sucsses.IsSuccess == true) {
                            game.MoveCard(player, i);
                            player.DisplayHand(i);
                            player.BustCheck(i);
                            player.CheckPlayerState();
                        }else{Console.WriteLine(sucsses.Message);}
                    }
                }

            }
        }

        public static void BlackJackInsurance(BlackJack game) {
            
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