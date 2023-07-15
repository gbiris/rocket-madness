using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables

    public Rigidbody2D rb;

    private Vector2 startPos;
    private Quaternion startRot;

    public ParticleSystem flames;
    public ParticleSystem smoke;
    private ParticleSystem.EmissionModule flamesEmission;
    private ParticleSystem.EmissionModule smokeEmission;

    public float gravity;
    public bool dead;


    public GameController gameController;

    public static PlayerController Instance { get; set; }
    #endregion

    #region Main Methods

    private void Awake()
    {
        Instance = this; 

        rb = GetComponent<Rigidbody2D>();

        startPos = base.transform.position;
        startRot = base.transform.rotation;

        gravity = rb.gravityScale;

        flamesEmission = flames.emission;
        smokeEmission = smoke.emission; 

        dead = true;
        rb.gravityScale = 0;

    }

    void Update()
    {
        if (!dead)
        {
            if (Input.anyKey)
            {
                flamesEmission.enabled = true;
            }

            if (!Input.anyKey)
            {
                flamesEmission.enabled = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (!dead)
		{
        Vector2 vel = rb.velocity;
        float ang = Mathf.Atan2(vel.y, x: 10) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(x:0, y:0, z:ang -90));

        if (Input.anyKey)
        {
            flamesEmission.enabled = true;
            rb.AddForce(Vector2.up * 1200f * gravity * Time.deltaTime);
        }

        if (!Input.anyKey)
        {
            flamesEmission.enabled = false;
        }
        }
    }
    #endregion

    #region Helper Methods

    public Vector2 GetVel()
	{
		return rb.velocity;
	}

    public void Restart()
	{
		ResetPlayer();
		// deadUI.SetActive(value: false);
		// SelectChar();
		// Difficulty.Instance.NewGame();
	}

	public void ResetPlayer()
	{
        rb.gravityScale = 0;
		dead = true;
		base.transform.position = startPos;
		base.transform.rotation = startRot;
		rb.velocity = Vector2.zero;
		smokeEmission.enabled = true;
		flamesEmission.enabled = true;
		CameraController.Instance.Restart();
	}
    private void Dead()
	{
		if (!dead)
		{
			dead = true;
			rb.AddForce(Vector2.right * 1000f);
			rb.AddTorque(400f);
			smokeEmission.enabled = false;
			flamesEmission.enabled = false;
            ScreenController.Instance.DeadUI();
			// AudioManager.Instance.Play("Dead");
			// deadUI.SetActive(value: true);
			// AudioManager.Instance.StopLoop("Fuel");
			UIController.Instance.UpdateDeadScreen(GameController.Instance.GetScore());
			// Difficulty.Instance.ResetCamera();
			// Object.Instantiate(explosion, base.transform.position, Quaternion.identity);
		}
	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle")) {
            Debug.Log("Obstacle hit.");
            Dead();
        } else if (other.gameObject.CompareTag("Scoring")) {
            GameController.Instance.IncreaseScore();
        }
    }

    #endregion
}
