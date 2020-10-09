using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This script purpose is to:
 * - Keep track player's money
 * - Add 200 when the player passes Go
 */
public class PlayerStats : MonoBehaviour
{
    public int money = 0;
    public bool paidGo = true;
    public BoxCollider GoSpace,JailSpace;
    PlayerMovement myPM;
    public gameManager myGM;
    // Start is called before the first frame update
    void Awake()
    {
        myPM = GetComponent<PlayerMovement>();
        money = 1500;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == GoSpace)//Adds 200 if the player goes past Go
        {
            if (!this.paidGo)
            {
                this.money += 200;
                this.paidGo = true;
            }
            Debug.Log(this.name + " is in the Go Area");
        }
        else if(other == JailSpace && myPM.currSpot == myPM.finalSpot)
        {
            myPM.sendPlayerToJail();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other == GoSpace)
        {
            this.paidGo = false;
        }
    }
    public void acceptPurchase()
    {
        if (this.myPM.isPlayerTurn)
        {
            //If clicks yes should take the player's money and make the player the owner
            this.money -= myPM.currSpot.GetComponent<WalkableScript>().PO.propertyValue;
            this.myPM.currSpot.GetComponent<WalkableScript>().PO.whoOwns = this.gameObject.name;
            this.myPM.PurchaseGUI.SetActive(false);
            this.myPM.isReadyToEndTurn = true;
        }

    }
    public void declinePurchase()
    {
        if (this.myPM.isPlayerTurn)
        {
            this.myPM.PurchaseGUI.SetActive(false);
            this.myPM.isReadyToEndTurn = true;
        }
    }
    public void payOtherPlayer(string playersName,int amount)
    {
        if (this.myPM.isPlayerTurn)
        {
            this.money -= amount;
            GameObject.Find(playersName).GetComponent<PlayerStats>().money += amount;

        }
    }


}
