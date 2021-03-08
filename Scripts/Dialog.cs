using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "Create Dialog Speech")]
[System.Serializable]
public class Dialog : ScriptableObject
{
    [SerializeField] string characterName;
    [SerializeField] GameObject character;
    [TextArea(3, 10)]
    [SerializeField] List<string> lines;

    [Header("Continue Dialog with another Speaker")]
    [SerializeField] ContinueTheDialog continueDialog;

    [Header("Answer Dialog with a Response")]
    [SerializeField] List<Responses> response;

    [Header("These responses only show if charisma is High enough")]
    [SerializeField] List<CharismaResponses> charismaResponse;

    [Header("Alter Coins/Score Amt And/Or give items")]
    [SerializeField] CoinsScoreAndItems coinsScoreItems;

    [Header("End of Dialog Stat Mod(Use - to reduce)")]
    [SerializeField] StatAltering alterStats;


    /*[SerializeField] int strengthBoost;
    [SerializeField] int intelligenceBoost;
    [SerializeField] int charismaBoost;
    [SerializeField] int beautyBoost;
    [SerializeField] int accuracyBoost;
    [SerializeField] int athleticismBoost;
    [SerializeField] int speedBoost;*/

    int[] playerStats = new int[6];
    string tempResponseLine;

    public string Name
    { get { return characterName; } }
    public List<string> Lines
    { get { return lines; } }

    public ContinueTheDialog BaseContinuedDialog
    { get { return continueDialog; } }
    public List<Responses> BaseResponse
    { get { return response; } }

    public List<CharismaResponses> CharismaResponse 
    { get { return charismaResponse; } }

    public GameObject Character
    { get { return character; } }

    public CoinsScoreAndItems CoinsScoreAndItem 
    { get { return coinsScoreItems; } }

    public StatAltering AlteredStats 
    { get { return alterStats; } }
    public string TempResponseLine 
    { get { return tempResponseLine; } }

   

    [System.Serializable]
    public class ContinueTheDialog
    {
        [SerializeField] Dialog continuedDialog;

        public Dialog ContinuedDialog
        { get { return continuedDialog; } }
    }

    [System.Serializable]
    public class Responses
    {
        [SerializeField] string responseLine;
        [SerializeField] Dialog response;

        public string ResponseLine
        { get { return responseLine; } }

        public Dialog Response
        { get { return response; } }
    }

    [System.Serializable]
    public class CharismaResponses 
    {
        [SerializeField] int charismaReq;
        [SerializeField] string responseLine;
        [SerializeField] Dialog charismaticDialog;
        public int CharismaReq
        { get { return charismaReq; } }
        public string ResponseLine
        { get { return responseLine; } }
        public Dialog CharismaticDialog 
        { get { return charismaticDialog; } }


    }

    [System.Serializable]
    public class StatAltering 
    {
        [SerializeField] int strengthBoost;
        [SerializeField] int intelligenceBoost;
        [SerializeField] int charismaBoost;
        [SerializeField] int beautyBoost;
        [SerializeField] int accuracyBoost;
        [SerializeField] int athleticismBoost;
        [SerializeField] int speedBoost;

        public void CalculateStats()
        {
            if (strengthBoost != 0)
                Stats.instance.UpdateStrength(strengthBoost);
            if (intelligenceBoost != 0)
                Stats.instance.UpdateIntelligence(intelligenceBoost);
            if (charismaBoost != 0)
                Stats.instance.UpdateCharisma(charismaBoost);
            if (beautyBoost != 0)
                Stats.instance.UpdateBeauty(beautyBoost);
            if (accuracyBoost != 0)
                Stats.instance.UpdateAccuracy(accuracyBoost);
            if (athleticismBoost != 0)
                Stats.instance.UpdateAthleticism(athleticismBoost);
            if (speedBoost != 0)
                Stats.instance.UpdateSpeed(speedBoost);
        }

    }

    [System.Serializable]
    public class CoinsScoreAndItems 
    {
        [SerializeField] int coins;
        [SerializeField] int score;
        [SerializeField] ItemObject item;

        public int Coins 
        { get { return coins; } }

        public int Score 
        { get { return score; } }

        public ItemObject TheItem 
        { get { return item; } }
    }
}
