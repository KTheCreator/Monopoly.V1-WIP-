using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class DiceScript : MonoBehaviour
{
    public int powerMeter = 500;
    Rigidbody rb;
    public Sides selectedResults;
    public int selectedVector;
    public Sides[] vectorValues;
    public Vector3[] vectorPoints;

    public bool hasLanded;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float bestDot = -1;
        for (int i = 0; i < vectorPoints.Length; i++)
        {
            var worldSpaceValueVector = this.transform.localToWorldMatrix.MultiplyVector(vectorPoints[i]);
            float dot = Vector3.Dot(worldSpaceValueVector, Vector3.up);
            if(dot > bestDot)
            {
                bestDot = dot;
                selectedVector = i;
            }
        }
        selectedResults = vectorValues[selectedVector];
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach(var valueVector in vectorPoints)
        {
            var worldSpaceValueVector = this.transform.localToWorldMatrix.MultiplyVector(valueVector);
            Gizmos.DrawLine(this.transform.position, this.transform.position + worldSpaceValueVector);
        }
    }
    public void throwDice()
    {
        float dirX = Random.Range(0, 500);
        float dirY = Random.Range(0, 500);
        float dirZ = Random.Range(0, 500);
        this.rb.AddForce(new Vector3(0,1,0) * powerMeter);
        this.rb.AddTorque(dirX,dirY,dirZ);
    }
    public int GetDiceResults()
    {
        return (int)selectedResults;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("DiceFloor"))
        {
            hasLanded = true;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("DiceFloor"))
            hasLanded = false;
    }
}
public enum Sides
{
    One = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five =5,
    Six=6
}
