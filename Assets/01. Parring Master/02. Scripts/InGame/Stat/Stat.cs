using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum WeaponType
{
    Left,
    Right,
    TwoHand
}

[Serializable]
public struct Stat
{
    [Range(1, 3)]
    [Tooltip("소수점은 0.5 단위로 사용할 것")]
    public float healthPoint;
    [Range(0, 3)]
    public int parringPoint;

    [Range(0, 8)]
    public float moveSpeed;
    [Range(0, 10)]
    public float rotateSpeed;

    public void Equip(WeaponStat weaponStat)
    {
        healthPoint += weaponStat.increaseHealth;
        moveSpeed += weaponStat.inceraseMoveSpeed;
    }

    public void DropWeapon(WeaponStat weaponStat)
    {
        healthPoint -= weaponStat.increaseHealth;
        moveSpeed -= weaponStat.inceraseMoveSpeed;
    }
}

[Serializable]
public struct WeaponStat
{
    public WeaponType type;

    public int increaseHealth;
    public int inceraseMoveSpeed;

    public float damage;

    public float attackRange;
    public float attackSpeed;

    public float knockbackPower;
    public float knockbackSpeed;

    public bool ignoreWall;
}
