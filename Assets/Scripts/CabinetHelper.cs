using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public interface ICabinet
{
    string Name { get; set; }
    int AdjShelfQty { get; set; }
    float AdjShelfSetback { get; set; }
    int DoorQuantity { get; set; }
    HingePoint DoorHand { get; set; }
    int DrawerQuantity { get; set; }
    float[] DrawerSizes { get; set; }
    float HeightOffFloor { get; set; }
    int MinLength { get; set; }
    int MaxLength { get; set; }
    int MinHeight { get; set; }
    int MaxHeight { get; set; }
    int MinDepth { get; set; }
    int MaxDepth { get; set; }

    List<IComponent> ComponentList { get; }

    void BuildCabinet(float worldLength, float worldHeight, float worldDepth, float worldThickness, Transform _parent);
}

public class BaseCabinet: ICabinet
{
    public string Name { get; set; }
    public int AdjShelfQty { get; set; }
    public float AdjShelfSetback { get; set; }
    public int DoorQuantity { get; set; }
    public HingePoint DoorHand { get; set; }
    public int DrawerQuantity { get; set; }
    public float[] DrawerSizes { get; set; }
    public float HeightOffFloor { get; set; }
    public int MinLength { get; set; }
    public int MaxLength { get; set; }
    public int MinHeight { get; set; }
    public int MaxHeight { get; set; }
    public int MinDepth { get; set; }
    public int MaxDepth { get; set; }

    private List<IComponent> _componentList = new List<IComponent>();
    public List<IComponent> ComponentList
    {
        get
        {
            return _componentList;
        }
        private set
        {
            ComponentList = _componentList;
        }
    }

    public void ConstructComponents()
    {
        _componentList.Add(new EndLeftComponent());
        _componentList.Add(new EndRightComponent());
        _componentList.Add(new BackComponent());
        _componentList.Add(new BottomComponent());
        _componentList.Add(new RailComponent());
        _componentList.Add(new AdjShelfComponent(AdjShelfQty, AdjShelfSetback));
        if (DoorQuantity == 2 || DoorQuantity == 1 && DoorHand == HingePoint.Left)
        {
            _componentList.Add(new DoorComponent(DoorQuantity, HingePoint.Left));
        }
        if (DoorQuantity == 2 || DoorQuantity == 1 && DoorHand == HingePoint.Right)
        {
            _componentList.Add(new DoorComponent(DoorQuantity, HingePoint.Right));
        }
    }

    public void BuildCabinet(float worldLength, float worldHeight, float worldDepth, float worldThickness, Transform _parent)
    {
        CabinetObjects.BuildCabinet(worldLength, worldHeight, worldDepth, worldThickness, _parent, this);
    }
}

public class OverheadCabinet : ICabinet
{
    public string Name { get; set; }
    public int AdjShelfQty { get; set; }
    public float AdjShelfSetback { get; set; }
    public int DoorQuantity { get; set; }
    public HingePoint DoorHand { get; set; }
    public int DrawerQuantity { get; set; }
    public float[] DrawerSizes { get; set; }
    public float HeightOffFloor { get; set; }
    public int MinLength { get; set; }
    public int MaxLength { get; set; }
    public int MinHeight { get; set; }
    public int MaxHeight { get; set; }
    public int MinDepth { get; set; }
    public int MaxDepth { get; set; }

    private List<IComponent> _componentList = new List<IComponent>();
    public List<IComponent> ComponentList
    {
        get
        {
            return _componentList;
        }
        private set
        {
            ComponentList = _componentList;
        }
    }

    public void ConstructComponents()

    {
        _componentList.Add(new EndLeftComponent());
        _componentList.Add(new EndRightComponent());
        _componentList.Add(new BackComponent());
        _componentList.Add(new BottomComponent());
        _componentList.Add(new TopComponent());
        _componentList.Add(new AdjShelfComponent(AdjShelfQty, AdjShelfSetback));
        if (DoorQuantity == 2 || DoorQuantity == 1 && DoorHand == HingePoint.Left)
        {
            _componentList.Add(new DoorComponent(DoorQuantity, HingePoint.Left));
        }
        if (DoorQuantity == 2 || DoorQuantity == 1 && DoorHand == HingePoint.Right)
        {
            _componentList.Add(new DoorComponent(DoorQuantity, HingePoint.Right));
        }
    }

    public void BuildCabinet(float worldLength, float worldHeight, float worldDepth, float worldThickness, Transform _parent)
    {
        CabinetObjects.BuildCabinet(worldLength, worldHeight, worldDepth, worldThickness, _parent, this);
    }
}

