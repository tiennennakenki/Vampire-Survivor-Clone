using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCtrl : SaiMonoBehaviour
{
    private static WeaponCtrl instance;
    public static WeaponCtrl Instance => instance;

    [SerializeField] public List<GameObject> listSkills = new List<GameObject>();

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
    }

    protected virtual void LoadListSkills()
    {
        if (this.listSkills != null) return;
        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform childTransform = transform.GetChild(i);

            GameObject childGameObject = childTransform.gameObject;
            this.listSkills.Add(childGameObject);
        }
        Debug.LogWarning(transform.name + ": LoadListSkills", gameObject);
    }

}
