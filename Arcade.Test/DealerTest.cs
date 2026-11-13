
namespace Arcade.Test;

public class DealerTest {

    private Deck blackjackdeck;

    [SetUp]
    public void SetUp() {
        blackjackdeck = new Deck();
    }

    [Test]
    public void DealerBlackjackTest(){
        BlackJack game = new BlackJack(blackjackdeck);
        Console.SetIn(new StringReader("10\n20\ny\n5\ny\n10\ny\n5\ny\n10"));
        game.PlayerCount = 2;

        game.players[0].SetBet(0);
        game.players[1].SetBet(0);

        Card two = new Card {
            Suit = "♠",
            Rank = "2",
            Value = 2,
        };

        Card king = new Card {
            Suit = "♥",
            Rank = "K",
            Value = 10,
        };
        Card ace = new Card {
            Suit = "♦",
            Rank = "A",
            Value = 11,
        };


        game.dealer.Hand[0].Add(two);
        game.dealer.Hand[0].Add(two);
        game.CheckInsurrance();
        game.dealer.Hand[0].Clear();


        game.dealer.Hand[0].Add(ace);
        game.dealer.Hand[0].Add(two);
        game.CheckInsurrance();

        foreach (Player player in game.players){
            Assert.True(player.InGame);
            Assert.AreNotEqual(0, player.Bet[0]);
        }
        Assert.AreEqual(10, game.players[0].Bet[0]);
        Assert.IsEmpty(game.players[0].Insurance);
        Assert.AreEqual(85, game.players[0].Chips);

        Assert.AreEqual(20, game.players[1].Bet[0]);
        Assert.IsEmpty(game.players[1].Insurance);
        Assert.AreEqual(70, game.players[1].Chips);

        game.dealer.Hand[0].Clear();
        game.players[0].Chips += 5;
        game.players[1].Chips += 10;
            
        game.players[0].Insurance = [0];
        game.players[1].Insurance = [0];

        game.dealer.Hand[0].Add(ace);
        game.dealer.Hand[0].Add(king);

        game.CheckInsurrance();

        foreach (Player player in game.players){
            Assert.False(player.InGame);
            Assert.IsEmpty(player.Bet);
            Assert.IsEmpty(player.Insurance);
        }
        Assert.AreEqual(10, game.players[0].Retuns);
        Assert.AreEqual(20, game.players[1].Retuns);
    }

    [Test]
    public void DealerHitTest() {
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

        blackjackdeck.deck.Add(two);
        blackjackdeck.deck.Add(three);
        blackjackdeck.deck.Add(queen);
        blackjackdeck.deck.Add(ace);
        blackjackdeck.deck.Add(five);

        BlackJack game = new BlackJack(blackjackdeck);

        Card four = new Card {
            Suit = "♠",
            Rank = "4",
            Value = 4,
        };

        Card king = new Card {
            Suit = "♥",
            Rank = "K",
            Value = 10,
        };


        game.dealer.Hand[0].Add(four);
        game.dealer.Hand[0].Add(king);

        game.DealerHitLoop();


        Assert.AreEqual(3, blackjackdeck.deck.Count);
        Assert.AreEqual(19, game.dealer.Hand[0].Sum(card => card.Value));

        game.dealer.Hand[0].Clear();
        game.dealer.Hand[0].Add(four);
        game.dealer.Hand[0].Add(king);
        
        game.DealerHitLoop();

        Assert.AreEqual(2, blackjackdeck.deck.Count);
        Assert.AreEqual(24, game.dealer.Hand[0].Sum(card => card.Value));
    }

}