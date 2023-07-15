using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float spawnRate;
    public float minHeight;
    public float maxHeight;

    public static Spawner Instance { get; set; }

    private void OnEnable()
    {
        Instance = this;
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        GameObject obstacles = Instantiate(prefab, transform.position, Quaternion.identity);
        obstacles.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
    }
}