using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    public Indicator indicatorPrefab;

    public List<Indicator> indicators = new List<Indicator>();

    public void Start()
    {
        CreatureHealth.OnDamageEventHandler += OnDamageEvent;
    }

    public void OnDamageEvent(CreatureDamageEvent e)
    {
        for(int index = 0; index < indicators.Count; index++)
        {
            try 
            {
                if (!indicators[index].gameObject.activeSelf)
                {
                    indicators[index].gameObject.SetActive(true);

                    TextMesh mesh = indicators[index].GetComponent<TextMesh>();
                    mesh.text = e.Damage.ToString();
                    mesh.color = Color.red;

                    break;
                }
            } 
            catch(MissingReferenceException) 
            { 
                indicators.RemoveAt(index);

                continue; 
            }
        }

        Indicator createIndicator = Instantiate(indicatorPrefab);
        createIndicator.transform.position = e.GetCreature.transform.position;

        TextMesh createMesh = createIndicator.GetComponent<TextMesh>();
        createMesh.text = e.Damage.ToString();
        createMesh.color = Color.red;

        createIndicator.StartCoroutine(createIndicator.Indicate());
    }
}
