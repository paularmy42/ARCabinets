using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HingePoint
{
    Left,
    Right,
    Top,
    Bottom
}

public interface IComponent
{
    float Px { get; }
    float Py { get; }
    float Pz { get; }
    float Sx { get; }
    float Sy { get; }
    float Sz { get; }
    bool IsInteractable { get; }

    void SetCabinetDimensions(float _length, float _height, float _depth, float _thickness);

    void BuildCabinetObject(Transform _parent);

    void BuildInteractableCabinetObject(Transform _parent);
}

public interface IInteractableComponent
{
    float HingeX { get; }
    float HingeY { get; }
    float HingeZ { get; }
    HingePoint HingeOn { get; }
    float RotateHingePoint { get; }
}

public class EndLeftComponent: IComponent
{
    public float Px { get; private set; }
    public float Py { get; private set; }
    public float Pz { get; private set; }
    public float Sx { get; private set; }
    public float Sy { get; private set; }
    public float Sz { get; private set; }
    public bool IsInteractable { get; private set; }

    public void SetCabinetDimensions(float _length, float _height, float _depth, float _thickness)
    {
        Px = -_length / 2 + _thickness / 2;
        Py = _height / 2;
        Pz = 0f;
        Sx = _thickness;
        Sy = _height;
        Sz = _depth;
        IsInteractable = false;
    }

    public void BuildCabinetObject(Transform _parent)
    {
        CabinetObjects.BuildCabinetObject(Sx, Sy, Sz, Px, Py, Pz, _parent);
    }

    public void BuildInteractableCabinetObject(Transform _parent)
    {
        //CabinetObjects.BuildInteractableCabinetObject(this, _parent);
    }
}

public class EndRightComponent : IComponent
{
    public float Px { get; private set; }
    public float Py { get; private set; }
    public float Pz { get; private set; }
    public float Sx { get; private set; }
    public float Sy { get; private set; }
    public float Sz { get; private set; }
    public bool IsInteractable { get; private set; }

    public void SetCabinetDimensions(float _length, float _height, float _depth, float _thickness)
    {
        Px = _length / 2 - _thickness / 2;
        Py = _height / 2;
        Pz = 0f;
        Sx = _thickness;
        Sy = _height;
        Sz = _depth;
        IsInteractable = false;
    }

    public void BuildCabinetObject(Transform _parent)
    {
        CabinetObjects.BuildCabinetObject(Sx, Sy, Sz, Px, Py, Pz, _parent);
    }

    public void BuildInteractableCabinetObject(Transform _parent)
    {
        //CabinetObjects.BuildInteractableCabinetObject(this, _parent);
    }
}

public class BackComponent : IComponent
{
    public float Px { get; private set; }
    public float Py { get; private set; }
    public float Pz { get; private set; }
    public float Sx { get; private set; }
    public float Sy { get; private set; }
    public float Sz { get; private set; }
    public bool IsInteractable { get; private set; }

    public void SetCabinetDimensions(float _length, float _height, float _depth, float _thickness)
    {
        Px = 0;
        Py = _height / 2;
        Pz = _depth / 2 - _thickness / 2;
        Sx = _length - _thickness * 2;
        Sy = _height;
        Sz = _thickness;
        IsInteractable = false;
    }

    public void BuildCabinetObject(Transform _parent)
    {
        CabinetObjects.BuildCabinetObject(Sx, Sy, Sz, Px, Py, Pz, _parent);
    }

    public void BuildInteractableCabinetObject(Transform _parent)
    {
        //CabinetObjects.BuildInteractableCabinetObject(this, _parent);
    }
}

public class BottomComponent : IComponent
{
    public float Px { get; private set; }
    public float Py { get; private set; }
    public float Pz { get; private set; }
    public float Sx { get; private set; }
    public float Sy { get; private set; }
    public float Sz { get; private set; }
    public bool IsInteractable { get; private set; }

    public void SetCabinetDimensions(float _length, float _height, float _depth, float _thickness)
    {
        Px = 0;
        Py = _thickness / 2;
        Pz = -_thickness / 2;
        Sx = _length - _thickness * 2;
        Sy = _thickness;
        Sz = _depth - _thickness;
        IsInteractable = false;
    }

    public void BuildCabinetObject(Transform _parent)
    {
        CabinetObjects.BuildCabinetObject(Sx, Sy, Sz, Px, Py, Pz, _parent);
    }

    public void BuildInteractableCabinetObject(Transform _parent)
    {
        //CabinetObjects.BuildInteractableCabinetObject(this, _parent);
    }
}

public class TopComponent : IComponent
{
    public float Px { get; private set; }
    public float Py { get; private set; }
    public float Pz { get; private set; }
    public float Sx { get; private set; }
    public float Sy { get; private set; }
    public float Sz { get; private set; }
    public bool IsInteractable { get; private set; }

    public void SetCabinetDimensions(float _length, float _height, float _depth, float _thickness)
    {
        Px = 0;
        Py = _height - _thickness / 2;
        Pz = -_thickness / 2;
        Sx = _length - _thickness * 2;
        Sy = _thickness;
        Sz = _depth - _thickness;
        IsInteractable = false;
    }

    public void BuildCabinetObject(Transform _parent)
    {
        CabinetObjects.BuildCabinetObject(Sx, Sy, Sz, Px, Py, Pz, _parent);
    }

