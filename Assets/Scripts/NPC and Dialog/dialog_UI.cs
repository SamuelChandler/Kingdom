using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class dialog_UI : MonoBehaviour
{
    public static dialog_UI instance;

    [SerializeField] TextMeshProUGUI _dialogText;
    [SerializeField] TextMeshProUGUI _nameText;
    [SerializeField] GameObject _dialog;
    [SerializeField] GameObject _choices;
    [SerializeField] TextMeshProUGUI _choice1;
    [SerializeField] TextMeshProUGUI _choice2;
    [SerializeField] private float _typeSpeed  = 10f;

    //variables used in display Next Paragraph
    private Queue<DialogSegment> paragraphs = new Queue<DialogSegment>();
    private bool conversationEnded;
    private DialogSegment p;

    public NPC currentlyTalkingNPC;

    private Coroutine typeDialogueCoroutine;
    private bool isTyping;

    private const string HTML_ALPHA = "<color=#00000000>";
    private const float MAX_TYPE_TIME = 0.1f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _dialog.SetActive(false);
        _choices.SetActive(false);
    }

    //displays the next paragraph returns true on new text 
    //being created and false on completeing the previos text 
    public ConversationStats DisplayNextParagraph(DialogText dialog,ConversationStats c)
    {
        //if there is nothing in the queue
        if (c.ConversationPointer == 0) { 
            if(!conversationEnded)
            {
                //start conversation
                StartConversation(dialog);
            }
            
        }

        if(c.ReadyToEnd && !isTyping) 
        {
            //end the conversation
            EndConversation();
            c.Ended = true;
        }

        //
        if (!isTyping && !c.Ended)
        {
            p = dialog._dialog[c.ConversationPointer];

            typeDialogueCoroutine = StartCoroutine(TypeDialogText(p));

            c.ReadyForNextParagraph = true;
            return c;
        }
        else
        {
            FinishParagraphEarly();

            c.ReadyForNextParagraph = false;
            return c;
        }


        
    }

    public void StartConversation(DialogText dialog)
    {
        _dialog.SetActive(true);

        _nameText.text = dialog.name;

        //add dialog text into queue
        for (int i = 0; i < dialog._dialog.Length; i++)
        {
            paragraphs.Enqueue(dialog._dialog[i]);
        }


    }

    public void EndConversation()
    {
        //clear the queue
        paragraphs.Clear();

        //return flag to false 
        conversationEnded = false;

        //stop displaying dialog box 
        _dialog.SetActive(false);
        _choices.SetActive(false);
    }

    private IEnumerator TypeDialogText(DialogSegment p)
    {
        isTyping = true;

        _dialogText.text = "";

        string originalText = p.Paragraph;
        string displayedText = "";
        int alphaIndex = 0;

        foreach (char x in originalText.ToCharArray())
        {
            alphaIndex++;

            _dialogText.text = originalText;

            displayedText = _dialogText.text.Insert(alphaIndex,HTML_ALPHA);

            _dialogText.text = displayedText;

            yield return new WaitForSeconds(MAX_TYPE_TIME/_typeSpeed);
        }

        isTyping = false;
    }

    public void FinishParagraphEarly()
    {
        //stop coroutine
        StopCoroutine(typeDialogueCoroutine);

        //finish displaying dialog
        _dialogText.text = p.Paragraph;

        //update is typing 
        isTyping = false;
    }

    public void DisplayChoices(string c1,string c2){
        _choices.SetActive(true);
        _choice1.text = c1;
        _choice2.text = c2;
    }

    public void ChoiceOne(){
        currentlyTalkingNPC.ResolveChoice(true);
        _choices.SetActive(false);
    }

    public void ChoiceTwo(){
        currentlyTalkingNPC.ResolveChoice(false);
        _choices.SetActive(false);
    }
}


