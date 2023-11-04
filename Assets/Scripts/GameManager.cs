using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Cameras settings")]
    [SerializeField] private GameObject fadePanel;
    [SerializeField] private KeyCode toggleCamerasKey = KeyCode.C;

    private void Awake() => Application.targetFrameRate = 60;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleCamerasKey) && !fadePanel.activeSelf)
            fadePanel.SetActive(true);
    }

    public void ToggleCameras() => PlayerController.Instance.ToggleCameras();
}
