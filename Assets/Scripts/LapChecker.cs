using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LapChecker : MonoBehaviour
{
    [Tooltip("Parent Transform of checkpoint objects")]
    public Transform checkPointParent;

    public List<CheckPointCollision> checkPointList; // List of checkpoints to check order
    private int checkPIndex = 0; // Integer to track Checkpoint progress

    public int trackLap = 0;

    public TMP_Text lapText;

    private void Awake()
    {
      foreach(Transform checkPoint in checkPointParent.GetComponentInChildren<Transform>())
        {
            Debug.Log(checkPoint);

            if (checkPoint.GetComponent<CheckPointCollision>())
            {
                CheckPointCollision checkPCol = checkPoint.GetComponent<CheckPointCollision>();

                checkPCol.SetCheckPoint(this);
                checkPointList.Add(checkPCol);

            }
        }
    }

    public void PlayerHitCheckpoint(CheckPointCollision hitCheckPoint)
    {
      if(hitCheckPoint == checkPointList[checkPIndex])
        {
            checkPIndex += 1;
            Debug.Log("Correct CheckPoint");

            if(checkPIndex >= checkPointList.Count)
            {
                checkPIndex = 0;
                trackLap += 1;
                lapText.text = "Lap " + trackLap;
                CardUpgrades();
            }
        }
        else
        {
            Debug.Log("Incorrect Checkpoint");
        }
    }


    private void CardUpgrades(){
    GameManager gameManag = GetComponent<GameManager>();
            gameManag.enabled = false;
            Time.timeScale = 0.01f; // Would be 0, but the mouse over function doesn't seem to work that way
            SceneManager.LoadScene("CardScene", LoadSceneMode.Additive);

    }

}
