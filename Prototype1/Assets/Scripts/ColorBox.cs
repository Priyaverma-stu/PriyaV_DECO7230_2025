using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class ColorBox : MonoBehaviour
{
    [Header("Color Settings")]
    public Color boxColor = Color.yellow;

    [Header("Debug")]
    public bool showDebugMessages = true;

    private Renderer boxRenderer;

    void Start()
    {
        boxRenderer = GetComponent<Renderer>();

        // Set the box's visual color
        if (boxRenderer != null)
        {
            boxRenderer.material.color = boxColor;
        }

        // Ensure we have a trigger collider
        Collider col = GetComponent<Collider>();
        if (col == null)
        {
            col = gameObject.AddComponent<BoxCollider>();
        }
        col.isTrigger = true;

        if (showDebugMessages)
            Debug.Log($"Color box setup with color: {boxColor}");
    }

    void OnTriggerEnter(Collider other)
    {
        if (showDebugMessages)
            Debug.Log($"Color box touched by: {other.name} with tag: {other.tag}");

        // Check if it's a brush
        if (other.CompareTag("Brush"))
        {
            BrushPickup brush = other.GetComponent<BrushPickup>();
            if (brush != null && brush.IsHeld())
            {
                // Give the brush this color
                brush.SetBrushColor(boxColor);

                if (showDebugMessages)
                    Debug.Log($"Brush got color: {boxColor} from {gameObject.name}");
            }
        }
    }
}

*/

public class ColorBox : MonoBehaviour
{
    [Header("Color Settings")]
    public Color boxColor = Color.yellow;
    
    [Header("Debug")]
    public bool showDebugMessages = true;
    
    private Renderer boxRenderer;
    
    void Start()
    {
        boxRenderer = GetComponent<Renderer>();
        
        // Set the box's visual color
        if (boxRenderer != null)
        {
            boxRenderer.material.color = boxColor;
        }
        
        // Ensure we have a trigger collider
        Collider col = GetComponent<Collider>();
        if (col == null)
        {
            col = gameObject.AddComponent<BoxCollider>();
        }
        col.isTrigger = true;
        
        if (showDebugMessages)
            Debug.Log($"Color box setup with color: {boxColor}");
    }
    
    void OnTriggerStay(Collider other)
    {
        // Only respond to player while holding brush
        if (other.CompareTag("Player"))
        {
            // Find the brush and check if player is holding it
            GameObject brushObj = GameObject.FindGameObjectWithTag("Brush");
            if (brushObj != null)
            {
                BrushPickup brush = brushObj.GetComponent<BrushPickup>();
                if (brush != null && brush.IsHeld())
                {
                    // Show instruction and handle click
                    if (showDebugMessages && Time.frameCount % 60 == 0)
                        Debug.Log($"Click to get {boxColor} color");
                    
                    // Click to get color
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        brush.SetBrushColor(boxColor);
                        if (showDebugMessages)
                            Debug.Log($"Clicked! Brush got color: {boxColor}");
                    }
                }
            }
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        // Keep the old trigger enter for backup, but make it silent
        if (other.CompareTag("Brush"))
        {
            BrushPickup brush = other.GetComponent<BrushPickup>();
            if (brush != null && brush.IsHeld())
            {
                brush.SetBrushColor(boxColor);
                if (showDebugMessages)
                    Debug.Log($"Brush touched color box - got color: {boxColor}");
            }
        }
    }
}