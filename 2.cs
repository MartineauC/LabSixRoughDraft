using System;
using System.Collections.Generic;

public class Card
{
    public string Suit { get; set; }
    public string Rank { get; set; }
    public int Value { get; set; }
}

public class Hand
{
    protected List<Card> cards;

    public Hand()
    {
        cards = new List<Card>();
    }

    public void AddCard(Card card)
    {
        cards.Add(card);
    }

    public void Clear()
    {
        cards.Clear();
    }

    public int GetScore()
    {
        int score = 0;
        bool hasAce = false;

        foreach (Card card in cards)
        {
            score += card.Value;
            if (card.Rank == "Ace")
            {
                hasAce = true;
            }
        }

        if (hasAce && score + 10 <= 21)
        {
            score += 10;
        }

        return score;
    }
}

public class BlackjackHand : Hand
{
    public bool HasAce()
    {
        foreach (Card card in cards)
        {
            if (card.Rank == "Ace")
            {
                return true;
            }
        }
        return false;
    }

    public bool IsBusted()
    {
        return GetScore() > 21;
    }
}
