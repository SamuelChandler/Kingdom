using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/New Dialogue Container")]
public class DialogText : ScriptableObject
{
    public string Name;

    [TextArea(5,10)]
    public string[] Paragraphs;


}
