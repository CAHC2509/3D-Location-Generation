using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsText;
    [SerializeField] private float updateInterval = 0.5f; // Update interval in seconds

    private float accum = 0.0f; // Accumulator for FPS calculation
    private int frames = 0; // Number of frames counted
    private float timeleft; // Time left until the next update

    private void Start() => timeleft = updateInterval;

    private void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        frames++;

        // Update the FPS text if the time interval has passed
        if (timeleft <= 0.0)
        {
            float fps = accum / frames;
            string fpsString = string.Format("{0} FPS", Mathf.RoundToInt(fps));
            fpsText.text = fpsString;

            // Reset the counters
            timeleft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
    }
}
