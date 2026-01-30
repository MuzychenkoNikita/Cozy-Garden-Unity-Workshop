using UnityEngine;

public class Plot : MonoBehaviour
{

    public bool IsPlanted => PlantObject != null;
    
    [field: SerializeField] public Plant PlantObject {get; private set;}

    [SerializeField] private float dirtPileOffset = 0.13f;

    public void Plant()
    {
        Debug.Log($"[Plot] Planting in Plot {name}");
        
        GameObject plantObj = Instantiate(SystemsProvider.PlantingSystem.Plants[0].gameObject, transform);
        PlantObject = plantObj.GetComponent<Plant>();
        
        plantObj.transform.SetLocalPositionAndRotation(Vector3.up * dirtPileOffset, Quaternion.identity);
        PlantObject.InitializePlant(this);
    
        SystemsProvider.PlantingSystem.DeactivatePlot(this);
    }

    public void OnTriggerEnter(Collider other)
    {
        SystemsProvider.PlantingSystem.ActivatePlot(this);
    }
    
    public void OnTriggerExit(Collider other)
    {
        SystemsProvider.PlantingSystem.DeactivatePlot(this);
    }
    
}
