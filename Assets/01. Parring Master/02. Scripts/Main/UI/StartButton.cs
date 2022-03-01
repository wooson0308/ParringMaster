using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<RectTransform>().DOScale(Vector3.one * 1.2f, 0.5f)
            .SetEase(Ease.OutQuad)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
