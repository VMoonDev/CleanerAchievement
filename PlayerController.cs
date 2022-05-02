using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D player;
    private PlayerInputControls playerInputControls;
    private StatsHandler playerStats;
    private bool runToggle = false;
    public Vector2 mousePosition;

    private void Awake(){
        player = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<StatsHandler>();
        playerInputControls = new PlayerInputControls();
        playerInputControls.Player.Enable();
    }

    public void Update()
    {
        MousePositionReporter();
    }

    public void FixedUpdate(){
        DirectionalMovementHandler();
    }

    public void DirectionalMovementHandler()
    {
        Vector2 inputVector = playerInputControls.Player.Movement.ReadValue<Vector2>() * playerStats.currentSpeed;
        if (inputVector != new Vector2(0f, 0f))
        {
            player.velocity = inputVector;
            if (Mathf.Abs(inputVector.x) > 0.5f && Mathf.Abs(inputVector.y) > 0.5f)
            {
                player.velocity = new Vector2(inputVector.x, inputVector.y / 2);
            }
        }
        
        else if (inputVector == new Vector2(0f, 0f))
        {
            player.Sleep();
        }
    }

    public void RunToggler()
    {
        runToggle = !runToggle;
        if (runToggle == true)
        {
            playerStats.currentSpeed *= 2f;
        }
        else
        {
            playerStats.currentSpeed = playerStats.moveSpeed;
        }
    }

    public void MousePositionReporter()
    {
        mousePosition = Mouse.current.position.ReadValue();
    }
}
