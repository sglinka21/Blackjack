using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public DealerController DealerController;
    public PlayerController PlayerController;
    public AudioClip audioChip;
    public AudioClip audioScore;
    public GameState GameState { get; set; }
    private Deck deck;
    void Awake()
    {
        DealerController.Dealer = new Dealer();
        PlayerController.Player = new Player("Player", 10000);
        deck = new Deck();
    }
    // Start is called before the first frame update
    void Start()
    {
        GameState = new GameState(StateOfGame.Betting);
        StartCoroutine(gameProcess());
    }

    IEnumerator gameProcess()
    {
        while (true)
        {
            switch (GameState.StateOfGame)
            {
                case StateOfGame.Betting:
                    {
                        break;
                    }
                case StateOfGame.DealFirstCardToPlayer:
                    {
                        yield return StartCoroutine(PlayerController.AddCard(deck.DealCard()));
                        GameState.StateOfGame = StateOfGame.DealSecondCardToPlayer;
                        break;
                    }
                case StateOfGame.DealSecondCardToPlayer:
                    {
                        yield return StartCoroutine(PlayerController.AddCard(deck.DealCard()));
                        GameState.StateOfGame = StateOfGame.DealTwoCardsToDealer;
                        break;
                    }
                
                case StateOfGame.DealTwoCardsToDealer:
                    {
                        yield return StartCoroutine(DealerController.AddCard(deck.DealCard()));
                        yield return StartCoroutine(DealerController.AddCard(deck.DealCard()));

                        if (IsBlackjack(PlayerController.Player.Hand))
                        {
                            //blackjack, player wins!
                            GameState = new GameState(StateOfGame.ProcessResult, Result.DealerLoseToBlackjack);
                        }
                        else
                        {
                            GameState.StateOfGame = StateOfGame.WaitForPlayerToAct;
                            
                        }


                        
                        break;
                    }
                case StateOfGame.WaitForPlayerToAct:
                    {
                        break;
                    }
                case StateOfGame.WaitForDealerToAct:
                    {
                        Action action = DealerController.DecideAction(PlayerController.Player.Hand);
                        if (action == Action.Hit)
                        {
                            yield return StartCoroutine(DealerController.AddCard(deck.DealCard()));
                            if (isBreaking(DealerController.Dealer.Hand))
                            {
                                GameState = new GameState(StateOfGame.ProcessResult, Result.DealerLose);
                            }
                        }
                        else if (action == Action.Stand)
                        {
                            GameState = new GameState(
                                StateOfGame.ProcessResult,
                                compareHands(DealerController.Dealer.Hand, PlayerController.Player.Hand));
                        }
                        break;
                    }
                case StateOfGame.ProcessResult:
                    {
                        
                        Result gameResult = (Result)GameState.Data;
                        if (gameResult == Result.DealerLoseToBlackjack)
                        {
                            PlayerController.Player.AddCash(2*PlayerController.Player.Bet);
                            PlayerController.Player.ResetBet();
                        }
                        else
                        {
                            DealerController.showSecondCard();
                            PlayerController.Player.ResetBet();
                        }
                        if (gameResult == Result.DealerLose)
                        {
                            PlayerController.Player.AddCash(2 * PlayerController.Player.Bet);
                        }
                        else if (gameResult == Result.PlayerLose)
                        {
                            //GameResultText.GetComponent<Text>().text = "Player Lose";
                            PlayerController.Player.ResetBet();
                        }
                        else if (gameResult == Result.Tie)
                        {
                            PlayerController.Player.BetCash(PlayerController.Player.Bet);
                        }

                        // Clean up...
                        //PlayerController.Player.ResetBet();
                        PlayerController.Player.Hand.Clear();
                        DealerController.Dealer.Hand.Clear();
                        deck.ShuffleCard();

                        GameState.StateOfGame = StateOfGame.WaitForRestart;
                        break;
                    }
                case StateOfGame.WaitForRestart:
                    {
                        GameState.StateOfGame = StateOfGame.Betting;
                        SceneManager.LoadScene("GameScene");
                        break;
                    }
            }
            yield return null;
        }
    }
    public void Chip10Button()
    {
        if (GameState.StateOfGame == StateOfGame.Betting)
        {
            AudioSource.PlayClipAtPoint(audioChip, Vector2.zero, SettingsController.GetSoundVolume());
            PlayerController.BetCash(10);
        }
    }

    public void Chip25Button()
    {
        if (GameState.StateOfGame == StateOfGame.Betting)
        {
            AudioSource.PlayClipAtPoint(audioChip, Vector2.zero, SettingsController.GetSoundVolume());
            PlayerController.BetCash(25);
        }
    }

    public void Chip50Button()
    {
        if (GameState.StateOfGame == StateOfGame.Betting)
        {
            AudioSource.PlayClipAtPoint(audioChip, Vector2.zero, SettingsController.GetSoundVolume());
            PlayerController.BetCash(50);
        }
    }

    public void Chip100Button()
    {
        if (GameState.StateOfGame == StateOfGame.Betting)
        {
            AudioSource.PlayClipAtPoint(audioChip, Vector2.zero, SettingsController.GetSoundVolume());
            PlayerController.BetCash(100);
        }
    }

    public void Chip250Button()
    {
        if (GameState.StateOfGame == StateOfGame.Betting)
        {
            AudioSource.PlayClipAtPoint(audioChip, Vector2.zero, SettingsController.GetSoundVolume());
            PlayerController.BetCash(250);
        }
    }

    public void DealButton() {
        if (GameState.StateOfGame == StateOfGame.Betting)
        {
            if (PlayerController.Player.Bet > 0)
            {
                GameState.StateOfGame = StateOfGame.DealFirstCardToPlayer;
            }
        }
    }

    public void StandButton() {
        if (GameState.StateOfGame == StateOfGame.WaitForPlayerToAct)
        {
            GameState.StateOfGame = StateOfGame.WaitForDealerToAct;
        }
    }

    public void HitButton()
    {
        if (GameState.StateOfGame == StateOfGame.WaitForPlayerToAct)
        {
            StartCoroutine(PlayerController.AddCard(deck.DealCard()));
            if (isBreaking(PlayerController.Player.Hand))
            {
                
                GameState = new GameState(StateOfGame.ProcessResult, Result.PlayerLose);
            }
            else
            {
                GameState.StateOfGame = StateOfGame.WaitForPlayerToAct;
            }
        }
    }

    public void DoubleDownButton()
    {
        if (GameState.StateOfGame == StateOfGame.WaitForPlayerToAct)
        {
            // Double bet
            PlayerController.BetCash(PlayerController.Player.Bet);
            // Hit
            StartCoroutine(PlayerController.AddCard(deck.DealCard()));
            if (isBreaking(PlayerController.Player.Hand))
            {
                // Check for breaking...
                GameState = new GameState(StateOfGame.ProcessResult, Result.PlayerLose);
            }
            else
            {
                // Stand
                GameState.StateOfGame = StateOfGame.WaitForDealerToAct;
            }
        }
    }

    public static int CalculateHand(Hand hand)
    {
        int sum = 0;
        bool haventSeenA = true;
        foreach (Card card in hand.Cards)
        {
            if (haventSeenA && card.Rank == 1)
            {
                sum += 11;
                haventSeenA = false;
            }
            else if (card.Rank > 10)
            {
                sum += 10;
            }
            else
            {
                sum += card.Rank;
            }
        }
        if (sum > 21 && !haventSeenA)
        {
            sum -= 10;
        }
        return sum;
    }

    private bool isBreaking(Hand hand)
    {
        return CalculateHand(hand) > 21;
    }

    // public for unit test
    // TODO: we probably want to move majority of the code to another class that doesn't inherit MonoBehaviour
    public bool IsBlackjack(Hand hand)
    {
        return CalculateHand(hand) == 21 && hand.Size == 2;
    }

    private Result compareHands(Hand dealerHand, Hand playerHand)
    {
        int dealerHandValue = CalculateHand(dealerHand);
        int playerHandValue = CalculateHand(playerHand);
        if (dealerHandValue > playerHandValue)
        {
            return Result.PlayerLose;
        }
        else if (dealerHandValue == playerHandValue)
        {
            return Result.Tie;
        }
        else
        {
            return Result.DealerLose;
        }
    }
}
