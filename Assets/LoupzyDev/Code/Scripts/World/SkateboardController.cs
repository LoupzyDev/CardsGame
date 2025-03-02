using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkateboardController : MonoBehaviour {
    public float alignSpeed = 5f;
    public float rayDistance = 5f;
    public float forwardForce = 10f;
    public float turnSpeed = 50f;
    public float brakeForce = 5f;
    private float forwardInput;
    private float turnInput;
    private Vector3 surfaceNormal;
    private bool onSurface;
    private Rigidbody rb;
    [SerializeField] private Animator animator;
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        ProcessInputs();
    }

    private void FixedUpdate() {
        AlignToSurface();
        ProcessMovement();
    }

    private void ProcessInputs() {
        forwardInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.W)) {
            animator.SetBool("isPushing", true);
        } else {
            animator.SetBool("isPushing", false);
        }
    }

    private void ProcessMovement() {
        if (!onSurface) return;

        // Aplicar fuerza hacia adelante o atr�s
        rb.AddForce(transform.right * forwardForce * forwardInput, ForceMode.Acceleration);

        // Girar gradualmente seg�n la velocidad actual
        float adjustedTurnSpeed = turnSpeed * (rb.linearVelocity.magnitude / 5f); // Escalar giro con la velocidad
        transform.Rotate(Vector3.up * turnInput * adjustedTurnSpeed * Time.fixedDeltaTime);

        // Aplicar freno de mano
        if (Input.GetKey(KeyCode.Space)) {
            rb.linearVelocity *= (1 - brakeForce * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionStay(Collision other) {
        onSurface = true;
        surfaceNormal = other.GetContact(0).normal;
    }

    private void OnCollisionExit(Collision other) {
        onSurface = false;
    }

    void AlignToSurface() {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, rayDistance)) {
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, alignSpeed * Time.fixedDeltaTime);
        }
    }
}
