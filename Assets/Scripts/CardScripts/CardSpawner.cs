using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardSpawner : MonoBehaviour
{
    public GameObject card; // Card Prefab
    public Vector3 startPos;
    public Transform[] cardPos = new Transform[4];
    public int cardCount = 4;
    public int choiceCount = 1;
    internal int freeCards;

    private float deleteTimer; // Timer for deleting scene once card is selected
    private bool sceneEnded = false;

    void Start()
    {
        SpawnCards();
    }


    void Update()
    {
        if (freeCards <= cardCount - choiceCount && !sceneEnded)
        {
            deleteTimer = transform.GetChild(0).GetComponent<CardSelectScript>().cardRemovalTime;
            sceneEnded = true;
            foreach (Transform cardObj in transform.GetComponentInChildren<Transform>())
            {


                CardSelectScript cardScript = cardObj.GetComponent<CardSelectScript>();
                cardScript.removeCard();
            }

        }
        else if(sceneEnded) {
            deleteTimer -= Time.deltaTime;
            if(deleteTimer <= 0)
            {
                if (GameObject.Find("GameManager"))
                {
                    GameObject gameManager = GameObject.Find("GameManager");
                    gameManager.GetComponent<SceneTestScript>().testNum += 1;
                }

                SceneManager.UnloadSceneAsync(2);
            }
        
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
