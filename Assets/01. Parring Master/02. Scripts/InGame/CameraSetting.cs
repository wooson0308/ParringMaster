using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraSetting : MonoBehaviour {

    private const float ZOOM_IN_SIZE = 20;
    private const float ZOOM_OUT_SIZE = 6;

	public Creature target;

    private Animator animator;

	new private Camera camera;
    private LimitArea limitArea;

    private Vector3 position;

    private float fixedZAxis;

    private float followSpeed;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

	private void FixedUpdate () {
        try
        {
            fixedZAxis = transform.position.z;

            if (target == null) return;

            float playerSpeed = target.stat.moveSpeed;

            if (playerSpeed >= 3)
                followSpeed = playerSpeed - 2;
            else
                followSpeed = playerSpeed;

            position = limitArea.Clamp(Vector3.Lerp(transform.position, target.transform.position, followSpeed * Time.fixedDeltaTime));

            position.z = fixedZAxis;

            transform.position = position;
        }
        catch (MissingReferenceException)
        {
            Debug.LogError("Missing Ref MapManager.. Plz Set up LimitArea");
        }
        catch (NullReferenceException)
        {
            Debug.LogError("Null Ref MapManager.. Plz Set up LimitArea");
        }
    }

    public void SetupLimitArea()
    {
        camera = GetComponent<Camera>();

        if (!camera.orthographic)
        {
            Debug.LogError("Error: main camera is not orthographic.");
            return;
        }

        float cameraHeight = camera.orthographicSize;
        float cameraWidth = cameraHeight * camera.aspect;

        limitArea = MapManager.LimitArea.AddMargin(cameraHeight, cameraWidth);
    }

    public void MoveTarget()
    {
        Vector3 movePos = target.transform.position;
        movePos.z = transform.position.z;

        transform.position = movePos;
    }
}

