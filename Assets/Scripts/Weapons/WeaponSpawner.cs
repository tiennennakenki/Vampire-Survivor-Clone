using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : Spawner
{
    private static WeaponSpawner instance;
    public static WeaponSpawner Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogWarning("Only 1 WeaponCtrl allow to exits");
        instance = this;
    }

    //protected override void LoadComponents()
    //{
    //    base.LoadComponents();
    //    this.LoadSkills();
    //}

    //protected virtual void LoadSkills()
    //{
    //    if (this.skills.Count > 0) return;
    //    Transform listSkill = transform.Find("Skills");
    //    int childCount = listSkill.childCount;

    //    for (int i = 0; i < childCount; i++)
    //    {
    //        Transform childTransform = listSkill.GetChild(i);

    //        GameObject childGameObject = childTransform.gameObject;
    //        this.skills.Add(childGameObject);
    //    }
    //    Debug.LogWarning(transform.name + ": LoadSkills", gameObject);
    //}
}
