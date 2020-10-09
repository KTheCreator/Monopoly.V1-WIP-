using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;
using Debug = UnityEngine.Debug;
public class PlayerMovement : MonoBehaviour
{

    private DiceScript Dice1,Dice2;
    private gameManager myGM;
    PlayerStats playerStats;
    Rigidbody myRB;
    Renderer playerRender;
    [Space]
    [Header("Curve Information")]

    public int numPoints = 50;
    
    public Vector3[] positions = new Vector3[50];
    public LineRenderer lineRenderer;
    public GameObject controlPoint;

    [Space]
    [Header("Player Info")]
    public GameObject currSpot;
    public GameObject finalSpot;
    public List<GameObject> player_Path;
    public float speed = 1;
    public bool isMoving = false;
    [SerializeField]
    private bool hasRolled,hasCalculated = false;
    [SerializeField]
    public int numberOfSpaces;
    public bool newTurn,rolledDoubles,landedOnGo = false;
    public bool isPlayerTurn = true;
    public bool isReadyToEndTurn = false;
    [SerializeField]
    private int dice1Results, dice2Results = 0;

    public int state = 0;

    [Space]
    [Header("Space Trigger Rules")]
    public bool jailTime;

    [Space]
    [Header("GUI Variables")]
    public GameObject NumberCounter;
    public GameObject Dice1Text;
    public GameObject Dice2Text;
    public Animation diceAnim;
    public Text PurchaseQuestion;
    public GameObject PurchaseGUI;
    
