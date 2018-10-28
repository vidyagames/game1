﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrapper : MonoBehaviour {

    [SerializeField]
    private GameObject[] _bootStrappedObjects; 

    void Awake()
    {
        foreach (GameObject GO in _bootStrappedObjects)
        {
            Instantiate(GO);
        }
        Destroy(gameObject);
    }
}
