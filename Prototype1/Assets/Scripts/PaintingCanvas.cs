using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingCanvas : MonoBehaviour
{
    [Header("Canvas Settings")]
    public Color canvasBaseColor = Color.white;
    
    [Header("Debug")]
    public bool showDebugMessages = true;
    
    private Renderer canvasRenderer;
    
    void Start()
    {
        canvasRenderer = GetComponent<Renderer>();
        
        // Set initial canvas color
        if (canvasRenderer != null)
        {
            canvasRenderer.material.color = canvasBaseColor;
        }
        
        // Ensure we have a trigger collider
        Collider col = GetComponent<Collider>();
        if (col == null)
        {
            col = gameObject.AddComponent<BoxCollider>();
        }
        col.isTrigger = true;
        
        if (showDebugMessages)
            Debug.Log("Canvas ready for painting!");
    }
    
    void OnTriggerStay(Collider other)
    {
        // Only care about Player and Brush - ignore other objects
        if (!other.CompareTag("Player") && !other.CompareTag("Brush"))
            return;
            
        if (showDebugMessages && Time.frameCount % 60 == 0)
            Debug.Log($"Canvas touched by: {other.name}");
        
        BrushPickup brush = null;
        
        // Check if it's a brush being held
        if (other.CompareTag("Brush"))
        {
            brush = other.GetComponent<BrushPickup>();
        }
        // Or if it's a player holding a brush
        else if (other.CompareTag("Player"))
        {
            GameObject brushObj = GameObject.FindGameObjectWithTag("Brush");
            if (brushObj != null)
            {
                brush = brushObj.GetComponent<BrushPickup>();
            }
        }
        
        // If we found a held brush, allow painting
        if (brush != null && brush.IsHeld())
        {
            // Show painting instruction
            if (showDebugMessages && Time.frameCount % 60 == 0)
                Debug.Log("Hold Spacebar to paint!");
            
            // Paint with spacebar only (mouse click is for color selection)
            if (Input.GetKey(KeyCode.Space))
            {
                PaintCanvas(brush.GetBrushColor());
            }
        }
    }
    
    void PaintCanvas(Color paintColor)
    {
        if (canvasRenderer != null)
        {
            // Blend the paint color with current canvas color
            canvasRenderer.material.color = Color.Lerp(canvasRenderer.material.color, paintColor, 0.1f);
            
            if (showDebugMessages && Time.frameCount % 30 == 0)
                Debug.Log($"Painting canvas with color: {paintColor}");
        }
    }
}