using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackHole : SaiMonoBehaviour
{
    [SerializeField] protected bool levelCompleted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D");
        if (collision.gameObject.CompareTag("Player") && !levelCompleted)
        {
            levelCompleted = true;
            this.UnlockNewLevel();
            //Invoke("CompleteLevel", 2f);
            GameManager.Instance.GameOver();
        }
    }

    protected virtual void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    protected virtual void UnlockNewLevel()
    {
        int currentUnlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (currentUnlockedLevel < 3)
        {
            if (SceneManager.GetActiveScene().buildIndex + 1 >= PlayerPrefs.GetInt("ReachedIndex"))
            {
                PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);

                PlayerPrefs.SetInt("UnlockedLevel", currentUnlockedLevel + 1);

                PlayerPrefs.Save();
            }
        }
        else
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);

            PlayerPrefs.SetInt("UnlockedLevel", 3);

            PlayerPrefs.Save();
        }
    }
}
