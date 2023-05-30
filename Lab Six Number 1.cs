using System;
using System.Collections.Generic;
using System.Linq;

public class Hand
{
    private List<Card> cards;

    public Hand()
    {
        cards = new List<Card>();
    }

    public Hand(Deck deck, int numCards)
    {
        cards = new List<Card>();
        DealCards(deck, numCards);
    }

    public int CardCount => cards.Count;

    public void AddCard(Card card)
    {
        cards.Add(card);
    }

    public Card DiscardCard(int index)
    {
        if (index < 0 || index >= cards.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Invalid index.");
        }

        Card discardedCard = cards[index];
        cards.RemoveAt(index);
        return discardedCard;
    }

    public Card GetCard(int index)
    {
        if (index < 0 || index >= cards.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Invalid index.");
        }

        return cards[index];
    }

    public bool HasCard(Card card)
    {
        return cards.Contains(card);
    }

    public bool HasCard(Value value, Suit suit)
    {
        return cards.Any(card => card.Value == value && card.Suit == suit);
    }

    public bool HasCard(Value value)
    {
        return cards.Any(card => card.Value == value);
    }

    public int IndexOfCard(Card card)
    {
        return cards.IndexOf(card);
    }

    public int IndexOfCard(Value value, Suit suit)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].Value == value && cards[i].Suit == suit)
            {
                return i;
            }
        }

        return -1;
    }

    public int IndexOfCard(Value value)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].Value == value)
            {
                return i;
            }
        }

        return -1;
    }

    public override string ToString()
    {
        return string.Join(", ", cards.Select(card => card.ToString()));
    }

    private void DealCards(Deck deck, int numCards)
    {
        for (int i = 0; i < numCards; i++)
        {
            Card card = deck.DrawCard();
            cards.Add(card);
        }
    }
}
