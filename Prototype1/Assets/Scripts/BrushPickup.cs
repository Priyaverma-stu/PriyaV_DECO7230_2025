using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class BrushPickup : MonoBehaviour
{
    public Transform holdPosition; // Assign the player's hand or camera position
    private bool isHeld = false;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            // Pick up or drop brush
            if (!isHeld)
            {
                transform.SetParent(holdPosition);
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
                GetComponent<Rigidbody>().isKinematic = true;
                isHeld = true;
            }
            else
            {
                transform.SetParent(null);
                GetComponent<Rigidbody>().isKinematic = false;
                isHeld = false;
            }
        }
    }
}

*/

/*

public class BrushPickup : MonoBehaviour
{
    [Header("Setup")]
    public Transform holdPosition; // Assign the player's hand or camera position
    public Material defaultBrushMaterial;

    [Header("Debug")]
    public bool showDebugMessages = true;

    private bool isHeld = false;
    private bool playerNearby = false;
    private Rigidbody rb;
    private Renderer brushRenderer;
    private Color currentBrushColor = Color.white;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        brushRenderer = GetComponent<Renderer>();

        // Ensure we have required components
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        // Make sure we have a trigger collider
        Collider col = GetComponent<Collider>();
        if (col == null)
        {
            col = gameObject.AddComponent<CapsuleCollider>();
        }
        col.isTrigger = true;

        if (showDebugMessages)
            Debug.Log("Brush setup complete. Walk near and press E to pickup.");
    }

    void Update()
    {
        // Alternative: Use distance instead of trigger
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            playerNearby = distance < 2f; // Within 2 units
        }

        // Handle pickup/drop input
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (!isHeld)
            {
                PickupBrush();
            }
            else
            {
                DropBrush();
            }
        }

        // Show UI hints
        if (playerNearby && !isHeld)
        {
            // You can replace this with actual UI later
            if (Time.frameCount % 60 == 0) // Show message every 60 frames
                Debug.Log("Press E to pick up brush");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Something entered brush trigger: {other.name} with tag: {other.tag}");

        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            if (showDebugMessages)
                Debug.Log("Player near brush - Press E to pickup!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            if (showDebugMessages)
                Debug.Log("Player left brush area");
        }
    }

    void PickupBrush()
    {
        if (holdPosition == null)
        {
            Debug.LogError("Hold Position not assigned! Please assign the player's hand position.");
            return;
        }

        transform.SetParent(holdPosition);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        rb.isKinematic = true;
        isHeld = true;

        if (showDebugMessages)
            Debug.Log("Brush picked up! Press E to drop, or touch color boxes to get paint.");
    }

    void DropBrush()
    {
        transform.SetParent(null);
        rb.isKinematic = false;
        isHeld = false;

        if (showDebugMessages)
            Debug.Log("Brush dropped!");
    }

    // Method to change brush color when touching color boxes
    public void SetBrushColor(Color newColor)
    {
        currentBrushColor = newColor;
        if (brushRenderer != null)
        {
            brushRenderer.material.color = newColor;
        }

        if (showDebugMessages)
            Debug.Log($"Brush color changed to: {newColor}");
    }

    public Color GetBrushColor()
    {
        return currentBrushColor;
    }

    public bool IsHeld()
    {
        return isHeld;
    }
}

*/


public class BrushPickup : MonoBehaviour
{
    [Header("Setup")]
    public Transform holdPosition; // Assign the player's hand or camera position
    public Material defaultBrushMaterial;

    [Header("Debug")]
    public bool showDebugMessages = true;

    private bool isHeld = false;
    private bool playerNearby = false;
    private Rigidbody rb;
    private Renderer brushRenderer;
    private Color currentBrushColor = Color.white;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        brushRenderer = GetComponent<Renderer>();

        // Ensure we have required components
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        // Freeze the brush in place initially
        rb.isKinematic = true;

        // Make sure we have a trigger collider
        Collider col = GetComponent<Collider>();
        if (col == null)
        {
            col = gameObject.AddComponent<CapsuleCollider>();
        }
        col.isTrigger = true;

        if (showDebugMessages)
            Debug.Log("Brush setup complete. Walk near and press E to pickup.");
    }

    void Update()
    {
        // Alternative: Use distance instead of trigger
        /* https://docs.unity3d.com/ScriptReference/GameObject.FindWithTag.html */
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            playerNearby = distance < 2f; // Within 2 units
        }

        // Handle pickup/drop input
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (!isHeld)
            {
                PickupBrush();
            }
            else
            {
                DropBrush();
            }
        }

        // Show UI hints
        if (playerNearby && !isHeld)
        {
            // You can replace this with actual UI later
            if (Time.frameCount % 60 == 0) // Show message every 60 frames
                Debug.Log("Press E to pick up brush");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Something entered brush trigger: {other.name} with tag: {other.tag}");

        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            if (showDebugMessages)
                Debug.Log("Player near brush - Press E to pickup!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            if (showDebugMessages)
                Debug.Log("Player left brush area");
        }
    }

    void PickupBrush()
    {
        if (holdPosition == null)
        {
            Debug.LogError("Hold Position not assigned! Please assign the player's hand position.");
            return;
        }

        transform.SetParent(holdPosition);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        rb.isKinematic = true;
        isHeld = true;

        if (showDebugMessages)
            Debug.Log("Brush picked up! Press E to drop, or touch color boxes to get paint.");
    }

    void DropBrush()
    {
        transform.SetParent(null);
        rb.isKinematic = false;
        isHeld = false;

        if (showDebugMessages)
            Debug.Log("Brush dropped!");
    }

    // Method to change brush color when touching color boxes
    public void SetBrushColor(Color newColor)
    {
        currentBrushColor = newColor;
        if (brushRenderer != null)
        {
            brushRenderer.material.color = newColor;
        }

        if (showDebugMessages)
            Debug.Log($"Brush color changed to: {newColor}");
    }

    public Color GetBrushColor()
    {
        return currentBrushColor;
    }

    public bool IsHeld()
    {
        return isHeld;
    }
}