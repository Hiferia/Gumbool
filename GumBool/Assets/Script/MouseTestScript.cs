using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTestScript : MonoBehaviour
{
    Vector2 mousePos;
    RaycastHit2D hitInfo2D;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            Debug.Log(rayHit.transform.position);
 
        }
    }
}
