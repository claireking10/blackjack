using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // script is used for both player and dealer

    // get other scripts
    public CardScript cardScript;
    public DeckScript deckScript;
    
    // Total value of player/dealer's hand
    public int handValue = 0;
    
    //betting money
    private int money = 1000;

    // array of card objs on table
    public GameObject[] hand;
    //index of next card to be turned over
    public int cardIndex = 0;
    //tracking aces for 1 to 11 conversions
    List<CardScript> aceList = new List<CardScript>();
    
    public void StartHand()
    {
       GetCard(); 
       GetCard();
    }

    // add a hand to the player/dealer's hand
    public int GetCard()
    {
        //get a card, use deal card to assign sprite and value to card on table
        int cardValue = deckScript.DealCard(hand[cardIndex].GetComponent<CardScript>());
        // show card on game screen
        hand[cardIndex].GetComponent<Renderer>().enabled = true;
        //add card value to running total of hand
        handValue += cardValue;
        // if value is 1, it is an ace
        if(cardValue == 1)
        {
            aceList.Add(hand[cardIndex].GetComponent<CardScript>());
        }
        //check if we shld use an 11 instaed of a 1
        AceCheck();
        cardIndex++;
        return handValue;
    }

    //search for needed ace conversions, 1 to 11 or vice versa
    public void AceCheck()
    {
        // for each ace in the list check
        foreach (CardScript ace in aceList)
        {
            if(handValue + 10 < 22 && ace.GetValueOfCard() == 1)
            {
                // if converting, adjust card obj value & hand
                ace.SetValue(11);
                handValue += 10;
            } else if (handValue > 21 && ace.GetValueOfCard() == 11)
            {
                // if converting, adjust gameobject value & hand value
                ace.SetValue(1);
                handValue -= 10;
            }
        }
    }

    //add or subtract from money for bets
    public void AdjustMoney(int amount)
    {
        money += amount;
    }

    // Output players current money amt
    public int GetMoney()
    {
        return money;
    }

    //hides all cards, resets to needed variables
    public void ResetHand()
    {
        for(int i = 0; i < hand.Length; i++)
        {
            hand[i].GetComponent<CardScript>().ResetCard();
            hand[i].GetComponent<Renderer>().enabled = false;
        }
        cardIndex = 0;
        handValue = 0;
        aceList = new List<CardScript>();
    }

}
