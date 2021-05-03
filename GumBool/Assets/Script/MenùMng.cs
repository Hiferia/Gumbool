using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenùMng : MonoBehaviour
{
    public RectTransform Button1, Button2;

    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
       
        if (Button1.gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W))
            {
                Button2.gameObject.SetActive(true);
                Button1.gameObject.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("Tutorial#1");
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W))
            {
                Button2.gameObject.SetActive(false);
                Button1.gameObject.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }

    }
}
