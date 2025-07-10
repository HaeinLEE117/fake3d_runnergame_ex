using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform[] railTransforms;

    private Transform playerTransform;
    public int positionIndex;

    void Awake()
    {
        playerTransform = GetComponent<Transform>();
        positionIndex = 1;
    }

    void Start()
    {
        InitPlayer();
    }

    public void InitPlayer()
    {
        playerTransform.position = railTransforms[positionIndex].position;
    }

    #region Movement
    public void GoRight()
    {
        if (positionIndex >= railTransforms.Length - 1) return;
        positionIndex++;
        playerTransform.position = railTransforms[positionIndex].position;
        
    }

    public void GoLeft()
    {
        
        if (positionIndex <= 0) return;
        positionIndex--;
        playerTransform.position = railTransforms[positionIndex].position;
    }
    #endregion
}