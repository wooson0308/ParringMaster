using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSkill : MonoBehaviour
{
    public enum Type
    {
        ACTIVE,
        PASSIVE
    }

    protected Weapon weapon;
    protected Creature creature;

    virtual protected void Awake()
    {
        weapon = GetComponent<Weapon>();
    }

    virtual public void InitSetting(Creature _creature)
    {
        creature = _creature;
    }

    #region Passive

    virtual public void SetPassiveSkill(int level, bool value)
    {
        if (!value)
        {
            PassiveOff(level);

            return;
        }

        switch (level)
        {
            case 1: 
                PassiveLvOne();
                break;
            case 2:
                PassiveLvTwo();
                break;
            case 3:
                PassiveLvThree();
                break;
        }
    }

    virtual protected void PassiveLvOne()
    {

    }

    virtual protected void PassiveLvTwo()
    {

    }

    virtual protected void PassiveLvThree()
    {

    }

    virtual protected void PassiveOff(int level)
    {
        
    }

    #endregion

    #region Active

    virtual public void SetActiveSkill(Vector2 moveVec, int level)
    {
        switch (level)
        {
            case 1:
                ActiveLvOne(moveVec);
                break;
            case 2:
                ActiveLvTwo(moveVec);
                break;
            case 3:
                ActiveLvThree(moveVec);
                break;
        }
    }

    virtual protected void ActiveLvOne(Vector2 moveVec)
    {

    }

    virtual protected void ActiveLvTwo(Vector2 moveVec)
    {

    }

    virtual protected void ActiveLvThree(Vector2 moveVec)
    {

    }

    #endregion
}
