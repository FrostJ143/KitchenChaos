using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public static class Loader
{
    public enum Scene {
        MainMenuScene,
        LoadingScene,
        GameScene,
    }
    
    private static Scene scene;
    
    public static void Load(Scene targetScene)
    {
        Loader.scene = targetScene;

        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }
    
    public static void LoaderCallBack()
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
