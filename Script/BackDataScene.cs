using System.Net.Http;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene2 : MonoBehaviour
{
    public void LoadSceen(string scene)
    {
        //GameObject.Find("Temperature").GetComponent<Button>();

        Global.resetData();
        SceneManager.LoadScene(scene);   
    }
    

}
