using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cube : MonoBehaviour
{
    private int _x=  0;
    public int X{
        get => _x;
        set => _x = value;
    }
    private int _y = 0;
    public int Y{
        get => _y;
        set => _y = value;
    }
    private int _cubeValue = 0; 
    public int CubeValue{
        get => _cubeValue;
        set => _cubeValue = value;
    }

    private FlagStatus _status = FlagStatus.UNFLAGGED;
    public FlagStatus Status{
        get => _status;
        set => _status = value;
    }
    private bool _hasBeenChecked = false;
    public bool HasBeenChecked{
        get => _hasBeenChecked;
        set => _hasBeenChecked = value;
    }
}
