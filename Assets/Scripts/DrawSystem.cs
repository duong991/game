using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DrawSystem : MonoBehaviour
{
    public GameSystem GameSystem;


    public TextMeshProUGUI InboxText;
    public TextMeshProUGUI OutboxText;
    public Transform InboxPositon;
    public Transform OutboxPositon;


    [HideInInspector]
    public List<Text> Memorycase;

    // Update is called once per frame

    private void Start()
    {
        Memorycase = new List<Text>();
        GameObject momoryGrid = transform.Find("MemoryGrid").gameObject;
        for(int i = 0; i < momoryGrid.transform.childCount;i++)
        {
            GameObject child = momoryGrid.transform.GetChild(i).gameObject;
            Text indexText = child.transform.Find("Index").GetComponent<Text>();
            Text memotyText = child.transform.Find("MemoryText").GetComponent<Text>();
            indexText.text = i.ToString();
            memotyText.text = "";
            Memorycase.Add(memotyText);

        }


    }

    public void UpdateInbox()
    {
        InboxText.text = "";
        foreach (int i in GameSystem.GetInbox())
            InboxText.text += i.ToString() + "\n";
    }
    
    public void UpdateOutbox()
    {

        OutboxText.text = "";
        foreach (int i in GameSystem.GetOutbox())
            OutboxText.text += i.ToString() + "\n";
    }
    public string getInBox()
    {
        return InboxText.text;
    }

    public string getOutBOx()
    {
        return OutboxText.text;
    }
    public void UpdateMemory(int[] memory)

    {
        for(int i=0;i<memory.Length;i++)
        {
            Memorycase[i].text = memory[i].ToString();
        }
    }


}
