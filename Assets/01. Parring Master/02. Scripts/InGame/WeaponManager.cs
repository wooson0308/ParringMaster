using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private static WeaponManager instance;
    public static WeaponManager Instance
    {
        get { return instance; }
    }

    public Transform dropWeaponStorage;
    public List<Transform> dropWeaponPool = new List<Transform>(); 

    private void Awake()
    {
        if (instance) return;
        instance = this;

    }

    public Transform GetDropWeaponParent()
    {
        for(int index = 0; index < dropWeaponPool.Count; index++)
        {
            if (!dropWeaponPool[index].gameObject.activeSelf)
            {
                dropWeaponPool[index].gameObject.SetActive(true);
                return dropWeaponPool[index];
            }
        }

        Transform dropWeaponParent = Instantiate(new GameObject(), dropWeaponStorage).transform;
        dropWeaponParent.name = "Drop Weapon";

        dropWeaponPool.Add(dropWeaponParent);

        return dropWeaponParent;
    }

    public void PickUp(Weapon weapon, Creature creature)
    {
        for (int index = 0; index < dropWeaponPool.Count; index++)
        {
            if (dropWeaponPool[index].childCount < 1) continue;
            Weapon dropWeapon = dropWeaponPool[index].GetComponentInChildren<Weapon>();
            if (weapon.GetInstanceID().Equals(dropWeapon.GetInstanceID()))
            {
                dropWeapon.transform.parent = creature.transform;
                dropWeaponPool[index].gameObject.SetActive(false);
                return;
            }
        }

        weapon.transform.parent = creature.transform;
    }
}
