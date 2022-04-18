using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

[System.Serializable]
public class Dialogue
{
    public DialogueData dialogueData;
    public DialogueBoxData[] dialogueBoxDatas;
    public UnityEvent[] eventsArray;
    public GameObject[] buttons;

    private Dictionary<Response, UnityEvent> eventDict;

    public Dictionary<Response, UnityEvent> ConnectEventsWithReplies()
    {
        eventDict = new Dictionary<Response, UnityEvent>();
        foreach (Line line in dialogueData.lines)
        {
            if (line.hasResponses)
            {
                foreach (Response response in line.responses)
                {
                    if (response.eventTrigger)
                    {
                        eventDict.Add(response, eventsArray[eventDict.Keys.Count]);
                    }
                }
            }
        }
        return eventDict;
    }

    public DialogueBoxData GetDialogueBoxDataWithName(string name)
    {
        foreach(DialogueBoxData data in dialogueBoxDatas)
        {
            Debug.Log(data.name);
            Debug.Log(name);
            if(data.name.ToLower() == name.ToLower())
            {
                return data;
            }
        }

        throw new Exception("Invalid character name");
    }
}