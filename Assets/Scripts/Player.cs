using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float Speed = 100f;
    public GameObject CurrentObj;
    public Image CurrentImg;
    DrawSystem drawSystem;
    public Text CurrentText;
    public int step = 0;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        drawSystem = FindObjectOfType<DrawSystem>();
    }

    public bool MoveToInput()
    {

        //animator.SetBool("Acmovequatrai", false);
        animator.SetBool("ActiveMove", true);

        animator.SetBool("AcMove_box", false);



        return MoveToPos(drawSystem.InboxPositon.position);
        step++;


    }
    public bool MoveToOutput()
    {
        animator.SetBool("ActiveMove", false);
        animator.SetBool("AcMove_box", true);
        //animator.SetBool("Acmovequatrai", false);

        return MoveToPos(drawSystem.OutboxPositon.position);
        step++;
    }


    // Update is called once per frame
    public bool MovetoMemory(int index)
    {
        if (animator.GetBool("ActiveMove") == false)
        {
            animator.SetBool("ActiveMove", true);
        }
        else
            animator.SetBool("AcMove_box",true);


        Vector3 targetPos = drawSystem.Memorycase[index].transform.position;
        return MoveToPos(targetPos + new Vector3(0,60,0));
        step++;

    }

    bool MoveToPos(Vector3 pos)
    {
        if(Vector3.Distance(transform.position,pos) > Speed)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, Speed * 60f * Time.deltaTime);
            return false;
        }
        return true;
        step++;
    }
     
    public void DisplayCurrent(string text)
    {

        alpha = 1f;
        SetCurrentAlpha(alpha);
        if (!CurrentObj.activeSelf)
        {

 
            
            CurrentObj.SetActive(true);
            

        }
        CurrentText.text = text;
    }

    public void SetStepcong()
    {
        step++;
    }

    public void SetStep()
    {
        this.step = 0;
    }
    public void HideCurrent()
    {
        if (CurrentObj.activeSelf)
            CurrentObj.SetActive(false);
    }
    float alpha = 1f;

    public bool DestroyCurrent()
    {
        alpha -= Time.deltaTime;
        if (alpha < 0)
            alpha = 0f;
        SetCurrentAlpha(alpha);
        return alpha > 0f;
    }

    public void SetCurrentAlpha(float alpha)
    {
        Color color = CurrentImg.color;
        color.a = alpha;
        CurrentImg.color = color;
        color = CurrentText.color;
        color.a = alpha;
        CurrentText.color = color;
    }
}
