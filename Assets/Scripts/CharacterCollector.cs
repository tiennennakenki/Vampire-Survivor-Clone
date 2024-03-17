using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CharacterCollector : SaiMonoBehaviour
{
    private static CharacterCollector instance;
    public static CharacterCollector Instance => instance;
    public CharacterData characterData;

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

    //public CharacterData GetData()
    //{
    //    return instance.characterData;
    //}
    public CharacterData GetData()
    {
        if (instance && instance.characterData)
        {
            Debug.LogWarning("1");
            return instance.characterData;
        }
        else
        {
            // Randomly pick a character if we are playing from the Editor.
            #if UNITY_EDITOR
            string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();
            List<CharacterData> characters = new List<CharacterData>();
            foreach (string assetPath in allAssetPaths)
            {
                if (assetPath.EndsWith(".asset"))
                {
                    CharacterData characterData = AssetDatabase.LoadAssetAtPath<CharacterData>(assetPath);
                    if (characterData != null)
                    {
                        characters.Add(characterData);
                    }
                }
            }

            // Pick a random character if we have found any characters.
            if (characters.Count > 0)
            {
                Debug.LogWarning($"{characters.Count} characters");
                return characters[Random.Range(0, characters.Count)];
            }
            #endif

            //// If no character data is assigned, we randomly pick one.
            //CharacterData[] characters = Resources.FindObjectsOfTypeAll<CharacterData>();
            //if (characters.Length > 0)
            //{
            //    return characters[Random.Range(0, characters.Length)];
            //}
        }
        Debug.LogWarning("null");
        return null;
    }

    public virtual void SelectCharacter(CharacterData character)
    {
        this.characterData = character;
    }

    public virtual void DestroySingleton()
    {
        instance = null;
        Destroy(gameObject);
    }
}
