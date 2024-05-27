using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackHole : SaiMonoBehaviour
{
    [SerializeField] protected bool levelCompleted;
    [SerializeField] protected int maxLevel;

    protected override void OnEnable()
    {
        base.OnEnable();
        this.maxLevel = 3;
        this.levelCompleted = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !levelCompleted)
        {
            levelCompleted = true;
            this.UnlockNewLevel();
            GameManager.Instance.GameOver();
        }
    }

    //protected virtual void CompleteLevel()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //}

    protected virtual void UnlockNewLevel()
    {
        int currentUnlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1); //1
        int reachedIndex = PlayerPrefs.GetInt("ReachedIndex", 1); //1
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //2

        Debug.Log("Current Unlocked Level: " + currentUnlockedLevel);
        Debug.Log("Reached Index: " + reachedIndex);
        Debug.Log("Current Scene Index: " + currentSceneIndex);

        if (currentUnlockedLevel < maxLevel)
        {
            if (currentSceneIndex -1 >= reachedIndex)
            {
                PlayerPrefs.SetInt("ReachedIndex", reachedIndex + 1);

                PlayerPrefs.SetInt("UnlockedLevel", currentUnlockedLevel + 1);

                PlayerPrefs.Save();

                Debug.Log("New level unlocked: " + (currentUnlockedLevel + 1));
                Debug.Log("New reached index: " + (reachedIndex + 1));
            }
        }
        else
        {
            //PlayerPrefs.SetInt("ReachedIndex", PlayerPrefs.GetInt("ReachedIndex"));

            //PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel"));

            //PlayerPrefs.Save();

            Debug.Log("Max level reached. No new level unlocked.");
        }
    }
}
