using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Ghost : MonoBehaviour
{ 
    public Movement movement { get; private set; }
    public GhostHome ghostHome { get; private set; }
    public GhostScatter ghostScatter { get; private set; }
    public GhostChase ghostChase { get; private set; }
    public GhostFrightened ghostFrightened { get; private set; }
    public GhostBehavior initialBehavior;
    public Transform target;
    public int points = 200;

    private void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.ghostHome = GetComponent<GhostHome>();
        this.ghostFrightened = GetComponent<GhostFrightened>();
        this.ghostChase = GetComponent<GhostChase>();
        this.ghostScatter = GetComponent<GhostScatter>();
    }

    private void Start()
    {
        ResetState();
    }

    public void SetPosition(Vector3 position)
    {
        position.z = this.transform.position.z;
        this.transform.position = position;
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();

        this.ghostFrightened.Disable();
        this.ghostChase.Disable(); 
        this.ghostScatter.Enable();

        if(this.ghostHome != this.initialBehavior)
        {
            this.ghostHome.Disable();
        }
        if(this.initialBehavior != null)
        {
            this.initialBehavior.Enable();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.ghostFrightened.enabled)
            {
                FindObjectOfType<GameManager>().GhostEaten(this);
            }
            else
            {
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }
}
