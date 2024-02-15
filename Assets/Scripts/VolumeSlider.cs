using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{

    //declaring variable
    public Slider mainMenuVolumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        // checks the volume value and sets it to what it was previously
        //if the volume hasnt been changed before it sets to 0.5
        if (!PlayerPrefs.HasKey("volumeValue"))
        {
            PlayerPrefs.SetFloat("volumeValue", 0.5f);
            loadVolume();
        }
        else
        {
            loadVolume();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //method for changing volume
    public void volumeChange()
    {
        // adjusts volume with the volume slider game object
        AudioListener.volume = mainMenuVolumeSlider.value;
        // method to save volume for next play
        saveVolume();
    }

    // method for saving volume 
    public void saveVolume()
    {
        // saves the value thats been set in the slider during a play session
        PlayerPrefs.SetFloat("volumeValue", mainMenuVolumeSlider.value);
    }

    // method for loading volume
    public void loadVolume()
    {
        // loads the value that was saved from the slider from a previous session then sets the value of the slider to it
        mainMenuVolumeSlider.value = PlayerPrefs.GetFloat("volumeValue");
    }
}
