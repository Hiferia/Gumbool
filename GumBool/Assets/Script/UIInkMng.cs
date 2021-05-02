using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIInkMng : MonoBehaviour
{
    public static UnityEvent<Inchiostri> OnChangeInk;
    public static UnityEvent<Inchiostri, float> OnDraw;
    public static UnityEvent<Inchiostri, float> OnRecharge;
    public static UnityEvent<string> OnActiveInk;
    public List<GameObject> InkSelectedFrames;
    public List<Image> InkContainers;
    public List<Transform> Tutorials;
    void OnEnable(){
        OnChangeInk = new UnityEvent<Inchiostri>();
        OnDraw = new UnityEvent<Inchiostri, float>();
        OnRecharge = new UnityEvent<Inchiostri, float>();
        OnActiveInk = new UnityEvent<string>();
        OnChangeInk.AddListener(OnChangeInkCallBack);
        OnDraw.AddListener(OnDrawCallBack);
        OnRecharge.AddListener(OnRechargeCallBack);
        OnActiveInk.AddListener(OnActiveInkCallBack);
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
    void OnActiveInkCallBack(string pencilInk)
    {
        switch (pencilInk)
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
        }
    }
    void OnRechargeCallBack(Inchiostri ink, float amount)
    {
        amount = MathUtils.Remap(amount, 0, 100, 0, 1);
        switch (ink)
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
        }
    }
    void OnDrawCallBack(Inchiostri ink, float amount){
        amount = MathUtils.Remap(amount, 0, 100, 0, 1);
        switch (ink)
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
        }
    }
    void OnChangeInkCallBack(Inchiostri ink){
        for (int i = 0; i < InkSelectedFrames.Count; i++)
        {
                InkSelectedFrames[i].SetActive(false);
        }
        switch (ink)
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
        }
    }
    void ShowTutorial(int index)
    {
        Tutorials[index].GetComponent<TutorialFadeOut>().Timer = 0;
        Tutorials[index].gameObject.SetActive(true);
    }

}
