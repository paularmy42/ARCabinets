using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetLibrary: MonoBehaviour
{
    public static List<ICabinet> Cabinets = new List<ICabinet>();
    public GameObject doorHinge;

    //Singleton for Cabinet Library
    private static CabinetLibrary _instance;
    public static CabinetLibrary instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CabinetLibrary>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        BaseCabinet twoDoorBase = new BaseCabinet();
        twoDoorBase.Name = "2DoorBase";
        twoDoorBase.AdjShelfQty = 1;
        twoDoorBase.AdjShelfSetback = 100;
        twoDoorBase.DoorQuantity = 2;
        twoDoorBase.HeightOffFloor = 0.150f;
        twoDoorBase.MinLength = 600;
        twoDoorBase.MaxLength = 1200;
        twoDoorBase.MinHeight = 600;
        twoDoorBase.MaxHeight = 800;
        twoDoorBase.MinDepth = 450;
        twoDoorBase.MaxDepth = 700;
        twoDoorBase.ConstructComponents();
        Cabinets.Add(twoDoorBase);

        BaseCabinet oneDoorBaseLH = new BaseCabinet();
        oneDoorBaseLH.Name = "1DoorBaseLH";
        oneDoorBaseLH.AdjShelfQty = 1;
        oneDoorBaseLH.AdjShelfSetback = 100;
        oneDoorBaseLH.DoorQuantity = 1;
        oneDoorBaseLH.DoorHand = HingePoint.Left;
        oneDoorBaseLH.HeightOffFloor = 0.150f;
        oneDoorBaseLH.MinLength = 200;
        oneDoorBaseLH.MaxLength = 600;
        oneDoorBaseLH.MinHeight = 600;
        oneDoorBaseLH.MaxHeight = 800;
        oneDoorBaseLH.MinDepth = 450;
        oneDoorBaseLH.MaxDepth = 700;
        oneDoorBaseLH.ConstructComponents();
        Cabinets.Add(oneDoorBaseLH);

        BaseCabinet oneDoorBaseRH = new BaseCabinet();
        oneDoorBaseRH.Name = "1DoorBaseRH";
        oneDoorBaseRH.AdjShelfQty = 1;
        oneDoorBaseRH.AdjShelfSetback = 100;
        oneDoorBaseRH.DoorQuantity = 1;
        oneDoorBaseRH.DoorHand = HingePoint.Right;
        oneDoorBaseRH.HeightOffFloor = 0.150f;
        oneDoorBaseRH.MinLength = 200;
        oneDoorBaseRH.MaxLength = 600;
        oneDoorBaseRH.MinHeight = 600;
        oneDoorBaseRH.MaxHeight = 800;
        oneDoorBaseRH.MinDepth = 450;
        oneDoorBaseRH.MaxDepth = 700;
        oneDoorBaseRH.ConstructComponents();
        Cabinets.Add(oneDoorBaseRH);

        OverheadCabinet twoDoorOverhead = new OverheadCabinet();
        twoDoorOverhead.Name = "2DoorOverhead";
        twoDoorOverhead.AdjShelfQty = 2;
        twoDoorOverhead.AdjShelfSetback = 2;
        twoDoorOverhead.DoorQuantity = 2;
        twoDoorOverhead.HeightOffFloor = 1.560f;
        twoDoorOverhead.MinLength = 600;
        twoDoorOverhead.MaxLength = 1200;
        twoDoorOverhead.MinHeight = 400;
        twoDoorOverhead.MaxHeight = 1000;
        twoDoorOverhead.MinDepth = 300;
        twoDoorOverhead.MaxDepth = 500;
        twoDoorOverhead.ConstructComponents();
        Cabinets.Add(twoDoorOverhead);

        //WeirdCabinet somethingStrange = new WeirdCabinet();
        //somethingStrange.Name = "StrangeCabinet";
        //somethingStrange.AdjShelfQty = 0;
        //somethingStrange.AdjShelfSetback = 0;
        //somethingStrange.HeightOffFloor = 0.400f;
        //somethingStrange.ConstructComponents();
        //Cabinets.Add(somethingStrange);
    }

}
