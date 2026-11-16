using Blackjack;

namespace Arcade.Test.ConsoleApp;

public class ProgramTest {



    [Test]
    public void CreateGameTest() {
        Console.SetIn(new StringReader("test\n9\n3\n\nJane\nJack"));
        Program.CreateGame();
    }

    [Test]
    public void BlackJackSetupTest() {
        Deck blackjackdeck = new Deck();
        BlackJack game = new BlackJack(blackjackdeck);
        Player JhonDoe = new Player();
        Player JaneDoe = new Player()
        {
            Name = "Jane Doe"
        };

        game.players.Add(JhonDoe);
        game.players.Add(JaneDoe);

        Console.SetIn(new StringReader("test\n9\n3\n\n200\nTest\n-10\n0\n20\n30"));
        Program.BlackJackSetup(game);
    }

    [Test]
    public void BlackJackSurrenderTest() {
        Deck blackjackdeck = new Deck();
        BlackJack game = new BlackJack(blackjackdeck);
        Player JhonDoe = new Player();
        Player JaneDoe = new Player()
        {
            Name = "Jane Doe"
        };
        game.players.Add(JhonDoe);
        game.players.Add(JaneDoe);

        Card two = new Card {
            Suit = "♠",
            Rank = "2",
            Value = 2,
        };

        Card three = new Card {
            Suit = "♥",
            Rank = "3",
            Value = 3,
        };

        Card queen = new Card {
            Suit = "♠",
            Rank = "Q",
            Value = 10,
        };
        Card five = new Card {
            Suit = "♥",
            Rank = "5",
            Value = 5,
        };

        game.players[0].Hand[0].Add(two);
        game.players[0].Hand[0].Add(three);

        game.players[1].Hand[0].Add(queen);
        game.players[1].Hand[0].Add(five);

        Console.SetIn(new StringReader("Y\n9"));
        Program.BlackJackSurrender(game);
    }

    [Test]
    public void EndTest(){
        Console.SetIn(new StringReader("3\njhon\nJane\nJack\n2\n20\n30\n40\ny\ny\nn\ny\n2\nJhonny\nJackie"));
        BlackJack game = Program.CreateGame();
        Program.BlackJackSetup(game);
        Program.BlackjackEnd(game);

        Assert.True(game.Playing);
        Assert.AreEqual(4, game.PlayerCount);

    }
}