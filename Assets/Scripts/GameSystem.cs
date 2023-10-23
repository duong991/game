using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using Random = System.Random;

public class GameSystem : MonoBehaviour
{
    public DrawSystem DrawSystem;
    public InputField ProgramInput;
    public Player Player;
    List<int> outbox;
    public GameObject BGendSence;

    public GameObject BGendCacLenh;


   // public GameObject ani;


    public GameObject EndListCM;


    public GameObject EndScene;

    public Button btnBack, btnContinue,btnRunProGram;
    public TextMeshProUGUI textLevel;

    public TextMeshProUGUI textCountStep;
    public TextMeshProUGUI textCountCmd;

    public GameObject DanhGiaHC;

    public GameObject HuongDan;
    public Button btnHD;
    public Button HideHD;

    Queue<int> inbox;

    int maxMemory;

    // public Text LevelName;
    public string BestCMD = "";
    public string BestStep = "";
    int[] expectedOutBox;
    int[] memory;


    public Animator animator;
    public Animator animatorPlayer;
    Instruction[] instructions;
    Dictionary<string, int> labels;


    int nextPrgPointer;
    int prgPointer;
    int? current;
    int levelInt = 0;
    int levelDG = 0;
    bool[] checkLevelWin = new bool[10];


    public void SetcheckLevelWin(int index)
    {
        checkLevelWin[index] = true;
    }
    private void Start()
    {

        btnHD.onClick.AddListener(() =>
        {
            HuongDan.SetActive(true);
            BGendSence.SetActive(true);
        });
        HideHD.onClick.AddListener(() =>
        {
            HuongDan.SetActive(false);
            BGendSence.SetActive(false);
        });
        //outbox = new List<int>();
        InitProgram();

        //SceneManager.UnloadSceneAsync(0);

        //Debug.Log(FindObjectsOfType<GetTextNameLevel>().Length);


        //print(inbox);

    }

    void InitProgram()
    {
        //animator.Play("InboxText");
        inbox = new Queue<int>(GenerateInputs());
        //ani.SetActive(true);
        //inbox.CopyTo(arrout, 0);
        outbox = new List<int>();
        maxMemory = 1;

        if (textLevel.text.Contains("3") == true)
        {
            maxMemory = 3;
        }
        else if (textLevel.text.Contains("4") == true)
        {
            maxMemory = 3;
        }
        else if (textLevel.text.Contains("5") == true)
        {
            maxMemory = 9;
        }
        else if (textLevel.text.Contains("6") == true)
        {
            maxMemory = 3;
        }
        else if (textLevel.text.Contains("7") == true)
        {
            maxMemory = 6;
        }
        else if (textLevel.text.Contains("8") == true)
        {
            maxMemory = 6;
        }
        else if (textLevel.text.Contains("9") == true)
        {
            maxMemory = 3;
        }
        else if (textLevel.text.Contains("10") == true)
        {
            maxMemory = 3;
        }
        memory = new int[maxMemory];

         

        //BEST
        if (textLevel.text.Contains("1") == true && textLevel.text.Contains("0") == false)
        {
            BestCMD = "6";
            BestStep = "6";
            levelInt = 1;


        }
        else if (textLevel.text.Contains("2") == true)
        {
            BestCMD = "4";
            BestStep = "21";
            levelInt = 2;



        }
        else if (textLevel.text.Contains("3") == true)
        {
            BestCMD = "8";
            BestStep = "31";
            levelInt = 3;


        }
        else if (textLevel.text.Contains("4") == true)
        {
            BestCMD = "7";
            BestStep = "26";
            levelInt = 4;
        }
        else if (textLevel.text.Contains("5") == true)
        {
            BestCMD = "6";
            BestStep = "16";
            levelInt = 5;
        }
        else if (textLevel.text.Contains("6") == true)
        {
            BestCMD = "7";
            BestStep = "31";
            levelInt = 6;
        }
        else if (textLevel.text.Contains("7") == true)
        {
            BestCMD = "10";
            BestStep = "49";
            levelInt = 7;
        }
        else if (textLevel.text.Contains("8") == true)
        {
            BestCMD = "15";
            BestStep = "53";
            levelInt = 8;
        }
        else if (textLevel.text.Contains("9") == true)
        {
            BestCMD = "11";
            BestStep = "46";
            levelInt = 9;
        }
        else if (textLevel.text.Contains("10") == true)
        {
            BestCMD = "12";
            BestStep = "25";
            levelInt = 10;
        }
        DrawSystem.UpdateInbox();
        DrawSystem.UpdateOutbox();

        //memory = GetMemory();

        //DrawSystem.UpdateMemory(memory);
        //DrawSystem.UpdateOutbox();


        // Debug.Log(LevelName.text);
        //foreach (int i in inbox)

    }
    void Update()
    {
        
    }
    string[] subInbox;
    public void RunProgram()
    {

        
        btnRunProGram.gameObject.SetActive(false);
        


        Player.SetStep();



        string inboxText = DrawSystem.getInBox();
        subInbox = inboxText.Split('\n');
        instructions = GenerateInstructions(ProgramInput.text);
        expectedOutBox = GenerateOutputs(inbox);
        

        StartCoroutine(Run());
   



        //btnRunProGram.gameObject.SetActive(true);

    }


