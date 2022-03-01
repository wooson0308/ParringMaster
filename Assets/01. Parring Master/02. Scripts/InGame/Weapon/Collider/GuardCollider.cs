using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardCollider : WeaponCollider
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    protected override void OnCreatureEnter(Creature hitCreature)
    {
        base.OnCreatureEnter(hitCreature);
    }

    protected override void OnWeaponEnter(WeaponCollider hitWeapon)
    {
        base.OnWeaponEnter(hitWeapon);
        
        if (!hitWeapon.GetComponent<AttackCollider>()) return;

        creature.attackController.ParringPoint++;

        hitWeapon.animatorCtrl.Stun();

        Vector3 moveDir = creature.transform.position - hitWeapon.creature.transform.position;
        hitWeapon.creature.moveController.Movement(moveDir, weapon.stat.knockbackPower, weapon.stat.knockbackSpeed);
    }

    protected override void OnTileEnter()
    {
        base.OnTileEnter();
    }
}
