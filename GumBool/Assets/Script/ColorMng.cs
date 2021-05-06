using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;


[System.Serializable]
public struct ColorType
{
    public string Name;
    public Color Color;
    public float TotalInk;

    public float ActualInk { get { return actualInk; } set { actualInk = value; } }
    public float actualInk;

    public float InkUsageMultiplayer;
    public float MassMultiplayer;
    public float GravityScale;
    public string Tag;
    public bool HasRigidBody;
    public bool HasCollider;
    public bool HasScript;
    //[HideInInspector]
    public Scripts[] scripts;
}

[System.Serializable]
public struct Scripts
{
    //public string Name;
    public string ScriptName;
    public MonoBehaviour Script;
}

//public enum Inchiostri { Black, Brown, Cyan, Orange, Last };

public class ColorMng : MonoBehaviour
{
    public Material defaultMatLine;
    public float Delay;
    public float ScrollFactor;
    float scroll;
    public float Distance;
    public GameObject Player;
    public ColorType[] Colors;

    List<Vector2> PositionSaved;

    //public Inchiostri inchiostro;
    public int ActualInk;

    float mass;

    //float currentInk;

    bool isPressing = false;

    Vector2 media;
    int count = 0;

    GameObject OggettoCheSiCrea;

    float counter;

    LineRenderer line;

    Dictionary<string, int> tagDictionary = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start()
    {
        PositionSaved = new List<Vector2>();
        /*if (SceneManager.GetActiveScene().name == "Tutorial#1")
        {
            Black = 0;
            Brown = 0;
            Cyan = 0;
            Orange = 0;
        }
        else
        {
            Black = 60;
            Brown = 60;
            Cyan = 60;
            Orange = 60;
        }*/

        for (int i = 0; i < Colors.Length; i++)
        {
            Colors[i].ActualInk = Colors[i].TotalInk;
            tagDictionary.Add(Colors[i].Tag, i);
        }

        UIInkMng.OnDictionaryInit.Invoke(tagDictionary);

        UIInkMng.OnRecharge.AddListener(OnRechargeInkAmount);
        UIInkMng.OnActiveInk.AddListener(OnActiveInkCallBack);
    }
    void OnActiveInkCallBack(string pencilInk)
    {
        if (tagDictionary.TryGetValue(pencilInk, out int i))
        {
            Colors[i].ActualInk = Colors[i].TotalInk;
        }
        /*switch (pencilInk)
        {
            case "BlackPencil":
                Black = 100;
                break;
            case "BrownPencil":
                Brown = 100;
                break;
            case "CyanPencil":
                Cyan = 100;
                break;
            case "OrangePencil":
                Orange = 100;
                break;
            default:
                break;
        }*/
    }
    void OnRechargeInkAmount(string tag, float amount)
    {
        if (tagDictionary.TryGetValue(tag, out int i))
        {
            Colors[i].ActualInk += amount;
        }
        /*switch (ink)
        {
            case Inchiostri.Black:
                Black += amount;
                break;
            case Inchiostri.Brown:
                Brown += amount;
                break;
            case Inchiostri.Cyan:
                Cyan += amount;
                break;
            case Inchiostri.Orange:
                Orange += amount;
                break;
            default:
                break;
        }*/
    }
    // Update is called once per frame
    void Update()
    {
        scroll += Input.mouseScrollDelta.y;
        if (scroll < -ScrollFactor)
        {
            ActualInk += 1;
            scroll = 0;
            if (ActualInk == Colors.Length)
            {
                ActualInk = 0;
            }
            UIInkMng.OnChangeInk.Invoke(ActualInk);
        }
        if (scroll > ScrollFactor)
        {
            ActualInk -= 1;
            scroll = 0;
            if (ActualInk < 0)
            {
                ActualInk = Colors.Length - 1;
            }
            UIInkMng.OnChangeInk.Invoke(ActualInk);
        }

        /*switch (inchiostro)
        {
            case Inchiostri.Black:
                UIInkMng.OnChangeInk.Invoke(Inchiostri.Black);
                currentInk = Black;
                break;
            case Inchiostri.Brown:
                UIInkMng.OnChangeInk.Invoke(Inchiostri.Brown);
                currentInk = Brown;
                break;
            case Inchiostri.Cyan:
                UIInkMng.OnChangeInk.Invoke(Inchiostri.Cyan);
                currentInk = Cyan;
                break;
            case Inchiostri.Orange:
                UIInkMng.OnChangeInk.Invoke(Inchiostri.Orange);
                currentInk = Orange;
                break;
            default:
                break;
        }*/

        //Debug.Log(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.Mouse0) && Colors[ActualInk].ActualInk > 0)
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

            line.startColor = Colors[ActualInk].Color;
            line.endColor = Colors[ActualInk].Color;

            /*if (inchiostro == Inchiostri.Black)
            {
                //OggettoCheSiCrea.AddComponent<EdgeCollider2D>();
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
                line.startColor = Color.cyan;
                line.endColor = Color.cyan;
            }
            if (inchiostro == Inchiostri.Orange)
            {
                Color orange = new Color(1, 0.549f, 0, 1);
                line.startColor = orange;
                line.endColor = orange;
            }*/
        }
        if (Input.GetKey(KeyCode.Mouse0) && Colors[ActualInk].ActualInk > 0)
        {
            //mi salvo i punti mentre disegno -> (se finisco inchiostro smetto di salvare i punti)
            counter += Time.deltaTime;
            if (counter >= Delay)
            {
                if (Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), Player.transform.position) < Distance)
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