    public void BackSceneChange()
    {
        SceneManager.LoadScene("Scenes/Change_Name", LoadSceneMode.Single);
    }



    public int CountCMD(string str)
    {
        string[] subbs = str.Split('\n');
        return subbs.Length;
    }
  
    IEnumerator Run()
    {
      


        prgPointer = 0;
        current = null;
        while (prgPointer < instructions.Length)
        {
            print(prgPointer + ":" + instructions[prgPointer]);
            nextPrgPointer = prgPointer + 1;
            instructions[prgPointer].Excute(this);
            yield return instructions[prgPointer].Animation(this);
            prgPointer = nextPrgPointer;

        }

        if (animator.GetBool("Active") == false)
        {

            animatorPlayer.SetBool("ActiveMove", false);
            animatorPlayer.SetBool("AcMove_box", false);
            animatorPlayer.SetBool("ActiveDefau", true);

            animator.SetBool("Active", true);
        }

        yield return new WaitForSeconds(4f);

        animator.SetBool("Active", !animator.GetBool("Active"));

        inbox = new Queue<int>(GenerateInputs());
        outbox = new List<int>();





        //Debug.Log(checkWin());
        btnRunProGram.gameObject.SetActive(true);

        
        //GameObject ListCM = transform.Find("ListCommand").gameObject;


        if (checkWin()==true)
        {
            //animatorPlayer.SetBool("ActiveMove", false);
            //animatorPlayer.SetBool("AcMove_box", false);
            //animatorPlayer.SetBool("ActiveDefau", true);
            //animator.enabled = true;
            textCountCmd.text = "Bạn đã dùng " + CountCMD(ProgramInput.text) + " lệnh" + "\n" + "Kết quả hoàn hảo " + BestCMD + " lệnh";
            textCountStep.text = "Bạn đã dùng " + Player.step.ToString() + " bước" + "\n" + "Kết quả hoàn hảo " + BestStep + " bước";

            GameObject hc = DanhGiaHC.transform.Find("HuyChuong1").gameObject;
            GameObject hc1 = DanhGiaHC.transform.Find("HuyChuong2").gameObject;
            GameObject hc2 = DanhGiaHC.transform.Find("HuyChuong3").gameObject;

            if(int.Parse(BestStep) <= Player.step || CountCMD(ProgramInput.text) <= int.Parse(BestCMD) )
            {
                hc.SetActive(true);
                hc1.SetActive(true);
                hc2.SetActive(true);
                levelDG = 3;
            }

            if ( Player.step > int.Parse(BestStep) || CountCMD(ProgramInput.text) > int.Parse(BestCMD))
            {
                hc.SetActive(true);
                hc1.SetActive(true);
                hc2.SetActive(false);
                levelDG = 2;
            }



            //animator.SetBool("Active", false);
            EndScene.SetActive(true);
            EndListCM.SetActive(false);
            BGendCacLenh.SetActive(false);
            BGendSence.SetActive(true);





        }
        else
        {

            //animator.enabled = true;
            InitProgram();
            ProgramInput.text = "";
        }

            

        btnBack.onClick.AddListener(() =>
        {
            //DataPlayer.Remove_Level();

            //DataPlayer.Remove_DG();
            animatorPlayer.SetBool("ActiveDefau", false);

            InitProgram();

            EndListCM.SetActive(true);
            //ListCM.SetActive(true);
            BGendCacLenh.SetActive(true);
            EndScene.SetActive(false);
            BGendSence.SetActive(false);



        });
            
        btnContinue.onClick.AddListener(() =>
        {


            try
            {

                if (DataPlayer.getCheckList()[levelInt] == false)
                {
                    DataPlayer.Add_ListCheckLevel(levelInt, true);

                    DataPlayer.Add_DG(levelDG);
                    DataPlayer.Add_Level(levelInt);





                    //Debug.Log(checkLevelWin[levelInt]);
                }




                else
                {

                    Debug.Log("OK");

                    DataPlayer.Remove_DG();

                    DataPlayer.Add_DG(levelDG);

                }

                SceneManager.LoadScene("Scenes/Change_Name", LoadSceneMode.Single);


            }
            catch (Exception ex)
            {

                SceneManager.LoadScene("Scenes/Change_Name", LoadSceneMode.Single);
            }

        });




            //DrawSystem.UpdateInbox();
            //DrawSystem.UpdateOutbox();
            //outbox = new List<int>();
            // outbox = new List<int>();
            //if(checkWin() == true)
            //{
            //Debug.Log("Win");
            //}
      
     
    }

