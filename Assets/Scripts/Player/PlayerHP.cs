using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : SaiMonoBehaviour, IHPBarInterface
{
    public int currentHP = 100;
    int IHPBarInterface.HP()
    {
        return this.currentHP;
    }
}
