
namespace Arcade.Test;

public class DeckTest {

    private Deck blackjackdeck;

    [SetUp]
    public void SetUp() {
        blackjackdeck = new Deck();
    }

    [TestCase("1", 1)]
    [TestCase("2", 2)]
    [TestCase("3", 3)]
    [TestCase("4", 4)]
    [TestCase("5", 5)]
    [TestCase("6", 6)]
    [TestCase("7", 7)]
    [TestCase("8", 8)]

    public void DeckSelectionTest(string input, int expectedNumber){
        blackjackdeck.SetDecksize(input);
        Assert.AreEqual(expectedNumber, blackjackdeck.DeckSize);
    }

    [TestCase("-1")]
    [TestCase("0")]
    [TestCase("9")]
    [TestCase("10")]
    [TestCase("Test")]
    [TestCase("")]

    public void DeckSelectionTestFail(string input){
        blackjackdeck.SetDecksize(input);
        Assert.AreEqual(1, blackjackdeck.DeckSize);
    }

    [Test]
    public void DeckGenerationtest(){
        bool result = blackjackdeck.CreateDeck();
        Assert.That(blackjackdeck.deck, Has.Some.Matches<Card>(
            card => card.Suit == "♦"
        ));
        Assert.That(blackjackdeck.deck, Has.Some.Matches<Card>(
            card => card.Suit == "♣"
        ));        
        Assert.That(blackjackdeck.deck, Has.Some.Matches<Card>(
            card => card.Suit == "♥"
        ));        
        Assert.That(blackjackdeck.deck, Has.Some.Matches<Card>(
            card => card.Suit == "♠"
        ));
        Assert.True(result);
    }

    [Test]
    public void DeckGenerationtestfail(){
        blackjackdeck.PlayingCards = new string[] {"3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};
        bool result = blackjackdeck.CreateDeck();
        Assert.IsEmpty(blackjackdeck.deck);
        Assert.False(result);
    }

    [Test]
    public void DeckGenerationtesparsefail(){
        blackjackdeck.PlayingCards  = new string[]  {"2", "3", "4", "test", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};
        bool result = blackjackdeck.CreateDeck();
        Assert.False(result);

        blackjackdeck.CreateDeck();
        Assert.That(blackjackdeck.deck, Has.Some.Matches<Card>(
            card => card.Rank == "2"
        ));
        Assert.That(blackjackdeck.deck, Has.Some.Matches<Card>(
            card => card.Rank == "3"
        ));        
        Assert.That(blackjackdeck.deck, Has.Some.Matches<Card>(
            card => card.Rank == "4"
        ));        
        Assert.That(blackjackdeck.deck, Has.Some.Matches<Card>(
            card => card.Value == 11
        ));        
        Assert.That(blackjackdeck.deck, Has.Some.Matches<Card>(
            card => card.Suit == "♦"
        ));
        Assert.AreEqual(52, blackjackdeck.deck.Count);
    }

    [TestCase("0")]
    [TestCase("1")]
    [TestCase("11")]
    [TestCase("12")]
    public void DeckGenerationValueOverandUnder(string value){
        blackjackdeck.PlayingCards = new  string[] {value, "3", "4", "test", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};
        bool result = blackjackdeck.CreateDeck();
        Assert.False(result);

        blackjackdeck.CreateDeck();
        Assert.That(blackjackdeck.deck, Has.Some.Matches<Card>(
            card => card.Rank == "2"
        ));
        Assert.That(blackjackdeck.deck, Has.None.Matches<Card>(
            card => card.Rank == value
        ));
        Assert.AreEqual(52, blackjackdeck.deck.Count);
    }

    [Test]
    public void TestingtwoDecks() {
        blackjackdeck.DeckSize = 2;
        blackjackdeck.CreateDeck();
        Assert.AreEqual(104, blackjackdeck.deck.Count);
    }

    [Test]
    public void EmptyDeckTest() {
        BlackJack game = new BlackJack(blackjackdeck);
        game.blackjackdeck.CreateDeck();
        game.blackjackdeck.DeckSize = 1;
        game.PlayerCount = 1;
        for (int i = 0; i < 100; i++){
            Card card = blackjackdeck.DrawCard();
            game.players[0].DrawACard(card, 0);
        }

        Assert.AreEqual(4, game.blackjackdeck.deck.Count);
    }
}