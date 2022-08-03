using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormManager : MonoBehaviour
{
    public static WormManager Instance;

    private Worm[] _worms;
    private Transform _wormCamera;
    private int _currWorm;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
    }

    public bool IsMyTurn(int i)
    {
        return i == _currWorm;
    }

    public void NextWorm()
    {

    }

    //private IEnumerator NextWormCRT()
    //{

    //}
}
