using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DoorType
{
    Open,
    Close,
    Lock
}

[Serializable]
public class Door : MonoBehaviour
{
    public GameObject parent;
    public DoorType type;
    public Transform movPoint;
    public string moveRoomName;

    public void SetActive(bool active)
    {
        try { parent.SetActive(active); }
        catch (MissingReferenceException) { }  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (type == DoorType.Close) return;
        
        if (collision.GetComponentInParent<PlayerController>())
        {
            if (collision.GetComponent<WeaponCollider>()) return;
            
            PlayerController player = collision.GetComponentInParent<PlayerController>();
            player.transform.position = movPoint.position;
            RoomManager.Instance.TryMoveRoom(moveRoomName);
        }
    }
}
