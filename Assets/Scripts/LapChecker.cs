using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LapChecker : MonoBehaviour
{
    [HideInInspector]
    public List<CheckPointCollision> checkPointList; // List of checkpoints to check order
    private int checkPIndex = 0; // Integer to track Checkpoint progress

    public int trackLap = 0;
    private int changeLap = 0;
    private int currentTrack = 0;

    public TMP_Text lapText;

    [Tooltip("How many laps it takes to transition from each track")]
    public List<int> trackChangeRequirements;
    [Tooltip("Track gameobjects to switch out when laps completed")]
    public List<GameObject> tracks;

    private void Awake()
    {
      SetUpCheckPoints( tracks[0].transform.Find("CheckPoints") );
      changeLap = trackLap + trackChangeRequirements[0];

    }

    private void SetUpCheckPoints(Transform checkPointParent){
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
                if(trackLap >= changeLap){

                    tracks[currentTrack].SetActive(false);

                    currentTrack += 1;
                    changeLap = trackLap + trackChangeRequirements[currentTrack];
                    tracks[currentTrack].SetActive(true);

                    checkPointList.Clear();

                    SetUpCheckPoints( tracks[currentTrack].transform.Find("CheckPoints") );

                }


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
