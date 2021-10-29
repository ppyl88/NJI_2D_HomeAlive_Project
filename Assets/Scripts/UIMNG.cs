using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMNG : MonoBehaviour
{
    Sound buttonSound;
    // Start is called before the first frame update
    void Start()
    {
        buttonSound = GetComponent<Sound>();
    }

    public void PlayButtonFunction()
    {
        buttonSound.SoundPlay(0);
        Time.timeScale = 1;
        //PauseUI를 찾아서 활성화
        GameObject.Find("Canvas").transform.Find("PauseUI").gameObject.SetActive(false);
    }
    public void ExitButtonFunction()
    {
        buttonSound.SoundPlay(0);
        Debug.Log("게임종료");
        Application.Quit();
    }
    public void HomeButtonFunction()
    {
        buttonSound.SoundPlay(0);
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
        Time.timeScale = 1;
    }
    public void RePlayInfiniteButtonFunction()
    {
        Time.timeScale = 1;
        buttonSound.SoundPlay(0);
        //UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene_Infinite");
        Invoke("PlayInfiniteModeButtonFunction", 0.2f);
    }
    public void PlayInfiniteModeButtonFunction()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene_Infinite");
        Time.timeScale = 1;
    }
    public void RePlayMissionButtonFunction()
    {
        Time.timeScale = 1;
        buttonSound.SoundPlay(0);
        //UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene_Infinite");
        Invoke("PlayMissionModeButtonFunction", 0.2f);
    }
    public void PlayMissionModeButtonFunction()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene_Mission");
        Time.timeScale = 1;
    }
    public void ExplainButtonFunction()
    {
        buttonSound.SoundPlay(0);
        GameObject.Find("Canvas").transform.Find("ExplainUI").gameObject.SetActive(true);
    }
    public void ExplainOffFunction()
    {
        GameObject.Find("Canvas").transform.Find("ExplainUI").gameObject.SetActive(false);
    }

}
