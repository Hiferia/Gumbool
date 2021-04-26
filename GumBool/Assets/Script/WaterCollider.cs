using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollider : MonoBehaviour
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
        if (collider.transform.tag == "player")
            collider.transform.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.transform.tag == "player")
            collider.transform.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
