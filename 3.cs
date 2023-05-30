using System;
using System.Collections.Generic;

class Card
{
    public string Suit { get; set; }
    public string Rank { get; set; }
    public int Value { get; set; }
}

class Deck
{
    private List<Card> cards;
    private Random random;

    public Deck()
    {
        cards = new List<Card>();
        random = new Random();

        string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
        string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

        foreach (string suit in suits)
        {
            foreach (string rank in ranks)
            {
                int value = int.TryParse(rank, out int result) ? result : rank == "A" ? 11 : 10;
                cards.Add(new Card { Suit = suit, Rank = rank, Value = value });
            }
        }
    }

    public void Shuffle()
    {
        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            Card card = cards[k];
            cards[k] = cards[n];
            cards[n] = card;
        }
    }

    public Card DealCard()
    {
        if (cards.Count == 0)
            throw new InvalidOperationException("The deck is empty.");

        Card card = cards[0];
        cards.RemoveAt(0);
        return card;
    }
}

class Hand
{
    private List<Card> cards;

    public Hand()
    {
        cards = new List<Card>();
    }

    public void AddCard(Card card)
    {
        cards.Add(card);
    }

    public int GetHandValue()
    {
        int value = 0;
        int numAces = 0;

        foreach (Card card in cards)
        {
            value += card.Value;
            if (card.Rank == "A")
                numAces++;
        }

        while (value > 21 && numAces > 0)
        {
            value -= 10;
            numAces--;
        }

        return value;
    }

    public void DisplayHand(string owner)
    {
        Console.WriteLine(owner + "'s hand:");
        foreach (Card card in cards)
        {
            Console.WriteLine("  " + card.Rank + " of " + card.Suit);
        }
        Console.WriteLine("Total value: " + GetHandValue());
    }
}

class BJHand : Hand
{
    public bool IsBusted()
    {
        return GetHandValue() > 21;
    }

    public bool IsBlackjack()
    {
        return GetHandValue() == 21 && CardsCount == 2;
    }

    public int CardsCount
    {
        get { return cards.Count; }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Blackjack!");

        int playerWins = 0;
        int dealerWins = 0;

        while (true)
        {
            Deck deck = new Deck();
            deck.Shuffle();

            BJHand playerHand = new BJHand();
            BJHand dealerHand = new BJHand();

            playerHand.AddCard(deck.DealCard());
            dealerHand.AddCard(deck.DealCard());
            playerHand.AddCard(deck.DealCard());
            dealerHand.AddCard(deck.DealCard());

            playerHand.DisplayHand("Player");
            dealerHand.DisplayHand("Dealer");

            if (playerHand.IsBlackjack() && dealerHand.IsBlackjack())
            {
                Console.WriteLine("Push! It's a tie.");
            }
            else if (playerHand.IsBlackjack())
            {
                Console.WriteLine("Player wins with Blackjack!");
                playerWins++;
            }
            else if (dealerHand.IsBlackjack())
            {
                Console.WriteLine("Dealer wins with Blackjack!");
                dealerWins++;
            }
            else
            {
                while (true)
                {
                    Console.Write("Do you want to 'HIT' or 'STAND'? ");
                    string choice = Console.ReadLine().Trim().ToUpper();

                    if (choice == "HIT")
                    {
                        playerHand.AddCard(deck.DealCard());
                        playerHand.DisplayHand("Player");

                        if (playerHand.IsBusted())
                        {
                            Console.WriteLine("Player busted. Dealer wins.");
                            dealerWins++;
                            break;
                        }
                    }
                    else if (choice == "STAND")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please enter 'HIT' or 'STAND'.");
                    }
                }

                if (!playerHand.IsBusted())
                {
                    while (dealerHand.GetHandValue() < 17)
                    {
                        dealerHand.AddCard(deck.DealCard());
                    }
                    dealerHand.DisplayHand("Dealer");

                    if (dealerHand.IsBusted())
                    {
                        Console.WriteLine("Dealer busted. Player wins.");
                        playerWins++;
                    }
                    else
                    {
                        int playerValue = playerHand.GetHandValue();
                        int dealerValue = dealerHand.GetHandValue();

                        if (playerValue > dealerValue)
                        {
                            Console.WriteLine("Player wins!");
                            playerWins++;
                        }
                        else if (playerValue < dealerValue)
                        {
                            Console.WriteLine("Dealer wins!");
                            dealerWins++;
                        }
                        else
                        {
                            Console.WriteLine("Push! It's a tie.");
                        }
                    }
                }
            }

            Console.WriteLine("Player Wins: " + playerWins);
            Console.WriteLine("Dealer Wins: " + dealerWins);

            Console.Write("Do you want to play another hand? (Y/N) ");
            string playAgain = Console.ReadLine().Trim().ToUpper();
            if (playAgain != "Y")
                break;

            Console.Clear();
        }

        Console.WriteLine("Thank you for playing Blackjack!");
    }
}
