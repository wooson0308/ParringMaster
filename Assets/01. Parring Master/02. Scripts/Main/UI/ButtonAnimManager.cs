using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Warriors.Main
{
    public class ButtonAnimManager : MonoBehaviour
    {
        public delegate void MoveEvent(bool doIncrease);
        public static event MoveEvent MoveEventHandler;

        public static ButtonAnimManager Instance { get; set; }

        [SerializeField] private List<ButtonAnim> animButtonList;
        [SerializeField] private List<RectTransform> movePointList;

        [Space]

        [SerializeField] private Button leftMoveButton;
        [SerializeField] private Button rightMoveButton;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }

            MoveEventHandler = delegate { };

            leftMoveButton.onClick.AddListener(() => MoveEventHandler(false));
            rightMoveButton.onClick.AddListener(() => MoveEventHandler(true));
        }

        public void DoMoveEvent(ButtonAnim button, bool doIncrease)
        {
            int nextIndex = doIncrease ? IncreaseIndex(button.Index) : DecreaseIndex(button.Index);

            Vector3 startPos = movePointList[button.Index].localPosition;
            Vector3 endPos = movePointList[nextIndex].localPosition;

            button.Movement(startPos, endPos);

            button.Index = doIncrease ? IncreaseIndex(button.Index) : DecreaseIndex(button.Index);
        }

        private int IncreaseIndex(int index)
        {
            return SetIndex(index, true);
        }

        private int DecreaseIndex(int index)
        {
            return SetIndex(index, false);
        }

        private int SetIndex(int index, bool doIncrease)
        {
            int returnIndex = index;

            returnIndex = doIncrease ? returnIndex + 1 : returnIndex - 1;

            if (returnIndex < 0) returnIndex = movePointList.Count - 1;
            else if (returnIndex >= movePointList.Count) returnIndex = 0;

            return returnIndex;
        }
    }
}


