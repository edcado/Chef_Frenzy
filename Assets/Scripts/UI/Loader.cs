using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene {MainMenu, MainScene, LoadScene }

    public static Scene targetScene;

    public static void Load(Scene targetScene)
    {
        Loader.targetScene = targetScene;

        SceneManager.LoadScene(Scene.LoadScene.ToString());

    }
       
    public static void Loading()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
