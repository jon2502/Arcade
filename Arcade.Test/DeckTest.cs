using Blackjack;

namespace Arcade.Test;

public class DeckTest {
    [TestCase("1")]
    [TestCase("2")]
    [TestCase("3")]
    [TestCase("4")]
    [TestCase("5")]
    [TestCase("6")]
    [TestCase("7")]
    [TestCase("8")]

    public void DeckSelectionTest(string input){
        Deck blackjackdeck = new Deck();
        Console.SetIn(new StringReader($"\n10\n9\ny\n0\n-1\n{input}"));
        blackjackdeck.SetDecksize();
        int intValue = Int32.Parse(input);
        
        Assert.AreEqual(intValue, blackjackdeck.DeckSize);
    }

    [Test]
    public void DeckGenerationtest(){
        Deck testdeck = new Deck();
        bool result = testdeck.CreateDeck();
        Assert.That(testdeck.deck, Has.Some.Matches<Card>(
            card => card.Suit == "♦"
        ));
        Assert.That(testdeck.deck, Has.Some.Matches<Card>(
            card => card.Suit == "♣"
        ));        
        Assert.That(testdeck.deck, Has.Some.Matches<Card>(
            card => card.Suit == "♥"
        ));        
        Assert.That(testdeck.deck, Has.Some.Matches<Card>(
            card => card.Suit == "♠"
        ));
        Assert.True(result);
    }

    [Test]
    public void DeckGenerationtestfail(){
        Deck testdeck = new Deck();
        testdeck.PlayingCards = new string[] {"3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};
        bool result = testdeck.CreateDeck();
        Assert.IsEmpty(testdeck.deck);
        Assert.False(result);
    }

    [Test]
    public void DeckGenerationtesparsefail(){
        Deck testdeck = new Deck();
        testdeck.PlayingCards  = new string[]  {"2", "3", "4", "test", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};
        bool result = testdeck.CreateDeck();
        Assert.False(result);

        testdeck.CreateDeck();
        Assert.That(testdeck.deck, Has.Some.Matches<Card>(
            card => card.Rank == "2"
        ));
        Assert.That(testdeck.deck, Has.Some.Matches<Card>(
            card => card.Rank == "3"
        ));        
        Assert.That(testdeck.deck, Has.Some.Matches<Card>(
            card => card.Rank == "4"
        ));        
        Assert.That(testdeck.deck, Has.Some.Matches<Card>(
            card => card.Value == 11
        ));        
        Assert.That(testdeck.deck, Has.Some.Matches<Card>(
            card => card.Suit == "♦"
        ));
        Assert.AreEqual(52, testdeck.deck.Count);
    }

    [TestCase("0")]
    [TestCase("1")]
    [TestCase("11")]
    [TestCase("12")]
    public void DeckGenerationValueOverandUnder(string value){
        Deck testdeck = new Deck();
        testdeck.PlayingCards = new  string[] {value, "3", "4", "test", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};
        bool result = testdeck.CreateDeck();
        Assert.False(result);

        testdeck.CreateDeck();
        Assert.That(testdeck.deck, Has.Some.Matches<Card>(
            card => card.Rank == "2"
        ));
        Assert.That(testdeck.deck, Has.None.Matches<Card>(
            card => card.Rank == value
        ));
        Assert.AreEqual(52, testdeck.deck.Count);
    }

    [Test]
    public void TestingtwoDecks() {
        Deck testdeck = new Deck();
        testdeck.DeckSize = 2;
        testdeck.CreateDeck();
        Assert.AreEqual(104, testdeck.deck.Count);
    }

    [Test]
    public void EmptyDeckTest() {
        Deck testdeck = new Deck();
        BlackJack game = new BlackJack(testdeck);
        game.blackjackdeck.CreateDeck();
        game.blackjackdeck.DeckSize = 1;

        Console.SetIn(new StringReader(""));
        game.PlayerCount = 1;
        game.SetPlayerNames();

        for (int i = 0; i < 100; i++){
            Card card = testdeck.DrawCard();
            game.players[0].DrawACard(card, 0);
        }

        Assert.AreEqual(4, game.blackjackdeck.deck.Count);
    }
}