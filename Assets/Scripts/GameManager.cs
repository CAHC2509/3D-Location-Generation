using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Cameras settings")]
    [SerializeField] private GameObject fadePanel;
    [SerializeField] private KeyCode toggleCamerasKey = KeyCode.C;

    [Space, Header("Other settings")]
    [SerializeField] private KeyCode resetSceneKey = KeyCode.R;

    private void Awake() => Application.targetFrameRate = 60;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // If the key is pressed, toggle cameras using a fade transition
        if (Input.GetKeyDown(toggleCamerasKey) && !fadePanel.activeSelf)
            fadePanel.SetActive(true);

        // If the key is pressed, restart the scene
        if (Input.GetKeyDown(resetSceneKey))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void ToggleCameras() => PlayerController.Instance.ToggleCameras();
}
