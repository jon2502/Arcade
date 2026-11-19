using System;

public class Player : Participant {
    public float Chips {get; set;} = 100;
    public List<float> Bet {get; set;} = [0];
    public List<float> Insurance {get; set;} = [0];

    public List<bool> Stand {get; set;} = [false];

    public float Retuns {get; set;} = 0;
    public bool InGame {get; set;} = true;

    public bool PlayerOption (string input) {
            if (input == "y" || input == "Y") {
                return true;
            } return false;
        }

    public Results SetBet(string input, int i) {
        Results results = new Results();
        try {
            float output = float.Parse(input);
            if (output <= 1) {
                results.IsSuccess = false;
                results.Message = $"{Name} please select a value of 2 or greater";
            } else if (output > Chips) {
                results.IsSuccess = false;
                results.Message = $"{Name} balance to low";
            } else {
                Chips -= output;
                Bet[i] = output;
                results.IsSuccess = true;
                results.Message = $"{Name} bet is {Bet[i]}";
            }
        } catch {
            results.IsSuccess = false;
            results.Message = $"{Name} please select a number";
        }
        return results;  
    }

    public Results SetInsurance(string input, int i) {
        Results results = new Results();
        try {
            float output = float.Parse(input);
            if (output > Chips) {
                results.IsSuccess = false;
                results.Message = $"{Name} balance to low";
            } else if (output > Bet[i] / 2) {
                results.IsSuccess = false;
                results.Message = $"{Name} Inssurance can max be half your bet";
            } else {
                Chips -= output;
                Insurance[i] = output;
                results.IsSuccess = true;
                results.Message = $"Insurance of {Insurance[i]} has been set for the hand";
            }
        } catch {
            results.IsSuccess = false;
            results.Message = $"{Name} please select a valid full number";
        }
        return results;
    }

    public void Inssurance(){
         Bet.Clear();
        InGame = false;
        for (int i = 0; i < Insurance.Count; i++) {
            Retuns += Insurance[i] * 2;
        };
        Insurance.Clear();
        Hand.Clear();
        CheckPlayerState();           
    }
    

    public bool PlayerBlackjack(int i) {
        bool result = Blackjack(i);
        if(result) {
            Console.WriteLine($"{Name} got BlackJack");
            Console.WriteLine($"{Bet[i] * 1.5f} has been aded to returns");

            Retuns = Bet[i] * 1.5f;
            Stand[i] = true;
            CheckPlayerState();
            return true;
        }
        return false;
    }

    public override bool BustCheck(int index){
        bool result = base.BustCheck(index);
        if(result) {
            Console.WriteLine($"{Name} has Busted with a hand of");
            DisplayHand(index);
            Stand[index] = true;
            Bet[index] = 0;
            CheckPlayerState();
            return true;
        }
        return false;
    }

    public Results DoubleDown(int i) {
        Results result = new Results();
            if(Bet[i] > Chips){
                result.Message = $"{Name} balance to low you cant Double down";
                result.IsSuccess = false;
            } else {
                Stand[i] = true;
                Chips -= Bet[i];
                Bet[i] *= 2;
                result.IsSuccess = true;
            }
        return result;
    }

    public Results CheckSplit(int i) {
        Results results = new Results();
        if(Hand[i].Count == 2 && Hand[i][0].Value == Hand[i][1].Value){
            if(Hand[i][0].Rank == "A" && Hand[i][1].Rank == "A") {
                results.Message = $"{Name} you have two aces so your hand will be split";
            } else {
                results.Message = $"""
                    {Name} You have cards with the same value and may split them if you choce
                    {DisplayHand(i)}
                """;
            }
            results.IsSuccess = true;
        } else {results.IsSuccess = false;}
        return results;
    }
    public void Split(int i) {
        Card card = Hand[i][0];
        Hand[i].RemoveAt(0);
        Hand.Add([card]);
        Bet.Add(0);
        Insurance.Add(0);
        Stand.Add(false);
    }

    public string Surrender(int i) {
        float Refund = Bet[i]/2;
        Chips += Refund;
        Bet.Clear();
        Insurance.Clear();
        Hand.Clear();
        Stand.Clear();
        return  $"Surrende accepted now returning {Refund} to your chip pool";
    }

    /*public bool StandOrHit(int i){
        Console.WriteLine($"{Name} would you like to Hit or stand for your hand of");
        DisplayHand(i);
        Console.WriteLine($"with a bet of {Bet[i]}");
        //bool Output = PlayerOption();
        return Output;
    }*/

    public bool Canplay(){
        double AllBets = Bet.Sum(bet => bet);
        if(Chips <= 0 && AllBets <= 0 && Retuns <= 0){
            return false;
        }
        return true;
    }

    /*public bool WantToContinue() {
        Console.WriteLine($"{Name} would you continue or would you like to leave the table");
        //bool Output = PlayerOption();
        //return Output;

    }*/

    public void CheckPlayerState(){
        if(Stand.All(x => x==true) || Hand.Count <= 0){
            Console.WriteLine($"{Name} has no active hands left and is now out of the game");
            InGame = false;
        } else {
            InGame = true;
        }
    }
}