public class WeirdCabinet : ICabinet
{
    public string Name { get; set; }
    public int AdjShelfQty { get; set; }
    public float AdjShelfSetback { get; set; }
    public int DoorQuantity { get; set; }
    public HingePoint DoorHand { get; set; }
    public int DrawerQuantity { get; set; }
    public float[] DrawerSizes { get; set; }
    public float HeightOffFloor { get; set; }
    public int MinLength { get; set; }
    public int MaxLength { get; set; }
    public int MinHeight { get; set; }
    public int MaxHeight { get; set; }
    public int MinDepth { get; set; }
    public int MaxDepth { get; set; }

    private List<IComponent> _componentList = new List<IComponent>();
    public List<IComponent> ComponentList
    {
        get
        {
            return _componentList;
        }
        private set
        {
            ComponentList = _componentList;
        }
    }

    public void ConstructComponents()
    {
        _componentList.Add(new EndLeftComponent());
        _componentList.Add(new BackComponent());
        _componentList.Add(new BottomComponent());
    }

    public void BuildCabinet(float worldLength, float worldHeight, float worldDepth, float worldThickness, Transform _parent)
    {
        CabinetObjects.BuildCabinet(worldLength, worldHeight, worldDepth, worldThickness, _parent, this);
    }
}

[RequireComponent(typeof(MeshFilter))]
public class CabinetObjects: MonoBehaviour
{
    public static void BuildCabinet(float _length, float _height, float _depth, float _thickness, Transform _parent, ICabinet _cabinet)
    {
        List<IComponent> componentlist = _cabinet.ComponentList;
        foreach (IComponent _component in componentlist.Where(n => n.IsInteractable == false))
        {
            _component.SetCabinetDimensions(_length, _height, _depth, _thickness);
            _component.BuildCabinetObject(_parent);
        }

        CombineMesh(_parent);
        foreach (IComponent _component in componentlist.Where(n => n.IsInteractable == true))
        {
            _component.SetCabinetDimensions(_length, _height, _depth, _thickness);
            _component.BuildInteractableCabinetObject(_parent);
        }
        _parent.position += new Vector3(0, _cabinet.HeightOffFloor, 0);
    }

    public static void BuildCabinetObject(float _sx, float _sy, float _sz, float _px, float _py, float _pz, Transform _parent)
    {
        GameObject _obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _obj.tag = "Carcase";
        _obj.transform.parent = _parent;
        _obj.transform.position = new Vector3(_px, _py, _pz);
        _obj.transform.localScale = new Vector3(_sx, _sy, _sz);
    }

    public static void BuildInteractableCabinetObject<T>(T _component, Transform _parent) where T :IComponent, IInteractableComponent
    {
        GameObject container = Instantiate(CabinetLibrary.instance.doorHinge, _parent);
        container.transform.localPosition = new Vector3(_component.HingeX, _component.HingeY, _component.HingeZ);
        container.transform.rotation = new Quaternion(_component.RotateHingePoint, 0, 0, 0);
        GameObject _obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _obj.tag = "Front";
        _obj.layer = 2;
        _obj.name = "DoorLeft";
        _obj.transform.parent = container.transform.GetChild(0).GetChild(0).transform;
        _obj.transform.localPosition = new Vector3(_component.Px, 0, 0);
        _obj.transform.localScale = new Vector3(_component.Sx, _component.Sy, _component.Sz);
        _obj.GetComponent<MeshRenderer>().material = PlacementManager.faceMat;
        Destroy(container.transform.GetChild(0).GetChild(0).GetComponent<BoxCollider>()); //Destroy box collider that is autogenerated by VRTK
    }

    public static void CombineMesh(Transform _parent)
    {
        MeshFilter[] meshFilters = _parent.gameObject.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        Destroy(_parent.gameObject.GetComponent<MeshCollider>());
        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
            i++;
        }
        _parent.GetComponent<MeshFilter>().mesh = new Mesh();
        _parent.GetComponent<MeshFilter>().mesh.CombineMeshes(combine, true);
        _parent.GetComponent<MeshFilter>().mesh.RecalculateBounds();
        _parent.GetComponent<MeshFilter>().mesh.RecalculateNormals();
        //MeshUtility.Optimize(_parent.GetComponent<MeshFilter>().mesh);
        _parent.gameObject.AddComponent<BoxCollider>();
        BoxCollider collider = _parent.gameObject.GetComponent<BoxCollider>();
        Debug.Log("collider center: " + collider.bounds.center);
        //collider.size = new Vector3(collider.size.x + 0.1f, collider.size.y, collider.size.z + 0.1f);
        _parent.gameObject.SetActive(true);
        //Find all children with tag "Carcase" and Destroy
        for (int j = 0; j < +_parent.childCount; j++)
        {
            Transform child = _parent.GetChild(j);
            if (child.tag == "Carcase")
            {
                Destroy(child.gameObject);
            }
        }
        Debug.Log("collider center: " + collider.bounds.center);
    }
}