    public List<int> GetInbox()
    {
        return new List<int>(inbox.ToArray());
    }
    public List<int> GetOutbox()
    {

        List<int> list = new List<int>(outbox.ToArray());
        list.Reverse();
        return list;
    }


    public int Dequeueinbox()
    {

        if (inbox.Count > 0)
            return inbox.Dequeue();
        else
        {
            nextPrgPointer = instructions.Length;
        }
        return -1;
    }
        
    public void SendToOutbox(int value)
    {
        //if (outbox.Count == 0)
        //{
        //    Debug.Log("erok");
        //}
        //else
            outbox.Add(value);
    }

    public void SetCurrent(int? value)
    {
        current = value;
    }

    public int? GetCurrent()
    { 
        //if(current.HasValue)
        return current;
    }

    public void JumpToLabel(string label)
    {
        nextPrgPointer = labels[label];
    }

    public void CopytoMemory(int index,int value)   
    {
        memory[index] = value;
    }

    public int[] GetMemory()
    {
        return memory;
    }

    public Instruction[] GenerateInstructions(string text)
    {
        List<Instruction> list = new List<Instruction>();
        labels = new Dictionary<string, int>();
        text += "\n";
        //int i = 0;
        text = text.Replace("\r\n", "\n");
        text = text.Trim();
        FindObjectOfType<InputField>().text = text;
        foreach (string linef in text.Split('\n'))
        {
            string line = linef.Trim();
            if(line.Contains("--") || line.Equals("") )
            {
                continue;
            }

            else if(line.Contains(":"))
            {
                labels.Add(line.Split(':')[0], list.Count);
                list.Add(new DoNothing());
            }
            else
            {
                List<string> args = new List<string>();
                foreach (string arg in line.Split(' '))
                    if (!arg.Equals(""))
                        args.Add(arg.ToLower());


                string command = args[0];
                string arg1 = "";
                bool adress = false;

                if (args.Count > 1)
                {
                    arg1 = args[1];
                    
                    if ((adress = arg1.StartsWith("[") && arg1.EndsWith("]")))
                        arg1 = arg1.Replace("[", "").Replace("]", "");

                    
                }

                if (command.Equals("inbox"))
                {
                    list.Add(new Inbox());
                }
                else if (command.Equals("outbox"))
                {
                    //Debug.Log("teST");
                    list.Add(new Outbox());
                }

                else if (command.Equals("copyto") && !arg1.Equals(""))
                {
                    list.Add(new Copyto(int.Parse(arg1)));
                }

                else if (command.Equals("copyfrom") && !arg1.Equals(""))
                {
                    list.Add(new CopyFrom(int.Parse(arg1),adress));
                }

                else if (command.Equals("add") && !arg1.Equals(""))
                {
                    //Debug.Log("add");
                    list.Add(new Add(int.Parse(arg1)));
                }
                else if (command.Equals("sub") && !arg1.Equals(""))
                {
                    //Debug.Log("add");
                    list.Add(new Sub(int.Parse(arg1)));

                }
           
                else if (command.Equals("jump") && !arg1.Equals(""))
                {
                    list.Add(new Jump(arg1));
                    
                }
                else if (command.Equals("jumpz") && !arg1.Equals(""))
                {
                    list.Add(new JumpZ(arg1));
                    
                }
            }
            
        

        }
        return list.ToArray();
        
    }
    int[] arr = new int [0];
    int[] arrout = new int[12];
    int[] GenerateInputs()
    {

        if (textLevel.text.Contains("1") == true && textLevel.text.Contains("0")==false)
        {
            arr = new int[3];
            arr[0] = 3;
            arr[1] = 7;
            arr[2] = 5;
        }
        else if (textLevel.text.Contains("2") == true)
        {

            arr = new int[10];
            for (int i = 0; i < 10; i++)
            {
                arr[i] = 999;
            }
        }
        else if (textLevel.text.Contains("3") == true)
        {
            arr = new int[] { 2, 1, 4, 3, 6, 5, 8, 7, 10, 9 };
            return arr;

        }

        else if (textLevel.text.Contains("4") == true)
        {
            arr = new int[] { 4, 3, 4, 8, 6, 5, 3, 1, 4, 7 };
            return arr;

        }

        else if (textLevel.text.Contains("5") == true)
        {
            arr = new int[] { 6, 0, -3, 0, 0, 4, 4, 0, 0, 9 };
            return arr;

        }
        else if (textLevel.text.Contains("6") == true)
        {
            arr = new int[] { 3, 5, 8, 2, 6, 5 };
            return arr;

        }
        else if (textLevel.text.Contains("7") == true)
        {
            arr = new int[] { 8, 1, 4, 5, 0, 3 };
            return arr;

        }
        else if (textLevel.text.Contains("8") == true)
        {
            arr = new int[] { 4, -2, 1, 6 };
            return arr;

        }
        else if (textLevel.text.Contains("9") == true)
        {
            arr = new int[] { 1, 9, 7, 1, -6, -6, 1, 7, 4, 5 };
            return arr;
        }
        else if (textLevel.text.Contains("10") == true)
        {
            arr = new int[] { 7, 5, 4, 4, -7, -5, 5, 7, 9, 9 };
            return arr;
        }

        return arr;
    }



