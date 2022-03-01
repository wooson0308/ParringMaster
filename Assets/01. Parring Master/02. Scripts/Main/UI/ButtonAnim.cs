using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Warriors.Main
{
    public class ButtonAnim : MonoBehaviour
    {
        private RectTransform rectTransform;

        [SerializeField] private int index;
        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        private void OnEnable()
        {
            rectTransform = GetComponent<RectTransform>();

            StartCoroutine(CoSetEvent());
        }

        private void Update()
        {
            if(index == 1)
            {
                if (DOTween.IsTweening(GetInstanceID() + "Center Loop")) return;

                rectTransform.localScale = Vector3.one;

                rectTransform.DOScale(Vector3.one * .95f, .5f)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.OutQuad)
                    .SetId(GetInstanceID() + "Center Loop");
            }
            else if(DOTween.IsTweening(GetInstanceID() + "Center Loop"))
            {
                DOTween.Kill(GetInstanceID() + "Center Loop");
            }
        }

        private IEnumerator CoSetEvent()
        {
            yield return null;

            ButtonAnimManager.MoveEventHandler += MoveEvent;
        }


        private void MoveEvent(bool doIncrease)
        {
            ButtonAnimManager.Instance.DoMoveEvent(this, doIncrease);
        }

        public void Movement(Vector3 startPos, Vector3 endPos)
        {
            rectTransform.localPosition = startPos;
            rectTransform.DOLocalMove(endPos, .5f)
                .SetEase(Ease.OutQuad);
        }
    }
}


