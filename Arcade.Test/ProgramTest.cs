using Blackjack;

namespace Arcade.Test;

public class ProgramTest {



    [Test]
    public void CreateGameTest() {
        Console.SetIn(new StringReader("test\n9\n3\n\nJane\nJack"));
        Program.CreateGame();
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