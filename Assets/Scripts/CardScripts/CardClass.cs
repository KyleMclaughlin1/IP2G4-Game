using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "cardObject", menuName = "ScriptableObjects/UpgradeCard", order = 14)]
public class CardClass : ScriptableObject
{
    // Start is called before the first frame update
public string cardName = "Undefined";

public Sprite cardSprite;

public string cardDesc = "If you're reading this, something has gone wrong";

public CardFunctionsEnum CardEffect = CardFunctionsEnum.AttackUp;

public bool oneTimeUse = false;

}
