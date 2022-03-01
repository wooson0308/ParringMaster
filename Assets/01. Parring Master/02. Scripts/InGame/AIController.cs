using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private Creature actor;
    public Creature Actor 
    {
        get { return actor; }
    }

    public Transform target;

    public float runawayRange;

    private void Start()
    {
        actor = GetComponent<Creature>();
    }

    private void FixedUpdate()
    {
        FSM();
    }

    private void FSM()
    {
        if (target == null) return;
        if (!target.gameObject.activeSelf) return;

        float distance = Vector2.Distance(transform.position, target.position);
        Vector2 dir = target.position - transform.position;

        actor.moveController.Rotate(dir);

        List<Weapon> attackWeapons = actor.attackController.CanAttackWeapon(distance);

        for(int index = 0; index < attackWeapons.Count; index++)
        {
            if (distance > runawayRange)
            {
                // TODO : ATTACK ROUTINE
                #region ATTACK ROUTINE - WooSon
                actor.moveController.Movement(Vector2.zero);
                actor.attackController.NormalAttack(attackWeapons[index], true);
                #endregion
            }
            else
            {
                // TODO : RUNAWY ROUTINE
                actor.attackController.NormalAttack(attackWeapons[index], false);
                actor.moveController.Movement(-dir);
            }
        }

        if (attackWeapons.Count <= 0)
        {
            // TODO : CHASE ROUTINE
            actor.attackController.NormalAttack(false);
            actor.moveController.Movement(dir);
        }
    }

    private void OnDisable()
    {
        RoomManager.Instance.DoorControl(DoorType.Open);
    }
}
