using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    public GameObject sectionGenerator; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Stickman") 
        {
            sectionGenerator.GetComponent<GenerateLevel>().GenerateSection();
        }  
    }

}
