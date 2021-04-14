using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum Inchiostri { Black, Brown, Cyan, Orange, Last };

public class TestRaycast : MonoBehaviour
{
    public Material defaultMatLine;
    public List<Vector2> PositionSaved;
    public float Delay;
    public Inchiostri inchiostro;

    Vector2 media;
    int count = 0;

    GameObject OggettoCheSiCrea;

    float counter;

    public float ScrollFactor;
    float scroll;

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
        scroll += Input.mouseScrollDelta.y;
        if (scroll < -ScrollFactor)
        {
            inchiostro -= 1;
            scroll = 0;
            if (inchiostro < 0)
            {
                inchiostro = Inchiostri.Last - 1;
            }
        }
        if (scroll > ScrollFactor)
        {
            inchiostro += 1;
            scroll = 0;
            if (inchiostro == Inchiostri.Last)
            {
                inchiostro = 0;
            }
        }

        //Debug.Log(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OggettoCheSiCrea = new GameObject();
            OggettoCheSiCrea.AddComponent<LineRenderer>();

            line = OggettoCheSiCrea.GetComponent<LineRenderer>();
            line.material = defaultMatLine;
            line.startWidth = 0.3f;
            line.endWidth = 0.3f;
            line.useWorldSpace = false;

            if (inchiostro == Inchiostri.Black)
            {
                OggettoCheSiCrea.AddComponent<EdgeCollider2D>();
                line.startColor = Color.black;
                line.endColor = Color.black;
            }
            if (inchiostro == Inchiostri.Brown)
            {
                Color borwn = new Color(0.545f, 0.27f, 0.074f, 1);
                line.startColor = borwn;
                line.endColor = borwn;
            }
            if (inchiostro == Inchiostri.Cyan)
            {
                OggettoCheSiCrea.AddComponent<EdgeCollider2D>();
                line.startColor = Color.cyan;
                line.endColor = Color.cyan;
            }
            if (inchiostro == Inchiostri.Orange)
            {
                Color orange = new Color(1, 0.549f, 0, 1);
                line.startColor = orange;
                line.endColor = orange;
            }
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //mi salvo i punti mentre disegno -> (se finisco inchiostro smetto di salvare i punti)
            counter += Time.deltaTime;
            if (counter >= Delay)
            {
                //TODO -> CHECK IF THE POSITION IS NOT THE SAME OF THE PREVIOUS ONE (se il player sta muovendo il mouse)
                if (PositionSaved.Count < 4 || Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), PositionSaved[PositionSaved.Count - 1]) > 0.3f)
                {
                    PositionSaved.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));

                    media.x += PositionSaved[PositionSaved.Count - 1].x;
                    media.y += PositionSaved[PositionSaved.Count - 1].y;

                    count++;

                    line.positionCount = PositionSaved.Count;
                    for (int i = 0; i < line.positionCount; i++)
                    {
                        line.SetPosition(i, PositionSaved[i]);
                    }
                }

                counter = 0;
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (inchiostro == Inchiostri.Black)
            {
                //Logiche di creazione collider tramite i punti salvati precedentemente
                GameObject parentObj = new GameObject();
                parentObj.transform.position = new Vector3(media.x / count, media.y / count);
                //OggettoCheSiCrea.transform.position = new Vector3(media.x / count, media.y / count);

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
                parentObj.AddComponent<Rigidbody2D>();
                OggettoCheSiCrea.transform.parent = parentObj.transform;
                count = 0;
                media = Vector2.zero;
            }
            if (inchiostro == Inchiostri.Brown)
            {
                OggettoCheSiCrea.AddComponent<Rope>();

                Vector3[] arrayPos = new Vector3[PositionSaved.Count];
                List<RopeSegment> ropeSegments = new List<RopeSegment>();

                for (int i = 0; i < arrayPos.Length; i++)
                {
                    arrayPos[i] = PositionSaved[i];
                    ropeSegments.Add(new RopeSegment(PositionSaved[i]));
                }

                OggettoCheSiCrea.GetComponent<Rope>().ropePositions = arrayPos;
                OggettoCheSiCrea.GetComponent<Rope>().segmentLength = arrayPos.Length;
                OggettoCheSiCrea.GetComponent<Rope>().ropeSegments = ropeSegments;


                //OggettoCheSiCrea.GetComponent<Rope>().ropeSegments = PositionSaved;
            }
            if (inchiostro == Inchiostri.Cyan)
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
                OggettoCheSiCrea.GetComponent<Rigidbody2D>().gravityScale = -2;
            }
            if (inchiostro == Inchiostri.Orange)
            {

            }

            //reset all list for the new object 
            PositionSaved.Clear();
        }


    }

}
