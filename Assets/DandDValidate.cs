using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;  
using System.Net;
using System.IO;
using System;

public class DandDValidate : MonoBehaviour
{
    [SerializeField] Transform slots;
    [SerializeField] Transform elements;
    public GameObject prefab;
    public GameObject modalLost;
    public GameObject modalWin;
    public GameObject yodoModal;
    public Sprite SuperFail;
    public Sprite fail1;
    public Sprite fail2;
    public Sprite fail3;
    public Sprite win1;
    public Sprite win2;
    public Sprite win3;

    private HttpWebRequest request;
    private const string ULR_API = "http://localhost:3000";

    void Start() {
            modalLost.SetActive(false);
            modalWin.SetActive(false);
            yodoModal.SetActive(false);

            request = WebRequest.Create(ULR_API + "/elements/level1") as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/json";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(responseStream);
            var pageContent = myStreamReader.ReadToEnd();
            jsonDataClass jsnData = JsonUtility.FromJson<jsonDataClass>("{\"elements\":"+ pageContent + "}");

            GameObject parentPanel = GameObject.Find("GridOptions");
            GameObject PPPanel = GameObject.Find("PeriodicTable");

                // GameObject frame = (GameObject)Instantiate(prefab);
                // GameObject frmaeChild =  frame.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
                // frmaeChild.name = "Oxigeno";
                // frmaeChild.GetComponent<DragHandlerScript>().slots =  PPPanel.transform;
                // Debug.Log(frmaeChild.name);
                // frame.transform.SetParent(parentPanel.transform, false);
            // GameObject frame = (GameObject)Instantiate(prefab);
            // GameObject frmaeChild =  frame.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
            // frmaeChild.name = "Oxigeno";
            // Debug.Log(frmaeChild.name);
            // frame.transform.SetParent(parentPanel.transform, false);

            //WORKS
            // GameObject panel = new GameObject();
            // Image i = panel.AddComponent<Image>();
            // i.color = Color.blue;
            // panel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 146.3f);
            // panel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 146.1f);
            // panel.transform.SetParent(parentPanel.transform, false);




            //WORKS
            foreach (Element item in jsnData.elements)
            {
                GameObject frame = (GameObject)Instantiate(prefab);
                frame.name = item.name + "-Frame";
                GameObject frmaeChild =  frame.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
                frmaeChild.name = item.name;
                frmaeChild.GetComponent<DragHandlerScript>().slots =  PPPanel.transform;
                frame.transform.SetParent(parentPanel.transform, false);
                GameObject elemtItem = GameObject.Find(item.name +"-Frame" + "/Slot").transform.GetChild(0).gameObject;
                GameObject elementName = GameObject.Find(elemtItem.name + "/Text").gameObject;
                GameObject elementSymb = GameObject.Find(elemtItem.name + "/Symbol").gameObject;
                elementName.GetComponent<Text>().text = item.name;
                elementName.GetComponent<Text>().fontSize = 29;
                elementName.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;;        

                elementSymb.GetComponent<Text>().text = item.symbol;
                elementSymb.GetComponent<Text>().fontSize = 50;
                elementSymb.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;;        
            }

        //     foreach (Transform item in elements)
        // {
            //.transform.GetChild(0)
            // GameObject elemtItem = GameObject.Find(item.name + "/Slot").transform.GetChild(0).gameObject;
            // Debug.Log(elemtItem.name);
            // elemtItem.text = "Hidro";
            // GameObject newText = GameObject.Find(elemtItem.name + "/Text").gameObject;
            // Debug.Log(newText.name);
            // newText.transform.SetParent(elemtItem.transform);
        //     newText.GetComponent<Text>().text = "Hidrogeno";
        //     newText.GetComponent<Text>().fontSize = 25;
        //     newText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;;        
        // }
    }

    public void validate(){
        bool isFail = false;
        GameObject lives = GameObject.Find("Lives");
        GameObject chibi = GameObject.Find("Chibi").transform.GetChild(0).gameObject;
        GameObject live1 = lives.transform.GetChild(0).gameObject;
        GameObject live2 = lives.transform.GetChild(1).gameObject;
        GameObject live3 = lives.transform.GetChild(2).gameObject;
        GameObject imgModal = modalLost.transform.GetChild(0).gameObject;
        GameObject imgModalWin = modalWin.transform.GetChild(1).gameObject;
        Image imgComp = imgModal.GetComponent<Image>();
        Image imgCompWin = imgModalWin.GetComponent<Image>();
        Image imgChibi = chibi.GetComponent<Image>();
        //WORKS
        foreach (Transform slotTransform in slots){
            string slotName = slotTransform.name;
            string[] element = slotName.Split('-');
            GameObject dragElement = GameObject.Find(element[0]);
            float distance = Vector3.Distance(slotTransform.transform.position, dragElement.transform.position);

            if (distance > 1)
            {
                isFail = true;
                modalLost.SetActive(true);
                Debug.Log("Tienes errores!"); 
            }
        }

        if (isFail)
        {
            if (live3.active)
            {
                imgComp.sprite = fail1;
                imgChibi.sprite = fail1;
                imgComp.preserveAspect = true;   
                live3.SetActive(false);
            }
            else
            {
                if (live2.active)
                {
                    imgComp.sprite = fail2;
                    imgChibi.sprite = fail2;
                    imgComp.preserveAspect = true;   
                    live2.SetActive(false);
                }
                else
                {
                    if (live1.active)
                    {
                        imgComp.sprite = fail3;
                        imgChibi.sprite = fail3;
                        imgComp.preserveAspect = true;   
                        live1.SetActive(false);
                    }
                }
            }
        }
        else
        {
            if (live3.active)
            {
                imgCompWin.sprite = win1;
            }
            else
            {
                if (live2.active)
                {
                    imgCompWin.sprite = win2; 
                }
                else
                {
                    if (live1.active)
                    {
                        imgCompWin.sprite = win3;
                    }
                }
            }
            modalWin.SetActive(true);
        }
    }

    public void closeLostModal(){
        GameObject lives = GameObject.Find("Lives");
        GameObject live1 = lives.transform.GetChild(0).gameObject;
        GameObject imgModalYodo = yodoModal.transform.GetChild(1).gameObject;
        Image imgCompYodo = imgModalYodo.GetComponent<Image>();
        modalLost.SetActive(false);
        if (!live1.active)
        {
            yodoModal.SetActive(true);
            imgCompYodo.sprite = SuperFail;
            
        }
    }

    public void navigateMenuLevel(){
        SceneManager.LoadScene("LevelMenu");
    }

    [Serializable]
    public class jsonDataClass
    {
        public List<Element> elements;
        
    }

    [Serializable]
    public class Element{
        public int atomicNumber ;
        public string name;
        public string symbol;
        public string color;
        public string period;
        public string group;
        public float atomicWeight;
    }
}
