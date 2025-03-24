using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;

public class GameController : MonoBehaviour
{
    public GameObject panelModifiers, prefabModButton, restartMenu, hud;
    public Text scoreCurrentT1, scoreCurrentT2, scoreBestT;
    [NonSerialized] public Countdown timer;
    int starsCount = 0;
    SoundController sc;
    bool ded = false;

    void Start()
    {
        GenerateNextLocation(false);
        GenerateNextLocation(true);
        SaveRecord sd = new SaveRecord();
        sc = GetComponent<SoundController>();
        scoreBestT.text = sd.Comparing(starsCount).ToString();
    }


    public void GenerateNextLocation(bool isNext)
    {
        GameObject prefabLocation = Resources.Load<GameObject>("Prefabs/Location");
        Vector3 position = new Vector3(0, -112.8f, 91.3f);
        string name = "Location Next";
        if (!isNext)
        {
            position = new Vector3(0, -22, -17);
            name = "Location";
        }
        GameObject next = Instantiate(prefabLocation, position, Quaternion.identity);
        next.name = name;
    }

    public void AddButton(GameObject modifier)
    {
        var newButton = Instantiate(prefabModButton, panelModifiers.transform);
        newButton.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = modifier.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite;
        newButton.GetComponent<Button>().onClick.AddListener(delegate
        { 
            modifier.GetComponent<Modifier>().ModifierActivation();
            Destroy(newButton);
        });
        modifier.transform.SetParent(newButton.transform);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }

    public void MenuActivateOnDed()
    {   
        timer = Countdown.CreateInstance;
        timer.StartTimer(1.3f);
        timer.Completed += () => 
        {    
            EndRun();
        };
    }

    void EndRun()
    {
        ded = true;
        MenuOpen();
        PlaySound("S Endgame");
        sc.mainMusic.Stop();
        SaveRecord sd = new SaveRecord();          
        scoreBestT.text = sd.Comparing(starsCount).ToString();
    }
    public void MenuOpen()
    {
        Time.timeScale = 0;
        hud.SetActive(false);
        restartMenu.SetActive(true);
    }

    public void MenuClose()
    {
        Time.timeScale = 1;
        hud.SetActive(true);
        restartMenu.SetActive(false);
    }

    public void MenuPlay()
    {
        if (ded) Restart();
        else MenuClose();
    }

    public void SoundTurn()
    {
        sc.SetSoundMode();
    }

    public void AddStars()
    {
        starsCount++;
        scoreCurrentT1.text = starsCount.ToString();
        scoreCurrentT2.text = starsCount.ToString();
    }
    public void ReduceStars()
    {
        starsCount--;
        scoreCurrentT1.text = starsCount.ToString();
        scoreCurrentT2.text = starsCount.ToString();
    }

    public void PlaySound(string soundName)
    {
        if (sc.soundsOn == true) GameObject.Find(soundName).GetComponent<AudioSource>().Play();
    }

}
