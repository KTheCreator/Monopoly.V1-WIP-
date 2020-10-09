using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
public class CameraScript : MonoBehaviour
{
    public Transform target;
    public PlayerMovement tPlayer;
    public float smoothSpeed;
    public Vector3 cameraOffset;
    Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    
    // Update is called once per frame

    void Update()
    {
        //Rotating the camerra around the board to focuse on the current player
        var currSide = tPlayer.currSpot.GetComponent<WalkableScript>().side;
        if (currSide == Boardside.Left)
        {
            cameraOffset.z = -2;
            cameraOffset.x = 0;
        }
        else if(currSide == Boardside.Bottom)
        {
            cameraOffset.z = 0;
            cameraOffset.x = 2;
        }
        else if(currSide == Boardside.Top)
        {
            cameraOffset.z = 0;
            cameraOffset.x = -2;
        }
        else if(currSide == Boardside.Right)
        {
            cameraOffset.z = 2;
            cameraOffset.x = 0;
        }
        Vector3 dPosition = target.position + cameraOffset;
        Vector3 sPosition = Vector3.SmoothDamp(transform.position, dPosition, ref velocity, smoothSpeed);
        transform.position = sPosition;
        Quaternion dRotation = Quaternion.Euler(25,(int)currSide,0);
        transform.rotation = Quaternion.Slerp(transform.rotation,dRotation,Time.deltaTime * smoothSpeed);
        
    }
}
