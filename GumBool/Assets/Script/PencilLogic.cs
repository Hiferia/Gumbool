using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PencilLogic : MonoBehaviour
{
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.transform.tag == "player")
        {
            UIInkMng.OnActiveInk.Invoke(transform.tag);
            Destroy(transform.gameObject);
        }
    }

}
