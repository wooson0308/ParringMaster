using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureEvent : Event
{
    private Creature creature;

    public CreatureEvent(Creature creature)
    {
        this.creature = creature;
    }

    public Creature GetCreature
    {
        get { return creature; }
    }
}

public class CreatureDamageEvent : CreatureEvent
{
    public enum DamageType
    {
        Attack,
        Fall,
        Self
    }

    private Creature attacker;
    private float damage;
    private DamageType damageType;

    public CreatureDamageEvent(Creature creature, Creature attacker, float damage, DamageType damageType) : base(creature)
    {
        this.attacker = attacker;
        this.damage = damage;
        this.damageType = damageType;
    }

    public float Damage
    {
        get { return damage; }
    }

    public DamageType Type
    {
        get { return damageType; }
    }

    public Creature Attacker
    {
        get { return attacker; }
    }
}
