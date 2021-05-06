using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIInkMng : MonoBehaviour
{
    public static UnityEvent<int> OnChangeInk;
    public static UnityEvent<string, float> OnDraw;
    public static UnityEvent<string, float> OnRecharge;
    public static UnityEvent<string> OnActiveInk;
    public static UnityEvent<Dictionary<string, int>> OnDictionaryInit;
    public List<GameObject> InkSelectedFrames;
    public List<Image> InkContainers;
    public List<Transform> Tutorials;

    Dictionary<string, int> tagDictionary = new Dictionary<string, int>();

    void OnEnable()
    {
        OnChangeInk = new UnityEvent<int>();
        OnDraw = new UnityEvent<string, float>();
        OnRecharge = new UnityEvent<string, float>();
        OnActiveInk = new UnityEvent<string>();
        OnDictionaryInit = new UnityEvent<Dictionary<string, int>>();
        OnChangeInk.AddListener(OnChangeInkCallBack);
        OnDraw.AddListener(OnDrawCallBack);
        OnRecharge.AddListener(OnRechargeCallBack);
        OnActiveInk.AddListener(OnActiveInkCallBack);
        OnDictionaryInit.AddListener(OnDictionaryInitCallback);
        if (SceneManager.GetActiveScene().name == "Tutorial#1")
        {
            for (int i = 0; i < InkContainers.Count; i++)
            {
                InkContainers[i].fillAmount = 0;
            }
        }
    }
    //private void Start()
    //{
    //    for (int i = 0; i < InkContainers.Count; i++)
    //    {
    //        InkContainers[i].color -= new Color(0, 0, 0, 0.5f);
    //    }
    //}

    void OnDictionaryInitCallback(Dictionary<string, int> dict)
    {
        tagDictionary = dict;
    }

    void OnActiveInkCallBack(string pencilInk)
    {
        if (tagDictionary.TryGetValue(pencilInk, out int i))
        {
            InkContainers[i].fillAmount = 1;
            if (SceneManager.GetActiveScene().name == "Tutorial#1")
            {
                ShowTutorial(i);
            }
        }
        /*switch (pencilInk)
        {
            case "BlackPencil":
                InkContainers[0].fillAmount = 1;
                if(SceneManager.GetActiveScene().name == "Tutorial#1")
                {
                    ShowTutorial(0);
                }
                break;
            case "BrownPencil":
                InkContainers[1].fillAmount = 1;
                if (SceneManager.GetActiveScene().name == "Tutorial#1")
                {
                    ShowTutorial(1);
                }
                break;
            case "CyanPencil":
                InkContainers[2].fillAmount = 1;
                if (SceneManager.GetActiveScene().name == "Tutorial#1")
                {
                    ShowTutorial(2);
                }
                break;
            case "OrangePencil":
                InkContainers[3].fillAmount = 1;
                if (SceneManager.GetActiveScene().name == "Tutorial#1")
                {
                    ShowTutorial(3);
                }
                break;
            default:
                break;
        }*/
    }
    void OnRechargeCallBack(string tag, float amount)
    {
        amount = MathUtils.Remap(amount, 0, 100, 0, 1);
        if (tagDictionary.TryGetValue(tag, out int i))
        {
            InkContainers[i].fillAmount += amount;
        }
        /*switch (ink)
        {
            case Inchiostri.Black:
                InkContainers[0].fillAmount += amount;
                break;
            case Inchiostri.Brown:
                InkContainers[1].fillAmount += amount;
                break;
            case Inchiostri.Cyan:
                InkContainers[2].fillAmount += amount;
                break;
            case Inchiostri.Orange:
                InkContainers[3].fillAmount += amount;
                break;
            default:
                break;
        }*/
    }
    void OnDrawCallBack(string tag, float amount)
    {
        amount = MathUtils.Remap(amount, 0, 100, 0, 1);
        if (tagDictionary.TryGetValue(tag, out int i))
        {
            InkContainers[i].fillAmount = amount;
        }
        /*switch (ink)
        {
            case Inchiostri.Black:
                InkContainers[0].fillAmount = amount;
                break;
            case Inchiostri.Brown:
                InkContainers[1].fillAmount = amount;
                break;
            case Inchiostri.Cyan:
                InkContainers[2].fillAmount = amount;
                break;
            case Inchiostri.Orange:
                InkContainers[3].fillAmount = amount;
                break;
            default:
                break;
        }*/
    }
    void OnChangeInkCallBack(int n)
    {
        for (int i = 0; i < InkSelectedFrames.Count; i++)
        {
            InkSelectedFrames[i].SetActive(false);
        }
        InkSelectedFrames[n].SetActive(true);
        /*switch (ink)
        {
            case Inchiostri.Black:
                InkSelectedFrames[0].SetActive(true);
                break;
            case Inchiostri.Brown:
                InkSelectedFrames[1].SetActive(true);
                break;
            case Inchiostri.Cyan:
                InkSelectedFrames[2].SetActive(true);
                break;
            case Inchiostri.Orange:
                InkSelectedFrames[3].SetActive(true);
                break;
            default:
                break;
        }*/
    }
    void ShowTutorial(int index)
    {
        Tutorials[index].GetComponent<TutorialFadeOut>().Timer = 0;
        Tutorials[index].gameObject.SetActive(true);
    }

}
