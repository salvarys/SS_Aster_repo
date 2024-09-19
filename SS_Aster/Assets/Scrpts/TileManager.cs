using UnityEngine;

public class TileManager : MonoBehaviour
{
    public Renderer tileRenderer;   

    private Color originalColor;   
    private Color currentColor;  

    void Start()
    {
        if (tileRenderer == null)
        {
            tileRenderer = GetComponent<Renderer>();
        } 

        originalColor = tileRenderer.material.color;
        currentColor = originalColor;  
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Renderer playerRenderer = other.GetComponent<Renderer>();
            if (playerRenderer != null)
            {
                tileRenderer.material.color = playerRenderer.material.color;
                currentColor = playerRenderer.material.color;  
            }
        }
    }
}
