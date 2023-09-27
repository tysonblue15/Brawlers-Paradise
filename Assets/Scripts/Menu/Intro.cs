using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public enum Scene
    {
        MainMenu,
        Dojo
    }

    void Start()
    {
        StartCoroutine(SwitchScene());
    }



    IEnumerator SwitchScene()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene(Scene.MainMenu.ToString());
    }
}
