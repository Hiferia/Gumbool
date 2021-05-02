using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeInkScript : MonoBehaviour
{
    
    void OnCollisionEnter2D(Collision2D collider)
    {
        Debug.Log("COLLISIONE");
        //Verifica cosa collide (inchiostri, oggetti ecc)
        //usiamo i tag
        switch (collider.transform.tag)
        {
            
            case "BlackInk":
                if (collider.transform.GetComponent<UnderWaterLogic>().canBeDeleted)
                {
                    if (transform.parent.GetComponent<Rigidbody2D>().mass > collider.transform.GetComponent<Rigidbody2D>().mass * 0.5f)
                    {
                        Debug.Log("IF");
                        UIInkMng.OnRecharge.Invoke(Inchiostri.Black, collider.transform.GetComponent<Rigidbody2D>().mass * 0.5f);
                        Destroy(collider.gameObject);
                        Destroy(transform.gameObject);
                    }
                }
                else
                {
                    Destroy(transform.gameObject);
                }
                break;
            case "BrownInk":
                if (transform.parent.GetComponent<Rigidbody2D>().mass > collider.transform.GetComponent<Rope>().Mass * 0.5f)
                {
                    UIInkMng.OnRecharge.Invoke(Inchiostri.Brown, collider.transform.GetComponent<Rope>().Mass * 0.5f);
                    Destroy(collider.gameObject);
                    Destroy(transform.gameObject);
                }
                break;
            case "CyanInk":
                if (collider.transform.GetComponent<UnderWaterLogic>().canBeDeleted)
                {
                    if (transform.parent.GetComponent<Rigidbody2D>().mass > collider.transform.GetComponent<Rigidbody2D>().mass * 0.5f)
                    {
                        UIInkMng.OnRecharge.Invoke(Inchiostri.Cyan, collider.transform.GetComponent<Rigidbody2D>().mass * 0.5f);
                        Destroy(collider.gameObject);
                        Destroy(transform.gameObject);
                    }
                }
                break;
            case "Enviroment":
                if (collider.transform.GetComponent<UnderWaterLogic>().canBeDeleted)
                {
                    if (transform.parent.GetComponent<Rigidbody2D>().mass > collider.transform.GetComponent<Rigidbody2D>().mass * 0.5f)
                    {
                        Destroy(collider.gameObject);
                        Destroy(transform.gameObject);
                    }
                }
                break;
            default:
                Destroy(transform.gameObject);
                break;
        }
    }
}
