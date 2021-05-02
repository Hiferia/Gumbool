using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFadeOut : MonoBehaviour
{
    public float Timer = 0;
    float delay = 6;

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= delay)
        {
            transform.gameObject.SetActive(false);
        }
    }
}
