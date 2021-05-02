using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "player")
        {
            collider.GetComponent<PlayerController>().enableMove = false;
            collider.GetComponent<PlayerController>().Sliding = true;
        }
        
        Debug.Log("ENTRO");

    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "player")
        {
            collider.GetComponent<PlayerController>().enableMove = true;
            collider.GetComponent<PlayerController>().Sliding = false;
        }
        Debug.Log("ESCO");

    }

}
