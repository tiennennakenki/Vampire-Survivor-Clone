using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : SaiMonoBehaviour
{
    private static SceneController instance;
    public static SceneController Instance => instance;

    [SerializeField] protected TextMeshProUGUI coin;
    [SerializeField] protected GameObject loadingScene;
    [SerializeField] protected Image loadingBarFill;

    protected override void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            instance = this;
        }

        this.UpdateCoin();
    }

    IEnumerator LoadSceneAsync(string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);

        if (loadingScene == null) 
            yield return null;
        loadingScene.SetActive(true);

        while(!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            loadingBarFill.fillAmount = progressValue;

            yield return null;
        }

    }

    public virtual void SceneChange(string name)
    {
        SceneManager.LoadScene(name);
        Time.timeScale = 1f;
        this.UpdateCoin();
    }

    public virtual void LoadingScene(string name)
    {
        StartCoroutine(LoadSceneAsync(name));
    }

    public virtual void Quit()
    {
        Application.Quit();
    }

    public virtual void UpdateCoin()
    {
        if (this.coin == null) return;
        this.coin.text = PlayerPrefs.GetFloat("TotalCoin").ToString();
    }
}
