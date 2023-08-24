using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject player;
    private Transform cameraTransform;
    private Vector3 originalPosition;

    public float shakeDuration = 0.2f;
    public float shakeAmount = 0.05f;
    private void Update()
    {
        transform.position = player.transform.position;
    } 

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        originalPosition = cameraTransform.localPosition;
    } 

    public void ShakeCamera()
    {
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            Vector3 randomOffset = Random.insideUnitSphere * shakeAmount;
            cameraTransform.localPosition = originalPosition + randomOffset;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        cameraTransform.localPosition = originalPosition;
    }
}
