using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSound : SaiMonoBehaviour
{
    protected override void Start()
    {
        SoundManager.Instance.PlayStartSoundEffect();
    }
}
