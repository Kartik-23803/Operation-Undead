using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera fpsCamera;
    [SerializeField] FirstPersonController fpscontroller;
    [SerializeField] float zoomedOut = 40f;
    [SerializeField] float zoomedIn = 20f;
    [SerializeField] float zoomedInSensitivity = 0.5f;
    [SerializeField] float zoomedOutSensitivity = 2f;
    [SerializeField] Sprite defaultCursor;
    [SerializeField] Sprite zoomCursor;
    GameObject gameOverCanvas;

    Image canvasImage;
    bool zoomToggle = false;


    void Awake()
    {
        gameOverCanvas = FindObjectOfType<Crosshair>().gameObject;
        if (gameOverCanvas != null)
        {
            canvasImage = gameOverCanvas.GetComponentInChildren<Image>();
        }
        else
        {
            Debug.LogWarning("Canvas object not found in the scene!");
        }
    }
    void OnEnable()
    {
        fpsCamera.m_Lens.FieldOfView = zoomedOut;
        canvasImage.sprite = defaultCursor;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if(canvasImage != null)
            {
                if(zoomToggle == false)
                {
                    ZoomIn();
                }
                else
                {
                    ZoomOut();
                }
            }
            else { return; }
        }
    }

    private void ZoomIn()
    {
        zoomToggle = true;
        fpsCamera.m_Lens.FieldOfView = zoomedIn;
        fpscontroller.RotationSpeed = zoomedInSensitivity;
        canvasImage.sprite = zoomCursor;
    }

    private void ZoomOut()
    {
        zoomToggle = false;
        fpsCamera.m_Lens.FieldOfView = zoomedOut;
        fpscontroller.RotationSpeed = zoomedOutSensitivity;
        canvasImage.sprite = defaultCursor;
    }
}