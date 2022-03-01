using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AttackCollider : WeaponCollider
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

        hitCreature.health.Hit(weapon.stat.damage, creature, CreatureDamageEvent.DamageType.Attack);

        Vector3 moveDir = creature.transform.position - hitCreature.transform.position;

        hitCreature.moveController.Movement(moveDir.normalized, weapon.stat.knockbackPower, weapon.stat.knockbackSpeed);
    }

    protected override void OnWeaponEnter(WeaponCollider hitWeapon)
    {
        base.OnWeaponEnter(hitWeapon);

        if (creature != null && hitWeapon.creature != null)
        {
            if (hitWeapon.creature.GetInstanceID().Equals(creature.GetInstanceID())) return;
        }

        hitWeapon.animatorCtrl.Cancel();
    }

    protected override void OnTileEnter()
    {
        base.OnTileEnter();

        animatorCtrl.Stun();
    }
}
