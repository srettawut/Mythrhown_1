using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;

    [Header("GAME STORY")]
    [SerializeField] private DialogueObject Chapter1;
    [SerializeField] private DialogueObject Chapter2;
    [SerializeField] private DialogueObject Chapter3;
    [SerializeField] private DialogueObject Chapter4;
    [SerializeField] private DialogueObject Chapter5;

    [Header("WHILE READING")]
    [SerializeField] private GameObject storyPanal;
    [SerializeField] private GameObject playerImage;
    [SerializeField] private GameObject cafebossImage;

    public bool IsOpen { get; private set; }

    private ResponseHandler responseHandler;
    private TypewriterEffect typewriterEffect;

    private void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();


        CloseDialogueBox();
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];

            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;

            yield return null;
            yield return new WaitUntil(() =>
    Input.GetMouseButtonDown(0) ||
    (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began));
        }

        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            storyPanal.SetActive(true);
            CloseDialogueBox();
        }
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        typewriterEffect.Run(dialogue, textLabel);

        while (typewriterEffect.IsRunning)
        {
            yield return null;

            if (Input.GetMouseButtonDown(0) ||
    (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                typewriterEffect.Stop();
            }
        }
    }

    private void CloseDialogueBox()
    {
        playerImage.SetActive(false);
        cafebossImage.SetActive(false);
        IsOpen = false;       
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }

    public void playChapter1()
    {
        playerImage.SetActive(true);
        storyPanal.SetActive(false);
        ShowDialogue(Chapter1);
    }
    public void playChapter2()
    {
        playerImage.SetActive(true);
        storyPanal.SetActive(false);
        ShowDialogue(Chapter2);
    }
    public void playChapter3()
    {
        playerImage.SetActive(true);
        cafebossImage.SetActive(true);
        storyPanal.SetActive(false);
        ShowDialogue(Chapter3);
    }
    public void playChapter4()
    {
        playerImage.SetActive(true);
        storyPanal.SetActive(false);
        ShowDialogue(Chapter4);
    }
    public void playChapter5()
    {
        playerImage.SetActive(true);
        storyPanal.SetActive(false);
        ShowDialogue(Chapter5);
    }
}
