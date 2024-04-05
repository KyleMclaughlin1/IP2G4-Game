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

CannonControl cannon = playerCannon.GetComponent<CannonControl>();

cannon.bulletDamageMultiplier += 1f;
}

private void AttackRateUp(){
Debug.Log("AttackRateUp triggered");


CannonControl cannon = playerCannon.GetComponent<CannonControl>();

cannon.fireRate /= 1.45f;
cannon.bulletDamageMultiplier -= 0.2f;

}

private void DoubleShot(){
Debug.Log("DoubleShot triggered");

CannonControl cannon = playerCannon.GetComponent<CannonControl>();

cannon.bulletCount += 1;
cannon.bulletDamageMultiplier /= 1.25f;
}

private void RapidFire(){
Debug.Log("RapidFire triggered");

CannonControl cannon = playerCannon.GetComponent<CannonControl>();

cannon.cannonAuto = true;
cannon.bulletDamageMultiplier *= 1.25f;
}



}


   public enum CardFunctionsEnum {
        AttackUp,
        AttackRateUp,
        DoubleShot,

        RapidFire


    }