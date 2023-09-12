// Unity modules
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector2 startPos;
    private Quaternion startRot;

    // Particle system variables
    public ParticleSystem flames;
    public ParticleSystem smoke;
    private ParticleSystem.EmissionModule flamesEmission;
    private ParticleSystem.EmissionModule smokeEmission;

    // Gravity and death state variables
    public float gravity;
    public bool dead;

    public static PlayerController Instance { get; set; }


    private void Awake()
    {
        Instance = this; 

        rb = GetComponent<Rigidbody2D>();

        // Store starting position and rotation
        startPos = base.transform.position;
        startRot = base.transform.rotation;

        // Store default gravity value
        gravity = rb.gravityScale;

        // Flame and smoke particle system
        flamesEmission = flames.emission;
        smokeEmission = smoke.emission; 

        // Set initial game state
        dead = true;
        rb.gravityScale = 0;

    }

    void Update()
    {
        // // If player is not dead turn enabled particles when pressing a button
        // if (!dead)
        // {
        //     if (Input.anyKey)
        //     {
        //         flamesEmission.enabled = true;
        //     }

        //     if (!Input.anyKey)
        //     {
        //         flamesEmission.enabled = false;
        //     }
        // }
    }

    void FixedUpdate()
    {
        if (!dead)
		{
            // Update player rotation based on velocity
            Vector2 vel = rb.velocity;
            float ang = Mathf.Atan2(vel.y, x: 10) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(x:0, y:0, z:ang -90));

            // Apply upward force and enable flames particle emission if any key is pressed
            if (Input.anyKey)
            {
                flamesEmission.enabled = true;
                rb.AddForce(Vector2.up * 1200f * gravity * Time.deltaTime);

                AudioManager.Instance.PlaySFX("Thrust");
            }

            // Disable flames particle emission if no keys are pressed
            if (!Input.anyKey)
            {
                flamesEmission.enabled = false;
            }
        }
    }

    // Get the player's velocity
    public Vector2 GetVel()
	{
		return rb.velocity;
	}

    // Restart the player's state (Separate function used for future additions)
    public void Restart()
	{
		ResetPlayer();
	}

    // Reset the player's properties to initial state
	public void ResetPlayer()
	{
        // Reset player physics
        rb.gravityScale = 0;
		rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;

        // Set position to starting values
        base.transform.position = startPos;
		base.transform.rotation = startRot;

        // Reset particle emissions and camera
		smokeEmission.enabled = true;
		flamesEmission.enabled = true;
		CameraController.Instance.Restart();

        // Update dead state
        dead = true;
	}

    // Handle player death state
    private void Dead()
	{
        // If player is not dead already do this
		if (!dead)
		{
			dead = true;
            // Rocket gets flinged
			rb.AddForce(Vector2.right * 1000f);
			rb.AddTorque(100f);
            // Disable particles
			smokeEmission.enabled = false;
			flamesEmission.enabled = false;
            // Turn on dead screen
            ScreenController.Instance.DeadUI();
            // Add score to dead scren
			UIController.Instance.UpdateDeadScreen(GameController.Instance.GetScore());
		}
	}

    // Handle collision events
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Call dead function if collide with obstacle
        if (other.gameObject.CompareTag("Obstacle")) {
            Debug.Log("Obstacle hit.");
            Dead();
        // Increase score if goes through scoring collider    
        } else if (other.gameObject.CompareTag("Scoring")) {
            GameController.Instance.IncreaseScore();
        }
    }
}
