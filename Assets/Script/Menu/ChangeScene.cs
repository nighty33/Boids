using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Menu"))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
