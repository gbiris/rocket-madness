using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{
    // Reference to the player's Transform
	public Transform player;

    // Smoothed velocity for camera movement
	private Vector2 vel;

    // Offset for camera position
    private Vector2 offset;

    // Reference to the Camera component
	private Camera cam;

    // Default camera zoom level
	private float defaultZoom;

    // Smoothed velocity for camera zooming
    private float zoomVel;

    // Singleton instance of CameraController
	public static CameraController Instance { get; private set; }

    // Called when the script instance is being loaded
	private void Awake()
	{
		Instance = this;
		cam = GetComponent<Camera>();
		defaultZoom = cam.orthographicSize;
		offset = new Vector2(0f, 0f);
	}

    // Called at fixed time intervals
	private void FixedUpdate()
	{
		if (!(player == null))
		{
			// Calculate the target position based on player's velocity and offset
			float num = PlayerController.Instance.GetVel().magnitude / 6f;
			Vector2 target = (Vector2)player.position + PlayerController.Instance.GetVel() * 0.3f + offset;

			// Smoothly move the camera to the target position
			base.transform.position = Vector2.SmoothDamp(base.transform.position, target, ref vel, 0.3f);
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, -10f);

			// Smoothly adjust the camera's orthographic size based on player's velocity
			cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, defaultZoom + num, ref zoomVel, 0.6f);
		}
	}

	// Reset the camera position aligned with the default player position.
	public void Restart()
	{
		base.transform.position = player.position;
	}
}
