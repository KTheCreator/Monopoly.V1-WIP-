using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkingPropertyScript : MonoBehaviour
{
    public List<PropertyObject> POLists = new List<PropertyObject>();
    [Space]
    [Header("Brown")]
    public string OldKentRoad = null;
    private WalkableScript OLK;
    public string Whitechapel = null;
    private WalkableScript WC;
    [Space]
    [Header("Light Blue")]
    public string Euston = null;
    private WalkableScript EU;
    public string The_Angel = null;
    private WalkableScript TA;
    public string Pentonville_Road = null;
    private WalkableScript PR;
    [Space]
    [Header("Pink")]
    public string PallMall = null;
    private WalkableScript PM;
    public string Whitehall = null;
    private WalkableScript WH;
    public string Northumberland_Road = null;
    private WalkableScript NR;
    [Space]
    [Header("Orange")]
    public string BowStreet = null;
    private WalkableScript BS;
    public string Marlborough_Street = null;
    private WalkableScript MS;
    public string Vine_Street = null;
    private WalkableScript VS;
    [Space]
    [Header("Red")]
    public string Fleet_Street = null;
    private WalkableScript FS;
    public string Trafalgar = null;
    private WalkableScript TR;
    public string Strand = null;
    private WalkableScript ST;
    [Space]
    [Header("Yellow")]
    public string Piccadilly = null;
    private WalkableScript PI;
    public string Coventry = null;
    private WalkableScript CO;
    public string Leceister = null;
    private WalkableScript LE;
    [Space]
    [Header("Green")]
    public string Bond_Street = null;
    private WalkableScript BOS;
    public string Oxford = null;
    private WalkableScript OX;
    public string Regent = null;
    private WalkableScript RE;
    [Space]
    [Header("Dark Blue")]
    public string Mayfair = null;
    private WalkableScript MA;
    public string Park_Lane = null;
    private WalkableScript PL;

    // Start is called before the first frame update
    void Start()
    {
        OLK = GameObject.Find("Old Kent Road").GetComponent<WalkableScript>();
        WC = GameObject.Find("Whitechapel Road").GetComponent<WalkableScript>();
        EU = GameObject.Find("Euston Road").GetComponent<WalkableScript>();
        TA = GameObject.Find("The Angel, Islington").GetComponent<WalkableScript>();
        PR = GameObject.Find("Pentonville Road").GetComponent<WalkableScript>();
        PM = GameObject.Find("Pall Mall").GetComponent<WalkableScript>();
        WH = GameObject.Find("Whitehall").GetComponent<WalkableScript>();
        NR = GameObject.Find("Northumberland Road").GetComponent<WalkableScript>();
        BS = GameObject.Find("Bow Street").GetComponent<WalkableScript>();
        MS = GameObject.Find("Marlborough Street").GetComponent<WalkableScript>();
        VS = GameObject.Find("Vine Street").GetComponent<WalkableScript>();
        FS = GameObject.Find("Fleet Street").GetComponent<WalkableScript>();
        TR = GameObject.Find("Trafalgar Square").GetComponent<WalkableScript>();
        ST = GameObject.Find("Strand").GetComponent<WalkableScript>();
        PI = GameObject.Find("Piccadilly").GetComponent<WalkableScript>();
        CO = GameObject.Find("Coventry Street").GetComponent<WalkableScript>();
        LE = GameObject.Find("Leicester Square").GetComponent<WalkableScript>();
        BOS = GameObject.Find("Bond Street").GetComponent<WalkableScript>();
        OX = GameObject.Find("Oxford Street").GetComponent<WalkableScript>();
        RE = GameObject.Find("Regent Street").GetComponent<WalkableScript>();
        MA = GameObject.Find("Mayfair").GetComponent<WalkableScript>();
        PL = GameObject.Find("Park Lane").GetComponent<WalkableScript>();


    }

    // Update is called once per frame
    void Update()
    {
        //Managing properties purchases
        //Will only update who owns it if a change is detected.
        if(OldKentRoad != OLK.PO.name) OldKentRoad = OLK.PO.name;

        else if(Whitechapel != WC.PO.name) Whitechapel = WC.PO.name;

        else if(Euston != EU.PO.name) Euston = EU.PO.name;

        else if(The_Angel != TA.PO.name) The_Angel = TA.PO.name;
        else if (Pentonville_Road != PR.PO.name) Pentonville_Road = PR.PO.name;
        else if (PallMall != PM.PO.name) PallMall = PM.PO.name;
        else if (Whitehall != WH.PO.name) Whitehall = WH.PO.name;
        else if(Northumberland_Road != NR.PO.name) Northumberland_Road = NR.PO.name;
        else if(BowStreet != BS.PO.name) BowStreet = BS.PO.name;
        else if (Marlborough_Street != MS.PO.name) Marlborough_Street = MS.PO.name;
        else if(Vine_Street != VS.PO.name) Vine_Street = VS.PO.name;
        else if (Fleet_Street != FS.PO.name) Fleet_Street = FS.PO.name;
        else if (Trafalgar != TR.PO.name) Trafalgar = TR.PO.name;
        else if (Strand != ST.PO.name) Strand = ST.PO.name;
        else if(Piccadilly != PI.PO.name) Piccadilly = PI.PO.name;
        else if(Coventry != CO.PO.name) Coventry = CO.PO.name;
        else if (Leceister != LE.PO.name) Leceister = LE.PO.name;
        else if(Bond_Street != BOS.PO.name) Bond_Street = BOS.PO.name;
        else if(Oxford != OX.PO.name) Oxford = OX.PO.name;
        else if(Regent != RE.PO.name) Regent = RE.PO.name;
        else if(Mayfair != MA.PO.name)Mayfair = MA.PO.name;
        else if (Park_Lane != PL.PO.name) Park_Lane = PL.PO.name;


    }

}
