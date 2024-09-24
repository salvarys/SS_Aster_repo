using UnityEngine;

public class TileManager: MonoBehaviour
{
    public Renderer tileRenderer;   
    private Color currentColor; 
    private GameController gameController; 

    private Transform lastPlayerOnTile; 

    void Start()
    {
        tileRenderer = GetComponent<Renderer>();

        gameController = FindObjectOfType<GameController>();

        currentColor = tileRenderer.material.color;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Renderer playerRenderer = other.GetComponent<Renderer>();
            if (playerRenderer != null && other.transform != lastPlayerOnTile)
            {
                tileRenderer.material.color = playerRenderer.material.color;
                currentColor = playerRenderer.material.color;

                gameController.TileCaptured(other.transform);

                lastPlayerOnTile = other.transform;
            }
        }
    }
}
