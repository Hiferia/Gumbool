using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemiesScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        Debug.Log("COLLISIONE");
        //Verifica cosa collide (inchiostri, oggetti ecc)
        //usiamo i tag
        switch (collider.transform.tag)
        {

            case "BlackInk":
                Destroy(collider.gameObject);
                break;
            case "BrownInk":
                    Destroy(collider.gameObject);
                break;
            case "CyanInk":
                    Destroy(collider.gameObject);
                break;
            case "Player":

                SceneManager.LoadScene("Liv1_Test");
             
                break;
            default:
              
                break;
        }
    }
}
