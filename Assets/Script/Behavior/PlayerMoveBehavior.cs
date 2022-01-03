using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Player Behavior")]
public class PlayerMoveBehavior : FilteredFlockBehavior
{
    private Vector3 move = Vector3.zero;

    public override void Update()
    {
        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (Input.GetButton("Jump"))
        {
            Debug.Log("Jump");
            move.y = 1.0f;
        }

    }
    

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Debug.Log("move" + move);
        return move;
    }
}