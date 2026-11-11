using Blackjack;

namespace Arcade.Test;

public class GameTest {
    [Test]
    public void PlayerBlackjackTest() {
        Deck testdeck = new Deck();
        BlackJack game = new BlackJack(testdeck);
        Console.SetIn(new StringReader("Aiden\nJane\n10\n20"));
        game.PlayerCount=2;
        game.SetPlayerNames();

        game.players[0].SetBet(0);
        game.players[1].SetBet(0);

        Card ace = new Card {
            Suit = "♦",
            Rank = "A",
            Value = 11,
        };
        Card king = new Card {
            Suit = "♥",
            Rank = "K",
            Value = 10,
        };

        Card two = new Card {
            Suit = "♠",
            Rank = "2",
            Value = 2,
        };

        Card five = new Card {
            Suit = "♣",
            Rank = "5",
            Value = 5,
        };

        game.players[0].Hand[0].Add(two);
        game.players[0].Hand[0].Add(five);

        game.players[1].Hand[0].Add(ace);
        game.players[1].Hand[0].Add(king);

        game.players[0].PlayerBlackjack(0);
        game.players[1].PlayerBlackjack(0);

        Assert.True( game.players[0].InGame);
        Assert.AreEqual(0,  game.players[0].Retuns);

        Assert.False(game.players[1].InGame);
        Assert.AreEqual(30,  game.players[1].Retuns);
    }

    [Test]
     public void PlayerBustTest(){
        Deck testdeck = new Deck();
        BlackJack game = new BlackJack(testdeck);
        Console.SetIn(new StringReader("Aiden\nJane\n10\n20"));
        game.PlayerCount = 2;
        game.SetPlayerNames();

        game.players[0].SetBet(0);
        game.players[1].SetBet(0);

        Card ten = new Card {
            Suit = "♦",
            Rank = "10",
            Value = 10,
        };
        Card three = new Card {
            Suit = "♦",
            Rank = "3",
            Value = 3,
        };
        Card king = new Card {
            Suit = "♥",
            Rank = "K",
            Value = 10,
        };

        Card six = new Card {
            Suit = "♠",
            Rank = "6",
            Value = 6,
        };

        Card five = new Card {
            Suit = "♣",
            Rank = "5",
            Value = 5,
        };

        Card ace1 = new Card {
            Suit = "♥",
            Rank = "A",
            Value = 11,
        };
    
        Card ace2 = new Card {
            Suit = "♣",
            Rank = "A",
            Value = 11,
        };


        game.players[0].Hand[0].Add(six);
        game.players[0].Hand[0].Add(five);
        game.players[0].Hand[0].Add(ace1);
        game.players[0].Hand[0].Add(ace2);
        
        game.players[1].Hand[0].Add(ten);
        game.players[1].Hand[0].Add(king);
        game.players[1].Hand[0].Add(three);
        
        Assert.AreEqual(33, game.players[0].Hand[0].Sum(card => card.Value));

        game.players[0].BustCheck(0);
        game.players[1].BustCheck(0);

        Assert.True(game.players[0].InGame);
        Assert.AreEqual(13, game.players[0].Hand[0].Sum(card => card.Value));
        Assert.AreEqual(10, game.players[0].Bet[0]);

        Assert.False(game.players[1].InGame);
        Assert.AreEqual(0, game.players[1].Bet[0]);
    }

    [Test]
    public void GameDone() {
        Deck testdeck = new Deck();
        BlackJack game = new BlackJack(testdeck);
        Console.SetIn(new StringReader("Aiden\nJane"));
        game.PlayerCount=2;
        game.SetPlayerNames();
        foreach(Player player in game.players){
            player.CheckPlayerState();
        }
        Assert.True(game.players[0].InGame);
        Assert.True(game.players[1].InGame);

        foreach(Player player in game.players){
            player.Hand.Clear();
            player.CheckPlayerState();
        }
        Assert.False(game.players[0].InGame);
        Assert.False(game.players[1].InGame);

        game.CheckIfGamesOver();
        Assert.False(game.Playing);
    }

    [Test]
    public void CheckIfNewRoundBegins(){
        Deck testdeck = new Deck();
        BlackJack game = new BlackJack(testdeck);
        Console.SetIn(new StringReader("Aiden\nJane"));
        game.PlayerCount = 2;
        game.SetPlayerNames();

        game.CheckIfNewRoundBegins();
        Assert.AreEqual(2, game.players.Count);
        Assert.AreEqual(2, game.PlayerCount);
        Assert.True(game.Playing);

        game.players.Clear();
        game.PlayerCount = 0;

        game.CheckIfNewRoundBegins();
        Assert.IsEmpty(game.players);
        Assert.AreEqual(0, game.PlayerCount);
        Assert.False(game.Playing);
    }

