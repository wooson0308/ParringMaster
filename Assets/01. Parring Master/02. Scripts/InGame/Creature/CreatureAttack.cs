using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureAttack : MonoBehaviour
{
    public Creature creature;

    // 패링 포인트
    private int currentPPoint;

    public int ParringPoint
    {
        get { return currentPPoint; }
        set 
        {
            int tempPoint = currentPPoint;

            currentPPoint = value;

            if (currentPPoint > 3) currentPPoint = 3;

            if (currentPPoint <= 0) UsePassiveSkill(false, tempPoint);
            else UsePassiveSkill(true);

            if(currentPPoint > 0) creature.SetParringPoint(currentPPoint);

            if (GetComponent<PlayerController>())
            {
                GetComponent<PlayerController>().SetPPointUI(currentPPoint);
            }
        }
    }

    private void Awake()
    {
        if(creature == null)
        {
            creature = GetComponent<Creature>();
        }
    }

    private void Start()
    {
        ParringPoint = creature.stat.parringPoint;
    }

    public List<Weapon> CanAttackWeapon(float range)
    {
        List<Weapon> attackWeapons = new List<Weapon>();

        for (int index = 0; index < creature.weapons.Count; index++)
        {
            float currentRange = creature.weapons[index].stat.attackRange;
            if (range <= currentRange) attackWeapons.Add(creature.weapons[index]);
        }

        return attackWeapons;
    }

    public void NormalAttack(Weapon wepaon, bool value)
    {
        wepaon.animatorController.OnAttack(value);
        if (value) wepaon.animatorController.RunningMotion = true;
    }

    public void NormalAttack(Weapon wepaon)
    {
        wepaon.animatorController.Attack();
        wepaon.animatorController.RunningMotion = true;
    }

    public void NormalAttack(bool value)
    {
        for (int index = 0; index < creature.weapons.Count; index++)
        {
            if (creature.weapons[index] == null) continue;
            creature.weapons[index].animatorController.OnAttack(value);
            if (value) creature.weapons[index].animatorController.RunningMotion = true;
        }
    }

    public void NormalAttack()
    {
        for (int index = 0; index < creature.weapons.Count; index++)
        {
            if (creature.weapons[index] == null) continue;
            creature.weapons[index].animatorController.Attack();
            creature.weapons[index].animatorController.RunningMotion = true;
        }
    }

    public void Parry()
    {
        for (int index = 0; index < creature.weapons.Count; index++)
        {
            if (creature.weapons[index] == null) continue;
            creature.weapons[index].animatorController.Guard();
            creature.weapons[index].animatorController.RunningMotion = true;
        }
    }

    public void Cancel()
    {
        for (int index = 0; index < creature.weapons.Count; index++)
        {
            if (creature.weapons[index] == null) continue;
            creature.weapons[index].animatorController.Cancel();
        }
    }

    private void UsePassiveSkill(bool value, int tempPoint)
    {
        for (int index = 0; index < creature.weapons.Count; index++)
        {
            Weapon currentWeapon = creature.weapons[index];
            currentWeapon.skill.SetPassiveSkill(tempPoint, value);
        }
    }

    private void UsePassiveSkill(bool value)
    {
        for (int index = 0; index < creature.weapons.Count; index++)
        {
            Weapon currentWeapon = creature.weapons[index];
            currentWeapon.skill.SetPassiveSkill(currentPPoint, value);
        }
    }

    public void UseSkill(Vector2 moveDir, Weapon _weapon)
    {
        if (ParringPoint <= 0) return;
        _weapon.skill.SetActiveSkill(moveDir, currentPPoint);
        _weapon.animatorController.Skill();
        ParringPoint = 0;

        _weapon.animatorController.RunningMotion = true;
    }
}
