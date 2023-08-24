using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LosingHandler : MonoBehaviour
{ 
    public GameObject Player;
    public GameObject EnviromentController;
    private Animator playerAnimator;
    private Rigidbody[] playerChildRB;

    private float timeToKill = 3.5f;
    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerChildRB = GetComponentsInChildren<Rigidbody>(); 
        Losing(false);
    }

    private void OnCollisionEnter(Collision collision)
    { 
        if (collision.collider.name == "WallPrefab(Clone)")
        {
            Losing(true);
            Player.GetComponent<PlayerMover>().Stop();
        }
    } 

    public void Losing(bool isEnabled)
    {
        if (isEnabled)
        {
            //Ragdoll logic
            Player.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            Player.GetComponent<PlayerMover>().Stop();

            playerAnimator.enabled = !isEnabled;
            foreach (Rigidbody rb in playerChildRB)
            {
                rb.isKinematic = !isEnabled;
            }

            //Endgame canvas active and pause
            EnviromentController.GetComponent<CanvasController>().EndGame(); 
            StartCoroutine(EnviromentController.GetComponent<PauseController>().StopGameWithTimer(timeToKill));
        }  
    } 
}
