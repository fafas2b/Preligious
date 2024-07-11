using UnityEngine;

public class BasicRigidBodyPush : MonoBehaviour
{
    // Capas de objetos que se pueden empujar
    public LayerMask pushLayers;
    
    // Variable que indica si se puede empujar
    public bool canPush;
    
    // Fuerza de empuje ajustable en un rango de 0.5 a 5
    [Range(0.5f, 5f)] public float strength = 1.1f;

    // Método llamado cuando el controlador de colisiones detecta una colisión
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Si se permite empujar, llamamos al método para empujar cuerpos rígidos
        if (canPush) PushRigidBodies(hit);
    }

    // Método para empujar cuerpos rígidos
    private void PushRigidBodies(ControllerColliderHit hit)
    {
        // https://docs.unity3d.com/ScriptReference/CharacterController.OnControllerColliderHit.html

        // Asegurarse de que hemos chocado con un cuerpo rígido no cinemático
        Rigidbody body = hit.collider.attachedRigidbody;
        
        // Si no hay cuerpo rígido o es cinemático, salir del método
        if (body == null || body.isKinematic) return;

        // Asegurarse de que solo empujamos las capas deseadas
        var bodyLayerMask = 1 << body.gameObject.layer;
        
        // Si la capa del objeto no está en las capas permitidas para empujar, salir del método
        if ((bodyLayerMask & pushLayers.value) == 0) return;

        // No queremos empujar objetos por debajo de nosotros
        if (hit.moveDirection.y < -0.3f) return;

        // Calcular la dirección del empuje desde la dirección del movimiento, solo movimiento horizontal
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0.0f, hit.moveDirection.z);

        // Aplicar el empuje y tener en cuenta la fuerza
        body.AddForce(pushDir * strength, ForceMode.Impulse);
    }
}