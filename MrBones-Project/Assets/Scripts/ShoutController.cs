using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nebby.UnityUtils;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class ShoutController : MonoBehaviour
{
    public GameObject shoutPrefab;
    public FloatReference shoutMaxCooldown;
    public FloatReference shoutCooldown;
    public float strength;
    public float strengthCoefficient;
    [Tooltip("Ran when the shout has completed.")]
    public UnityEvent<Vector2, float> OnShout;


    public void HandleShoutProcess(InputAction.CallbackContext context, Vector2 lookVector)
    {
        switch(context.phase)
        {
            case InputActionPhase.Started:
                break;
            case InputActionPhase.Performed:
                strength = Mathf.Max(context.ReadValue<float>(), strength);
                break;
            case InputActionPhase.Canceled:
                Shout(lookVector);
                strength = 0;
                break;
        }
    }

    private void Shout(Vector2 direction)
    {
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var shoutProjectile = Instantiate(shoutPrefab, transform.position, transform.rotation);
        var component = shoutProjectile.GetComponent<ShoutProjectile>();
        component.direction = direction == Vector2.zero ? Vector2.right : direction;
        float finalStrength = strength * strengthCoefficient;
        component.speed = finalStrength;
        OnShout?.Invoke(direction, finalStrength);
    }
}
