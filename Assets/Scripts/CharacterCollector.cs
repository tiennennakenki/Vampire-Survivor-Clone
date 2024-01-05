using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterCollector : SaiMonoBehaviour
{
    private static CharacterCollector instance;
    public static CharacterCollector Instance => instance;
    [SerializeField] protected CharacterSO characterData;

    protected override void Awake()
    {
        base.Awake();
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("Only 1 CharacterCollector allow to exits");
            Destroy(gameObject);
        }
    }

    public CharacterSO GetData()
    {
        return instance.characterData;
    }

    public virtual void SelectCharacter(CharacterSO character)
    {
        this.characterData = character;
    }

    public virtual void DestroySingleton()
    {
        instance = null;
        Destroy(gameObject);
    }
}
