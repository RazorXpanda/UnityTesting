using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private Transform player;
    Vector3 cameraPosition;

    private void Start()
    {
        mainCam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        cameraPosition = player.position + (mainCam.ScreenToWorldPoint(Input.mousePosition) - player.position) / 4;
        cameraPosition.z = 0f;

        LeanTween.move(this.gameObject, cameraPosition, .075f);
    }
}
