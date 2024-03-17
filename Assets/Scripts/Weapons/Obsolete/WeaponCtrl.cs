using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCtrl : SaiMonoBehaviour
{
    private static WeaponCtrl instance;
    public static WeaponCtrl Instance => instance;

    [SerializeField] public List<GameObject> listSkills = new List<GameObject>();
    [SerializeField] public List<GameObject> passiveItems = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogWarning("Only 1 WeaponCtrl allow to exits");
        instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadListSkills();
        this.LoadPassiveItems();
    }

    protected virtual void LoadListSkills()
    {
        if (this.listSkills.Count > 0) return;
        Transform listSkill = transform.Find("Skills");
        int childCount = listSkill.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform childTransform = listSkill.GetChild(i);

            GameObject childGameObject = childTransform.gameObject;
            this.listSkills.Add(childGameObject);
        }
        Debug.LogWarning(transform.name + ": LoadListSkills", gameObject);
    }

    protected virtual void LoadPassiveItems()
    {
        if (this.passiveItems.Count > 0) return;
        Transform passiveItems = transform.Find("Passive Items");
        int childCount = passiveItems.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform childTransform = passiveItems.GetChild(i);

            GameObject childGameObject = childTransform.gameObject;
            this.passiveItems.Add(childGameObject);
        }
        Debug.LogWarning(transform.name + ": LoadPassiveItems", gameObject);
    }
}
