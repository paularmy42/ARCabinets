using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ARSessionOrigin))]
public class PlacementManager : MonoBehaviour
{
    public GameObject marker;
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
    static List<ARRaycastHit> touch2_Hits = new List<ARRaycastHit>();

    [HideInInspector]
    public bool collisionLeft = false;
    [HideInInspector]
    public bool collisionRight = false;
    [HideInInspector]
    public bool collisionRear = false;
    [HideInInspector]
    public Vector3 snapPos;
    [HideInInspector]
    public GameObject collisionObject;

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
        else if (Input.touchCount == 1)
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
                    if (state == CabinetState.Snapped && collisionRight)
                    {
                        if (currentPosition.x - s_Hits[0].pose.position.x > 0.2f)
                        {
                            collisionRight = false;
                        }
                        newPos.x = snapPos.x;
                    }
                    else if (state == CabinetState.Snapped && collisionLeft)
                    {
                        if (s_Hits[0].pose.position.x - currentPosition.x > 0.2f)
                        {
                            collisionLeft = false;
                        }
                        newPos.x = snapPos.x;
                    }
                    else
                    {
                        newPos.x = s_Hits[0].pose.position.x;
                    }
                    if (state == CabinetState.Snapped && collisionRear)
                    {
                        if (currentPosition.z - s_Hits[0].pose.position.z > 0.2f)
                        {
                            collisionRear = false;
                        }
                        newPos.z = snapPos.z;
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
        else if (Input.touchCount == 2)
        {
            //Dual-touch to rotate cabinet
            var touch2 = Input.GetTouch(1);

            if (m_SessionOrigin.Raycast(touch2.position, touch2_Hits, TrackableType.PlaneWithinPolygon))
            {
                Vector3 direction = objPlacement.transform.position - touch2_Hits[0].pose.position;
                direction.y = 0;
                Vector3 turn = Vector3.Cross(Vector3.up, direction);
                objPlacement.transform.rotation = Quaternion.LookRotation(turn);
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


    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("On collision: " + collision.gameObject.GetComponent<Collider>().bounds.center);
        collisionObject = collision.gameObject;
        float snapDistSide = collision.gameObject.GetComponent<Collider>().bounds.size.x / 2 + gameObject.GetComponent<Collider>().bounds.size.x / 2;
        float snapDistRear = collision.gameObject.GetComponent<Collider>().bounds.size.z / 2 + gameObject.GetComponent<Collider>().bounds.size.z / 2;
        float posOffsetSide = collision.transform.position.x - gameObject.transform.position.x;
        float posOffsetRear = collision.transform.position.z - gameObject.transform.position.z;
        if (!collisionLeft && !collisionRight)
        {
            objPlacement.transform.rotation = collisionObject.transform.rotation;
            manager.cabinetState = CabinetState.Snapped;
            if (Mathf.Abs(posOffsetSide) >= (snapDistSide) - 0.05f)
            {
                if (posOffsetSide > 0)
                {
                    //Debug.Log("Right");
                    collisionRight = true;
                    snapPos.x = collision.transform.position.x - (snapDistSide);
                    snapPos.y = gameObject.transform.position.y;
                    snapPos.z = gameObject.transform.position.z;
                    gameObject.transform.position = snapPos;
                }
                else
                {
                    //Debug.Log("Left");
                    collisionLeft = true;
                    snapPos.x = collision.transform.position.x + snapDistSide;
                    snapPos.y = gameObject.transform.position.y;
                    snapPos.z = gameObject.transform.position.z;
                    gameObject.transform.position = snapPos;
                }
            }
        }
        if (!collisionRear)
        {
            manager.cabinetState = CabinetState.Snapped;
            if (Mathf.Abs(posOffsetRear) >= (snapDistRear) - 0.05f)
            {
                //Debug.Log("Rear");
                collisionRear = true;
                snapPos.x = gameObject.transform.position.x;
                snapPos.y = gameObject.transform.position.y;
                snapPos.z = collision.transform.position.z - (snapDistRear);
                gameObject.transform.position = snapPos;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //Debug.Log("Collision exit");
        manager.cabinetState = CabinetState.Instantiated;
        collisionLeft = false;
        collisionRight = false;
        collisionRear = false;
        collisionObject = null;
    }



    public void CabinetStateChangedHandler(CabinetState newState)
    {
        state = newState;
        if (newState == CabinetState.Snapped)
        {
            Debug.Log(string.Format("Placement Manager - Cabinet Snapped: {0}", objPlacement.gameObject.transform.position.ToString()));
        }
        if (newState == CabinetState.Placed)
        {
            Debug.Log(string.Format("XZ angle is {0}", objPlacement.transform.eulerAngles.y));
            Debug.Log(string.Format("X,Z point of object origin is {0},{1}", objPlacement.transform.position.x, objPlacement.transform.position.z));
            float dZ = Mathf.Sin(objPlacement.transform.eulerAngles.y * Mathf.Deg2Rad) * 0.3f;
            float dX = Mathf.Cos(objPlacement.transform.eulerAngles.y * Mathf.Deg2Rad) * 0.3f;
            float newX = objPlacement.transform.position.x + dX;
            float newZ = objPlacement.transform.position.z - dZ;
            Debug.Log(string.Format("X,Z point of object opposite end is {0},{1}", newX, newZ));

            Instantiate(marker, new Vector3(newX, 0, newZ), Quaternion.identity);
            objPlacement = null;
        }
    }
}



