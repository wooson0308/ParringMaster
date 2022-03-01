using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public static string nextScene;
    public Text LoadingBarText;

    [SerializeField]
    public Image progressBar;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    string nextSceneName;
    public static void LoadScene(string sceneName)
    {
        Time.timeScale = 1;
        nextScene = sceneName;
        SceneManager.LoadScene("Load");
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;

            timer += Time.deltaTime;

            //LoadingBarText.text = (progressBar.fillAmount * 100f).ToString("N0") + " %";

            if (op.progress >= 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);

                if (progressBar.fillAmount == 1.0f)
                    op.allowSceneActivation = true;
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);

                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
        }
    }
}
