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
    [SerializeField] Enemy enemyRef = null;

    [SerializeField] private List<AudioSource> allAudioSource;

    void Start()
    {
        Init();
    }

    void Init()
    {


        enemyRef = GetComponent<Enemy>();
        if (enemyRef)
        {
            enemyRef.OnAttackLanded += SetPitch; 
        }
        playerMoveCompoRef = GetComponent<MovementComponent>();
        if (!playerMoveCompoRef) return;
        playerMoveCompoRef.OnDash += SetPitch;
        playerMoveCompoRef.OnInvisibilityStarted += SetPitch;

    }

    private void SetPitch()
    {
        int _size = allAudioSource.Count;
        for (int i = 0; i < _size; i++) 
        { 
            allAudioSource[i].pitch = FindRandomFloat();
        }
        //audioSource.pitch = FindRandomFloat();
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

