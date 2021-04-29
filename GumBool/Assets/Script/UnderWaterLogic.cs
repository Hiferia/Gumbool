using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderWaterLogic : MonoBehaviour
{
    public bool canBeDeleted = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.transform.tag)
        {
            case "Water":
                canBeDeleted = false;
                break;
            default:
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.transform.tag)
        {
            case "Water":
                canBeDeleted = true;
                break;
            default:
                break;
        }
    }
}
