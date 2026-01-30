using UnityEngine;
using UnityEngine.UI;

public class PlantingIndicator : MonoBehaviour
{
    
    [SerializeField] private Text indicatorText;

    private void Awake()
    {
        SystemsProvider.PlantingSystem.PlotActivationChanged += HandleCanPlantChanged;
    }

    private void OnDestroy()
    {
        SystemsProvider.PlantingSystem.PlotActivationChanged -= HandleCanPlantChanged;
    }

    private void HandleCanPlantChanged()
    {
        bool activateIndicator = SystemsProvider.PlantingSystem.IsPlotActive && SystemsProvider.PlantingSystem.ActivePlot.IsPlanted == false;
        
        indicatorText.gameObject.SetActive(activateIndicator);
    }
}
