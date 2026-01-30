using System;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class SystemsProvider : MonoBehaviour
{

    public static PlantingSystem PlantingSystem { get; private set; }

    private void Awake()
    {
        PlantingSystem = GetComponentInChildren<PlantingSystem>();
    }

    private void Start()
    {
        
    }
}
