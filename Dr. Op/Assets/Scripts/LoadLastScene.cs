using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLastScene : MonoBehaviour
{
    public void loadLastScene()
    {
        SceneManager.LoadScene(0);
    }
}