    int[] GenerateOutputs(IEnumerable<int> inputs)
    {


        return arrout;
    }




    public bool checkWin()
    {

        string outboxTExt = DrawSystem.getOutBOx();
        string[] subOutbox = outboxTExt.Split('\n');
        if (textLevel.text.Contains("1") == true && textLevel.text.Contains("0") == false)
        {
            if (subOutbox.Length == 4)
            {
                return true;
            }
            else
                return false;
        }
        else if (textLevel.text.Contains("2") == true)
        {
            //Debug.Log(subOutbox.Length);
            Debug.Log(Player.step);
            if (subOutbox.Length == 11)
            {
                return true;
            }
            else
                return false;
        }
        else if(textLevel.text.Contains("3") == true)
        {

            Debug.Log(Player.step);
            for (int i=0;i<subOutbox.Length-1;i++)
            {

                if (subOutbox[i] != (10-i).ToString() && subOutbox[i]!="")
                {
                    return false;
                }
            }

            if (subOutbox[0] != "")
            {
                return true;
            }
        }

        else if(textLevel.text.Contains("4") == true)
        {
            int[] arrinCheck = { 11 , 4 ,11 ,12 ,7 };

            for (int i = 0; i < subOutbox.Length - 1; i=i+1)
            {

                //Debug.Log(subOutbox[i] + arrinCheck[i]);
                if ( subOutbox[i] != arrinCheck[i].ToString() && subOutbox[i] != "")
                {
                    return false;
                }

            }
            if(subOutbox[0]!="")
            {
                return true;
            }    

            
        }
        else if(textLevel.text.Contains("5") == true)
        {
            int[] arrinCheck = { 9 , 4, 4, -3, 6 };

            for (int i = 0; i < subOutbox.Length - 1; i++)
            {

                //Debug.Log(subOutbox[i] + arrinCheck[i]);
                if (subOutbox[i] != arrinCheck[i].ToString() && subOutbox[i] != "")
                {
                    return false;
                }

            }
            if (subOutbox[0] != "")
            {
                return true;
            }

        }
        else if(textLevel.text.Contains("6") == true)
        {
            int[] arrinCheck = {  15 , 18 ,6 , 24 ,15 ,9 };

            for (int i = 0; i < subOutbox.Length - 1; i = i + 1)
            {

                //Debug.Log(subOutbox[i] + arrinCheck[i]);
                if (subOutbox[i] != arrinCheck[i].ToString() && subOutbox[i] != "")
                {
                    return false;
                }

            }
            if (subOutbox[0] != "")
            {
                return true;
            }

        }

        else if(textLevel.text.Contains("7") == true)
        {
            int[] arrinCheck = { 24 , 0 , 40 , 32 , 8, 64 };

            for (int i = 0; i < subOutbox.Length - 1; i = i + 1)
            {

                //Debug.Log(subOutbox[i] + arrinCheck[i]);
                if (subOutbox[i] != arrinCheck[i].ToString() && subOutbox[i] != "")
                {
                    return false;
                }

            }
            if (subOutbox[0] != "")
            {
                return true;
            }

        }
        else if(textLevel.text.Contains("8") == true)
        {
            int[] arrinCheck = { 240, 40 , -80,160 };

            for (int i = 0; i < subOutbox.Length - 1; i = i + 1)
            {

                //Debug.Log(subOutbox[i] + arrinCheck[i]);
                if (subOutbox[i] != arrinCheck[i].ToString() && subOutbox[i] != "")
                {
                    return false;
                }

            }
            if (subOutbox[0] != "")
            {
                return true;
            }

        }
        else if (textLevel.text.Contains("9") == true)
        {
            int[] arrinCheck = { - 1, 1, -6, 6, 0, 0, 6, -6, -8, 8 };

            for (int i = 0; i < subOutbox.Length - 1; i = i + 1)
            {

                //Debug.Log(subOutbox[i] + arrinCheck[i]);
                if (subOutbox[i] != arrinCheck[i].ToString() && subOutbox[i] != "")
                {
                    return false;
                }

            }
            if (subOutbox[0] != "")
            {
                return true;
            }
            

        }
        else if (textLevel.text.Contains("10") == true)
        {
            int[] arrinCheck = { 9,4 };

            for (int i = 0; i < subOutbox.Length - 1; i = i + 1)
            {

                //Debug.Log(subOutbox[i] + arrinCheck[i]);
                if (subOutbox[i] != arrinCheck[i].ToString() && subOutbox[i] != "")
                {
                    return false;
                }

            }
            if (subOutbox[0] != "")
            {
                return true;
            }

        }

        else
            return false;

        return false;



    }

}
