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
        else
        {
            var touch = Input.GetTouch(0);

            if (m_SessionOrigin.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
            {
                if (!objPlacement)
                {
                    InstantiateCabinet(s_Hits[0]);
                }
                else
                {
                    currentPosition = objPlacement.transform.position;
                    Vector3 newPos = new Vector3();
                    if (state == CabinetState.Snapped && manager.collisionRight)
                    {
                        if (currentPosition.x - s_Hits[0].pose.position.x > 0.2f)
                        {
                            manager.collisionRight = false;
                        }
                        newPos.x = manager.snapPos.x;
                    }
                    else if (state == CabinetState.Snapped && manager.collisionLeft)
                    {
                        if (s_Hits[0].pose.position.x - currentPosition.x > 0.2f)
                        {
                            manager.collisionLeft = false;
                        }
                        newPos.x = manager.snapPos.x;
                    }
                    else
                    {
                        newPos.x = s_Hits[0].pose.position.x;
                    }
                    if (state == CabinetState.Snapped && manager.collisionRear)
                    {
                        if (currentPosition.z - s_Hits[0].pose.position.z > 0.2f)
                        {
                            manager.collisionRear = false;
                        }
                        newPos.z = manager.snapPos.z;
                    }
                    else
                    {
                        newPos.z = s_Hits[0].pose.position.z;
                    }
                    newPos.y = objPlacement.transform.position.y;
                    objPlacement.transform.position = newPos;
                }
            }
        }
    }


    void InstantiateCabinet(ARRaycastHit _hit)
    {
        Debug.Log("Instantiating new cabinet");
        objPlacement = Instantiate(cabinetPrefab, _hit.pose.position, _hit.pose.rotation);
        manager = objPlacement.GetComponent<CabinetManager>();
        Debug.Log("New cabinet instantiated");
    }

    public void CabinetStateChangedHandler(CabinetState newState)
    {
        state = newState;
        if (newState == CabinetState.Snapped)
        {
            Debug.Log(string.Format("Placement Manager - Cabinet Snapped: {0}", objPlacement.gameObject.transform.position.ToString()));
            objPlacement.transform.rotation = manager.collisionObject.transform.rotation;
        }
        if (newState == CabinetState.Placed)
        {
            objPlacement = null;
        }
    }
}



