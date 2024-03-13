using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class CardSelectScript : MonoBehaviour
{
    // Start is called before the first frame update
    [Tooltip("How much the scale of the card inceases")]
    public float hoverScaleIncrement = 1.25f;
    private float startScale; // What the card scale starts at
    private Vector3 storedScale;
    private float hoverScale; // What the card scales to on selected
    internal bool mouseHover = false; // True when mouse is hovering over card
    internal bool cardSelected = false; //Turns true when the card has been clicked
    private bool cardRemoved = false; //True if card is the last card, meaning it must be discarded
    private float cardRemovalLerp = 0f; // Stores how far along the exit Lerp/Slerp the card is;
    private float cardRemovalTimer = 0f; // Stores time for the lerp on card exit
    [Tooltip("How long the card takes to exit screen after being selected")]
    public float cardRemovalTime = 3f;
    private Vector3 startPos;
    [Tooltip("Where the card leaves to after being selected")]
    public Vector3 endPos;
    [Tooltip("Where the ignored card will leave to after all other cards are selected")]
    public Vector3 ignoredEndPos;
    [Tooltip("Moves the card faster each frame while leaving")]
    public float cardAccel = 0.05f;
    private float cardSpeedBoost = 0; // Tracks incease from cardAccel.
    private CardSpawner cardControl; // Link to card spawner script, for tracking card amount
    public UnityEvent sendCard; // Event for sending card info


    internal string cardDesc = "This card is useless"; // Temp variable to take the place of the cards description

    void Start()
    {
        startScale = transform.localScale.x; // Sets startscale to objects original size (on x)
        hoverScale = startScale + hoverScaleIncrement; // Sets hoverScale
        storedScale = transform.localScale;
        startPos = transform.position;
        cardControl = GetComponentInParent<CardSpawner>();
        sendCard.AddListener(cardControl.receiveCardInfo);
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseHover && Input.GetKeyDown(KeyCode.Mouse0) && cardControl.freeCards > 1 && !cardSelected && !cardRemoved)
        {

            sendCardInfo();


            cardSelected = true;
            cardControl.freeCards -= 1;
            transform.localScale = storedScale;
        }
        if (cardSelected)
        {
            transform.position = Vector3.Slerp(startPos, endPos, cardRemovalLerp);
            cardRemovalTimer += Time.deltaTime + (cardSpeedBoost * Time.deltaTime);
            cardRemovalLerp = cardRemovalTimer / cardRemovalTime;
            cardSpeedBoost += cardAccel;
            if (cardRemovalLerp >= 1f)
            {
                Destroy(gameObject);
            }
        }
        else if (cardRemoved)
        {
            transform.position = Vector3.Slerp(startPos, ignoredEndPos, cardRemovalLerp);
            cardRemovalTimer += Time.deltaTime + (cardSpeedBoost * Time.deltaTime);
            cardRemovalLerp = cardRemovalTimer / cardRemovalTime;
            cardSpeedBoost += cardAccel;
        }





    }
    void OnMouseOver()
    {
        if (!cardSelected && transform.GetComponent<CardSelectScript>().enabled)
        {
            mouseHover = true;
            StartCoroutine(ScaleCard());
        }
    }

    void OnMouseExit()
    {
        if (!cardSelected && transform.GetComponent<CardSelectScript>().enabled)
        {
            mouseHover = false;
            do
            {
                StartCoroutine(DeScaleCard());
            } while (transform.localScale.x > startScale);

        }
    }

    public void sendCardInfo()
    {
        sendCard.Invoke();
    }


    public void removeCard()
    {
        cardRemoved = true;
    }


    IEnumerator ScaleCard()
    {
        if (mouseHover && transform.localScale.x < hoverScale)
        {
            transform.localScale = new Vector3(transform.localScale.x + 0.1f, transform.localScale.y + 0.1f, transform.localScale.z);
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            yield return null;
        }


    }

    IEnumerator DeScaleCard()
    {
        if (!mouseHover)
        {
            transform.localScale = new Vector3(transform.localScale.x - 0.1f, transform.localScale.y - 0.1f, transform.localScale.z);
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            yield return null;
        }

    }

}