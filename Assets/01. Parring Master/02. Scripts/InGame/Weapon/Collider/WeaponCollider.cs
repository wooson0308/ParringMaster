using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeaponCollider : MonoBehaviour
{
    public Creature creature;
    public WeaponAnimation animatorCtrl;
    public Weapon weapon;

    virtual protected void Start()
    {
        InitSetup();
    }

    public void InitSetup()
    {
        creature = GetComponentInParent<Creature>();
        weapon = GetComponentInParent<Weapon>();
        animatorCtrl = transform.GetComponentInParent<WeaponAnimation>();
    }

    public void SetCreature(Creature _creature)
    {
        creature = _creature;
    }

    virtual protected void Update()
    {
        transform.localPosition = Vector3.zero;
    }

    virtual protected void OnTriggerEnter2D(Collider2D collision)
    {
        // 체크 순서 : 타일 > 무기 (가드) > 크리쳐

        if (collision.GetComponent<TilemapCollider2D>() && !weapon.stat.ignoreWall)
        {
            OnTileEnter();
        }

        else if (collision.GetComponent<WeaponCollider>())
        {
            WeaponCollider hitWeapon = collision.GetComponent<WeaponCollider>();
            OnWeaponEnter(hitWeapon);
        }

        else if (collision.GetComponentInParent<Creature>())
        {
            Creature hitCreature = collision.GetComponentInParent<Creature>();
            if(creature != null)
            {
                if (hitCreature.GetInstanceID().Equals(creature.GetInstanceID())) return;
            }

            OnCreatureEnter(hitCreature);
        }
    }

    virtual protected void OnCreatureEnter(Creature hitCreature)
    {

    }

    virtual protected void OnWeaponEnter(WeaponCollider hitWeapon)
    {

    }

    virtual protected void OnTileEnter()
    {

    }

    virtual protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<TilemapCollider2D>())
        {
            OnTileExit();
        }

        else if (collision.GetComponent<WeaponCollider>())
        {
            WeaponCollider hitWeapon = collision.GetComponent<WeaponCollider>();
            OnWeaponExit(hitWeapon);
        }

        else if (collision.GetComponentInParent<Creature>())
        {
            Creature hitCreature = collision.GetComponentInParent<Creature>();
            if (creature != null)
            {
                if (hitCreature.GetInstanceID().Equals(creature.GetInstanceID())) return;
            }

            OnCreatureExit(hitCreature);
        }
    }

    virtual protected void OnCreatureExit(Creature hitCreature)
    {

    }

    virtual protected void OnWeaponExit(WeaponCollider hitWeapon)
    {

    }

    virtual protected void OnTileExit()
    {

    }
}
