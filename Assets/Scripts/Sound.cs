using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    [SerializeField] Image soundON;
    [SerializeField] Image soundOFF;
    private bool muted = false;
    void Start()
    {
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();

        }
        else
            Load();

        UpdateButtonIcon();
        AudioListener.pause = muted;
    }

    // Update is called once per frame
    public void OnButtonPress()
    {
        if(muted==false)
        {
            muted = true;
            AudioListener.pause = true;
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
        }
        Save();
        UpdateButtonIcon();

    }

    public void UpdateButtonIcon()
    {
        if(muted == false)
        {
            soundOFF.enabled = false;
            soundON.enabled = true;
        }
        else
        {
            soundOFF.enabled = true;
            soundON.enabled = false;
        }
    }

    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }
    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
}
