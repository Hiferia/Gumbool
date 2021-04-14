using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Inchiostri { Black, Brown, Cyan, Orange, Last };

public class TestCollider : MonoBehaviour
{
    public Material defaultMatLine;
    public List<Vector2> PositionSaved;
    public float Delay;
    public Inchiostri inchiostro;

    public float Black, Brown, Cyan, Orange;
    public float BlackMul, BrownMul, CyanMul, OrangeMul;

    public float massMul;

    float mass;

    float currentInk;

    bool isPressing = false;

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
        Black = 100;
        Brown = 100;
        Cyan = 100;
        Orange = 100;
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

        switch (inchiostro)
        {
            case Inchiostri.Black:
                currentInk = Black;
                break;
            case Inchiostri.Brown:
                currentInk = Brown;
                break;
            case Inchiostri.Cyan:
                currentInk = Cyan;
                break;
            case Inchiostri.Orange:
                currentInk = Orange;
                break;
            default:
                break;
        }

        //Debug.Log(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.Mouse0) && currentInk > 0)
        {
            isPressing = true;

            mass = 0;

            OggettoCheSiCrea = new GameObject();
            OggettoCheSiCrea.AddComponent<LineRenderer>();

            line = OggettoCheSiCrea.GetComponent<LineRenderer>();
            line.material = defaultMatLine;
            line.startWidth = 0.3f;
            line.endWidth = 0.3f;
            line.useWorldSpace = false;

            if (inchiostro == Inchiostri.Black)
            {
                //OggettoCheSiCrea.AddComponent<EdgeCollider2D>();
                OggettoCheSiCrea.AddComponent<PolygonCollider2D>();
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
                //OggettoCheSiCrea.AddComponent<EdgeCollider2D>();
                OggettoCheSiCrea.AddComponent<PolygonCollider2D>();
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
        if (Input.GetKey(KeyCode.Mouse0) && currentInk > 0)
        {
            //mi salvo i punti mentre disegno -> (se finisco inchiostro smetto di salvare i punti)
            counter += Time.deltaTime;
            if (counter >= Delay)
            {
                //TODO -> CHECK IF THE POSITION IS NOT THE SAME OF THE PREVIOUS ONE (se il player sta muovendo il mouse)
                if (PositionSaved.Count < 2)
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
                else if (Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), PositionSaved[PositionSaved.Count - 1]) > 0.3f)
                {
                    float distance = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), PositionSaved[PositionSaved.Count - 1]);

                    PositionSaved.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));

                    media.x += PositionSaved[PositionSaved.Count - 1].x;
                    media.y += PositionSaved[PositionSaved.Count - 1].y;

                    switch (inchiostro)
                    {
                        case Inchiostri.Black:
                            Black -= distance * BlackMul;
                            currentInk = Black;
                            break;
                        case Inchiostri.Brown:
                            Brown -= distance * BrownMul;
                            currentInk = Brown;
                            break;
                        case Inchiostri.Cyan:
                            Cyan -= distance * CyanMul;
                            currentInk = Cyan;
                            break;
                        case Inchiostri.Orange:
                            Orange -= distance * OrangeMul;
                            currentInk = Orange;
                            break;
                        default:
                            break;
                    }

                    mass += distance * massMul;

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

        if (isPressing)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0) || currentInk < 0)
            {
                isPressing = false;

                if (inchiostro == Inchiostri.Black)
                {
                    //Logiche di creazione collider tramite i punti salvati precedentemente
                    GameObject parentObj = new GameObject();
                    parentObj.transform.position = new Vector3(media.x / count, media.y / count);
                    //OggettoCheSiCrea.transform.position = new Vector3(media.x / count, media.y / count);

                    //EdgeCollider2D collider = OggettoCheSiCrea.GetComponent<EdgeCollider2D>();
                    PolygonCollider2D collider = OggettoCheSiCrea.GetComponent<PolygonCollider2D>();
                    Vector2[] arrayPos = new Vector2[PositionSaved.Count * 2];
                    for (int i = 0; i < arrayPos.Length / 2; i++)
                    {
                        //Vector2 offset = new Vector2(-0.101f, -0.101f);
                        arrayPos[i] = PositionSaved[i];//+offset;
                    }
                    for (int i = arrayPos.Length / 2; i < arrayPos.Length; i++)
                    {
                        Vector2 offset = new Vector2(0.01f, 0.01f);
                        arrayPos[i] = PositionSaved[arrayPos.Length - i - 1] + offset;
                    }
                    //collider.points
                    collider.points = arrayPos;
                    //collider.edgeRadius = 0.15f;
                    parentObj.AddComponent<Rigidbody2D>();
                    parentObj.GetComponent<Rigidbody2D>().mass = mass;
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
                    GameObject parentObj = new GameObject();
                    parentObj.transform.position = new Vector3(media.x / count, media.y / count);
                    //OggettoCheSiCrea.transform.position = new Vector3(media.x / count, media.y / count);

                    //EdgeCollider2D collider = OggettoCheSiCrea.GetComponent<EdgeCollider2D>();
                    PolygonCollider2D collider = OggettoCheSiCrea.GetComponent<PolygonCollider2D>();
                    Vector2[] arrayPos = new Vector2[PositionSaved.Count * 2];
                    for (int i = 0; i < arrayPos.Length / 2; i++)
                    {
                        //Vector2 offset = new Vector2(-0.101f, -0.101f);
                        arrayPos[i] = PositionSaved[i];//+offset;
                    }
                    for (int i = arrayPos.Length / 2; i < arrayPos.Length; i++)
                    {
                        Vector2 offset = new Vector2(0.01f, 0.01f);
                        arrayPos[i] = PositionSaved[arrayPos.Length - i - 1] + offset;
                    }
                    //collider.points
                    collider.points = arrayPos;
                    //collider.edgeRadius = 0.15f;
                    parentObj.AddComponent<Rigidbody2D>();
                    parentObj.GetComponent<Rigidbody2D>().gravityScale = -2;
                    parentObj.GetComponent<Rigidbody2D>().mass = mass;
                    OggettoCheSiCrea.transform.parent = parentObj.transform;
                    count = 0;
                    media = Vector2.zero;
                }
                if (inchiostro == Inchiostri.Orange)
                {

                }

                //reset all list for the new object 
                PositionSaved.Clear();
            }
        }


    }

}
