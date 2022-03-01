using UnityEngine;

public class EspadonSkill : WeaponSkill
{
    #region Passive

    protected override void Awake()
    {
        base.Awake();
    }

    public override void InitSetting(Creature _creature)
    {
        base.InitSetting(_creature);
    }

    public override void SetPassiveSkill(int level, bool value)
    {
        base.SetPassiveSkill(level, value);
    }

    protected override void PassiveLvOne()
    {
        base.PassiveLvOne();
    }

    protected override void PassiveLvTwo()
    {
        base.PassiveLvTwo();
    }

    protected override void PassiveLvThree()
    {
        base.PassiveLvThree();

        weapon.stat.ignoreWall = true;

    }

    protected override void PassiveOff(int level)
    {
        base.PassiveOff(level);

        if(level == 1)
        {

        }

        if(level == 2)
        {

        }

        if(level == 3)
        {
            weapon.stat.ignoreWall = false;
        }
    }

    #endregion

    #region Active

    public override void SetActiveSkill(Vector2 moveVec, int level)
    {
        base.SetActiveSkill(moveVec, level);
    }

    protected override void ActiveLvOne(Vector2 moveVec)
    {
        base.ActiveLvOne(moveVec);

        creature.moveController.Movement(-moveVec.normalized, 2f, 8f);
        creature.moveController.Rotate(moveVec, true);
    }

    protected override void ActiveLvTwo(Vector2 moveVec)
    {
        base.ActiveLvTwo(moveVec);

        creature.moveController.Movement(-moveVec.normalized, 2f, 8f);
        creature.moveController.Rotate(moveVec, true);
    }

    protected override void ActiveLvThree(Vector2 moveVec)
    {
        base.ActiveLvThree(moveVec);

        creature.moveController.Movement(-moveVec.normalized, 2f, 8f);
        creature.moveController.Rotate(moveVec, true);
    }

    #endregion
}
