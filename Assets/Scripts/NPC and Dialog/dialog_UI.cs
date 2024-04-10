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
    [SerializeField] private float _typeSpeed  = 10f;

    //variables used in display Next Paragraph
    private Queue<string> paragraphs = new Queue<string>();
    private bool conversationEnded;
    private string p;

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
    }

    private void FixedUpdate()
    {
       
    }
    public void displayOneLiner(string name, string text)
    {
        _nameText.text = name;
        _dialogText.text = text;
        _dialog.SetActive(true);
    }

    public void DisplayNextParagraph(DialogText dialog)
    {
        //if there is nothing in the queue
        if (paragraphs.Count == 0) { 
            if(!conversationEnded)
            {
                //start conversation
                StartConversation(dialog);
            }
            else
            {
                //end the conversation
                EndConversation();
            }
        }

        // if queue is not empty
        if (!isTyping)
        {
            p = paragraphs.Dequeue();

            typeDialogueCoroutine = StartCoroutine(TypeDialogText(p));
        }
        

        //update convo text
        //_dialogText.text = p;

        //update conversation ended flag
        if(paragraphs.Count == 0 )
        {
            conversationEnded = true;
        }
    }


    private void StartConversation(DialogText dialog)
    {
        _dialog.SetActive(true);

        _nameText.text = dialog.name;

        //add dialog text into queue
        for (int i = 0; i < dialog.Paragraphs.Length; i++)
        {
            paragraphs.Enqueue(dialog.Paragraphs[i]);
        }


    }

    private void EndConversation()
    {
        //clear the queue
        paragraphs.Clear();

        //return flag to false 
        conversationEnded = false;

        //stop displaying dialog box 
        _dialog.SetActive(false);
    }

    private IEnumerator TypeDialogText(string p)
    {
        isTyping = true;

        _dialogText.text = "";

        string originalText = p;
        string displayedText = "";
        int alphaIndex = 0;

        foreach (char x in p.ToCharArray())
        {
            alphaIndex++;

            _dialogText.text = originalText;

            displayedText = _dialogText.text.Insert(alphaIndex,HTML_ALPHA);

            _dialogText.text = displayedText;

            yield return new WaitForSeconds(MAX_TYPE_TIME/_typeSpeed);
        }

        isTyping = false;
    }
}