                        /*switch (inchiostro)
                        {
                            case Inchiostri.Black:
                                Black -= distance * BlackMul;
                                UIInkMng.OnDraw.Invoke(Inchiostri.Black, Black);
                                currentInk = Black;
                                break;
                            case Inchiostri.Brown:
                                Brown -= distance * BrownMul;
                                UIInkMng.OnDraw.Invoke(Inchiostri.Brown, Brown);
                                currentInk = Brown;
                                break;
                            case Inchiostri.Cyan:
                                Cyan -= distance * CyanMul;
                                UIInkMng.OnDraw.Invoke(Inchiostri.Cyan, Cyan);
                                currentInk = Cyan;
                                break;
                            case Inchiostri.Orange:
                                Orange -= distance * OrangeMul;
                                UIInkMng.OnDraw.Invoke(Inchiostri.Orange, Orange);
                                currentInk = Orange;
                                break;
                            default:
                                break;
                        }*/
                        Colors[ActualInk].ActualInk -= distance * Colors[ActualInk].InkUsageMultiplayer;
                        UIInkMng.OnDraw.Invoke(Colors[ActualInk].Tag, Colors[ActualInk].ActualInk);
                        //currentInk = Colors[ActualInk].TotalInk;

                        mass += distance * Colors[ActualInk].MassMultiplayer;

                        count++;

                        line.positionCount = PositionSaved.Count;
                        for (int i = 0; i < line.positionCount; i++)
                        {
                            line.SetPosition(i, PositionSaved[i]);
                        }
                    }
                }

                counter = 0;
            }
        }

        if (isPressing)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0) || Colors[ActualInk].ActualInk < 0 || Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), Player.transform.position) > Distance)
            {
                isPressing = false;
                if (PositionSaved.Count > 2)
                {
                    GameObject parentObj = new GameObject();
                    OggettoCheSiCrea.tag = Colors[ActualInk].Tag;
                    if (Colors[ActualInk].HasCollider)
                    {
                        //Logiche di creazione collider tramite i punti salvati precedentemente
                        parentObj.transform.position = new Vector3(media.x / count, media.y / count);
                        OggettoCheSiCrea.AddComponent<PolygonCollider2D>();
                        parentObj.tag = Colors[ActualInk].Tag;
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
                        collider.points = arrayPos;
                    }
                    if (Colors[ActualInk].HasRigidBody)
                    {
                        parentObj.AddComponent<Rigidbody2D>();
                        parentObj.GetComponent<Rigidbody2D>().mass = mass;
                        parentObj.GetComponent<Rigidbody2D>().gravityScale = Colors[ActualInk].GravityScale;
                        OggettoCheSiCrea.transform.parent = parentObj.transform;
                        parentObj.AddComponent<UnderWaterLogic>();
                    }
                    if (!Colors[ActualInk].HasCollider && !Colors[ActualInk].HasRigidBody)
                    {
                        Destroy(parentObj);
                    }
                    if (Colors[ActualInk].HasScript)
                    {
                        for (int i = 0; i < Colors[ActualInk].scripts.Length; i++)
                        {
                            OggettoCheSiCrea.AddComponent(Colors[ActualInk].scripts[i].Script.GetType());
                        }
                        EventVariableMng.OnSendingPositions.Invoke(PositionSaved, Player);
                    }
                    /*if (inchiostro == Inchiostri.Black)
                    {

                        //Logiche di creazione collider tramite i punti salvati precedentemente
                        GameObject parentObj = new GameObject();
                        parentObj.transform.position = new Vector3(media.x / count, media.y / count);
                        //OggettoCheSiCrea.transform.position = new Vector3(media.x / count, media.y / count);

                        //EdgeCollider2D collider = OggettoCheSiCrea.GetComponent<EdgeCollider2D>();
                        OggettoCheSiCrea.AddComponent<PolygonCollider2D>();
                        OggettoCheSiCrea.tag = "BlackInk";
                        parentObj.tag = "BlackInk";
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
                        parentObj.AddComponent<UnderWaterLogic>();
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
                        OggettoCheSiCrea.tag = "BrownInk";
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
                        parentObj.tag = "CyanInk";

                        //EdgeCollider2D collider = OggettoCheSiCrea.GetComponent<EdgeCollider2D>();
                        OggettoCheSiCrea.AddComponent<PolygonCollider2D>();
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
                        parentObj.GetComponent<Rigidbody2D>().gravityScale = -1;
                        parentObj.GetComponent<Rigidbody2D>().mass = mass;
                        OggettoCheSiCrea.transform.parent = parentObj.transform;
                        count = 0;
                        media = Vector2.zero;
                        parentObj.AddComponent<UnderWaterLogic>();
                    }
                    if (inchiostro == Inchiostri.Orange)
                    {
                        //Logiche di creazione collider tramite i punti salvati precedentemente
                        GameObject parentObj = new GameObject();
                        parentObj.transform.position = new Vector3(media.x / count, media.y / count);
                        //OggettoCheSiCrea.transform.position = new Vector3(media.x / count, media.y / count);

                        //EdgeCollider2D collider = OggettoCheSiCrea.GetComponent<EdgeCollider2D>();
                        OggettoCheSiCrea.AddComponent<PolygonCollider2D>();
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
                        parentObj.GetComponent<Rigidbody2D>().gravityScale = 0;
                        OggettoCheSiCrea.AddComponent<OrangeInkScript>();

                        OggettoCheSiCrea.transform.parent = parentObj.transform;
                        count = 0;
                        media = Vector2.zero;
                        parentObj.AddComponent<UnderWaterLogic>();
                    }*/

                    //reset all list for the new object 
                }
                else
                {
                    Destroy(OggettoCheSiCrea);
                }
                count = 0;
                media = Vector2.zero;
                PositionSaved.Clear();
            }
        }
        if (!isPressing)
        {
            PositionSaved.Clear();
        }


    }

}