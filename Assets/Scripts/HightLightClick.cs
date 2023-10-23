using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HightLightClick : MonoBehaviour
{
    private List<GameObject> objectList = new List<GameObject>();
    public GameObject prefab;
    // Start is called before the first frame update

    public GameObject GetOjpool()
    {
        foreach(var highlightobj in objectList)
        {
            if(!highlightobj.activeInHierarchy)
            {
                highlightobj.SetActive(true);
                return highlightobj;
            }
        }
        var obj = Instantiate(prefab);
        objectList.Add(obj);
        return obj;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var obj = GetOjpool();

            var positon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            positon.z = 4;

            obj.transform.position = positon;
        }
    }
}
