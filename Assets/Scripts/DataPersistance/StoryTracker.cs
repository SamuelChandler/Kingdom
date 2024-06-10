using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "StoryTracker",menuName = "StoryTracker")]
public class StoryTracker : ScriptableObject
{
    [SerializeField] public StoryEvent[] events;


    public void SetEventState(List<int> idList){
        foreach(int id in idList){
            events[id].completed = true;
        }
    }

    public List<int> GetEventState(){
        
        List<int> idList = new List<int>();

        for(int i =0; i < events.Length; i++){
            if(events[i].completed){
                idList.Add(i);
            }
        }

        return idList;
    }

}

[Serializable]
public class StoryEvent
{
    public bool completed;

    [TextArea(5,10)]
    public String desc;
}