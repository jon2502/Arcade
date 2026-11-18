using Blackjack;

namespace Arcade.Test.ConsoleApp;

public class SetupTest {
    private Deck blackjackdeck;
    private BlackJack game;

    [SetUp]
    public void SetUp() {
        blackjackdeck = new Deck();
        game = new BlackJack(blackjackdeck);
    }


    public void playersetup() {
        Player JhonDoe = new Player(){
            Chips = 80,
            Bet = [20]
        };
        Player JaneDoe = new Player()
        {
            Name = "Jane Doe",
            Chips = 20,
            Bet = [80]
        };
        game.players.Add(JhonDoe);
        game.players.Add(JaneDoe);
    }

    [Test]
    public void BlackJackSetupTest() {
        Console.SetIn(new StringReader("test\n9\n-1\n3\nJane\nJack\nJennifer\ntest\n11\n-2\n3\n200\nTest\n-10\n0\n20\n30\n4"));
        BlackJack testblackjack = Program.CreateGame();
        Program.BlackJackSetup(testblackjack);

        Assert.AreEqual(3, testblackjack.players.Count);
    }

    [Test]
    public void BlackJackSurrenderTest() {
        playersetup();      
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
    public void BlackJackSplitTest() {
        playersetup();
        Card ace = new Card {
            Suit = "♥",
            Rank = "A",
            Value = 11,
        };
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
        Card king = new Card {
            Suit = "♠",
            Rank = "K",
            Value = 10,
        };

        game.blackjackdeck.deck.Add(ace);
        game.blackjackdeck.deck.Add(two);
        game.blackjackdeck.deck.Add(three);
        game.blackjackdeck.deck.Add(five);
        game.blackjackdeck.deck.Add(five);

        Player Jack = new Player()
        {
            Name = "Jack"
        };
        
        Player Jennifer = new Player()
        {
            Name = "Jennifer"
        };
        game.players.Add(Jack);
        game.players.Add(Jennifer);
        Assert.AreEqual(4, game.players.Count);


        game.players[0].Hand[0].Add(ace);
        game.players[0].Hand[0].Add(ace);

        game.players[1].Hand[0].Add(queen);
        game.players[1].Hand[0].Add(king);

        game.players[2].Hand[0].Add(ace);
        game.players[2].Hand[0].Add(ace);
        game.players[2].Hand[0].Add(ace);

        game.players[3].Hand[0].Add(king);
        game.players[3].Hand[0].Add(five);
        Console.SetIn(new StringReader("20\n15\nn"));

        Program.BlackJackSplit(game);

        Assert.AreEqual(3, game.players[0].Hand.Count);
        List<Card> hand1 = new List<Card>();
        hand1.Add(ace);
        hand1.Add(three);

        List<Card> hand2 = new List<Card>();
        hand2.Add(ace);
        hand2.Add(two);

        List<Card> hand3 = new List<Card>();
        hand3.Add(ace);
        hand3.Add(five);

        Assert.AreEqual(hand1, game.players[0].Hand[0]);
        Assert.AreEqual(hand2, game.players[0].Hand[1]);
        Assert.AreEqual(hand3, game.players[0].Hand[2]);

        Assert.That(game.players[1].Hand, Has.Count.EqualTo(1));
        Assert.That(game.players[2].Hand, Has.Count.EqualTo(1));
        Assert.That(game.players[3].Hand, Has.Count.EqualTo(1));

        Assert.AreEqual(3, game.players[0].Bet.Count);

        Assert.AreEqual(20, game.players[0].Bet[0]);
        Assert.AreEqual(20, game.players[0].Bet[1]);
        Assert.AreEqual(15, game.players[0].Bet[2]);
        Assert.AreEqual(80, game.players[1].Bet[0]);
        Assert.AreEqual(0, game.players[2].Bet.Sum(bet=>bet));
        Assert.AreEqual(0, game.players[3].Bet.Sum(bet=>bet));
    }

    [Test]
    public void BlackJackDoubleDowntest() {
        playersetup();
         Card two = new Card {
            Suit = "♠",
            Rank = "2",
            Value = 2,
        };

        Card queen = new Card {
            Suit = "♠",
            Rank = "Q",
            Value = 10,
        };
                
        game.blackjackdeck.deck.Add(queen);

        Player Jennifer = new Player()
        {
            Name = "Jennifer",
            Chips = 95,
            Bet = [5]
        };

        game.players.Add(Jennifer);

        game.players[0].Hand[0].Add(two);
        game.players[0].Hand[0].Add(two);

        game.players[1].Hand[0].Add(two);
        game.players[1].Hand[0].Add(two);

        game.players[2].Hand[0].Add(two);
        game.players[2].Hand[0].Add(two);

        Console.SetIn(new StringReader("y\ny\nn"));
        Program.BlackJackDoubleDown(game);

    }

}