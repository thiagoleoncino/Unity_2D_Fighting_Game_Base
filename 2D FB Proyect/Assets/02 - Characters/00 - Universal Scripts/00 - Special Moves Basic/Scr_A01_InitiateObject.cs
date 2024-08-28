using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_A01_InitiateObject : MonoBehaviour
{
    [System.Serializable]
    public class SpecialMovePrefab
    {
        public string eventName;
        public Transform objectSpawn;
        public GameObject objectPrefab;
        public Action specialFunction;
    }

    public SpecialMovePrefab[] specialMoves;

    private void Start()
    {
        for (int i = 0; i < specialMoves.Length; i++)
        {
            int index = i;  
            specialMoves[index].specialFunction = () => InstantiateObject(specialMoves[index]);
        }
    }

    private void InstantiateObject(SpecialMovePrefab move)
    {
        if (move.objectPrefab != null && move.objectSpawn != null)
        {
            GameObject instantiatedObject = Instantiate(move.objectPrefab, move.objectSpawn.position, move.objectSpawn.rotation);
            instantiatedObject.tag = this.gameObject.tag;
        }
        else
        {
            Debug.LogWarning("¡objectPrefab o objectSpawn not assaigned!");
        }
    }

    // Método para ser llamado desde el Animation Manager
    public void TriggerEventByName(string eventName)
    {
        foreach (SpecialMovePrefab move in specialMoves)
        {
            if (move.eventName == eventName)
            {
                move.specialFunction?.Invoke();
                return;
            }
        }

        Debug.LogWarning($"No se encontró ningún evento con el nombre: {eventName}");
    }
}

