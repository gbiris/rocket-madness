using UnityEngine;
using System;

public class Obstacles : MonoBehaviour
{
    // Speed at which the obstacle moves to the left
    public float speed = 5f;

    // X coordinate of the left edge of the screen
    private float leftEdge;

    // Singleton instance of Obstacles
    public static Obstacles Instance { get; set; }

    // Called when the script instance is being loaded
    private void Awake()
    {
        Instance = this;

        // Calculate the X coordinate of the left edge of the screen in world space
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 10f;
    }

    // Called once per frame
    private void Update()
    {
        // If the player is dead, do not update the obstacle's position
        if (PlayerController.Instance.dead)
		{
			return;
		}

        // Move the obstacle to the left based on its speed and the frame time
        transform.position += Vector3.left * speed * Time.deltaTime;

        // If the obstacle goes beyond the left edge of the screen, destroy it
        if (transform.position.x < leftEdge) {
            Destroy(gameObject);
        }
    }
}
