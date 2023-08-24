using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine; 

public class GenerateLevel : MonoBehaviour
{
    public GameObject ground;
    public GameObject pickup;
    public GameObject obstacle;
    public GameObject triggerToGenerate;
    private GameObject section; 
    private float sectionPos; 
    private int counter; 
    private void Start()
    {
        GenerateSection();
        GenerateSection();
        GenerateSection();
    } 

    public void GenerateSection()
    { 
        int sectionLifeTime = 20;
        counter++;
        section = new GameObject("section" + counter);
        Destroy(section.gameObject, sectionLifeTime);
        GenerateSectionObjects(); 
    }

    private IEnumerator moveToDesignatedPosition(Vector3 destination, GameObject objectToAscend)
    {
        float SpeedOfAscending = 0.4f;
        while (true)
        { 
            if (objectToAscend.transform.position == destination)
                break;
            objectToAscend.transform.position = Vector3.MoveTowards(objectToAscend.transform.position, destination, SpeedOfAscending);
            yield return null;
        }
    } 

    private void GenerateSectionObjects()
    {
        float step = ground.transform.localScale.z;
        int depth = 50;
        GenerateGround(step, depth); 
        GeneratePickups(step, depth); 
        GenerateObstacle(step, depth);
        GenerateTrigger(step);  
    }

    private void GenerateGround(float localStep, int localDepth)
    {
        GameObject currentGround = Instantiate(ground, new Vector3(0, -3 - localDepth, sectionPos), Quaternion.identity, section.transform);
        StartCoroutine(moveToDesignatedPosition(new Vector3(0, -3, sectionPos), currentGround));
        sectionPos += localStep;
    }

    private void GeneratePickups(float localStep, int localDepth)
    { 
        float pickupPos = sectionPos - localStep / 2;
        float pickupStep = 6;
        for (int i = 0; i < 3; i++)
        {
            pickupPos -= pickupStep;
            GameObject currentPickup = Instantiate(pickup, new Vector3(0, -localDepth, pickupPos), Quaternion.identity, section.transform);
            StartCoroutine(moveToDesignatedPosition(new Vector3(Random.Range(-2, 2), 0, pickupPos), currentPickup));
        }
    }  

    private void GenerateObstacle(float localStep, int localDepth)
    {
        for (int i = -2; i < 3; i++) 
        {
            for(int j = 0; j < 4; j++)
            {
                if(Random.Range(0, 100) > 50)
                {
                    GameObject currentObstacle = Instantiate(obstacle, new Vector3(i, j - localDepth, sectionPos - localStep / 2), Quaternion.identity, section.transform);
                    StartCoroutine(moveToDesignatedPosition(new Vector3(i, j, sectionPos - localStep / 2), currentObstacle));
                } 
            }
        }  
    }
    private void GenerateTrigger(float localStep)
    {
        Instantiate(triggerToGenerate, new Vector3(0, 0, sectionPos - localStep / 2 + 1), Quaternion.identity, section.transform);
    }

}
