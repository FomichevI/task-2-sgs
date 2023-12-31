using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScene : MonoBehaviour
{
    public TextMeshProUGUI ProgressText;
    public Image ProgressBar;

    private int _loadProgress = 0;

    void Start()
    {
        StartCoroutine(DisplayLoadingScreen(PlayerPrefs.GetString("SceneToLoad")));
    }

    IEnumerator DisplayLoadingScreen(string sceneName)
    {
        ProgressText.text = "Загрузка... " + _loadProgress + "%";

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            _loadProgress = (int)(async.progress * 100);
            ProgressText.text = "Загрузка... " + _loadProgress + "%";
            ProgressBar.fillAmount = async.progress;
            if (async.progress > 0.8)
            {
                yield return new WaitForSeconds(1);
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}