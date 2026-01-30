using System;
using System.Collections.Generic;
using UnityEngine;

public class PlantingSystem : MonoBehaviour
{
    
    public event Action PlotActivationChanged;
    
    public bool IsPlotActive => ActivePlot != null;
    public Plot ActivePlot => Plots.Count > 0 ? Plots[0] : null;

    [field: SerializeField] public List<Plot> Plots { get; private set; }
    [field: SerializeField] public Plant[] Plants {private set; get;}

    public void ActivatePlot(Plot plot)
    {
        Debug.Log($"[PlantingSystem] Activated Plot {plot.name}");
        Plots.Add(plot);
        PlotActivationChanged.Invoke();
    }

    public void DeactivatePlot(Plot plot)
    {
        Debug.Log($"[PlantingSystem] De-Activate Plot {plot.name}");
        if(Plots.Contains(plot))
            Plots.Remove(plot);
        PlotActivationChanged.Invoke();
    }
}
