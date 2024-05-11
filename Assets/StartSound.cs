using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSound : SaiMonoBehaviour
{
    protected override void Start()
    {
        SoundController.Instance.PlayStartSoundEffect();
    }
}
