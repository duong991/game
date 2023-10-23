using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hightlightautodes : MonoBehaviour
{
    public float liftTime;
    private void OnEnable()
    {
        StartCoroutine(DisableObj());
    }
    IEnumerator DisableObj()
    {
        yield return new WaitForSeconds(liftTime);
        gameObject.SetActive(false);
    }
}
