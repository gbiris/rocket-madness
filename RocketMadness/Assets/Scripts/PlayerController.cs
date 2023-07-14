using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables

    private Rigidbody2D rb;

    private Vector2 startPos;
    private Quaternion startRot;

    public ParticleSystem flames;
    public ParticleSystem smoke;
    private ParticleSystem.EmissionModule flamesEmission;
    private ParticleSystem.EmissionModule smokeEmission;

    private float gravity;
    public bool dead;

    public GameController gameController;

    #endregion

    #region Main Methods

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        startPos = base.transform.position;
        startRot = base.transform.rotation;

        gravity = rb.gravityScale;

        flamesEmission = flames.emission;
        smokeEmission = smoke.emission; 
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            flamesEmission.enabled = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            flamesEmission.enabled = false;
        }
    }

    void FixedUpdate()
    {
        if (!dead)
		{
        Vector2 vel = rb.velocity;
        float ang = Mathf.Atan2(vel.y, x: 10) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(x:0, y:0, z:ang -90));

        if (Input.GetKey(KeyCode.Space))
        {
            flamesEmission.enabled = true;
            rb.AddForce(Vector2.up * 1200f * gravity * Time.deltaTime);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            flamesEmission.enabled = false;
        }
        }
    }
    #endregion

    #region Helper Methods

    public void UpdateGravity()
    {
        if(gameController.gameActive)
        {
            gravity = 3;
        }
    }
    #endregion
}
