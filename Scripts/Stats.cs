using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public static Stats instance;
    IDMenu idMenu;

    //Stats
    /*[SerializeField] int health;
    [SerializeField] int strength;
    [SerializeField] int intelligence;
    [SerializeField] int charisma;
    [SerializeField] int beauty;
    [SerializeField] int accuracy;
    [SerializeField] int athleticism;
    [SerializeField] int speed;
    [SerializeField] int randomStat;*/

    
    
    public int Health
    { get; set; }
    public int Strength
    { get; set; }
    public int Intelligence
    { get; set; }
    public int Charisma
    { get; set; }
    public int Beauty
    { get; set; }

    public int Accuracy
    { get; set; }

    public int Athleticism
    { get; set; }

    public int Speed
    { get; set; }
    public int RandomStat
    { get; set; }

    private void Start()
    {
        instance = this;
        RandomizeStats();
        StatsClamped();
    }
    void StatsClamped()
    {
        Mathf.Clamp(Strength, 0, 100);
        Mathf.Clamp(Intelligence, 0, 100);
        Mathf.Clamp(Charisma, 0, 100);
        Mathf.Clamp(Beauty, 0, 100);
        Mathf.Clamp(Accuracy, 0, 100);
        Mathf.Clamp(Athleticism, 0, 100);
        Mathf.Clamp(Speed, 0, 100);
        Mathf.Clamp(RandomStat, 0, 100);
    }
    void RandomizeStats() 
    {
        UpdateStrength(UnityEngine.Random.Range(1, 46));
        UpdateIntelligence(UnityEngine.Random.Range(1, 46));
        UpdateCharisma(UnityEngine.Random.Range(1, 46));
        UpdateBeauty(UnityEngine.Random.Range(1, 46));
        UpdateAccuracy(UnityEngine.Random.Range(1, 46));
        UpdateAthleticism(UnityEngine.Random.Range(1, 46));
        UpdateSpeed(UnityEngine.Random.Range(1, 46));
        //UpdateRandomStat(UnityEngine.Random.Range(1, 46));

    }

    public void UpdateStrength(int changeAmt) 
    {
        this.Strength += changeAmt;
        IDMenu.instance.str.value = Strength;
    }
    public void UpdateIntelligence(int changeAmt) 
    {
        this.Intelligence += changeAmt;
        IDMenu.instance.intelligence.value = Intelligence;
    }
    public void UpdateCharisma(int changeAmt) 
    {
        this.Charisma += changeAmt;
        IDMenu.instance.charisma.value = Charisma;
    }
    public void UpdateBeauty(int changeAmt) 
    {
        this.Beauty += changeAmt;
        IDMenu.instance.beauty.value = Beauty;
    }
    public void UpdateAccuracy(int changeAmt)
    {
        this.Accuracy += changeAmt;
        IDMenu.instance.accuracy.value = Accuracy;
    }
    public void UpdateAthleticism(int changeAmt)
    {
        this.Athleticism += changeAmt;
        IDMenu.instance.athleticism.value = Athleticism;
    }
    public void UpdateSpeed(int changeAmt)
    {
        this.Speed += changeAmt;
        IDMenu.instance.speed.value = Speed;
    }
    public void UpdateRandomStat(int changeAmt)
    {
        this.RandomStat += changeAmt;
        IDMenu.instance.beauty.value = RandomStat;
    }


}

    

    

