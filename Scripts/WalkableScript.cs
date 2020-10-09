using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class WalkableScript : MonoBehaviour
{

    public float walkPointOffset = .75f;
    public float xOffset = .5f;
    public float zOffest = .5f;
    public GameObject nextInLine;
    public Boardside side;
    public PropertyObject PO;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetWalkPoint()
    {
        float y = transform.localPosition.y + walkPointOffset;
        float x = transform.localPosition.x + xOffset;
        float z = transform.localPosition.z + zOffest;
        Vector3 point = new Vector3(x,y,z);
        return point;
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawSphere(GetWalkPoint(), .1f);
        if (nextInLine == null)
            return;
        Gizmos.color = Color.green; 
        Gizmos.DrawLine(GetWalkPoint(), nextInLine.GetComponent<WalkableScript>().GetWalkPoint());
    }

}

public enum Boardside
{
    Bottom=270,
    Left=0,
    Top=90,
    Right=180
}
