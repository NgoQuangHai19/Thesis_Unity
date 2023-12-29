using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Dictionary<string, int> dict = new Dictionary<string, int>();
    private void Start()
    {
        dict.Add("temperature", 0);
        dict.Add("humidity", 0);
        dict.Add("co", 0);
        dict.Add("co2", 0);
        dict.Add("so2", 0);
        dict.Add("no2", 0);
        dict.Add("pm2.5", 0);
        dict.Add("o3", 0);
        dict.Add("pm10", 0);
    }
    public static void resetData()
    {
        /*foreach(KeyValuePair<string,int> d in dict)
         {
             dict[d.Key] = 0;
         }*/
       dict.Clear();
       Window_Graph.valueAndTimeList.Clear();
       Window_Graph.enable_Graph = 0;
        
    }    
}
