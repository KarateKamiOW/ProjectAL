using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public enum DialogState { Base, Response, Continue }
public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }

    [SerializeField] GameObject dialogBox;
    [SerializeField] GameObject thinkBox;
    [SerializeField] Text nameText;
    [SerializeField] Text dialogText;
    [SerializeField] int lettersPerSecond;
    [SerializeField] Text[] choiceText = new Text[3];
    [SerializeField] List<Image> choiceImages;
    [SerializeField] Vector3 npcOffset;
    [SerializeField] Vector3 playerOffset;
    [SerializeField] Vector3 thinkBoxOffset;

    public event Action OnShowDialog;
    public event Action OnCloseDialog;

    Dialog dialog;
    int currentLine = 0;
    bool isTyping;
    bool paused;
    DialogState state;
    DialogState? prevState;
    int currentAction;
    List<Dialog> theResponseList = new List<Dialog>();

    public Dialog Dialog 
    { get { return dialog; } }


    private void Awake()
    {
        Instance = this;
        paused = false;
    }

    public void StartDialog()
    {
        Instance.state = DialogState.Base;
    }

    public void HandleUpdate()
    {
        paused = true;
        if (state == DialogState.Base)
        {
            HandleDialog();
        }
        else if (state == DialogState.Response)
        {
            HandleResponseSelection();
        }
        else if (state == DialogState.Continue)
        {
            HandleContinuedDialog();
        }
    }

    public IEnumerator ShowDialog(Dialog dialog)
    {
        yield return new WaitForEndOfFrame();

        OnShowDialog?.Invoke();

        this.dialog = dialog;
        //dialogBox.transform.position = CharacterController2D.instance.DialogOffset + npcOffset;
        nameText.text = dialog.Name;
        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }

    void DialogPlacement()
    {
        nameText.text = dialog.Name;

        /*if (dialog.Character.tag == "Player")
            dialogBox.transform.position = CharacterController2D.instance.DialogOffset + npcOffset;  // was playerOffset
        else
            dialogBox.transform.position = CharacterController2D.instance.DialogOffset + npcOffset;*/
    }

    public void HandleDialog()
    {
        prevState = state;

        if (Input.GetKeyDown(KeyCode.F) && !isTyping)
        {
            ++currentLine;
            //dialog.AlteredStats.CalculateStats();
            if (currentLine < dialog.Lines.Count)
            {
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            }
            else
            {
                //dialog.AlteredStats.CalculateStats();
                if (dialog.BaseContinuedDialog.ContinuedDialog != null && dialog.BaseResponse.Count == 0)
                {
                    dialog.AlteredStats.CalculateStats();
                    currentLine = 0;
                    ContinueDialogPortal();
                    return;
                }

                if (dialog.BaseResponse.Count != 0 && dialog.BaseContinuedDialog.ContinuedDialog == null)
                {

                    //prevState = state;
                    currentLine = 0;
                    int j = 0;
                    HideChoiceText();
                    UpdatingResponseList();
                    UpdatingResponseNameLines();

                    /*foreach (var i in dialog.BaseResponse)
                    {
                        //choiceText.Add(null);
                        choiceText[j].text = i.ResponseLine;
                        j++;
                    }*/
                    ResponseSelection();      //will handle response selectors
                    return;

                }
                else
                {
                    currentLine = 0;
                    dialogBox.SetActive(false);
                    OnCloseDialog?.Invoke();
                    //dialog.CalculateStats();
                    dialog.AlteredStats.CalculateStats();
                    CharacterController2D.instance.Paused = false;

                }

            }
        }
    }

    void HandleContinuedDialog()
    {
        thinkBox.SetActive(false);
        dialogBox.SetActive(false);
        //DialogPlacement(dialog.BaseContinuedDialog.ContinuedDialog);
        dialogBox.SetActive(true);
        nameText.text = dialog.BaseContinuedDialog.ContinuedDialog.Name;
        StartCoroutine(TypeDialog(dialog.BaseContinuedDialog.ContinuedDialog.Lines[0]));
        DialogSelection();
    }

    public void HandleResponseSelection()
    {
        //Handles Player Input

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            ++currentAction;
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow))
            --currentAction;
        
        //currentAction = Mathf.Clamp(currentAction, 0, dialog.BaseResponse.Count - 1);
        currentAction = Mathf.Clamp(currentAction, 0, theResponseList.Count - 1);
        UpdateDialogActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.F))
        {
            thinkBox.SetActive(false);
            dialogBox.SetActive(false);
            //DialogPlacement();
            dialogBox.SetActive(true);
            nameText.text = theResponseList[currentAction].Name;
            //StartCoroutine(TypeDialog(dialog.BaseResponse[currentAction].Response.Lines[0]));
            StartCoroutine(TypeDialog(theResponseList[currentAction].Lines[0]));
            DialogSelection();
        }
    }

    void DialogSelection()
    {
        state = DialogState.Base;

        if (prevState == DialogState.Response)
        {
            //dialog = dialog.BaseResponse[currentAction].Response;
            dialog = theResponseList[currentAction];
        }

        if (prevState == DialogState.Continue)
        {
            dialog = dialog.BaseContinuedDialog.ContinuedDialog;
        }

    }

    void ResponseSelection()
    {
        state = DialogState.Response;
        prevState = state;
        thinkBox.SetActive(true);
        thinkBox.transform.position = CharacterController2D.instance.DialogOffset + thinkBoxOffset;
    }

    void ContinueDialogPortal()
    {
        state = DialogState.Continue;
        prevState = state;
    }

    public IEnumerator TypeDialog(string dialog)
    {
        
        isTyping = true;
        dialogText.text = "";
        foreach (var letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isTyping = false;
        yield break;
    }
    void UpdateDialogActionSelection(int SelectedAction)
    {
        for (int i = 0; i < choiceImages.Count; ++i)
        {
            if (i == SelectedAction)
            {
                choiceImages[i].GetComponent<Image>().enabled = true;
            }
            else
            {
                choiceImages[i].GetComponent<Image>().enabled = false;

            }
        }
    }
    void HideChoiceText() 
    {
        //choiceText[i].gameObject.SetActive(false);
        for (int i = 0; i < 4; i++) 
        {
            choiceText[i].text = "";
        }
    }

    void UpdatingResponseList() 
    {
        theResponseList.Clear();
        int j = 0;
        int k = 0;
        foreach (var i in dialog.BaseResponse) 
        {
            theResponseList.Add(dialog.BaseResponse[j].Response);
            //theResponseList[j] = dialog.BaseResponse[j].Response;
            j++;
                //dialog.BaseResponse[currentAction].Response
        }
        foreach (var i in dialog.CharismaResponse)
        {
            if (Stats.instance.Charisma >= dialog.CharismaResponse[k].CharismaReq)
            {
                theResponseList.Add(dialog.CharismaResponse[k].CharismaticDialog);
                //theResponseList[j] = dialog.CharismaResponse[j].CharismaticDialog;
                k++;
            }
        }
    }

    void UpdatingResponseNameLines() 
    {
        int j = 0;
        int k = 0;
        foreach (var i in dialog.BaseResponse)
        {
            //choiceText.Add(null);
            choiceText[j].text = i.ResponseLine;
                //dialog.BaseResponse[j].ResponseLine;
            j++;
        }
        foreach (var i in dialog.CharismaResponse) 
        {
            if (Stats.instance.Charisma >= dialog.CharismaResponse[k].CharismaReq) 
            {
                choiceText[j].text = i.ResponseLine;
                k++;
                j++;
            }
        }
    }

   
}
