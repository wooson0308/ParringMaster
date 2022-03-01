using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public float limitPadding;

    private static MapManager instance;
    public static MapManager Instance { get { return instance; } }

    private SpriteRenderer mapRenderer;
    private LimitArea limitArea;

    void Awake()
    {
        if (instance)
        {
            Debug.LogError("Error: already instance class. MapManager.cs");
            return;
        }

        instance = this;
    }

    public void SetMapRenderer(SpriteRenderer _sprite)
    {
        mapRenderer = _sprite;
        SetLimitArea();
    }

    public void SetLimitArea()
    {
        Vector2 min = mapRenderer.bounds.min;
        Vector2 max = mapRenderer.bounds.max;

        limitArea = new LimitArea(min, max);
    }

    public static LimitArea LimitArea
    {
        get { return instance.limitArea; }
    }
}