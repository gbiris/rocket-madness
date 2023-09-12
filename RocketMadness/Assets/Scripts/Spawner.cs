using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Prefab to be spawned
    public GameObject prefab;

    // Rate at which the prefab spawns
    public float spawnRate;

    // Minimum and maximum height variation for spawning
    public float minHeight;
    public float maxHeight;

    // Singleton instance of Spawner
    public static Spawner Instance { get; set; }

    // Called when the script instance is being loaded
    private void OnEnable()
    {
        Instance = this;

        // Repeatedly invoke the Spawn method with a delay of spawnRate seconds
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    // Called when the script instance is being disabled
    private void OnDisable()
    {
        // Cancel the repeating Spawn invocations
        CancelInvoke(nameof(Spawn));
    }

    // Spawn a new prefab with random height variation
    private void Spawn()
    {
        // Instantiate the prefab at the current transform position
        GameObject obstacles = Instantiate(prefab, transform.position, Quaternion.identity);

        // Randomly adjust the spawned prefab's position vertically
        obstacles.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
    }
}
