using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public bool onMultipleDamage;

    private float damage = 0;

    public int maxCollidCreature = 1;
    private int currentCollidCreature = 0;

    private void Start()
    {
        StartCoroutine(TryAttack(1, new Vector3(1, 0.25f, 1), 1f));
    }

    private void TryReset()
    {
        StopAllCoroutines();
        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }

    public IEnumerator TryAttack(float damage, Vector3 maxSize, float speed)
    {
        this.damage = damage;

        while (true)
        {
            float x = transform.localScale.x >= maxSize.x ? 0 : speed;
            float y = transform.localScale.y >= maxSize.y ? 0 : speed;

            if (x == 0 && y == 0) break;

            Vector3 movePos = new Vector3(x, y, 1);

            transform.localScale += movePos * Time.deltaTime;

            yield return null;
        }

        transform.localScale = maxSize;

        yield return new WaitForEndOfFrame();

        TryReset();
    }

    private void AttackCreature(Creature target)
    {
        target.health.Hit(damage, null, CreatureDamageEvent.DamageType.Attack);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Creature>())
        {
            AttackCreature(collision.GetComponentInParent<Creature>());

            if (!onMultipleDamage) TryReset();
            if (++currentCollidCreature >= maxCollidCreature) TryReset();
        }
    }
}
