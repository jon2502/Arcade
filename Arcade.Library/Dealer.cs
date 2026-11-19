public class Dealer : Participant {
    
    public Results DoesTheDealerHaveAnAce() {
        Results results = new Results();
        if(Hand[0][0].Rank == "A") {
                results.Message = $"the {Name} has an Ace would you like you place insurance?";
                results.IsSuccess = true;
        } else {results.IsSuccess = false;} ;
        return results;
    }        
    public Results DealerBlackjack() {
        Results result = new Results();
        bool blackjack = Blackjack(0);
        if(blackjack) {
            result.Message =  $"""
                the {Name} got BlackJack with a hand of {Hand[0][0].Suit}{Hand[0][0].Rank} and {Hand[0][1].Suit}{Hand[0][1].Rank}
                All players lose
            """;
            result.IsSuccess = true;
        } else {
            result.Message = $"the {Name}'s hand is {Hand[0][0].Suit}{Hand[0][0].Rank} and {Hand[0][1].Suit}{Hand[0][1].Rank}";
            result.IsSuccess = false;
        }
        return result;
    }
    
    public bool Dealerhit(){
        int Handsum = Hand[0].Sum(card => card.Value);
        DisplayHand(0);
        bool result = BustCheck(0);
        if (result){
            return true;
        }  return false;
    }
}