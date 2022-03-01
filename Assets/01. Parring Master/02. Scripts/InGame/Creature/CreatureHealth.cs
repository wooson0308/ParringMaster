using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureHealth : MonoBehaviour
{
    public delegate void OnDamageEvent(CreatureDamageEvent e);
    public static event OnDamageEvent OnDamageEventHandler;

    public Creature creature;

    private float HealthPoint
    {
        get { return creature.stat.healthPoint; }
    }

    private float currentHealth;

    private void SetHealth(float health)
    {
        currentHealth = health;

        if (GetComponent<PlayerController>())
        {
            GetComponent<PlayerController>().SetHealthUI(currentHealth);
        }

        if(currentHealth <= 0)
        {
            Death();
        }
    }

    private void Awake()
    {
        if (creature == null)
        {
            creature = GetComponent<Creature>();
        }
    }

    private void Start()
    {
        SetHealth(3);
    }

    private bool canHit = true;

    public void Hit(float damage, Creature attacker, CreatureDamageEvent.DamageType damageType)
    {
        if (!canHit) return;

        CreatureDamageEvent damageEvent = new CreatureDamageEvent(creature, attacker, damage, damageType);
        OnDamageEventHandler(damageEvent);

        SetHealth(currentHealth - damage);

        if (currentHealth <= 0) return;
        creature.animator.SetTrigger("Hit");
        StartCoroutine(DamageDelayCoroutine());
    }

    private IEnumerator DamageDelayCoroutine()
    {
        if (!canHit) yield break;

        canHit = false;
        yield return new WaitForSeconds(Constants.HIT_DELAY);
        canHit = true;
    }

    public void Death()
    {
        if (GetComponent<PlayerController>()) SceneChanger.LoadScene("Main");
        // TODO : 플레이어 죽음
        StartCoroutine(DeathCoroutine());
    }

    private IEnumerator DeathCoroutine()
    {
        creature.DropWeapons();
        yield return new WaitForEndOfFrame();
        gameObject.SetActive(false);
    }

    public void Revive()
    {
        currentHealth = HealthPoint;
        creature.moveController.ResetKnockback();
    }

    private void OnDisable()
    {
        OnDamageEventHandler = delegate { };
    }
}
