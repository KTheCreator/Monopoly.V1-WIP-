using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    [SerializeField]
    private List<PlayerMovement> players;
    public int pointer = 0;
    private CameraScript myCam;
    // Start is called before the first frame update
    void Start()
    {
        myCam = GameObject.Find("Main Camera").GetComponent<CameraScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if (pointer > players.Count - 1)
            pointer = 0;
        for(int i = 0; i < players.Count; i++)
        {
            //int currentP = pointer % players.Count;
            if (i == pointer)
            {

                players[i].isPlayerTurn = true;
                myCam.target = players[i].transform;
                myCam.tPlayer = players[i];
            }
            else
                players[i].isPlayerTurn = false;
        }
        
    }

    public void nextPlayer()
    {
        
        pointer++;
        players[pointer].state = 0;
        
    }
    
    
    
}
