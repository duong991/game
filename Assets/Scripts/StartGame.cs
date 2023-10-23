using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{

    public Animator animator;
    public Button btnBACK;

    [SerializeField] private List<Button> ListButton = new List<Button>();


    [SerializeField] private List<GameObject> MedalList = new List<GameObject>();
    
    
    
    
    public Color newColor;

    void Start()
    {
        // init

        btnBACK.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);


        });


        List<int> listLevel = DataPlayer.getLevel();
        List<int> listDG = DataPlayer.getDG();
        
        //Debug.Log(listLevel.Count);
        //Debug.Log(listDG.Count);


     

        for (int i=0;i<ListButton.Count;i++)
        {
            string textBtn = ListButton[i].name;
            string[] subs = textBtn.Split(' ');


            
            if (int.Parse(subs[1])  != listLevel.Count && int.Parse(subs[1]) > listLevel.Count) 
            {

                ListButton[i].enabled = false;
                ListButton[i].image.enabled = false;
                ColorBlock cb = ListButton[i].colors;
                cb.normalColor = newColor;
                ListButton[i].colors = cb;

                
            }    
        }
        for(int i = 0; i<listDG.Count;i++)
        {

                
                if (listDG[i] == 3)
                {
                    for (int j = 0; j < MedalList[i-1].transform.childCount; j++)
                    {
                        GameObject child = MedalList[i - 1].transform.GetChild(j).gameObject;
                        child.SetActive(true);

                    }
                }
                else if(listDG[i] ==1)
                {

                }
                else
                {
                    GameObject child = MedalList[i - 1].transform.GetChild(0).gameObject;
                    child.SetActive(true);
                    GameObject child1 = MedalList[i - 1].transform.GetChild(1).gameObject;
                    child1.SetActive(true);
                }
        }




        ListButton[0].onClick.AddListener(()=> 
        {
            animator.SetBool("ActiveEND", true);
            StartCoroutine(LoadLevel(1));  
               
        });
        ListButton[1].onClick.AddListener(() =>
        {
            animator.SetBool("ActiveEND", true);
            StartCoroutine(LoadLevel(2));


        });
        ListButton[2].onClick.AddListener(() =>
        {
            animator.SetBool("ActiveEND", true);
            StartCoroutine(LoadLevel(3));


        });
        ListButton[3].onClick.AddListener(() =>
        {
            animator.SetBool("ActiveEND", true);
            StartCoroutine(LoadLevel(4));
        });
        ListButton[4].onClick.AddListener(() =>
        {
            animator.SetBool("ActiveEND", true);
            StartCoroutine(LoadLevel(5));
        });
        ListButton[5].onClick.AddListener(() =>
        {
            animator.SetBool("ActiveEND", true);
            StartCoroutine(LoadLevel(6));
        });
        ListButton[6].onClick.AddListener(() =>
        {
            animator.SetBool("ActiveEND", true);
            StartCoroutine(LoadLevel(7));
        });
        ListButton[7].onClick.AddListener(() =>
        {
            animator.SetBool("ActiveEND", true);
            StartCoroutine(LoadLevel(8));
        });
        ListButton[8].onClick.AddListener(() =>
        {
            animator.SetBool("ActiveEND", true);
            StartCoroutine(LoadLevel(9));
        });
        ListButton[9].onClick.AddListener(() =>
        {
            animator.SetBool("ActiveEND", true);
            StartCoroutine(LoadLevel(10));
        });
        ListButton[10].onClick.AddListener(() =>
        {
            animator.SetBool("ActiveEND", true);
            StartCoroutine(LoadLevel(11));
        });

    }

    private IEnumerator LoadLevel(int level )
    {
        //SceneManager.LoadScene("Scenes/Change_Name");
        
        
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene($"Scenes/Level_{level}", LoadSceneMode.Single);
    }
}
