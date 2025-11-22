using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraScript : MonoBehaviour
{

    public Transform player;
    public Vector3 targetLookOffset;
    public Vector3 targetFollowOffset;
    public Vector3 target, lookTarget;
    Vector3 lookTargetVelocity;

    public float smoothTime = 0.55f;
    Vector3 currentVelocity;

    public float distance = 3;
    public float height = 2;
    public float shoulderOffset = 2;
    public bool switchShoulder;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //transform.LookAt(player.position + targetLookOffset);
        //transform.position = Vector3.SmoothDamp(transform.position, player.position + targetFollowOffset, ref currentVelocity, smoothTime);

        target = player.position + (-player.transform.forward * distance);

        Vector3 verticalPosition = Vector3.up * height;
        Vector3 shoulderPosition = switchShoulder ? transform.right * -shoulderOffset : transform.right * shoulderOffset;

        transform.position = Vector3.SmoothDamp(transform.position, target + shoulderPosition + verticalPosition, ref currentVelocity, smoothTime);

        lookTarget = Vector3.SmoothDamp(lookTarget, player.position + verticalPosition + shoulderPosition, ref lookTargetVelocity, smoothTime);
        transform.LookAt(lookTarget);


    }
}
