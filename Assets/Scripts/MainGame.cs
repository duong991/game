using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    public Button btnStart;

    // Start is called before the first frame update
    void Start()
    {
        btnStart.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Change_Name", LoadSceneMode.Single);


        });


    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QUIT");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
