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
}

private void AttackRateUp(){
Debug.Log("AttackRateUp triggered");
}

private void DoubleShot(){
Debug.Log("DoubleShot triggered");
}

private void RapidFire(){
Debug.Log("RapidFire triggered");
}



}


   public enum CardFunctionsEnum {
        AttackUp,
        AttackRateUp,
        DoubleShot,

        RapidFire


    }