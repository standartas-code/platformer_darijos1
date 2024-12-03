using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;
    public float trackSpeed = 10;

    [Range(0.1f, 0.9f)]
    public float smoothness = 0.125f;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        var targetPosition = Vector3.MoveTowards(transform.position, target.position + offset, trackSpeed * Time.deltaTime);

        var smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothness);

        transform.position = smoothedPosition;
    }
}
