using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;

    [Range(10, 500)]
    public int startingCount = 250;
    const float AgentDensity = 0.08f;
    public bool spheredSpawn = false;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;
    [Range(50f, 500f)]
    public float avoidanceRadioRadiusMultiplier = 5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    float squareRadioAvoidanceRadius;
    public float SquareRadioAvoidanceRadius { get { return squareRadioAvoidanceRadius; } }

    public bool isPlayer = false;

    FlockAgent spawnInSphere()
    {
        FlockAgent newAgent;
        Vector3 pos = (Vector3)Random.insideUnitSphere * startingCount * AgentDensity;
        if (startingCount < 20) pos *= 5;
        newAgent = Instantiate(
            agentPrefab,
            pos,
            Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
            transform
            );
        return newAgent;
    }

    FlockAgent spawnInCircle()
    {
        FlockAgent newAgent;
        Vector3 pos = Random.insideUnitCircle * startingCount * AgentDensity;
        pos.Set(pos.x, 0.5f, pos.y);
        if (startingCount < 25) pos *= 5;
        newAgent = Instantiate(
            agentPrefab,
            pos + transform.position,
            Quaternion.LookRotation(pos, Vector3.up),
            transform
            );
        return newAgent;
    }

    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
        squareRadioAvoidanceRadius = squareNeighborRadius * avoidanceRadioRadiusMultiplier * avoidanceRadioRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = (spheredSpawn == true) ? spawnInSphere() : spawnInCircle();

            newAgent.name = "Agent " + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);

            if (isPlayer == true) behavior.Update();

            Vector3 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);
        foreach (Collider c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }

}
