using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Warriors.Game
{
    public class Chest : MonoBehaviour
    {
        private bool isOpen;

        [SerializeField] private SpriteRenderer m_Renderer;

        [Space]

        [SerializeField] private Sprite openChesetSprite;
        [SerializeField] private Sprite closeChestSprite;

        [SerializeField] private GameObject dropWeaponPrefab;

        public UnityEvent openEvent;

        private void OnEnable()
        {
            StartCoroutine(CoDelayEnable());
            IEnumerator CoDelayEnable()
            {
                yield return null;

                RoomManager.MoveRoomEventHandler += MoveRoomEvent;
            }
        }

        private void MoveRoomEvent(string roomName)
        {
            if (!roomName.Equals("Lobby"))
            {
                m_Renderer.sprite = closeChestSprite;
                isOpen = false;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("Player") && !isOpen)
            {
                m_Renderer.sprite = openChesetSprite;
                isOpen = true;

                Instantiate(dropWeaponPrefab).transform.localPosition = transform.localPosition + Vector3.down;

                openEvent.Invoke();
            }
        }
    }

}

