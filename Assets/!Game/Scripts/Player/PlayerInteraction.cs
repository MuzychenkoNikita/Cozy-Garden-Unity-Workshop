using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    
    [SerializeField] private InputActionReference interactAction;

    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private PlayerController playerController;

    private void OnEnable()
    {
        if (interactAction == null)
            return;

        interactAction.action.performed += OnInteract;
        interactAction.action.Enable();
    }

    private void OnDisable()
    {
        if (interactAction == null)
            return;
        
        interactAction.action.performed -= OnInteract;
        interactAction.action.Disable();
    }
    
    private void OnInteract(InputAction.CallbackContext obj)
    {
        Debug.Log($"[PlayerInteraction] Interact Action Performed");

        if (SystemsProvider.PlantingSystem.IsPlotActive == false) return;
        
        playerAnimator.TriggerPlowAnimation();
        playerController.LookAtPoint(SystemsProvider.PlantingSystem.ActivePlot.transform.position);
        
        SystemsProvider.PlantingSystem.ActivePlot.Plant();
    }

}
