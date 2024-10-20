using Data;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Playertest : MonoBehaviour
{
    [SerializeField] private CharData charData = new CharData();

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            PlayerManager.Inst().AddChar(charData);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerManager.Inst().PlayerSave();
        }
    }
}
