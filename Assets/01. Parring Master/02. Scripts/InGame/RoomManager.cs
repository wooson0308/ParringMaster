using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;



public class RoomManager : MonoBehaviour
{
    public delegate void MoveRoomEvent(string roomName);
    public static event MoveRoomEvent MoveRoomEventHandler;

    private static RoomManager instance;
    public static RoomManager Instance
    {
        get { return instance; }
    }

    [Tooltip("테스트 여부는 Awake() 기준으로 확인합니다.")] public bool onTest;
    public GameObject limitTestCanvas;
    public Button lobyTestButton;
    public Button ArenaTestButton;

    [Space]

    public List<Room> rooms;

    [Space]

    public CameraSetting cameraSetting;

    [Space]

    public AIController enemy;
    public Transform enemySpawnPoint;
    public List<Weapon> enemyWeaponPrefabs = new List<Weapon>();
    private int weaponIndex = 0;

    [Space]

    public Image fadeImage;
    public float fadeSpeed;

    private string currentRoomName = "Lobby";
    private Room currentRoom;

    private void Awake()
    {
        if (instance) { return; }
        instance = this;

        if (!onTest)
        {
            limitTestCanvas.SetActive(false);
            return;
        }

        limitTestCanvas.SetActive(true);

        //lobyTestButton.onClick.AddListener(() => ChangeRoom("Lobby"));
        //ArenaTestButton.onClick.AddListener(() => ChangeRoom("Arena"));

        MoveRoomEventHandler = delegate { };
    }

    private void Start()
    {
        StartCoroutine(CoStartEnable());
        IEnumerator CoStartEnable()
        {
            yield return null;

            ChangeRoom(rooms[0].roomName);
            DoorControl(DoorType.Lock);
        }
    }

    public void OpenDoor()
    {
        currentRoom.DoorControl(DoorType.Open);
    }

    public void CloseDoor()
    {
        currentRoom.DoorControl(DoorType.Close);
    }

    public void LockDoor()
    {
        currentRoom.DoorControl(DoorType.Lock);
    }

    public void DoorControl(DoorType doorType)
    {
        currentRoom.DoorControl(doorType);
    }

    public void ChangeRoom(string roomName)
    {
        Room findRoom = SearchRoom(roomName);

        if(findRoom == null)
        {
            Debug.LogError("Can't find Room. Plz check rooms in RoomManager inspector.");
            return;
        }

        findRoom.SetMapRenderer();
        cameraSetting.SetupLimitArea();

        currentRoomName = roomName;
        currentRoom = findRoom;

        MoveRoomEventHandler(roomName);

        switch (findRoom.type)
        {
            case RoomType.Lobby:
                DoorControl(DoorType.Open);
                break;
            case RoomType.Arena:
                DoorControl(DoorType.Lock);
                SpawnEnemy();
                break;
        }
    }

    public IEnumerator FadeInOut()
    {
        StartCoroutine(FadeIn());

        while(fadeImage.color != Color.black)
        {
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(FadeOut());
    }
    
    public IEnumerator FadeOut()
    {
        Color currentColor = Color.black;

        while(fadeImage.color.a > 0)
        {
            currentColor.a -= fadeSpeed * Time.deltaTime;
            fadeImage.color = currentColor;
            yield return null;
        }

        fadeImage.color = Color.clear;
    }

    public IEnumerator FadeIn()
    {
        Color currentColor = Color.clear;

        while (fadeImage.color.a < 1)
        {
            currentColor.a += fadeSpeed * Time.deltaTime;
            fadeImage.color = currentColor;
            yield return null;
        }

        fadeImage.color = Color.black;
    }

    public void SpawnEnemy()
    {
        enemy.transform.position = enemySpawnPoint.position;

        enemy.gameObject.SetActive(true);
        StartCoroutine(ReciveCoroutine());
    }
    private IEnumerator ReciveCoroutine()
    {
        yield return new WaitForEndOfFrame();
        enemy.Actor.health.Revive();

        if (weaponIndex >= enemyWeaponPrefabs.Count) weaponIndex = 0;

        Weapon spawnWeapon = Instantiate(enemyWeaponPrefabs[weaponIndex++]);
        spawnWeapon.PickUp(enemy.Actor);
    }

    public void TryMoveRoom(string roomName)
    {
        StartCoroutine(MoveRoomCoroutine(roomName));
    }

    private IEnumerator MoveRoomCoroutine(string roomName)
    {
        DoorControl(DoorType.Close);
        StartCoroutine(FadeInOut());

        while (fadeImage.color != Color.black)
        {
            yield return null;
        }

        
        ChangeRoom(roomName);
    }

    private Room SearchRoom(string roomName)
    {
        foreach (Room room in rooms)
        {
            if (room.roomName.Equals(roomName))
            {
                return room;
            }
        }

        return null;
    }
}
