using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{

    public Slider mainMenuVolumeSlider;

    // Start is called before the first frame update
    void Start()
    {
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

    public void volumeChange()
    {
        AudioListener.volume = mainMenuVolumeSlider.value;
        saveVolume();
    }

    public void saveVolume()
    {
        PlayerPrefs.SetFloat("volumeValue", mainMenuVolumeSlider.value);
    }

    public void loadVolume()
    {
        mainMenuVolumeSlider.value = PlayerPrefs.GetFloat("volumeValue");
    }
}
