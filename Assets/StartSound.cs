using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSound : SaiMonoBehaviour
{
    protected override void Start()
    {
        SoundManager.Instance.PlayStartSoundEffect();

        //PlayerPrefs.SetInt("ReachedIndex", 1);

        //PlayerPrefs.SetInt("UnlockedLevel", 1);
        //PlayerPrefs.SetFloat("TotalCoin", 0);

        //PlayerPrefs.Save();
    }
}
