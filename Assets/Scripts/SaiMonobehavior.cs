using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaiMonoBehaviour : MonoBehaviour
{
    protected virtual void Reset()
    {
        this.LoadComponents();
        this.ResetValue();
    }

    protected virtual void Awake()
    {
        this.LoadComponents();
    }

    protected virtual void LoadComponents()
    {
        // For override
    }

    protected virtual void Start()
    {
        //
    }

    protected virtual void Update()
    {
        //
    }

    protected virtual void FixedUpdate()
    {
        //
    }

    protected virtual void ResetValue()
    {
        // For override
    }

    protected virtual void OnEnable()
    {
        // For override
    }

    protected virtual void OnDisable()
    {
        // For override
    }


}
