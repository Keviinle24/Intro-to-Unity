using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed; 
    public Vector3 jumpForce = new Vector3(0.0f, 7.0f, 0.0f);
    public float rotationSpeed = 1f;
    private Rigidbody rb;
    public float yRotate = 0f;
    private Vector3 startPosition = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
    }

    void Jump() {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded()) {
            rb.AddForce(jumpForce, ForceMode.Impulse);
        }
    }

    void RotateView() {
        yRotate += Input.GetAxis("Mouse X") * rotationSpeed;
        transform.eulerAngles = new Vector3(0, yRotate, 0);
    }
    bool isGrounded() {
        int layerMask =  LayerMask.GetMask("Ground");
        Vector3 offset = new Vector3(0.0f, -0.5f, 0.0f);
        return Physics.Raycast(transform.position + offset, Vector3.down, 0.6f, layerMask);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = Vector3.zero;
        if (Input.GetKey(KeyCode.A)) {
            moveDir.x = -1.0f;
        }
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift)) {
            moveDir.x = -5.0f;
        }
        if (Input.GetKey(KeyCode.W)) {
            moveDir.y = 1.0f;
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift)) {
            moveDir.y = 5.0f;
        }
        if (Input.GetKey(KeyCode.S)) {
            moveDir.y = -1.0f;
        }
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift)) {
            moveDir.y = -5.0f;
        }
        if (Input.GetKey(KeyCode.D)) {
            moveDir.x = 1.0f;
        }
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift)) {
            moveDir.x = 5.0f;
        }
            Vector3 movement = transform.forward * moveDir.y + transform.right * moveDir.x;
            moveDir = movement * speed * Time.deltaTime;
            transform.Translate(moveDir, Space.World);
            
            Jump();
            RotateView();
        }

        private void OnTriggerEnter(Collider other) {
            Debug.Log("Trigger hit");
            transform.position = startPosition;
            rb.velocity = Vector3.zero;
        }
    }

