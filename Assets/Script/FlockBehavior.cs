using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlockBehavior : ScriptableObject
{
    public abstract void Update();

    public abstract Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock);
}
