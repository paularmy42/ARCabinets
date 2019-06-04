using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

[RequireComponent(typeof(MeshFilter))]
public class ProceduralObjectConstructor : MonoBehaviour
{
    private float worldLength;
    private float worldDepth;
    private float worldHeight;
    private float worldThickness;
    private string cabinetSelection;

    private GameObject leftDoorContainer;
    private GameObject rightDoorContainer;

    void Start()
    {
        List<ICabinet> library = new List<ICabinet>();
        library.AddRange(CabinetLibrary.Cabinets);

        PlacementManager manager = UnityEngine.Object.FindObjectOfType<PlacementManager>();
        
        worldLength = PlacementManager.length / 1000f;
        worldDepth = PlacementManager.depth / 1000f;
        worldHeight = PlacementManager.height / 1000f;
        worldThickness = manager.thickness / 1000f;
        cabinetSelection = PlacementManager.cabinetType;

        ICabinet cabinetToBuild = library.Find(x => x.Name.Contains(cabinetSelection));
        cabinetToBuild.BuildCabinet(worldLength, worldHeight, worldDepth, worldThickness, this.transform);
    }
}
