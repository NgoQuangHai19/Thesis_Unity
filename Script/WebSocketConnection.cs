using UnityEngine;
using UnityEngine.UI;
using System;
using WebSocketSharp;
using TMPro;

public class WebSocketConnection : MonoBehaviour
{
    private WebSocket socket;
    public TMP_Text CO;
    public TMP_Text CO2;
    public TMP_Text HUMIDITY;
    public TMP_Text NO2;
    public TMP_Text O3;
    public TMP_Text PM10;
    public TMP_Text PM2_5;
    public TMP_Text SO2;
    public TMP_Text TEMPERATURE;
    public TMP_Text inforCabinet;
    public string co;
    public string co2;
    public string humidity;
    public string no2;
    public string o3;
    public string pm10;
    public string pm2_5;
    public string so2;
    public string temperature;
    public TMP_Text[] textObjects;
    
    string linkSocket = "ws://192.168.0.115:8080/websocket?id=" + SimpleBarcodeScanner.qr;
    /*private string linkSocket = "ws://192.168.1.111:8080/websocket?id=" + "00001";*/
    private void Start()
    {
        // Thiết lập kết nối WebSocket
        Debug.Log(linkSocket);
        socket = new WebSocket(linkSocket);

        // Đăng ký các sự kiện
        socket.OnOpen += OnSocketOpen;
        socket.OnMessage += OnSocketMessage;
        socket.OnError += OnSocketError;
        socket.OnClose += OnSocketClose;

        // Kết nối tới máy chủ
        socket.Connect();
        textObjects = FindObjectsOfType<TMP_Text>();
        // Gán đối tượng thích hợp 
        inforCabinet.text ="Tủ " + SimpleBarcodeScanner.qr;
        
        foreach (TMP_Text textObject in textObjects)
        {
            if (textObject.name == "CO")
            {
                CO = textObject;
            }
            else if (textObject.name == "CO2")
            {
                CO2 = textObject;
            }
            else if (textObject.name == "HUMIDITY")
            {
                HUMIDITY = textObject;
            }
            else if (textObject.name == "NO2")
            {
                NO2 = textObject;
            }
            else if (textObject.name == "O3")
            {
                O3 = textObject;
            }
            else if (textObject.name == "PM10")
            {
                PM10 = textObject;
            }
            else if (textObject.name == "PM2_5")
            {
                PM2_5 = textObject;
            }
            else if (textObject.name == "SO2")
            {
                SO2 = textObject;
            }
            else if (textObject.name == "TEMPERATURE")
            {
                TEMPERATURE = textObject;
            }
        }
    }

    private void OnDestroy()
    {
        // Đóng kết nối WebSocket trước khi đối tượng bị hủy
        if (socket != null && socket.IsAlive)
        {
            socket.Close();
        }
    }
    void Update()
    {
        CO.text = co;
        CO2.text = co2;
        HUMIDITY.text = humidity;
        NO2.text = no2;
        O3.text = o3;
        PM10.text = pm10;
        PM2_5.text = pm2_5;
        SO2.text = so2;
        TEMPERATURE.text = temperature;
        //Debug.Log(inforCabinet.text);
    }

    private void OnSocketOpen(object sender, System.EventArgs e)
    {
        Debug.Log("WebSocket connection opened");

        // Gửi tin nhắn tới máy chủ
        socket.Send("co,no2,o3,so2,humidity,pm10,pm2_5,co2,temperature");
        socket.Send(SimpleBarcodeScanner.qr);
    }

    private void OnSocketMessage(object sender, MessageEventArgs e)
    {
        Debug.Log("Received message from server: " + e.Data);
        SimpleJSON.JSONNode jData = SimpleJSON.JSONNode.Parse(e.Data);
        //Debug.Log(jData);
        if (jData["topic"] == "co")
        {
            co = jData["val"];
        }
        else if (jData["topic"] == "co2")
        {
            co2 = jData["val"];
        }
        else if (jData["topic"] == "humidity")
        {
            humidity =  jData["val"];
        }
        else if (jData["topic"] == "no2")
        {
            no2 =  jData["val"];
        }
        else if (jData["topic"] == "o3")
        {
            o3 =  jData["val"];
        }
        else if (jData["topic"] == "so2")
        {
            so2 =  jData["val"];
        }
        else if (jData["topic"] == "pm10")
        {
            pm10 =  jData["val"];
        }
        else if (jData["topic"] == "pm2_5")
        {
            pm2_5 =  jData["val"];
        }
        else if (jData["topic"] == "temperature")
        {
            temperature =  jData["val"];
        }

        if (jData["topic"] == Window_Graph.sLabel)
        {
            Window_Graph.valueAndTime a;
            a.val = jData["val"];
            a.time = jData["create_at"];
            Debug.Log(a.val);
            Debug.Log(a.time);
            Window_Graph.valueAndTimeList.Add(a);
        }
        /*string[] Data = e.Data.Split(" ");
        if (Data[0] == "co")
        {
            co = "CO: " + Data[1];
        }
        else if (Data[0] == "co2")
        {
            co2 = "CO2: " + Data[1];
        }
        else if (Data[0] == "humidity")
        {
            humidity = "humi: " + Data[1];
        }
        else if (Data[0] == "no2")
        {
            no2 = "NO2: " + Data[1];
        }
        else if (Data[0] == "o3")
        {
            o3 = "O3: " + Data[1];
        }
        else if (Data[0] == "so2")
        {
            so2 = "so2 " + Data[1];
        }
        else if (Data[0] == "pm10")
        {
            pm10 = "PM10: " + Data[1];
        }
        else if (Data[0] == "pm2-5")
        {
            pm2_5 = "PM2_5: " + Data[1];
        }
        else if (Data[0] == "temperature")
        {
            temperature = "temp: " + Data[1];
        }*/


    }

    private void OnSocketError(object sender, ErrorEventArgs e)
    {
        Debug.LogError("WebSocket error: " + e.Message);
    }

    private void OnSocketClose(object sender, CloseEventArgs e)
    {
        Debug.Log("WebSocket connection closed with code: " + e.Code);
    }
}