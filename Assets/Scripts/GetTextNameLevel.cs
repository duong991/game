using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetTextNameLevel : MonoBehaviour
{
    public Text textLevel;

    public string GetText()
    {
        return textLevel.text;  
    }
   
}
