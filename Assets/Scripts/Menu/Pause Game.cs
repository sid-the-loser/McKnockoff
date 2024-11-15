using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private bool isPaused = false;

    public void TogglePause()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
            isPaused = false;
            Debug.Log("Game Unpaused");
        }
        else
        {
            Time.timeScale = 0f;
            isPaused = true;
            Debug.Log("Game paused");
        }
    }
}
