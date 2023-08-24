using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{ 
    private void Start()
    {
        StopGame();
    }

    public void StopGame()
    { 
        Time.timeScale = 0f;
    }

    public void StartGame()
    { 
        Time.timeScale = 1f;
    }

    public IEnumerator StopGameWithTimer(float timeToKill)
    {
        yield return new WaitForSeconds(timeToKill);
        StopGame();
    }
}
