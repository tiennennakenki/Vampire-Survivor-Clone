using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : SaiMonoBehaviour
{
    [SerializeField] protected Button button;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadButton();
    }

    protected virtual void LoadButton()
    {
        if (this.button != null) return;
        button = GetComponent<Button>();

        Debug.LogWarning(transform.name + ": LoadButton", gameObject);
    }

    protected override void Start()
    {
        button.onClick.AddListener(PlaySound);
    }

    protected virtual void PlaySound()
    {
        SoundManager.Instance.PlayClickSoundEffect();
    }
}
