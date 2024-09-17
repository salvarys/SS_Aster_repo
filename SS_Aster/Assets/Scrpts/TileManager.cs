using UnityEngine;

public class TileManager : MonoBehaviour
{
    public Renderer tileRenderer; 
    private Color originalColor;  

    void Start()
    {

        if (tileRenderer == null)
        {
            tileRenderer = GetComponent<Renderer>();
        }

        originalColor = tileRenderer.material.color;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Renderer playerRenderer = other.GetComponent<Renderer>();
            if (playerRenderer != null)
            {
                tileRenderer.material.color = playerRenderer.material.color;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tileRenderer.material.color = originalColor;
        }
    }
}
