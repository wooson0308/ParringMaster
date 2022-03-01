using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    public WeaponStat stat;
    public WeaponAnimation animatorController;
    public WeaponSkill skill;

    private WeaponCollider[] colliders;

    private void Awake()
    {
        colliders = GetComponentsInChildren<WeaponCollider>();
        skill = GetComponent<WeaponSkill>();
    }

    private void Start()
    {
        if (!GetComponentInParent<Creature>()) animatorController.LootMode(true);
    }

    public void PickUp(Creature creature)
    {
        WeaponManager.Instance.PickUp(this, creature);
        animatorController.LootMode(false);
        creature.AddWeapon(this);

        for (int index = 0; index < colliders.Length; index++)
        {
            colliders[index].SetCreature(creature);
        }

        skill.InitSetting(creature);
    }

    public void Drop()
    {
        if (GetComponentInParent<Creature>() == null) return;

        Transform dropParent = WeaponManager.Instance.GetDropWeaponParent();
        dropParent.localPosition = GetComponentInParent<Creature>().transform.position;
        transform.parent = dropParent;
        animatorController.LootMode(true);

        for (int index = 0; index < colliders.Length; index++)
        {
            colliders[index].SetCreature(null);
        }
    }
}
