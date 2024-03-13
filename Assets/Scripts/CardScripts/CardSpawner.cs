using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    public GameObject card; // Card Prefab
    public Vector3 startPos;
    public Transform[] cardPos = new Transform[4];
    public int cardCount = 4;
    public int choiceCount = 1;
    internal int freeCards;

    void Start()
    {
        SpawnCards();
    }


    void Update()
    {
        if (freeCards <= cardCount - choiceCount)
        {
            do
            {
                CardSelectScript cardScript = transform.GetChild(0).transform.GetComponent<CardSelectScript>();
                cardScript.removeCard();
            } while (transform.GetChild(0));
            }


    }

    public void SpawnCards()
    {
        for (int i = 0; i < cardCount; i++)
        {
            GameObject newCard = Instantiate(card, transform);
            newCard.transform.position = startPos;
            StartCoroutine(PositionCard(newCard, i));
            freeCards = cardCount;
        }
    }

    public void receiveCardInfo()
    {
        Debug.Log("Event worked");
    }

    IEnumerator PositionCard(GameObject newCard, int cardId)
    {
        float lerpTimer = 0f;
        do
        {
            newCard.transform.position = Vector3.Lerp(startPos, cardPos[cardId].position, lerpTimer);
            lerpTimer += 0.025f;
            yield return new WaitForSeconds(0.025f);
        } while (lerpTimer <= 1);

        newCard.GetComponent<CardSelectScript>().enabled = true;

        yield return null;
    }

}
