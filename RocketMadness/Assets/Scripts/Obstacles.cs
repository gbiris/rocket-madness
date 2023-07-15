using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public Transform top;
    public Transform bottom;

    public float speed = 5f;
    private float leftEdge;


    public static Obstacles Instance { get; set; }

    private void Awake()
    {
        Instance = this;
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 10f;
    }

    private void Update()
    {
        if (PlayerController.Instance.dead)
		{
			return;
		}
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < leftEdge) {
            Destroy(gameObject);
        }
    }
}