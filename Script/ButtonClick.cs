using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    public void LoadSceen(string sceenName)
    {
        SceneManager.LoadScene(sceenName);
    }
}
