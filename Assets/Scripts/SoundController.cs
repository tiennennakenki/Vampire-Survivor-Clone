using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundController : SaiMonoBehaviour
{
    private static SoundController instance;
    public static SoundController Instance => instance;

    [SerializeField] protected AudioSource clickSoundEffect;
    [SerializeField] protected AudioSource attackSoundEffect;
    [SerializeField] protected AudioSource lightningRingSoundEffect;
    [SerializeField] protected AudioSource collectSoundEffect;
    [SerializeField] protected AudioSource enemyHurtSoundEffect;
    [SerializeField] protected AudioSource characterHurtSoundEffect;
    [SerializeField] protected AudioSource itemUnlockedSoundEffect;
    [SerializeField] protected AudioSource itemFailedSoundEffect;

    protected override void Awake()
    {
        base.Awake();
        if(instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadClickSoundEffect();
        this.LoadAttackSoundEffect();
        this.LoadLightningRingSoundEffect();
        this.LoadCollectSoundEffect();
        this.LoadEnemyHurtSoundEffect();
        this.LoadCharacterHurtSoundEffect();
        this.LoadItemUnlockedSoundEffect();
        this.LoadItemFailedSoundEffect();
    }

    protected virtual void LoadClickSoundEffect()
    {
        if (this.clickSoundEffect != null) return;

        AudioSource[] audioSources = GetComponents<AudioSource>();

        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.clip.name == "click")
            {
                this.clickSoundEffect = audioSource;
                Debug.LogWarning(transform.name + ": LoadClickSoundEffect", gameObject);
            }
        }
    }

    protected virtual void LoadAttackSoundEffect()
    {
        if (this.attackSoundEffect != null) return;

        AudioSource[] audioSources = GetComponents<AudioSource>();

        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.clip.name == "attack")
            {
                this.attackSoundEffect = audioSource;
                Debug.LogWarning(transform.name + ": LoadAttackSoundEffect", gameObject);
            }
        }
    }

    protected virtual void LoadLightningRingSoundEffect()
    {
        if (this.lightningRingSoundEffect != null) return;

        AudioSource[] audioSources = GetComponents<AudioSource>();

        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.clip.name == "lightningRing")
            {
                this.lightningRingSoundEffect = audioSource;
                Debug.LogWarning(transform.name + ": LoadLightningRingSoundEffect", gameObject);
            }
        }
    }

    protected virtual void LoadCollectSoundEffect()
    {
        if (this.collectSoundEffect != null) return;

        AudioSource[] audioSources = GetComponents<AudioSource>();

        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.clip.name == "collect")
            {
                this.collectSoundEffect = audioSource;
                Debug.LogWarning(transform.name + ": LoadCollectSoundEffect", gameObject);
            }
        }
    }

    protected virtual void LoadEnemyHurtSoundEffect()
    {
        if (this.enemyHurtSoundEffect != null) return;

        AudioSource[] audioSources = GetComponents<AudioSource>();

        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.clip.name == "enemyHurt")
            {
                this.enemyHurtSoundEffect = audioSource;
                Debug.LogWarning(transform.name + ": LoadEnemyHurtSoundEffect", gameObject);
            }
        }
    }

    protected virtual void LoadCharacterHurtSoundEffect()
    {
        if (this.characterHurtSoundEffect != null) return;

        AudioSource[] audioSources = GetComponents<AudioSource>();

        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.clip.name == "characterHurt")
            {
                this.characterHurtSoundEffect = audioSource;
                Debug.LogWarning(transform.name + ": LoadCharacterHurtSoundEffect", gameObject);
            }
        }
    }

    protected virtual void LoadItemUnlockedSoundEffect()
    {
        if (this.itemUnlockedSoundEffect != null) return;

        AudioSource[] audioSources = GetComponents<AudioSource>();

        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.clip.name == "itemUnlocked")
            {
                this.itemUnlockedSoundEffect = audioSource;
                Debug.LogWarning(transform.name + ": LoadItemUnlockedSoundEffect", gameObject);
            }
        }
    }

    protected virtual void LoadItemFailedSoundEffect()
    {
        if (this.itemFailedSoundEffect != null) return;

        AudioSource[] audioSources = GetComponents<AudioSource>();

        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.clip.name == "itemFailed")
            {
                this.itemFailedSoundEffect = audioSource;
                Debug.LogWarning(transform.name + ": LoadItemFailedSoundEffect", gameObject);
            }
        }
    }

    public virtual void PlayClickSoundEffect()
    {
        this.clickSoundEffect.Play();
    }

    public virtual void PlayAttackSoundEffect()
    {
        this.attackSoundEffect.Play();
    }

    public virtual void PlayLightningRingSoundEffect()
    {
        this.lightningRingSoundEffect.Play();
    }

    public virtual void PlayCollectSoundEffect()
    {
        this.collectSoundEffect.Play();
    }

    public virtual void PlayEnemyHurtSoundEffect()
    {
        this.enemyHurtSoundEffect.Play();
    }

    public virtual void PlayCharacterHurtSoundEffect()
    {
        this.characterHurtSoundEffect.Play();
    }

    public virtual void PlayItemUnlockedSoundEffect()
    {
        this.itemUnlockedSoundEffect.Play();
    }

    public virtual void PlayItemFailedSoundEffect()
    {
        this.itemFailedSoundEffect.Play();
    }
}
