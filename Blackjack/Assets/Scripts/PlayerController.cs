using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Transform DealerTransform;
    public Player Player { set; get; }
    public TextMeshProUGUI BetText;
    public TextMeshProUGUI CashText;
    public GameObject CardsObject;
    public GameObject HandObject;
    // Start is called before the first frame update

   
    void Start()
    {
        
        updateBet();
        updateCash();
        
    }

    public IEnumerator AddCard(Card card)
    {
        Player.Hand.AddCard(card);
        GameObject cardObject = Instantiate(CardsObject, DealerTransform.position, Quaternion.identity) as GameObject;
        cardObject.transform.SetParent(HandObject.transform);
        cardObject.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Images/Card/" + card.ToString());
        Vector3 position = transform.position;
        position.x += (Player.Hand.Size - 1) * 150;
        yield return StartCoroutine(cardObject.GetComponent<AnimationScript>().StartAnimation(position));
    }

    public void BetCash(int amount)
    {
        Player.BetCash(amount);
        updateBet();
        updateCash();
        Player.SaveBet();
    }

    public void AddCash(int cash) {
        Player.AddCash(cash);
        updateBet();
        updateCash();
        Player.SaveCash();
    }

    public void ResetBet() {
        Player.ResetBet();
        updateBet();
    }

    private void updateBet()
    {
        BetText.GetComponent<TextMeshProUGUI>().text = "$" + Player.Bet.ToString();
    }

    private void updateCash()
    {
        CashText.GetComponent<TextMeshProUGUI>().text = "$" + Player.Cash.ToString();
    }
}
