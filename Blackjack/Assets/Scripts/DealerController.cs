using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealerController : MonoBehaviour
{
    public GameObject CardObject;
    public GameObject HandObject;
    
    public Dealer Dealer { set; get; }

    private GameObject secondCardObject;

    public IEnumerator AddCard(Card card)
    {
        Dealer.Hand.AddCard(card);
        GameObject cardObject =
            Instantiate(CardObject, gameObject.transform.position, Quaternion.identity)
            as GameObject;
        cardObject.transform.SetParent(HandObject.transform);
        if (Dealer.Hand.Size != 2)
        {
            cardObject.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Images/Card/" + card.ToString());
        }
        else
        {
            secondCardObject = cardObject;
        }
        Vector3 position = transform.position;
        position.x += (Dealer.Hand.Size - 1) * 150;
        position.y -= 10;
        yield return StartCoroutine(cardObject.GetComponent<AnimationScript>().StartAnimation(position));
    }

    public Action DecideAction(Hand playerHand)
    {
        int handValue = GameController.CalculateHand(Dealer.Hand);
        if (handValue > 16)
        {
            return Action.Stand;
        }
        else
        {
            return Action.Hit;
        }
    }

    public void showSecondCard()
    {
        secondCardObject.GetComponent<Image>().overrideSprite =
            Resources.Load<Sprite>("Images/Card/" + Dealer.Hand.Cards[1].ToString());
    }
}
