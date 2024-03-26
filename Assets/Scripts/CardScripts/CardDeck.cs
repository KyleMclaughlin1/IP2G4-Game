using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    // Start is called before the first frame update
[Tooltip("Cards in the deck")]
public List<CardClass> classList;

[Tooltip("Card for the deck to draw when there are no cards left")]
public CardClass EmptyDeckCard;

void Awake(){
List<CardClass> storeList = new List<CardClass>(classList);



for (int i = 0; i < classList.Count; ++i){
int rand = Random.Range(0,storeList.Count);
classList[i] = storeList[rand];
storeList.RemoveAt(rand);
}

// ^ shuffles the list

}


}
