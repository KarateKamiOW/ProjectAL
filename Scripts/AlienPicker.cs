using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlienPicker : MonoBehaviour
{
    public static AlienPicker instance;

    public Animator anim;
    public RuntimeAnimatorController[] alienAnims;
    public Sprite[] IdImages;
    static int yourAlien;
    static int alienGenderNum;
    AlienBase alienBase;

    Sprite idPic;

    public int YourAlien
    { get; set; }

    public Sprite IDPic
    { get { return idPic; } }


    private void Awake()
    {
        instance = this;
        yourAlien = UnityEngine.Random.Range(1, 5);
        anim.runtimeAnimatorController = alienAnims[yourAlien - 1];
        idPic = IdImages[yourAlien - 1];
        //alienBase.AlienGender = AlienBase.Gender.Female;
        //AlienDetails();
        
    }
    private void Start()
    {
        AlienDetails();
    }

    static void AlienDetails() 
    {
        var aBase = AlienBase.instance; 
        
        switch (yourAlien) 
        {
            case 1:
                aBase.AlienRace = AlienBase.Race.Neafum;
                alienGenderNum = UnityEngine.Random.Range(1, 3);
                AlienGender(alienGenderNum);
                break;
            case 2:
                aBase.AlienRace = AlienBase.Race.Norps;
                alienGenderNum = UnityEngine.Random.Range(1, 3);
                AlienGender(alienGenderNum);
                break;
            case 3:
                aBase.AlienRace = AlienBase.Race.Tiggle;
                alienGenderNum = UnityEngine.Random.Range(1, 3);
                AlienGender(alienGenderNum);
                break;
            case 4:
                aBase.AlienRace = AlienBase.Race.Quip;
                alienGenderNum = UnityEngine.Random.Range(1, 3);
                AlienGender(alienGenderNum);
                break; 
            default:
                break;
        }

        void AlienGender(int genderNum)
        {
            if (genderNum == 1)
                AlienBase.instance.AlienGender = AlienBase.Gender.Male;
            else if (genderNum == 2)
                AlienBase.instance.AlienGender = AlienBase.Gender.Female;
            else if (genderNum == 3)
                AlienBase.instance.AlienGender = AlienBase.Gender.Neutral;
            else
                AlienBase.instance.AlienGender = AlienBase.Gender.None;

        }

    }
    

}
