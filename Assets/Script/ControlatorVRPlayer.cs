using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]

public class ControlatorVRPlayer : MonoBehaviour
{
    private CharacterController DriverPlayer;
    private Vector3 MovimientoEnDireccion = Vector3.zero;
    private Vector2 Entrada;

    private CollisionFlags BanderasDeColision;

    public float FuerzaTocarSuelo;
    public float Gravedad;

    // Start is called before the first frame update
    void Start()
    {
        DriverPlayer = GetComponent<CharacterController>();
    }

    // FixeUpdate is called once per frame
    void FixedUpdate()
    {
        Vector3 MovimientoDeseado = transform.forward * Entrada.y + transform.right * Entrada.x;

        RaycastHit hitInfo;
        Physics.SphereCast(transform.position, DriverPlayer.radius,Vector3.down, out hitInfo,
        DriverPlayer.height / 2f, Physics.AllLayers,QueryTriggerInteraction.Ignore);

        MovimientoDeseado = Vector3.ProjectOnPlane(MovimientoDeseado, hitInfo.normal).normalized;
        
        if (DriverPlayer.isGrounded)
        {
            MovimientoEnDireccion.y = -FuerzaTocarSuelo;
        }else{
            MovimientoEnDireccion += Physics.gravity * Gravedad * Time.fixedDeltaTime;
        }

        BanderasDeColision = DriverPlayer.Move(MovimientoEnDireccion * Time.fixedDeltaTime);
    }
}
