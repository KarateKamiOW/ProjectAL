using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : MonoBehaviour, Interactable
{
    

    [SerializeField] Dialog dialog;
    [SerializeField] Dialog secondDialogOpt;    //This dialog is used if the first dialog as been activated already.

    [Header("Special Effect happens if you reach this Dialog")]
    [SerializeField] Dialog dialogInteraction;  //

    bool hasTalkedBefore = false;

    
    public void Interact() 
    {
        if (this.hasTalkedBefore) 
            StartCoroutine(DialogManager.Instance.ShowDialog(secondDialogOpt));
        else
            StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
        this.hasTalkedBefore = true;

    }

    public void Update()
    {
        if (DialogManager.Instance.Dialog == this.dialogInteraction && dialogInteraction != null)
        {
            Debug.Log("Step Achieved.");
            //CoinsAndScore.instance.ChangeCoinAmount(dialogInteraction.CoinsScoreAndItem.Coins);
            //CoinsAndScore.instance.ChangeScoreAmount(dialogInteraction.CoinsScoreAndItem.Score);
            //AlienBase.instance.inventory.AddItem(this.dialogInteraction.CoinsScoreAndItem.TheItem, 1);
            
        }

    }

}
