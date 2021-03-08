using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IDMenu : MonoBehaviour
{
    public static IDMenu instance;

    public GameObject pauseScreen;
    public Slider str, intelligence, charisma, beauty, accuracy, athleticism, speed;
    public Image idPic;

    AlienPicker alienPicker;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetValues();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) 
        {
            OpenIDScreen(true);
        }
        if (Input.GetKeyUp(KeyCode.Tab)) 
        {
            OpenIDScreen(false);
        }
    }

    private void OpenIDScreen(bool activate) 
    {
        pauseScreen.SetActive(activate);
    }

    private void SetValues() 
    {
        str.maxValue = 100f;
        intelligence.maxValue = 100f;
        charisma.maxValue = 100f;
        beauty.maxValue = 100f;
        accuracy.maxValue = 100f;
        athleticism.maxValue = 100f;
        speed.maxValue = 100f;
        idPic.GetComponent<Image>().sprite = AlienPicker.instance.IDPic; 
    }
}
