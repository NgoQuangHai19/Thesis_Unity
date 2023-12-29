using System.Net.Http;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void LoadSceen(string sensor)
    {
        //GameObject.Find("Temperature").GetComponent<Button>();

        Global.dict[sensor] = 1;
        SceneManager.LoadScene("chart");
        Window_Graph.enable = 1;
        Window_Graph.enable_Graph = 0;
        Debug.Log(sensor);    
    }
    async void GetData()
    {
        Debug.Log("Get data");
        string url = "http://192.168.106.233:8080";
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                Debug.Log("Success");
            }
            else Debug.Log("Fail get data");              
        }
    }

}
