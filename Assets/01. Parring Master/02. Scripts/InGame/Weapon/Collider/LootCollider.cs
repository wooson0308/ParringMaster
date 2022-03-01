using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootCollider : WeaponCollider
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

        if (!hitCreature.GetComponentInParent<PlayerController>()) return;

        hitCreature.GetComponentInParent<PlayerController>().ActiveLootButton(weapon, true);
    }

    protected override void OnCreatureExit(Creature hitCreature)
    {
        base.OnCreatureExit(hitCreature);

        if (!hitCreature.GetComponent<PlayerController>()) return;

        hitCreature.GetComponentInParent<PlayerController>().ActiveLootButton(weapon, false);
    }

    protected override void OnWeaponEnter(WeaponCollider hitWeapon)
    {
        base.OnWeaponEnter(hitWeapon);
    }

    protected override void OnTileEnter()
    {
        base.OnTileEnter();
    }
}
