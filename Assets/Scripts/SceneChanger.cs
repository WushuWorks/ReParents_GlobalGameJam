using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void GameScene() {
        SceneManager.LoadScene("Game Scene");
    }

    public void IntroScene()
    {
        SceneManager.LoadScene("Intro Scene");
    }

    public void LoseScene()
    {
        SceneManager.LoadScene("Lose Scene");
    }

    public void WinScene()
    {
        SceneManager.LoadScene("Win Scene");
    }

    public void CreditsScene()
    {
        SceneManager.LoadScene("Credits Scene");
    }

}
