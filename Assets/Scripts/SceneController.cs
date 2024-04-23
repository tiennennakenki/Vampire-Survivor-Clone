using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : SaiMonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI coin;

    protected override void Awake()
    {
        base.Awake();
        this.UpdateCoin();
    }

    public virtual void SceneChange(string name)
    {
        SceneManager.LoadScene(name);
        Time.timeScale = 1f;
        this.UpdateCoin();
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
