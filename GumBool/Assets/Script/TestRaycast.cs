using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRaycast : MonoBehaviour
{
    public Material defaultMatLine;
    public List<Vector2> PositionSaved;
    public float counter;
    GameObject OggettoCheSiCrea;

    List<GameObject> OggettiCheHoCreato;
    LineRenderer line;
    
    // Start is called before the first frame update
    void Start()
    {
        PositionSaved = new List<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OggettoCheSiCrea = new GameObject();
            OggettoCheSiCrea.AddComponent<LineRenderer>();
            OggettoCheSiCrea.AddComponent<EdgeCollider2D>();
            line = OggettoCheSiCrea.GetComponent<LineRenderer>();
            line.material = defaultMatLine;
            line.startWidth = 0.3f;
            line.endWidth = 0.3f;
            line.useWorldSpace = false;
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            
            
            //mi salvo i punti mentre disegno -> (se finisco inchiostro smetto di salvare i punti)
            counter += Time.deltaTime;
            if (counter >= 0.05f)
            {
                //TODO -> CHECK IF THE POSITION IS NOT THE SAME OF THE PREVIOUS ONE (se il player sta muovendo il mouse)
                PositionSaved.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));

                line.positionCount = PositionSaved.Count;
                for (int i = 0; i < line.positionCount; i++)
                {
                    line.SetPosition(i, PositionSaved[i]);
                }
                counter = 0;
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            //Logiche di creazione collider tramite i punti salvati precedentemente
            EdgeCollider2D collider = OggettoCheSiCrea.GetComponent<EdgeCollider2D>();
            Vector2[] arrayPos = new Vector2[PositionSaved.Count];
            for (int i = 0; i < arrayPos.Length; i++)
            {
                arrayPos[i] = PositionSaved[i];
            }
            collider.points = arrayPos;
            collider.edgeRadius = 0.15f;
            for (int i = 0; i < PositionSaved.Count; i++)
            {
                Debug.Log(PositionSaved[i]);
            }
            OggettoCheSiCrea.AddComponent<Rigidbody2D>();

            //reset all list for the new object 
            PositionSaved.Clear();
        }

       
    }

}
