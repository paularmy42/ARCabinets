using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ARSessionOrigin))]
public class PlacementManager : MonoBehaviour
{
    public GameObject cabinetPrefab;
    ARSessionOrigin m_SessionOrigin;

    private static int _length;
    public static int length
    {
        get { return _length; }
        set { _length = value; }
    }

    private static int _depth;
    public static int depth
    {
        get { return _depth; }
        set { _depth = value; }
    }

    private static int _height;
    public static int height
    {
        get { return _height; }
        set { _height = value; }
    }

    public int thickness;

    public Material[] faceMaterials;

    private static Material _faceMat;
    public static Material faceMat
    {
        get { return _faceMat; }
        set { _faceMat = value; }
    }

    private static string _cabinetType;
    public static string cabinetType
    {
        get { return _cabinetType; }
        set { _cabinetType = value; }
    }

    private GameObject objPlacement;

    private CabinetState state;
    private CabinetManager manager;
    private Vector3 currentPosition;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();


    void Awake()
    {
        m_SessionOrigin = GetComponent<ARSessionOrigin>();
    }

    void Start()
    {
        CabinetManager.OnCabinetStateChanged.AddListener(CabinetStateChangedHandler);
    }

    void Update()
    {
        if (Input.touchCount == 0)
        {
            if (objPlacement)
            {
                objPlacement.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                objPlacement.GetComponent<CabinetManager>().cabinetState = CabinetState.Placed;
                objPlacement = null;
            }
            else
            {
                return;
            }
        }

        var touch = Input.GetTouch(0);

        if (m_SessionOrigin.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            if (!objPlacement)
            {
                Debug.Log("Object not placed, placing object");
                InstantiateCabinet(s_Hits[0]);
            }
            else
            {
                Vector3 newPos = s_Hits[0].pose.position;
                objPlacement.transform.position = newPos;
            }
        }
            //if (activeController)
            //{
            //    if (activeController.GetComponent<VRTK_ControllerEvents>().triggerClicked && pointer && objPlacement)
            //    {
            //        Debug.Log("Placing: " + objPlacement.GetComponent<Collider>().bounds.center);
            //        currentPosition = objPlacement.transform.position;
            //        Vector3 newPos = new Vector3();
            //        if (state == CabinetState.Snapped && manager.collisionRight)
            //        {
            //            if(currentPosition.x - pointer.pointerRenderer.GetDestinationHit().point.x > 0.2f)
            //            {
            //                manager.collisionRight = false;
            //            }
            //            newPos.x = manager.snapPos.x;
            //        }
            //        else if(state == CabinetState.Snapped && manager.collisionLeft)
            //        {
            //            if (pointer.pointerRenderer.GetDestinationHit().point.x - currentPosition.x > 0.2f)
            //            {
            //                manager.collisionLeft = false;
            //            }
            //            newPos.x = manager.snapPos.x;
            //        }
            //        else
            //        {
            //            newPos.x = pointer.pointerRenderer.GetDestinationHit().point.x;
            //        }
            //        if (state == CabinetState.Snapped && manager.collisionRear)
            //        {
            //            if (currentPosition.z - pointer.pointerRenderer.GetDestinationHit().point.z > 0.2f)
            //            {
            //                manager.collisionRear = false;
            //            }
            //            newPos.z = manager.snapPos.z;
            //        }
            //        else
            //        {
            //            newPos.z = pointer.pointerRenderer.GetDestinationHit().point.z;
            //        }
            //        newPos.y = objPlacement.transform.position.y;
            //        objPlacement.transform.position = newPos;
            //    }
            //    if (!activeController.GetComponent<VRTK_ControllerEvents>().triggerClicked && objPlacement)
            //    {
            //        objPlacement.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            //        objPlacement.GetComponent<CabinetManager>().cabinetState = CabinetState.Placed;
            //    }
            //    if(objPlacement)
            //    {
            //        state = manager.cabinetState;
            //        if (objPlacement.gameObject.GetComponent<CabinetManager>().cabinetState == CabinetState.Placed)
            //        {
            //            Debug.Log("does this code ever run?");
            //            objPlacement.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            //            objPlacement = null;
            //        }
            //    }
            //}
    }

        //void OnLeftTriggerClicked(object sender, ControllerInteractionEventArgs e)
        //{
        //    activeController = leftController;
        //    InstantiateCabinet(activeController);
        //}

        //void OnLeftTriggerUnclicked(object sender, ControllerInteractionEventArgs e)
        //{
        //    if(objPlacement)
        //    {
        //        objPlacement.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        //        objPlacement.GetComponent<CabinetManager>().cabinetState = CabinetState.Placed;
        //    }
        //}

        //void OnRightTriggerClicked(object sender, ControllerInteractionEventArgs e)
        //{
        //    activeController = rightController;
        //    //InstantiateCabinet(activeController);
        //}

    void InstantiateCabinet(ARRaycastHit _hit)
    {
        Debug.Log("Instantiating new cabinet");
        //VRTK_Pointer[] pointers = _controller.GetComponents<VRTK_Pointer>();
        //pointer = pointers[1];
        //Transform hit = pointer.pointerRenderer.GetDestinationHit().transform;
        //if (hit.gameObject.layer == 8)
        //{
        objPlacement = Instantiate(cabinetPrefab, _hit.pose.position, _hit.pose.rotation);
        manager = objPlacement.GetComponent<CabinetManager>();
        Debug.Log("New cabinet instantiated");
        //}
    }

    public void CabinetStateChangedHandler(CabinetState newState)
    {
        if (newState == CabinetState.Snapped)
        {
            Debug.Log(string.Format("Placement Manager - Cabinet Snapped: {0}", objPlacement.gameObject.transform.position.ToString()));
            //objPlacement.GetComponent<CabinetManager>().cabinetState = CabinetState.Placed;
        }
        if (newState == CabinetState.Placed)
        {
            //Debug.Log("Placement Manager - Cabinet Placed: " + objPlacement.transform.position.z);
            //objPlacement.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            objPlacement = null;
        }
    }
}



