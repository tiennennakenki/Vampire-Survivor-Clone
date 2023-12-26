using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicBehaviour : MeleeWeaponBehaviour
{
    public GarlicCtrl garlicCtrl;

    protected override void Start()
    {
        base.Start();
        garlicCtrl = FindObjectOfType<GarlicCtrl>();
    }

    protected override void Update()
    {
        base.Update();
    }
}
