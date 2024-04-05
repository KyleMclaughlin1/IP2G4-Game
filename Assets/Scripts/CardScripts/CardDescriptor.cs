using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardDescriptor : MonoBehaviour
{
    public TMP_Text descText;
public void UpdateDescriptionText(string cardDesc){

descText.text = cardDesc;

//Sets ui text to the text of the currently highlighted card.

}

}