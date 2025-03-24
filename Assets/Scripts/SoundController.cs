using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    int soundMode;
    public AudioSource mainMusic;
    [SerializeField] Image soundButtonImage;
    [SerializeField] Sprite soundButton0, soundButton1, soundButton2;
    [SerializeField] AudioSource buttonMenuS, buttonPauseS;

    public bool soundsOn;

    void Start()
    {
        if (PlayerPrefs.HasKey("SoundMode")) soundMode = PlayerPrefs.GetInt("SoundMode");       
        else soundMode = 2;
        ActivateMode();       
    }

    public void SetSoundMode()
    {
        if (soundMode > 0) soundMode--;
        else soundMode = 2;
        ActivateMode();
    }

    void ActivateMode()
    {
        switch (soundMode)
        {
            case 0:
                soundButtonImage.sprite = soundButton0;
                soundsOn = false;
                buttonMenuS.volume = 0;
                buttonPauseS.volume = 0;
                mainMusic.Pause();            
                break;
            case 1:             
                soundButtonImage.sprite = soundButton1;
                soundsOn = true;
                buttonMenuS.volume = 1;
                buttonPauseS.volume = 1;
                mainMusic.Pause();
                break;
            case 2:
                soundButtonImage.sprite = soundButton2;
                soundsOn = true;
                buttonMenuS.volume = 1;
                buttonPauseS.volume = 1;
                mainMusic.Play();               
                break;
        }         
        PlayerPrefs.SetInt("SoundMode", soundMode);
        PlayerPrefs.Save();
    }
}
