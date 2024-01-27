using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipFlopPitchComponent : MonoBehaviour
{

    [SerializeField] public float startPitch = 1.0f;
    [SerializeField,Range(0.1f,0.9f)] public float randomFloatMin = 0.5f;
    [SerializeField, Range(1f, 1.5f)] public float randomFloatMax = 1.2f;
    [SerializeField]private MovementComponent playerMoveCompoRef = null;


    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        Init();
    }

    void Init()
    {
        playerMoveCompoRef = GetComponent<Player>().GetComponent<MovementComponent>();
        if (!playerMoveCompoRef) return;

        audioSource = GetComponent<AudioSource>();
        if (!audioSource) return;
        audioSource.pitch = startPitch;

        playerMoveCompoRef.OnDash += SetPitch;
    }

    private void SetPitch()
    {
        audioSource.pitch = FindRandomFloat();
        Debug.Log("Set Pitch Called");
    }

    float FindRandomFloat()
    { 
        return UnityEngine.Random.Range(randomFloatMin, randomFloatMax);
    }

    void Update()
    {


    }
}

