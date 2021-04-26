using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeInkScript : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collider)
    {
        //Verifica cosa collide (inchiostri, oggetti ecc)
        //usiamo i tag
        switch (collider.transform.tag)
        {
            case "BlackInk":
                if(transform.parent.GetComponent<Rigidbody2D>().mass > collider.transform.GetComponent<Rigidbody2D>().mass * 0.5f)
                {
                    UIInkMng.OnRecharge.Invoke(Inchiostri.Black, collider.transform.GetComponent<Rigidbody2D>().mass * 0.5f);
                    Destroy(collider.gameObject);
                    Destroy(transform.gameObject);
                }
                break;
            case "BrownInk":
                break;
            case "Enviroment":
                break;
            default:
                break;
        }
    }
}
