using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    [SerializeField]
    public Stat stat;

    public Animator animator;

    public List<Weapon> weapons;

    public CreatureMove moveController;
    public CreatureAttack attackController;

    public CreatureHealth health;

    private void Start()
    {
        moveController = GetComponent<CreatureMove>();
        attackController = GetComponent<CreatureAttack>();
        health = GetComponent<CreatureHealth>();
    }

    public bool RunningWeaponsMotion()
    {
        if (weapons.Count <= 0) return false;

        for(int index = 0; index < weapons.Count; index++)
        {
            if (weapons[index].animatorController.RunningMotion) return true;
        }

        return false;
    }

    public void PickupAndDrop(Weapon _weapon)
    {
        if(_weapon.stat.type == WeaponType.TwoHand)
        {
            DropWeapons();
        } 

        else
        {
            for (int index = 0; index < weapons.Count; index++)
            {
                if (weapons[index] == null) continue;
                if (weapons[index].stat.type == WeaponType.TwoHand)
                {
                    stat.DropWeapon(weapons[index].stat);
                    weapons[index].Drop();
                    weapons.RemoveAt(index);
                    continue;
                }
                if (_weapon.stat.type == weapons[index].stat.type)
                {
                    stat.DropWeapon(weapons[index].stat);
                    weapons[index].Drop();
                    weapons.RemoveAt(index);
                }
            }
        }

        stat.Equip(_weapon.stat);
        _weapon.PickUp(this);
    }

    /// <summary>
    /// Test Function
    /// </summary>
    public void DropWeapons()
    {
        for (int index = 0; index < weapons.Count; index++)
        {
            stat.DropWeapon(weapons[index].stat);
            weapons[index].Drop();
        }

        weapons.Clear();
    }

    public void SetParringPoint(int parringPoint)
    {
        for(int index = 0; index < weapons.Count; index++)
        {
            weapons[index].animatorController.SetParringPoint(parringPoint);
        }
    }


    public void AddWeapon(Weapon _weaponPrefab)
    {
        weapons.Add(_weaponPrefab);
    }
}