    // Start is called before the first frame update
    void Start()
    {
        myGM = GameObject.Find("GameController").GetComponent<gameManager>();
        Dice1 = GameObject.Find("Dice1").GetComponent<DiceScript>();
        Dice2 = GameObject.Find("Dice2").GetComponent<DiceScript>();
        myRB = GetComponent<Rigidbody>();
        playerRender = GetComponent<Renderer>();
        playerStats = GetComponent<PlayerStats>();
        lineRenderer.positionCount = numPoints;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerTurn)
        {
            Color _color = playerRender.material.color;
            _color.a = 1f;
            playerRender.material.color = _color;
            GetCurrentPosisition();
            switch (state)
            {
                case 0: //Resetting player
                    Debug.Log("State 0" + this.name);
                    state0();
                    break;

                case 1://Throwing Dice
                    Debug.Log("State 1");
                    state1();
                    break;


                case 2://Calculate the route for the player
                    CreatePath();
                    NumberCounter.SetActive(true);
                    state = 3;
                    
                    break;

                case 3://Moving player
                    string noReplace = string.Format("{0:0}", player_Path.Count);
                    NumberCounter.GetComponent<Text>().text = noReplace; 
                    if (currSpot != finalSpot)
                    {
                        if (Input.GetKeyDown(KeyCode.E) && !isMoving)
                        {
                            
                            DrawCurve(player_Path[0]);

                            StartCoroutine(MovePlayerCoroutine());
                            if(player_Path.Count > 0)
                                player_Path.RemoveAt(0);
                        }
                    }
                    else { state = 4; }
                    
                    break;

                case 4://Ending Procedure
                    NumberCounter.SetActive(false);
                    //Step 1: Checking if the property is owned:
                    if (currSpot.GetComponent<WalkableScript>().PO != null)
                    {
                        //If the property is not owned, give the player option to purchase
                        if (currSpot.GetComponent<WalkableScript>().PO.whoOwns == null)
                        {
                            PurchaseGUI.SetActive(true);
                            //Then give the player the option to purchase.
                            PurchaseQuestion.text = "Would You Like To Purchase: " + currSpot.GetComponent<WalkableScript>().PO.propertyName;
                        }
                        //If the property is already owned, need to pay the player that owns it
                        else if (currSpot.GetComponent<WalkableScript>().PO.whoOwns != this.gameObject.name)
                        {
                            playerStats.payOtherPlayer(currSpot.GetComponent<WalkableScript>().PO.whoOwns, currSpot.GetComponent<WalkableScript>().PO.tierPrices[0]);
                        }
                    }
                    
                    if (this.isReadyToEndTurn)
                    {
                        if (this.rolledDoubles)
                            Debug.Log("Press Y for another turn");
                        else
                            Debug.Log("Press Y for Next player");
                        if (Input.GetKeyDown(KeyCode.Y))
                        {
                            if (this.rolledDoubles)
                                this.state = 0;
                            else
                            {

                                myGM.pointer++;
                            }

                        }
                    }
                    break;


            }
        }
        else
        {
            Color _color = playerRender.material.color;
            _color.a = .5f;
            playerRender.material.color = _color;
            this.state = 0;
        }
        

    }
    
    //Creates a the points that the player will have to travel to get to its position
    private void CreatePath()
    {
        List<GameObject> path = new List<GameObject>();
        bool found = false;
        GameObject tmpPoint = currSpot;
        while (!found)
        {
            path.Add(tmpPoint.GetComponent<WalkableScript>().nextInLine);
            tmpPoint = tmpPoint.GetComponent<WalkableScript>().nextInLine;
            if (tmpPoint == finalSpot)
                found = true;
        }
        player_Path = path;
    }

    private void FollowPath()
    {
        isMoving = true;
        StartCoroutine(MovePlayerCoroutine());
        player_Path.RemoveAt(0);

    }

    private Vector3 CalculateQuadCurve(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        /*float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;
        return p;*/
        return (Mathf.Pow(u, 2) * p0) + (2 * u * t * p1) + (Mathf.Pow(t, 2) * p2);
    }
    private void GetCurrentPosisition()
    {
        RaycastHit playerRay;
        if (Physics.Raycast(transform.position, -transform.up, out playerRay))
        {
            currSpot = playerRay.collider.gameObject;
        }
        
        
    }
    private void GetFinalPosition()
    {
        GameObject tmpPos = currSpot;
        for(int i = 0; i < numberOfSpaces; i++)
        {
            if (tmpPos.GetComponent<WalkableScript>().nextInLine != null)
                tmpPos = tmpPos.GetComponent<WalkableScript>().nextInLine;
            
        }
        finalSpot = tmpPos;
    }
    private void DrawCurve(GameObject nextPosInSeq)
    {
        Vector3 cP = new Vector3((currSpot.GetComponent<WalkableScript>().GetWalkPoint().x + nextPosInSeq.GetComponent<WalkableScript>().GetComponent<WalkableScript>().GetWalkPoint().x) / 2, 2, (currSpot.GetComponent<WalkableScript>().GetWalkPoint().z + nextPosInSeq.GetComponent<WalkableScript>().GetComponent<WalkableScript>().GetWalkPoint().z) / 2);
        controlPoint.transform.position = cP;
        for (int i = 1; i < numPoints + 1; i++)
        {
            float t = i / (float)numPoints;
            positions[i - 1] = CalculateQuadCurve(t, currSpot.GetComponent<WalkableScript>().GetWalkPoint(),controlPoint.transform.position, nextPosInSeq.GetComponent<WalkableScript>().GetWalkPoint());
           
        }
        lineRenderer.SetPositions(positions);
    }

    private void state0()
    {
        this.rolledDoubles = false;
        this.hasCalculated = false;
        this.hasRolled = false;
        this.newTurn = false;
        this.landedOnGo = false;
        this.isReadyToEndTurn = false;

        state = 1;
        return;
    }
    private void state1()
    {
        Debug.Log("Press Space to Roll for: " + this.name);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dice1.throwDice(); Dice2.throwDice();

            hasRolled = true;
        }
        if (this.hasRolled)
        {
            if (Dice1.hasLanded && Dice2.hasLanded)
            {
                StartCoroutine(WaitforResult());
                
                return;
            }
        }
    }



    private void endProcedure()
    {
        if (rolledDoubles)
            this.newTurn = true;
        else
            myGM.nextPlayer();
   
    }
   
    public void sendPlayerToJail()
    {
        DrawCurve(GameObject.Find("GoToJail"));
        StartCoroutine(MovePlayerCoroutine());
    }
    IEnumerator MovePlayerCoroutine()
    {
        isMoving = true;
        for(int i = 0; i < positions.Length; i++)
        {
            Vector3 nextPos = positions[i];
            while(Vector3.Distance(transform.position,nextPos) > .0001)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
                if (i == positions.Length - 1)
                    isMoving = false;
                yield return null;
            }
        }

    }
    IEnumerator WaitforResult()
    {
        yield return new WaitForSeconds(.5f);
        dice1Results = (int)Dice1.selectedResults;
        dice2Results = (int)Dice2.selectedResults;
        string dice1Replace = string.Format("{0:0}", dice1Results);
        string dice2Replace = string.Format("{0:0}", dice2Results);
        //Dice1Text.SetActive(true); Dice2Text.SetActive(true);
        Dice1Text.GetComponent<Text>().text = dice1Replace;
        Dice2Text.GetComponent<Text>().text = dice2Replace;
        numberOfSpaces = dice1Results + dice2Results;
        diceAnim.Play();
        yield return new WaitForSeconds(0.1f);
        //Checks if the player rolls a doubles
        if (dice1Results == dice2Results)
            rolledDoubles = true;
        yield return new WaitForSeconds(.1f);
        GetFinalPosition();
        yield return new WaitForSeconds(.1f);
        //startMoving = true;
        state = 2;
    }

    
    
}
