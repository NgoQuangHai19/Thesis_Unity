using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using System.Net.Http;
using UnityEngine.Networking;
using TMPro;
//using UnityEngine.UIElements;

public class Window_Graph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    private RectTransform label;
    private List<GameObject> gameObjectList;
    private List<int> valueList = new List<int>() { };
    private List<string> sDate = new List<string>();
    private int value = 1;

    public TMP_Text resultText;
    public TMP_Text inforSensor;
    public struct valueAndTime
    {
        public int val;
        public string time;
    }
    public static List<valueAndTime> valueAndTimeList = new List<valueAndTime>();
    private string date;
    private DateTime t = new System.DateTime();
    private valueAndTime a = new valueAndTime();
    public static int enable = 1;
    public static int enable_Graph = 0;
    public static string sLabel = "co";
    //string data = ""{"val":9.26, "create_at":"2023-11-25"},{ "val":9.26,"create_at":"2023-11-25"};
    private void Start()
    {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();
        label = graphContainer.Find("label").GetComponent<RectTransform>();
        gameObjectList = new List<GameObject>();
        /*foreach (KeyValuePair<string, int> Dict in Global.dict)
        {
            if (Dict.Value == 1)
            {
                sLabel = Dict.Key;
                inforSensor.text = sLabel;
                Debug.Log(sLabel);
                
                break;
            }
        }
        GetData();*/
    }

    private void Update()
    {
        if (enable == 1)
        {
            t = System.DateTime.Now;
            string Day = t.ToString("yyyy-MM-ddThh:mm:ss");
            Debug.Log(Day);
            date = t.ToString("yyyy-MM-dd");
            sDate.Insert(0, date);
            valueList.Add(100);
            a.val = 100;
            a.time = date;
            //valueAndTimeList.Insert(0, a);
            for (int i = 0; i < 0; i++)
            {
                t = t.AddDays(-1);
                string d = t.ToString("yyyy-MM-dd");
                sDate.Insert(0, d);
                valueList.Add(10 * i);
                a.val = 10 * i;
                a.time = d;
                //valueAndTimeList.Insert(0, a);
            }
            foreach (KeyValuePair<string, int> Dict in Global.dict)
            {
                if (Dict.Value == 1)
                {
                    sLabel = Dict.Key;
                    inforSensor.text = sLabel.ToUpper();
                    Debug.Log(sLabel);

                    break;
                }
            }
            GetData();
            enable = 0;
        }
        /*value++;
        if (value % 100 == 0)
        {
            valueList.Add(value);
            a.val = value;
            t = System.DateTime.Now;
            date = t.ToString("yyyy-MM-dd");
            a.time = date;
            valueAndTimeList.Add(a);
        }*/
        if (enable_Graph == 1)
        {
            ShowGraph(valueList, (string _i) => "" + (_i), (float _f) => "" + Mathf.RoundToInt(_f), (string _l) => "" + (_l));
            float data = valueAndTimeList[valueAndTimeList.Count - 1].val;
            resultText.text = data.ToString();
        }
    }

    /*private void Awake()
    {
        *//*graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();

        //CreateCircle(new Vector2(200, 200));
        ShowGraph(valueList, (int _i) => "Day " + (_i + 1), (float _f) => "" + Mathf.RoundToInt(_f));*//*
    }*/

    //create point
    private GameObject CreateCircle(Vector2 anchoredPosition, int check)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        if(check == 1)
        {
            rectTransform.sizeDelta = new Vector2(30, 30);
        }
        else rectTransform.sizeDelta = new Vector2(10, 10);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    async void GetData()
    {
        DateTime dTime = new System.DateTime();
        dTime = System.DateTime.Now;
        string nTime = dTime.AddDays(1).ToString("yyyy-MM-dd");
        dTime = dTime.AddDays(-7);
        string lTime = dTime.ToString("yyyy-MM-dd");
        //string url = "http://192.168.0.115:8080/api/sensors/00001/co/2023-11-24/2023-11-27";
        string url = "http://192.168.0.115:8080/api/sensors/" + SimpleBarcodeScanner.qr + "/" + sLabel + "/" + lTime + "/" + nTime;
        Debug.Log(url);
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                Debug.Log("Success");
                string data = await response.Content.ReadAsStringAsync();
                SimpleJSON.JSONNode jdata = SimpleJSON.JSONNode.Parse(data);
                for (int i = 0; i < jdata.Count; i++)
                {
                    a.val = jdata[i]["val"];
                    a.time = jdata[i]["create_at"];
                    valueAndTimeList.Add(a);
                }
                enable_Graph = 1;
                for (int i = 0; i < valueAndTimeList.Count; i++)
                {
                    Debug.Log(valueAndTimeList[i].val + " " + valueAndTimeList[i].time);
                }
            }
            else
            {
                Debug.Log("Fail get data");
            }

        }
    }

    //create points in graph
    private void ShowGraph(List<int> valueList, Func<string, string> getAxisLabelX = null, Func<float, string> getAxisLabelY = null, Func<string, string> getAxisLabel = null)
    {
        if (getAxisLabel == null)
        {
            getAxisLabel = delegate (string _l) { return _l; };
        }
        if (getAxisLabelX == null)
        {
            getAxisLabelX = delegate (string _i) { return _i; };
        }
        if (getAxisLabelY == null)
        {
            getAxisLabelY = delegate (float _f) { return Mathf.RoundToInt(_f).ToString(); };
        }

        /*foreach (valueAndTime valueAndTime in valueAndTimeList)
        {
            Debug.Log(valueAndTime.val + " " + valueAndTime.time);
        }*/

        foreach (GameObject gameObject in gameObjectList)
        {
            Destroy(gameObject);
        }
        gameObjectList.Clear();
        float graphHeight = graphContainer.sizeDelta.y;
        float graphLength = graphContainer.sizeDelta.x;
        //float yMaximum = valueList[0];
        float yMaximum = valueAndTimeList[0].val;
        /*foreach (int value in valueList)
        {
            if(value > yMaximum)
            {
                yMaximum = value;
            }
        }*/
        for (int i = 0; i < valueAndTimeList.Count; i++)
        {
            if (valueAndTimeList[i].val > yMaximum)
            {
                yMaximum = valueAndTimeList[i].val;
            }
        }
        yMaximum = yMaximum * 1.5f;
        float xSize = graphLength / sDate.Count;

        RectTransform labelG = Instantiate(label);
        labelG.SetParent(graphContainer, false);
        labelG.gameObject.SetActive(true);
        labelG.anchoredPosition = new Vector2(graphLength / 2, graphHeight + 10);
        labelG.GetComponent<Text>().text = getAxisLabel(sLabel);
        gameObjectList.Add(labelG.gameObject);

        GameObject lastCircleGameObject = null;
        for (int i = 0; i < sDate.Count; i++)
        {
            float xPosition = i * xSize;
            float temp = xPosition;
            int counter = 0;
            for (int j = 0; j < valueAndTimeList.Count; j++)
            {
                if (valueAndTimeList[j].time == sDate[i]) counter++;
            }
            int pos = 0;
            for (int j = 0; j < valueAndTimeList.Count; j++)
            {
                if (pos == counter) break;
                else
                {
                    if (valueAndTimeList[j].time == sDate[i])
                    {
                        float yPosition = (valueAndTimeList[j].val / yMaximum) * graphHeight;
                        temp = temp + xSize / counter * (pos >= 1 ? 1 : 0);
                        pos++;
                        GameObject circleGameObject = CreateCircle(new Vector2(temp, yPosition), j == (valueAndTimeList.Count-1)?1:0);
                        gameObjectList.Add(circleGameObject);
                        if (lastCircleGameObject != null)
                        {
                            GameObject dotConnectionGameObject = CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
                            gameObjectList.Add(dotConnectionGameObject);
                        }
                        lastCircleGameObject = circleGameObject;
                    }
                }
            }
            /*float yPosition = (valueList[i] / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            gameObjectList.Add(circleGameObject);
            if (lastCircleGameObject != null)
            {
                GameObject dotConnectionGameObject =  CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
                gameObjectList.Add(dotConnectionGameObject);
            }
            lastCircleGameObject = circleGameObject;*/
            // Create value for Ox
            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer, false);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xPosition, 0f);
            labelX.GetComponent<Text>().text = getAxisLabelX(sDate[i]);
            gameObjectList.Add(labelX.gameObject);
        }
        //Create value for Oy
        int separatorCount = 10;
        for (int i = 0; i <= separatorCount; i++)
        {
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer, false);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f / separatorCount;
            labelY.anchoredPosition = new Vector2(-7f, normalizedValue * graphHeight + (i == 0 ? 50 : 0));
            labelY.GetComponent<Text>().text = getAxisLabelY(normalizedValue * yMaximum);
            gameObjectList.Add(labelY.gameObject);
        }
    }

    //connect points in graph
    private GameObject CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 10f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
        return gameObject;
    }
}