    public void BuildInteractableCabinetObject(Transform _parent)
    {
        //CabinetObjects.BuildInteractableCabinetObject(this, _parent);
    }
}

public class RailComponent : IComponent
{
    public float Px { get; private set; }
    public float Py { get; private set; }
    public float Pz { get; private set; }
    public float Sx { get; private set; }
    public float Sy { get; private set; }
    public float Sz { get; private set; }
    public bool IsInteractable { get; private set; }

    public void SetCabinetDimensions(float _length, float _height, float _depth, float _thickness)
    {
        Px = 0;
        Py = _height - _thickness / 2;
        Pz = -_depth / 2 + 0.130f / 2; //Replace hardcoded 0.130f with user modifiable variable
        Sx = _length - _thickness * 2;
        Sy = _thickness;
        Sz = 0.130f; //Replace hardcoded 0.130f with user modifiable variable
        IsInteractable = false;
    }

    public void BuildCabinetObject(Transform _parent)
    {
        CabinetObjects.BuildCabinetObject(Sx, Sy, Sz, Px, Py, Pz, _parent);
    }

    public void BuildInteractableCabinetObject(Transform _parent)
    {
        //CabinetObjects.BuildInteractableCabinetObject(this, _parent);
    }
}

public class AdjShelfComponent : IComponent
{
    public float Px { get; private set; }
    public float Py { get; private set; }
    public float Pz { get; private set; }
    public float Sx { get; private set; }
    public float Sy { get; private set; }
    public float Sz { get; private set; }
    public int AdjShelfQty { get; private set; }
    public float AdjShelfSetback { get; private set; }
    private float Height { get; set; }
    public bool IsInteractable { get; private set; }

    public AdjShelfComponent(int _adjShelfQty, float _adjShelfSetback)
    {
        AdjShelfQty = _adjShelfQty;
        AdjShelfSetback = _adjShelfSetback;
    }

    public void SetCabinetDimensions(float _length, float _height, float _depth, float _thickness)
    {
        if(AdjShelfSetback/1000 > _depth)
        {
            AdjShelfSetback = 0;
        }
        Px = 0;
        Py = _height / (AdjShelfQty + 1);
        Pz = -_thickness / 2 + AdjShelfSetback/1000/2;
        Sx = _length - _thickness * 2;
        Sy = _thickness;
        Sz = _depth - _thickness - AdjShelfSetback/1000;
        Height = _height;
        IsInteractable = false;
    }

    public void BuildCabinetObject(Transform _parent)
    {
        float _pyoverride;
        for (int i = 0; i < AdjShelfQty; i++)
        {
            _pyoverride = (Height / (AdjShelfQty + 1)) * (i + 1);
            CabinetObjects.BuildCabinetObject(Sx, Sy, Sz, Px, _pyoverride, Pz, _parent);
        }
    }

    public void BuildInteractableCabinetObject(Transform _parent)
    {
        //CabinetObjects.BuildInteractableCabinetObject(this, _parent);
    }
}

public class DoorComponent : IComponent, IInteractableComponent
{
    public float Px { get; private set; }
    public float Py { get; private set; }
    public float Pz { get; private set; }
    public float Sx { get; private set; }
    public float Sy { get; private set; }
    public float Sz { get; private set; }
    public bool IsInteractable { get; private set; }

    public float HingeX { get; private set; }
    public float HingeY { get; private set; }
    public float HingeZ { get; private set; }
    public HingePoint HingeOn { get; private set; }
    public float RotateHingePoint { get; private set; }

    public int DoorQty { get; private set; }

    public DoorComponent(int _doorQty, HingePoint _hingeOn)
    {
        IsInteractable = true;
        HingeOn = _hingeOn;
        DoorQty = _doorQty;
    }

    public void SetCabinetDimensions(float _length, float _height, float _depth, float _thickness)
    {
        Px = 0f;
        Py = 0.001f;
        Pz = -0.002f;
        Sx = _length / DoorQty - 0.002f;
        Sy = _height - 0.002f;
        Sz = _thickness;

        if(HingeOn == HingePoint.Left)
        {
            Px = _length / (2 * DoorQty);
            HingeX = -_length / 2;
            HingeY = _height / 2;
            HingeZ = -_depth / 2 - _thickness / 2;
            RotateHingePoint = 0f;
        }
        if (HingeOn == HingePoint.Right)
        {
            Px = -(_length / (2 * DoorQty));
            HingeX = _length / 2;
            HingeY = _height / 2;
            HingeZ = -_depth / 2 - _thickness / 2;
            RotateHingePoint = 180f;
        }
        if (HingeOn == HingePoint.Top)
        {
            HingeX = 0f;
            HingeY = _height / 2;
            HingeZ = -_depth / 2 - _thickness / 2;
            RotateHingePoint = 90f;
        }
        if (HingeOn == HingePoint.Bottom)
        {
            HingeX = 0f;
            HingeY = _height / 2;
            HingeZ = -_depth / 2 - _thickness / 2;
            RotateHingePoint = 270f;
        }
    }

    public void BuildCabinetObject(Transform _parent)
    {
        CabinetObjects.BuildCabinetObject(Sx, Sy, Sz, Px, Py, Pz, _parent);
    }

    public void BuildInteractableCabinetObject(Transform _parent)
    {
        CabinetObjects.BuildInteractableCabinetObject(this, _parent);
    }
}

