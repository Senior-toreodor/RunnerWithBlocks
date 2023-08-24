using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{ 
    public GameObject canvases;
    private GameObject pointsCanvas;
    private GameObject endScreenCanva;
    private GameObject startScreenCanva;

    public static int pointsCount = 0;
    private void Start()
    {
        pointsCount = 0;
        pointsCanvas = canvases.transform.GetChild(0).gameObject;
        endScreenCanva = canvases.transform.GetChild(1).gameObject;
        startScreenCanva = canvases.transform.GetChild(2).gameObject; 
    }

    private void Update()
    {  
        pointsCanvas.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "" + pointsCount;
    }

    public void Restart()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GetComponent<PauseController>().StartGame(); 
    }

    public void EndGame()
    {
        pointsCanvas.SetActive(false);
        endScreenCanva.SetActive(true);
        endScreenCanva.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "" + pointsCount; 
    }

    public void Play()
    {
        startScreenCanva.SetActive(false);
        GetComponent<PauseController>().StartGame();
    }
}
