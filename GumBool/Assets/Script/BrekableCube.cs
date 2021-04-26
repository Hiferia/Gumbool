using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrekableCube : MonoBehaviour
{
    public float MassToDestroy = 5;
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
        if (collider.transform.tag == "BlackInk" && collider.transform.GetComponent<Rigidbody2D>().mass > MassToDestroy)
        {
            Destroy(transform.gameObject);
        }
       
    }
}
