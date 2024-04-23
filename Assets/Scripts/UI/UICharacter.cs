using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICharacter : SaiMonoBehaviour
{
    [SerializeField] protected CharacterData characterData;
    [SerializeField] protected Image characterAvatar;
    [SerializeField] protected TextMeshProUGUI characterName;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCharacterData();
        this.LoadCharacterAvatar();
        this.LoadCharacterName();
    }

    protected virtual void LoadCharacterData()
    {
        if (characterData != null) return;

        string respath = "Characters/" + gameObject.name;
        this.characterData = Resources.Load<CharacterData>(respath);

        Debug.LogWarning(transform.name+  " : LoadCharacterData", gameObject);
    }

    protected virtual void LoadCharacterAvatar()
    {
        if (characterData == null) return;
        if(characterAvatar != null) return;

        this.characterAvatar = transform.Find("Character Avatar").GetComponent<Image>();

        Debug.LogWarning(transform.name + " : LoadCharacterAvatar", gameObject);
    }

    protected virtual void LoadCharacterName()
    {
        if (characterData == null) return;
        if (characterName != null) return;

        this.characterName = transform.Find("Character Name").GetComponent<TextMeshProUGUI>();

        Debug.LogWarning(transform.name + " : LoadCharacterAvatar", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        CharacterCollector.OnCharacterBought += UpdateCharacterAvatar;
        this.characterAvatar.sprite = this.characterData.Icon;
        this.characterName.text = this.characterData.Name;

        this.UpdateCharacterAvatar(characterData);
    }

    protected virtual void UpdateCharacterAvatar(CharacterData characterData)
    {
        if (this.characterData.IsBought)
        {
            this.characterAvatar.color = Color.white;
        }
        else
        {
            this.characterAvatar.color = Color.black;
        }
    }


    protected virtual void UpdateUIData()
    {
        if (characterData != null) return;
        if(characterData.IsBought)
        {
            this.characterAvatar.color = Color.black;
        }
        else
        {
            this.characterAvatar.color = Color.white;
        }
    }
}
