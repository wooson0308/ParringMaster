using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CreatureMove : MonoBehaviour
{
    public Creature creature;

    private float currentMoveSpeed;

    private float MoveSpeed
    {
        get
        {
            return creature.stat.moveSpeed;
        }
    }

    private float RotateSpeed
    {
        get
        {
            return creature.stat.rotateSpeed;
        }
    }

    private void Awake()
    {
        if (creature == null)
        {
            creature = GetComponent<Creature>();
        }
    }

    public void Movement(Vector2 moveVec)
    {
        if (doMove) return;

        currentMoveSpeed = MoveSpeed;

        if (creature.weapons != null)
        {
            if (creature.RunningWeaponsMotion()) currentMoveSpeed = 2;

            for (int index = 0; index < creature.weapons.Count; index++)
            {
                if (creature.weapons[index] == null) continue;
                creature.weapons[index].animatorController.MoveAnim(moveVec, currentMoveSpeed);
            }
        }

        if (moveVec.Equals(Vector2.zero))
        {
            creature.animator.SetBool("Movement", false);
            return;
        }
        else
        {
            creature.animator.SetBool("Movement", true);
            creature.animator.SetFloat("Move Anim Speed", currentMoveSpeed);
        }

        moveVec.Normalize();
        moveVec *= currentMoveSpeed * Time.fixedDeltaTime;

        transform.position += (Vector3)moveVec;
    }

    public void Movement(Vector3 moveDir, float _movePower, float _moveSpeed)
    {
        if (!gameObject.activeSelf) return;
        StartCoroutine(MovementCoroutine(moveDir, _movePower, _moveSpeed));
    }

    bool doMove;
    public void ResetKnockback()
    {
        doMove = false;
    }

    bool moveHitTile;

    private IEnumerator MovementCoroutine(Vector3 moveDir, float _movePower, float _moveSpeed)
    {
        Vector3 movePos = transform.position - moveDir * _movePower;

        if (doMove) yield break;
        doMove = true;

        while (Vector3.Distance(transform.position, movePos) > 0.1)
        {
            //transform.position -= dir * knockbackSpeed * Time.deltaTime;
            if(moveHitTile)
            {
                moveHitTile = false;
                movePos = transform.position;
                break;
            }

            transform.position = Vector3.Lerp(transform.position, movePos, _moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = movePos;

        doMove = false;
    }

    public void Rotate(Vector3 rotateVector)
    {
        Rotate(rotateVector, false);
    }

    public void Rotate(Vector3 rotateVector, bool isLook)
    {
        if (rotateVector.Equals(Vector3.zero)) return;
        if (!isLook && doMove) return;

        float angle = -1 * Mathf.Rad2Deg * Mathf.Atan2(rotateVector.x, rotateVector.y) + 180;

        Quaternion newRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = isLook ? 
            Quaternion.RotateTowards(transform.rotation, newRotation, RotateSpeed) :
            Quaternion.Euler(newRotation.eulerAngles);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(doMove && collision.gameObject.GetComponent<TilemapCollider2D>())
        {
            moveHitTile = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (doMove && collision.gameObject.GetComponent<TilemapCollider2D>())
        {
            moveHitTile = true;
        }
    }
}
