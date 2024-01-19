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
    [SerializeField] private Sprite dotSprite;
    private RectTransform graphContainer;
    public static RectTransform labelTemplateX;
    public static RectTransform labelTemplateY;
    public static RectTransform label;
    public static RectTransform labelData;
    public static RectTransform labelUnit;
    public static List<GameObject> gameObjectList;
    private List<int> valueList = new List<int>() { };
    private List<string> sDate = new List<string>();
    private int value = 1;
    public struct valueAndTime
    {
        public float val;
        public string time;
    }
    public static List<valueAndTime> valueAndTimeList = new List<valueAndTime>();
    private string date;
    private DateTime t = new System.DateTime();
    private valueAndTime a = new valueAndTime();
    public static int enable = 1;
    public static int enable_Graph = 0;
    public static string sLabel = "";
    private int counter_data = 10;
    private string labelGraph = "";
    private string unitGraph = "";
    public TMP_Text Value_Sensor;
    public TMP_Text Infor_Sensor;
    //string data = ""{"val":9.26, "create_at":"2023-11-25"},{ "val":9.26,"create_at":"2023-11-25"};
    private void Start()
    {
        enable_Graph = 0;
        GetData();
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();
        label = graphContainer.Find("label").GetComponent<RectTransform>();
        labelData = graphContainer.Find("labelData").GetComponent<RectTransform>();
        labelUnit = graphContainer.Find("labelUnit").GetComponent<RectTransform>();
        gameObjectList = new List<GameObject>();
        /*foreach (KeyValuePair<string, int> Dict in Global.dict)
        {
            if (Dict.Value == 1)
            {
                sLabel = Dict.Key;
                break;
            }
        }
        if (sLabel == "CO")
        {
            labelGraph = "CO";
            unitGraph = "ppm";
        }
        if (sLabel == "CO2")
        {
            labelGraph = "CO2";
            unitGraph = "ppm";
        }
        if (sLabel == "Độ Ẩm")
        {
            labelGraph = "HUMIDITY";
            unitGraph = "%";
        }
        if (sLabel == "NO2")
        {
            labelGraph = "NO2";
            unitGraph = "ppm";
        }
        if (sLabel == "Ozone")
        {
            labelGraph = "Ozone";
            unitGraph = "ppm";
        }
        if (sLabel == "Bụi 10")
        {
            labelGraph = "PM10";
            unitGraph = "ppm";
        }
        if (sLabel == "Bụi 2.5")
        {
            labelGraph = "PM2.5";
            unitGraph = "ppm";
        }
        if (sLabel == "SO2")
        {
            labelGraph = "SO2";
            unitGraph = "ppm";
        }
        if (sLabel == "Nhiệt Độ")
        {
            labelGraph = "TEMPERATURE";
            unitGraph = "oC";
        }*/
    }

    private void Update()
    {
        Debug.Log(valueAndTimeList.Count);
        /*if (enable == 1)
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
            for (int i = 0; i < counter_data; i++)
            {
                t = t.AddDays(-1);
                string d = t.ToString("yyyy-MM-dd");
                sDate.Insert(0, d);
                valueList.Add(10 * i);
                a.val = (i % 2 == 0) ? 10 : 50;
                a.time = d;
                valueAndTimeList.Insert(0, a);
            }
            enable = 0;
        }
        value++;
        if (value % 101 == 0)
        {
            valueList.Add(value);
            a.val = (value % 2 == 0) ? 10 : 50;
            t = System.DateTime.Now;
            date = t.ToString("yyyy-MM-dd");
            a.time = date;
            valueAndTimeList.Add(a);

            valueAndTimeList.RemoveAt(0);
            //ShowGraph(valueList, (string _i) => "" + (_i), (float _f) => "" + Mathf.RoundToInt(_f), (string _l) => "" + (_l));
        }
        if (enable_Graph == 0) ShowGraph(valueList, (string _i) => "" + (_i), (float _f) => "" + Mathf.RoundToInt(_f), (string _l) => "" + (_l), (float _f1) => "" + Mathf.RoundToInt(_f1), (string _u) => "" + (_u));*/
        if (enable == 1)
        {
            t = System.DateTime.Now;
            string Day = t.ToString("yyyy-MM-ddThh:mm:ss");
            //Debug.Log(Day);
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
            /*foreach (KeyValuePair<string, int> Dict in Global.dict)
            {
                if (Dict.Value == 1)
                {
                    sLabel = Dict.Key;
                    //inforSensor.text = sLabel.ToUpper();
                    Debug.Log(sLabel);

                    break;
                }
            }*/
            foreach (KeyValuePair<string, int> Dict in Global.dict)
            {
                if (Dict.Value == 1)
                {
                    sLabel = Dict.Key;
                    break;
                }
            }
            if (sLabel == "CO_0001")
            {
                labelGraph = "CO";
                unitGraph = "ppm";
            }
            if (sLabel == "CO2_0001")
            {
                labelGraph = "CO2";
                unitGraph = "ppm";
            }
            if (sLabel == "humi_0001")
            {
                labelGraph = "HUMIDITY";
                unitGraph = "%";
            }
            if (sLabel == "NO2_0001")
            {
                labelGraph = "NO2";
                unitGraph = "ppm";
            }
            if (sLabel == "O3_0001")
            {
                labelGraph = "Ozone";
                unitGraph = "ppm";
            }
            if (sLabel == "PM10_0001")
            {
                labelGraph = "PM10";
                unitGraph = "ppm";
            }
            if (sLabel == "PM25_0001")
            {
                labelGraph = "PM2.5";
                unitGraph = "ppm";
            }
            if (sLabel == "SO2_0001")
            {
                labelGraph = "SO2";
                unitGraph = "ppm";
            }
            if (sLabel == "temp_0001")
            {
                labelGraph = "TEMPERATURE";
                unitGraph = "oC";
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
            ShowGraph(valueList, (string _i) => "" + (_i), (float _f) => "" + _f, (string _l) => "" + (_l), (float _f1) => "" + _f1, (string _u) => "" + (_u));
            float data = valueAndTimeList[valueAndTimeList.Count - 1].val;
            Value_Sensor.text = valueAndTimeList[valueAndTimeList.Count - 1].val.ToString();
            Infor_Sensor.text = valueAndTimeList[valueAndTimeList.Count - 1].time.Substring(0,19);
            
            //resultText.text = data.ToString();
        }
        else
        {
            if (gameObjectList != null)
            {
                foreach (GameObject gameObject in gameObjectList)
                {

                    Destroy(gameObject);
                }
                gameObjectList.Clear();
            }
        }
        /* else
         {
             labelData = null;
             labelGraph = null;
             labelTemplateX = null;
             labelTemplateY = null;
             labelUnit = null;
         }*/
    }

    /*private void Awake()
    {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();

        //CreateDot(new Vector2(200, 200));
        ShowGraph(valueList, (int _i) => "Day " + (_i + 1), (float _f) => "" + Mathf.RoundToInt(_f));
    }*/

    //create point
    private GameObject CreateDot(Vector2 anchoredPosition, int check)
    {
        GameObject gameObject = new GameObject("dot", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = dotSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        if (check == 0)
        {
            rectTransform.sizeDelta = new Vector2(10, 10);
        }
        else rectTransform.sizeDelta = new Vector2(30, 30);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    async void GetData()
    {
        DateTime dTime = new System.DateTime();
        dTime = System.DateTime.Now;
        string nTime = dTime.AddDays(1).ToString("yyyy-MM-dd");
        dTime = dTime.AddDays(-15);
        string lTime = dTime.ToString("yyyy-MM-dd");
        //string url = "http://192.168.83.233:8080/api/sensors/bkair_0001/co/2023-11-24T10:21:13/2023-11-27T10:21:13";
        //string url = "http://10.229.33.207:8080/api/sensors/bkair_0001/" + sLabel + "/"+ counter_data+"/" + lTime + "/" + nTime;
        string url = "https://iot-server-product.onrender.com/api/sensors/bkair_0001/CO2_0001/20/2023-01-01/2024-01-15";
        Debug.Log(sLabel);
        //Debug.Log(url);
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                //Debug.Log("Success");
                string data = await response.Content.ReadAsStringAsync();
                //Debug.Log(data);
                SimpleJSON.JSONNode jdata = SimpleJSON.JSONNode.Parse(data);
                for (int i = 0; i < jdata.Count; i++)
                {
                    a.val = jdata[i]["sensor_value"];
                    a.time = jdata[i]["created_at"];
                    valueAndTimeList.Add(a);
                }
                enable_Graph = 1;
                //Debug.Log(valueAndTimeList.Count);
                /*for (int i = 0; i < valueAndTimeList.Count; i++)
                {
                    Debug.Log(valueAndTimeList[i].val + " " + valueAndTimeList[i].time);
                }*/
            }
            /*else
            {
                Debug.Log("Fail get data");
            }*/

        }
    }

    //create points in graph
    private void ShowGraph(List<int> valueList, Func<string, string> getAxisLabelX = null, Func<float, string> getAxisLabelY = null, Func<string, string> getAxisLabel = null, Func<float, string> getAxisLabelD = null, Func<string, string> getAxisLabelU = null)
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
            getAxisLabelY = delegate (float _f) { return _f.ToString(); };
        }

        if (getAxisLabelD == null)
        {
            getAxisLabelD = delegate (float _f1) { return _f1.ToString(); };
        }

        if (getAxisLabelU == null)
        {
            getAxisLabelU = delegate (string _u) { return _u; };
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
        for (int i = 0; i < valueAndTimeList.Count; i++)
        {
            if (valueAndTimeList[i].val > yMaximum)
            {
                yMaximum = valueAndTimeList[i].val;
            }
        }
        yMaximum = yMaximum * 1.2f;
        float xSize = graphLength / counter_data;

        RectTransform labelG = Instantiate(label);
        labelG.SetParent(graphContainer, false);
        labelG.gameObject.SetActive(true);
        labelG.anchoredPosition = new Vector2(graphLength / 2, graphHeight + 10);
        labelG.GetComponent<Text>().text = getAxisLabel(labelGraph);
        gameObjectList.Add(labelG.gameObject);


        RectTransform labelU = Instantiate(labelUnit);
        labelU.SetParent(graphContainer, false);
        labelU.gameObject.SetActive(true);
        labelU.anchoredPosition = new Vector2(0, graphHeight + 60);
        labelU.GetComponent<Text>().text = getAxisLabelU(unitGraph);
        gameObjectList.Add(labelU.gameObject);
        //GameObject lastDotGameObject = null;
        for (int i = 0; i < counter_data; i++)
        {
            float xPosition = i * xSize + xSize / 2;
            float yPosition = (valueAndTimeList[i].val / yMaximum) * graphHeight;
            GameObject barGameObject = CreateBar(new Vector2(xPosition, yPosition), xSize * .9f);
            gameObjectList.Add(barGameObject);
            RectTransform labelD = Instantiate(labelData);
            labelD.SetParent(graphContainer, false);
            labelD.gameObject.SetActive(true);
            labelD.anchoredPosition = new Vector2(xPosition, yPosition + 30);
            labelD.GetComponent<Text>().text = getAxisLabelD(valueAndTimeList[i].val);
            gameObjectList.Add(labelD.gameObject);
            /*GameObject dotGameObject = CreateDot(new Vector2(xPosition, yPosition), i == (valueAndTimeList.Count - 1) ? 1 : 0);
            gameObjectList.Add(dotGameObject);
            if (lastDotGameObject != null)
            {
                GameObject dotConnectionGameObject = CreateDotConnection(lastDotGameObject.GetComponent<RectTransform>().anchoredPosition, dotGameObject.GetComponent<RectTransform>().anchoredPosition);
                gameObjectList.Add(dotConnectionGameObject);
            }
            lastDotGameObject = dotGameObject;*/



            // Create value for Ox
            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer, false);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xPosition, 0f);
            labelX.GetComponent<Text>().text = getAxisLabelX(valueAndTimeList[i].time.Substring(5,14));
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

    private GameObject CreateBar(Vector2 graphPosition, float barWidth)
    {
        GameObject gameObject = new GameObject("bar", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(66 / 255f, 153 / 255f, 44 / 255f, 1.0f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(graphPosition.x, 0f);
        rectTransform.sizeDelta = new Vector2(barWidth, graphPosition.y);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.pivot = new Vector2(.5f, 0f);
        return gameObject;
    }

   


}

