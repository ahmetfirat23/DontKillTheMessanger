using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [HideInInspector] public Dialogue dialogue;
    [HideInInspector] public DialogueData dialogueData;

    [HideInInspector] public Line line;
    [HideInInspector] public DialogueBoxData boxData;

    [HideInInspector] public bool finished = false;
    [HideInInspector] public bool started = false;

    [SerializeField] private AudioSource[] characterSounds;

    private bool autoClick = false;
    private bool skipClick = false;
    private bool isTyped = false;

    private int nextLineID = 0;
    private int currentSound = 0;
    private int soundArrayLength;
    private Sprite[] sentence;

    private void Awake()
    {
        soundArrayLength = characterSounds.Length;
        started = false;
    }

    private void Update()
    {
        if (!line.hasResponses || (line.hasResponses && line.responses[0].connectedID == -1))
        {
            if (autoClick || (skipClick && isTyped))
            {
                skipClick = false;
                autoClick = false;
                isTyped = false;
                boxData.dialogueBox.SetActive(false);
                nextLineID++;
                if (line.hasResponses && line.responses[0].connectedID == -1)
                {
                    nextLineID = -1;
                }

                DisplayNextSentence(nextLineID);
            }
            else if (skipClick && !isTyped)
            {
                skipClick = false;
                autoClick = false;
                isTyped = false;
                StopAllCoroutines();
                StartCoroutine(DisplayWholeText());
            }
        }
        else
        {
            if ((skipClick && !isTyped) || isTyped)
            {
                skipClick = false;
                autoClick = false;
                isTyped = false;
                DisplayButtons(line);
                StopAllCoroutines();
                StartCoroutine(DisplayWholeText());
            }
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        finished = false;
        started = true;
        nextLineID = 0;
        this.dialogue = dialogue;
        dialogueData = dialogue.dialogueData;

        DisplayNextSentence(nextLineID);
    }

    public void DisplayNextSentence(int lineID)
    {
        skipClick = false;
        autoClick = false;
        isTyped = false;

        if (lineID == -1)
        {
            StopAllCoroutines();
            //boxData.dialogueText.text = "";
            EndDialogue();
            return;
        }

        line = dialogueData.lines[lineID];

        boxData = dialogue.GetDialogueBoxDataWithName(line.name);
        boxData.dialogueBox.SetActive(true);

        sentence = line.sprites;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }


IEnumerator TypeSentence(Sprite[] sentence)
    {
        characterSounds[currentSound].Stop();
        characterSounds[currentSound].volume = Random.Range(0.17f, 0.20f);
        characterSounds[currentSound].pitch = Random.Range(1.25f, 1.35f);
        characterSounds[currentSound].Play();
        
        Queue<Image> images = new Queue<Image>();
        foreach (Image image in boxData.dialogueImageHolder.GetComponentsInChildren<Image>())
        {
            image.enabled = false;
            image.sprite = null;
            images.Enqueue(image);
        }
        
        foreach (Sprite word in sentence)
        {
            Image image = images.Dequeue();
            image.enabled = true;
            image.sprite = word; 
            //currentSound = Random.Range(0, blipArrayLength);
            yield return new WaitForSeconds(0.06f);
        }
        isTyped = true;
        yield return new WaitForSeconds(3);
        autoClick = true;
        boxData.dialogueBox.SetActive(false);
    }

    void EndDialogue()
    {
        finished = true;
        started = false;
        characterSounds[currentSound].Stop();
        boxData.dialogueBox.SetActive(false);
    }

    public void OnSkipClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (finished == false)
            {
                skipClick = true;
            }
            else
            {
                return;
            }
        }
    }

    public IEnumerator DisplayWholeText()
    {
        Queue<Image> images = new Queue<Image>();
        foreach (Image image in boxData.dialogueImageHolder.GetComponentsInChildren<Image>())
        {
            image.enabled = false;
            image.sprite = null;
            images.Enqueue(image);
        }
        
        foreach (Sprite word in sentence)
        {
            Image image = images.Dequeue();
            image.enabled = true;
            image.sprite = word;
        }

        yield return new WaitForSeconds(3f);
        autoClick = true;
        boxData.dialogueBox.SetActive(false);
    }

    private void DisplayButtons(Line line)
    {
        for (int i = 0; i < line.responses.Length; i++)
        {
            dialogue.buttons[i].SetActive(true);
            int spriteIndex = 0;
            GameObject imageHolder = dialogue.buttons[i].transform.GetChild(0).gameObject;
            foreach(Image image in imageHolder.GetComponentsInChildren<Image>())
            {
                image.sprite = line.responses[i].sprites[spriteIndex];
                spriteIndex++;
            }
            int ID = line.responses[i].connectedID;
            dialogue.buttons[i].GetComponent<Button>().onClick.AddListener(() => OnButtonClick(ID));
        }
    }

    public void OnButtonClick(int lineID)
    {      
        for (int i = 0; i < line.responses.Length; i++)
        {
            dialogue.buttons[i].GetComponent<Button>().onClick.RemoveAllListeners();
            dialogue.buttons[i].SetActive(false);
        }

        boxData.dialogueBox.SetActive(false);

        nextLineID = lineID;
        StopAllCoroutines();
        DisplayNextSentence(nextLineID);
    }
}
