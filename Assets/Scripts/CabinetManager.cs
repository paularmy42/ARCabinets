using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum CabinetState
{
    Instantiated,
    Snapped,
    Placed
}
public class CabinetStateEvent : UnityEvent<CabinetState>
{
    //Do stuff in here if needed
}

public class CabinetManager : MonoBehaviour
{
    public static CabinetStateEvent OnCabinetStateChanged = new CabinetStateEvent();
    private CabinetState _cabinetState = CabinetState.Instantiated;
    public CabinetState cabinetState
    {
        get
        {
            return _cabinetState;
        }

        set
        {
            if (_cabinetState != value)
            {
                //Debug.Log(string.Format("Cabinet State before changed from {0} to {1}", _cabinetState, value));
                _cabinetState = value;
                OnCabinetStateChanged.Invoke(_cabinetState);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        OnCabinetStateChanged.AddListener(CabinetStateChangedHandler);
        cabinetState = CabinetState.Instantiated;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CabinetStateChangedHandler(CabinetState newState)
    {
        if (newState == CabinetState.Snapped)
        {
            Debug.Log("Snapped");
        }
        if (newState == CabinetState.Placed)
        {
            Debug.Log("Placed");
        }
    }
}
