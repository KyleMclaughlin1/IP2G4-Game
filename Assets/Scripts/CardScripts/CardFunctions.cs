using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFunctions : MonoBehaviour
{
    // Start is called before the first frame update
public CardFunctionsEnum cardFunc;
delegate void funcDelegate();

public List<System.Action> functionList = new List<System.Action>();

System.Action upgrade;

public GameObject player;
public GameObject playerCannon;


public void Start(){
   // upgrade = cardFunc.ToString();
//for (int i = 0; Enum.GetNames(typeof(CardFunctionsEnum)).Length > i; i++)
//{
functionList.Add(AttackUp);
functionList.Add(AttackRateUp);
functionList.Add(DoubleShot);
functionList.Add(RapidFire);


//}

}


public void runCardFunc(){
int enumInt = System.Convert.ToInt32(cardFunc);

upgrade = functionList[enumInt];

upgrade();

}
 
public void AttackUp(){
Debug.Log("Attackup triggered");

playerCannon.GetComponent<CannonControl>().bulletDamageMultiplier += 1;
}

private void AttackRateUp(){
Debug.Log("AttackRateUp triggered");

playerCannon.GetComponent<CannonControl>().fireRate /= 2;

}

private void DoubleShot(){
Debug.Log("DoubleShot triggered");

playerCannon.GetComponent<CannonControl>().bulletCount += 1;
}

private void RapidFire(){
Debug.Log("RapidFire triggered");

playerCannon.GetComponent<CannonControl>().cannonAuto = true;

}



}


   public enum CardFunctionsEnum {
        AttackUp,
        AttackRateUp,
        DoubleShot,

        RapidFire


    }