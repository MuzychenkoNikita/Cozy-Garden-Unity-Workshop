using System;
using UnityEngine;

[Serializable]
public class PlantData
{
    [field: SerializeField] public float InitialGrowTime {private set; get;}
    [field: SerializeField] public PlantStage[] Stages {private set; get;}
}

[Serializable]
public class PlantStage
{
    [field: SerializeField] public float GrowTime {private set; get;}
    [field: SerializeField] public GameObject Object {private set; get;}
}

public class Plant : MonoBehaviour
{

    private Plot _plot;
    [field: SerializeField] public PlantData PlantData { get; private set; }

    private PlantStage _stage => PlantData.Stages[_growthStage];

    private int _growthStage;
    private float _growthTimer;
    private float _plantTime;

    private GameObject _plantObject;
    
    public void InitializePlant(Plot plot)
    {
        _plot = plot;
    }

    private void Start()
    {
        _plantTime = Time.time;
        
        for(int i = 0; i < PlantData.Stages.Length; i++)
        {
            PlantData.Stages[i].Object.SetActive(false);
        }
    }

    private void Update()
    {
        // Initial Growth check
        if (_growthStage <= 0)
        {
            if (Time.time - _plantTime >= PlantData.InitialGrowTime)
            {
                ProgressGrowth();
            }
        }
        // All other growth check
        else
        {
            if (Time.time - _growthTimer >= _stage.GrowTime)
            {
                ProgressGrowth();
            }
        }
    }

    private void ProgressGrowth()
    {
        if(_growthStage >= PlantData.Stages.Length - 1)
            return;
        
        if (_plantObject != null)
        {
            _plantObject.gameObject.SetActive(false);
        }
        _growthTimer = Time.time;
        _growthStage++;
        _plantObject = _stage.Object;
        _plantObject.SetActive(true);
    }
}
