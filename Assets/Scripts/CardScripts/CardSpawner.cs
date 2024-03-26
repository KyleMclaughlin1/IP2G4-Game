using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardSpawner : MonoBehaviour
{
    public GameObject card; // Card Prefab
    public CardDescriptor cardDescriptor;
    public Vector3 startPos;
    public Transform[] cardPos = new Transform[4];
    public int cardCount = 4;
    public int choiceCount = 1;
    internal int freeCards;

    private float deleteTimer; // Timer for deleting scene once card is selected
    private bool sceneEnded = false;

    private CardDeck cardDeck; // Reference to card deck script


    void Start()
    {
        cardDeck = GetComponent<CardDeck>();
        SpawnCards();
    }


    void Update()
    {
        if (freeCards <= cardCount - choiceCount && !sceneEnded)
        {
            deleteTimer = transform.GetChild(0).GetComponent<CardSelectScript>().cardRemovalTime / 2;
            sceneEnded = true;
            foreach (Transform cardObj in transform.GetComponentInChildren<Transform>())
            {


                CardSelectScript cardScript = cardObj.GetComponent<CardSelectScript>();
                cardScript.removeCard();
            }

        }
        else if(sceneEnded) {
            deleteTimer -= Time.unscaledDeltaTime;
            if(deleteTimer <= 0)
            {
                if (GameObject.Find("GameManager"))
                {
                    GameObject gameManager = GameObject.Find("GameManager");
                   // gameManager.GetComponent<SceneTestScript>().testNum += 1;
                    gameManager.GetComponent<GameManager>().enabled = true;

                    Time.timeScale = 1f;
                }

                SceneManager.UnloadSceneAsync(2);
            }
        
        }


    }

public CardClass DrawCard(){
    if(cardDeck.classList[0]){
CardClass drawnCard = cardDeck.classList[0];
//Get card on top of deck

cardDeck.classList.RemoveAt(0);
//Remove card from deck

return drawnCard;
    }
        return cardDeck.EmptyDeckCard;
        //Deck is empty, get replacement card

}


    public void SpawnCards()
    {
        for (int i = 0; i < cardCount; i++)
        {
            GameObject newCard = Instantiate(card, transform);
            newCard.transform.position = startPos;
            CardSelectScript newCardScript = newCard.GetComponent<CardSelectScript>();
            newCardScript.cardInfo = DrawCard();
            newCardScript.sendCard.AddListener(this.receiveCardInfo);
            newCardScript.highLightcard.AddListener(cardDescriptor.UpdateDescriptionText);
            StartCoroutine(PositionCard(newCard, i));
            freeCards = cardCount;
        }
    }

    public void receiveCardInfo(CardClass cardInfo)
    {
        Debug.Log("Event worked " + cardInfo.cardName);
        CardFunctions CardEff = GetComponent<CardFunctions>();
        CardEff.cardFunc = cardInfo.CardEffect;
        CardEff.runCardFunc();
    }

    IEnumerator PositionCard(GameObject newCard, int cardId)
    {
        float lerpTimer = 0f;
        do
        {
            newCard.transform.position = Vector3.Lerp(startPos, cardPos[cardId].position, lerpTimer);
            lerpTimer += 0.025f;
            yield return new WaitForSecondsRealtime(0.025f);
        } while (lerpTimer <= 1);

        newCard.GetComponent<CardSelectScript>().enabled = true;

        yield return null;
    }

}
