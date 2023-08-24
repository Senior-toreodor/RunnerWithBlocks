using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CollisionsHandler : MonoBehaviour
{
    public GameObject player;
    public GameObject mainCamera;
    public GameObject markerPrefab; 

    public ParticleSystem collisionParticleSystem; 
     
    public bool HasVibrated = false;
    private float stackCount = 1; 
    private int maxCubes = 5;
    private int knockedCubesLifeTime = 4;
    private float jumpForse = 15;
    private float markerKillTime = 3;

    private void OnTriggerEnter(Collider collision)
    {
        Rigidbody cubeRB = GetComponent<Rigidbody>(); 

        if (collision.name == "Pickup(Clone)")
        { 
            GenerateText();
            collision.gameObject.SetActive(false);
            CanvasController.pointsCount++;  

            if (player.transform.GetChild(1).transform.childCount < maxCubes)
            {
                emmitParticleBurst();
                player.GetComponent<PlayerMover>().Jump(1); 

                GameObject buferCube = Instantiate(player.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject, player.transform.GetChild(1).gameObject.transform);
                buferCube.transform.localPosition = new Vector3(buferCube.transform.localPosition.x,  stackCount, buferCube.transform.localPosition.z);   

                stackCount++;  
            } 
        }  
    }  

    private void OnCollisionEnter(Collision collision)
    { 
        if (collision.collider.name == "WallPrefab(Clone)")
        {
            player.GetComponent<PlayerMover>().Jump(0); 
            mainCamera.GetComponent<FollowCamera>().ShakeCamera(); 

            Destroy(gameObject, knockedCubesLifeTime);

            transform.parent = null;
            if (!HasVibrated)
            {
                Handheld.Vibrate();
                HasVibrated = true;
            }  
        }
    }
     
    private void GenerateText()
    {
        GameObject newMarker = Instantiate(markerPrefab, transform.position + new Vector3(0, 3, 0), Quaternion.identity);
        newMarker.SetActive(true);
        newMarker.GetComponent<Rigidbody>().AddForce((new Vector3(5, 10, 0)) * jumpForse);
        Destroy(newMarker.gameObject, markerKillTime);
    }

    private void emmitParticleBurst()
    {
        var emissionModule = collisionParticleSystem.emission;  
        emissionModule.enabled = true;
        collisionParticleSystem.Play(); 
    }
}
