using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReceiver : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    public void OnMove(InputAction.CallbackContext context) => playerController.HandleMovement(context);

    public void OnLook(InputAction.CallbackContext context) => playerController.HandleLook(context);

    public void OnFire(InputAction.CallbackContext context) => playerController.HandleFire(context);

    public void OnInteract(InputAction.CallbackContext context) => playerController.HandleInteraction(context);
}