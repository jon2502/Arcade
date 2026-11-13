
namespace Arcade.Test;

public class PlayerTest {

    private Deck blackjackdeck;

    [SetUp]
    public void SetUp() {
        blackjackdeck = new Deck();
    }

    [TestCase("1")]
    [TestCase("2")]
    [TestCase("3")]
    [TestCase("4")]
    [TestCase("5")]
    [TestCase("6")]
    [TestCase("7")]
    public void SelectPlayeramountTestIntCorrect(string value) {
        BlackJack game = new BlackJack(blackjackdeck);
        //Console.SetIn(new StringReader($"value\n-1\n10\n{value}"));
        //game.SetPlayerCount();
    }

    /*[Test]
    public void PlayerListtest() {
        BlackJack game = new BlackJack(blackjackdeck);
        Console.SetIn(new StringReader("Aiden\n\nJennifer"));
        game.PlayerCount = 3;
        game.SetPlayerNames();
    
        Assert.AreEqual("Aiden", game.players[0].Name);

        Assert.AreEqual("Jhon Doe", game.players[1].Name);

        Assert.AreEqual("Jennifer", game.players[2].Name);

    }

    [Test]
    public void StartingHandTest() {
        BlackJack game = new BlackJack(blackjackdeck);
        Console.SetIn(new StringReader("Aiden\nJane"));
        game.PlayerCount = 2;
        game.SetPlayerNames();

        
        game.blackjackdeck.CreateDeck();

        game.GetStartingHands();

        Assert.AreEqual(46, blackjackdeck.deck.Count);
        Assert.AreEqual(2, game.players[0].Hand[0].Count);
        Assert.AreEqual(2, game.players[1].Hand[0].Count);
        Assert.AreEqual(2, game.dealer.Hand[0].Count);
    }

    [Test]
    public void SetBetTest() {
        BlackJack game = new BlackJack(blackjackdeck);
        Console.SetIn(new StringReader("Aiden\ntest\n150\n0\n-1\n20"));
        game.PlayerCount = 1;
        game.SetPlayerNames();
        game.players[0].SetBet(0);

        Assert.AreEqual(20, game.players[0].Bet[0]);

        Assert.AreEqual(80, game.players[0].Chips);
    }

    [Test]
    public void SetInsuranceTest() {
        BlackJack game = new BlackJack(blackjackdeck);
        Console.SetIn(new StringReader("Aiden\nJane\nJack\n40\n100\n60\ny\ntest\n150\n20\nn\nY\n40\n30"));
        game.PlayerCount = 3;

        game.SetPlayerNames();
        game.players[0].SetBet(0);
        game.players[1].SetBet(0);
        game.players[2].SetBet(0);

        for (int i = 0; i < game.players.Count; i++) {
            game.players[i].SetInsurance();
        }
        


        Assert.AreEqual(20, game.players[0].Insurance[0]);
        Assert.AreEqual(0, game.players[1].Insurance[0]);
        Assert.AreEqual(30, game.players[2].Insurance[0]);


        Assert.AreEqual(40, game.players[0].Chips);
        Assert.AreEqual(0, game.players[1].Chips);
        Assert.AreEqual(10, game.players[2].Chips);
    }

    [Test]
     public void SurrenderTest(){
        BlackJack game = new BlackJack(blackjackdeck);
        Console.SetIn(new StringReader("\n100\ny"));
        game.PlayerCount = 1;

        game.SetPlayerNames();

        game.players[0].SetBet(0);
        game.DoPlayersWantToSurrender();

        Assert.IsEmpty(game.players[0].Bet);
        Assert.IsEmpty(game.players[0].Insurance);
        Assert.AreEqual(50, game.players[0].Chips);
        Assert.False(game.players[0].InGame);

    }

    [Test]
    public void SplitTest(){
        
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

        blackjackdeck.deck.Add(ace);
        blackjackdeck.deck.Add(two);
        blackjackdeck.deck.Add(three);
        blackjackdeck.deck.Add(five);
        blackjackdeck.deck.Add(five);
        blackjackdeck.deck.Add(five);
        blackjackdeck.deck.Add(five);
        blackjackdeck.deck.Add(five);
        blackjackdeck.deck.Add(five);
        blackjackdeck.deck.Add(five);
        blackjackdeck.deck.Add(five);

        BlackJack game = new BlackJack(blackjackdeck);
        Console.SetIn(new StringReader("Aiden\nJane\nJack\nJennifer\n20\n15\ny\n10"));
        game.PlayerCount = 4;

        game.SetPlayerNames();
        game.players[0].Hand[0].Add(ace);
        game.players[0].Hand[0].Add(ace);

        game.players[1].Hand[0].Add(queen);
        game.players[1].Hand[0].Add(king);

        game.players[2].Hand[0].Add(ace);
        game.players[2].Hand[0].Add(ace);
        game.players[2].Hand[0].Add(ace);

        game.players[3].Hand[0].Add(king);
        game.players[3].Hand[0].Add(five);

        Assert.AreEqual(4, game.players.Count);
        game.DoPlayersWantToSplit();

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

        Assert.AreEqual(2, game.players[1].Hand.Count);
        Assert.That(game.players[2].Hand, Has.Exactly(1).Items);
        Assert.That(game.players[3].Hand, Has.Exactly(1).Items);

        Assert.AreEqual(3, game.players[0].Bet.Count);

        Assert.AreEqual(0, game.players[0].Bet[0]);
        Assert.AreEqual(20, game.players[0].Bet[1]);
        Assert.AreEqual(15, game.players[0].Bet[2]);
        Assert.AreEqual(10, game.players[1].Bet[1]);
        Assert.AreEqual(0, game.players[2].Bet.Sum(bet=>bet));
        Assert.AreEqual(0, game.players[3].Bet.Sum(bet=>bet));    
    }
    
    [Test]
    public void NoSplitTest(){        
        BlackJack game = new BlackJack(blackjackdeck);
        game.blackjackdeck.CreateDeck();
        Console.SetIn(new StringReader("\nAiden\nJane"));
        game.PlayerCount = 3;
        game.SetPlayerNames();

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

        game.players[0].Hand[0].Add(queen);
        game.players[0].Hand[0].Add(king);

        game.players[1].Hand[0].Add(king);
        game.players[1].Hand[0].Add(five);
        
        foreach (Player player in game.players) {
            for (int HandIndex = 0; HandIndex < player.Hand.Count; HandIndex++) {
                while (true) {
                    Console.SetIn(new StringReader("n"));
                    bool DidSplit = player.Split(HandIndex);
                    Assert.False(DidSplit);
                    break;
                }
            }
        }
    }

    [Test]
    public void DoubleDowntest() {
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
        
        Card five = new Card {
            Suit = "♠",
            Rank = "5",
            Value = 5,
        };
                
        blackjackdeck.deck.Add(queen);
        blackjackdeck.deck.Add(queen);
        blackjackdeck.deck.Add(queen);
        blackjackdeck.deck.Add(queen);
        blackjackdeck.deck.Add(queen);
        blackjackdeck.deck.Add(queen);


        BlackJack game = new BlackJack(blackjackdeck);
        Console.SetIn(new StringReader("Aiden\nJane\nJennifer\nJack\n40\n60\n20\n10"));
        game.PlayerCount = 4;

        game.SetPlayerNames();

        game.players[0].Hand[0].Add(two);
        game.players[0].Hand[0].Add(two);

        game.players[1].Hand[0].Add(two);
        game.players[1].Hand[0].Add(two);

        game.players[2].Hand[0].Add(five);
        game.players[2].Hand[0].Add(queen);

        game.players[3].Hand[0].Add(five);
        game.players[3].Hand[0].Add(queen);

        game.players[0].SetBet(0);
        game.players[1].SetBet(0);
        game.players[2].SetBet(0);
        game.players[3].SetBet(0);

        Assert.AreEqual(40, game.players[0].Bet[0]);
        Assert.AreEqual(60, game.players[0].Chips);
        Assert.AreEqual(60, game.players[1].Bet[0]);
        Assert.AreEqual(40, game.players[1].Chips);
        Assert.AreEqual(20, game.players[2].Bet[0]);
        Assert.AreEqual(80, game.players[2].Chips);
        Assert.AreEqual(10, game.players[3].Bet[0]);
        Assert.AreEqual(90, game.players[3].Chips);
        
        Console.SetIn(new StringReader("y\ny\ny\nn"));
        game.DoubleDownOption();

        Assert.AreEqual(14, game.players[0].Hand[0].Sum(card => card.Value));
        Assert.AreEqual(80, game.players[0].Bet[0]);
        Assert.AreEqual(20, game.players[0].Chips);
        Assert.AreEqual(3, game.players[0].Hand[0].Count);
        Assert.True(game.players[0].Stand[0]);
        Assert.False(game.players[0].InGame);


        Assert.AreEqual(4, game.players[1].Hand[0].Sum(card => card.Value));
        Assert.AreEqual(60, game.players[1].Bet[0]);
        Assert.AreEqual(40, game.players[1].Chips);
        Assert.True(game.players[1].InGame);

        Assert.AreEqual(25, game.players[2].Hand[0].Sum(card => card.Value));
        Assert.True(game.players[2].Stand[0]);
        Assert.False(game.players[2].InGame);
        Assert.AreEqual(0, game.players[2].Bet[0]);
        Assert.AreEqual(3, game.players[2].Hand[0].Count);
        Assert.AreEqual(60, game.players[2].Chips);

        Assert.AreEqual(15, game.players[3].Hand[0].Sum(card => card.Value));
        Assert.False(game.players[3].Stand[0]);
        Assert.True(game.players[3].InGame);
        Assert.AreEqual(10, game.players[3].Bet[0]);
        Assert.AreEqual(90, game.players[3].Chips);
        Assert.AreEqual(2, game.players[3].Hand[0].Count);
    }

    [Test]
    public void ContinueTest() {
        BlackJack game = new BlackJack(blackjackdeck);                
        Console.SetIn(new StringReader("3\nAiden\nJhon\nJackie\ny\ny\nn"));
        game.SetPlayerCount();
        game.SetPlayerNames();
        game.DoPlayersWantContinue();
        Assert.AreEqual(2, game.players.Count);
        Assert.AreEqual(2, game.PlayerCount);
    }

    [Test]
    public void LeaveTest() {
        BlackJack game = new BlackJack(blackjackdeck);
        List<string> PlayerNames = new List<string> {"Aiden"};
        Console.SetIn(new StringReader("Aiden"));
        game.PlayerCount = 1;

        game.SetPlayerNames();
        Console.SetIn(new StringReader("n"));
        bool result = game.players[0].WantToContinue();
        Assert.False(result);
    }

    [Test]
    public void HitTest(){
        BlackJack game = new BlackJack(blackjackdeck);
        Console.SetIn(new StringReader("Aiden"));
        game.PlayerCount = 1;
        game.SetPlayerNames();
        game.blackjackdeck.CreateDeck();
        Console.SetIn(new StringReader("y"));
        bool result = game.players[0].StandOrHit(0);
        Assert.True(result);    
        
        }

    [Test]
    public void StandTest(){
        BlackJack game = new BlackJack(blackjackdeck);
        Console.SetIn(new StringReader("Jane"));
        game.PlayerCount = 1;
        game.SetPlayerNames();
        game.blackjackdeck.CreateDeck();
        Console.SetIn(new StringReader("n"));
        bool result = game.players[0].StandOrHit(0);
        Assert.False(result);
    }

        [Test]
    public void CanPlayTest(){
        BlackJack game = new BlackJack(blackjackdeck);
        Console.SetIn(new StringReader("Aiden"));
        game.PlayerCount = 1;
        game.SetPlayerNames();
        bool TrueResult = game.players[0].Canplay();
        Assert.True(TrueResult);
        game.players[0].Chips = 0;
        bool FalseResult = game.players[0].Canplay();
        Assert.False(FalseResult);
    }
    */
}