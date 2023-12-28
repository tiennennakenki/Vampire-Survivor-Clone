using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public abstract class Spawner : SaiMonoBehaviour
{
    [Header("Spawner")]
    [SerializeField] protected Transform holder;
    [SerializeField] protected List<Transform> poolObjs = new List<Transform>();
    [SerializeField] protected float currentCoolDown = 0;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadHolder();
    }

    protected virtual void LoadHolder()
    {
        if (this.holder != null) return;
        this.holder = transform.Find("Holder");
        Debug.Log(transform.name + ": LoadHolder", gameObject);
    }

    protected virtual void SpawnWeapon()
    {
        this.currentCoolDown -= Time.deltaTime;
        if (this.currentCoolDown <= 0)
        {
            this.Attack();
        }
    }

    public virtual Transform GetObjectFromPool(Transform prefab)
    {
        foreach (Transform poolObj in this.poolObjs)
        {
            if (poolObj.name == prefab.name)
            {
                poolObj.position = new Vector3(0,0,0);
                poolObj.rotation = Quaternion.identity;
                poolObj.localScale = Vector3.one;
                this.poolObjs.Remove(poolObj);
                return poolObj;
            }
        }

        Transform newPrefab = Instantiate(prefab);
        newPrefab.name = prefab.name;
        Debug.Log(newPrefab.name);
        return newPrefab;
    }

    public virtual void Despawn(Transform obj)
    {
        if (this.poolObjs.Contains(obj)) return;

        this.poolObjs.Add(obj);
        obj.gameObject.SetActive(false);
    }

    protected abstract void Attack();
}
