using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    Lobby,
    Arena
}

public class Room : MonoBehaviour
{
    public string roomName;
    public RoomType type;
    public SpriteRenderer limitArea;
    [SerializeField]
    public List<Door> doors;

    public void SetMapRenderer()
    {
        MapManager.Instance.SetMapRenderer(limitArea);
    }

    public void DisactiveDoors()
    {
        for (int index = 0; index < doors.Count; index++)
        {
            doors[index].SetActive(false);
        }
    }

    public void DoorControl(DoorType doorType)
    {
        DisactiveDoors();

        for (int index = 0; index < doors.Count; index++)
        {
            if (doors[index].type == doorType)
            {
                doors[index].SetActive(true);
                break;
            }
        }
    }
}
