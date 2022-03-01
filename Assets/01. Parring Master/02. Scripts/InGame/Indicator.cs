using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public float maxY = 0.2f;
    public float speed;

    public IEnumerator Indicate()
    {
        float y = transform.position.y;
        while (transform.position.y < y + maxY)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            yield return null;
        }

        transform.position = Vector3.right * transform.position.x + Vector3.up * maxY;
        gameObject.SetActive(false);
    }
}