    [Test]
   public void ResultTest(){
        Deck testdeck = new Deck();
        BlackJack game = new BlackJack(testdeck);
        Console.SetIn(new StringReader("Aiden\nJane\nJack\n10\n40\n30\n100"));
        game.PlayerCount=3;

        game.SetPlayerNames();

        
        Card ten = new Card {
            Suit = "♦",
            Rank = "10",
            Value = 10,
        };
        Card three = new Card {
            Suit = "♦",
            Rank = "3",
            Value = 3,
        };
        Card king = new Card {
            Suit = "♥",
            Rank = "K",
            Value = 10,
        };

        Card six = new Card {
            Suit = "♠",
            Rank = "6",
            Value = 6,
        };

        Card five = new Card {
            Suit = "♣",
            Rank = "5",
            Value = 5,
        };

        Card ace = new Card {
            Suit = "♥",
            Rank = "A",
            Value = 11,
        };
        game.players[0].Hand.Add([]);
        game.players[0].Bet.Add(0);

        game.players[0].SetBet(0);
        game.players[0].SetBet(1);
        game.players[1].SetBet(0);
        game.players[2].SetBet(0);


        game.dealer.Hand[0].Add(five);
        game.dealer.Hand[0].Add(six);
        game.dealer.Hand[0].Add(five);


        game.players[0].Hand[0].Add(five);
        game.players[0].Hand[0].Add(six);
        game.players[0].Hand[0].Add(five);

        game.players[0].Hand[1].Add(six);
        game.players[0].Hand[1].Add(three);
        game.players[0].Hand[1].Add(ace);

        game.players[1].Hand[0].Add(three);
        game.players[1].Hand[0].Add(ace);

        game.players[2].Hand[0].Add(five);
        game.players[2].Hand[0].Add(six);

        game.Results();
        Assert.AreEqual(2, game.players[0].Hand.Count);
        Assert.AreEqual(16, game.players[0].Hand[0].Sum(card=>card.Value));
        Assert.AreEqual(20, game.players[0].Hand[1].Sum(card=>card.Value));

        Assert.AreEqual(140, game.players[0].Chips);
        Assert.AreEqual(70, game.players[1].Chips);
        Assert.AreEqual(0, game.players[2].Chips);
    }

    [Test]
    public void CanPlayersContinuetest(){
        Deck testdeck = new Deck();
        BlackJack game = new BlackJack(testdeck);
        Console.SetIn(new StringReader("Aiden\n\n\nJane\nJack"));
        game.PlayerCount = 5;
        game.SetPlayerNames();

        Assert.AreEqual(5, game.players.Count);

        game.players[0].Chips = 0;
        game.players[1].Chips = 0;

        game.CanPlayersContinue();

        Assert.AreEqual(3, game.players.Count);
    }

    [Test]
    public void PlayersWantToContinueTest() {
        Deck testdeck = new Deck();
        BlackJack game = new BlackJack(testdeck);
        game.PlayerCount = 5;
        Console.SetIn(new StringReader("Aiden\n\n\nJane\nJack"));

        game.SetPlayerNames();

        Assert.AreEqual(5, game.players.Count);

        Console.SetIn(new StringReader("n\nn\ny\ny\ny"));

        game.DoPlayersWantContinue();
        Assert.AreEqual(3, game.players.Count);
    }

    [Test]
    public void CheckIfnewPlayersJoinTest(){
        Deck testdeck = new Deck();
        BlackJack game = new BlackJack(testdeck);
        Console.SetIn(new StringReader("Aiden\n\n\nJane\nJack"));
        game.PlayerCount = 5;
        game.SetPlayerNames();

        Assert.AreEqual(5, game.players.Count);

        Console.SetIn(new StringReader("Y\n2\njhonny\nJennifer"));
        game.CheckIfnewPlayersJoin();

        Assert.AreEqual(7, game.players.Count);
    }

    [Test]
    public void TurnRotationTest() {
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

        Card ace = new Card {
            Suit = "♥",
            Rank = "A",
            Value = 11,
        };
        Card five = new Card {
            Suit = "♥",
            Rank = "5",
            Value = 5,
        };

        Deck testdeck = new Deck();
        testdeck.deck.Add(two);
        testdeck.deck.Add(three);
        testdeck.deck.Add(queen);
        testdeck.deck.Add(ace);
        testdeck.deck.Add(five);

        BlackJack game = new BlackJack(testdeck);
        Console.SetIn(new StringReader("Aiden\nJane\ny\nn\nn\nn"));
        game.PlayerCount = 2;
        game.SetPlayerNames();

        game.players[0].Hand.Add([]);
        game.players[0].Stand.Add(false);
        game.players[0].Bet.Add(0);
        game.players[0].Insurance.Add(0);

        Assert.AreEqual(2, game.players[0].Hand.Count);
        Assert.AreEqual(2, game.players[0].Stand.Count);

        game.players[0].Hand[0].Add(two);
        game.players[0].Hand[0].Add(three);

        game.players[0].Hand[1].Add(queen);
        game.players[0].Hand[1].Add(five);

        game.players[1].Hand[0].Add(ace);
        game.players[1].Hand[0].Add(three);

        game.TurnRotation();

        Assert.AreEqual(3, game.players[0].Hand[0].Count);
        Assert.AreEqual(7, game.players[0].Hand[0].Sum(card => card.Value));

        Assert.AreEqual(2, game.players[0].Hand[1].Count);
        Assert.AreEqual(15, game.players[0].Hand[1].Sum(card => card.Value));

        Assert.AreEqual(2, game.players[1].Hand[0].Count);
        Assert.AreEqual(14, game.players[1].Hand[0].Sum(card => card.Value));

        Assert.False(game.Playing);
    }
}