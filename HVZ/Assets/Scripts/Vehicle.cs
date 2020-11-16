using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : MonoBehaviour
{

    protected Vector3 position;
    protected Vector3 direction;
    protected Vector3 velocity;
    protected Vector3 acceleration;

    [MinAttribute(0.0001f)]
    public float mass = 1f;
    public float radius = 1f;
    public float maxSpeed = 2f;
    public float maxForce = 1f;


    public List<Wall> Walls = new List<Wall>();
    public float avoidanceRange = 8;

    protected List<Wall> avoidList = new List<Wall>();

    public Mesh boxMesh;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        direction = Vector3.right;
        velocity = Vector3.zero;
        acceleration = Vector3.zero;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CalcSteeringForces();

        // defaults things
        velocity += acceleration * Time.deltaTime;

        position += velocity * Time.deltaTime;

        transform.position = position;

        direction = velocity.normalized;

        acceleration = Vector3.zero;
    }

    protected void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    void WrapVehicle()
    {

    }

    
    protected abstract void CalcSteeringForces();

 
    public Vector3 Seek(Vector3 targetPos)
    {
        Vector3 desiredVelocity = targetPos - position;

        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        Vector3 seekingForce = desiredVelocity - velocity;

        return seekingForce;
    }

    public Vector3 Seek(GameObject target)
    {
        return Seek(target.transform.position);
    }

    public Vector3 Flee(Vector3 targetPos)
    {
        Vector3 desiredVelocity = position - targetPos;

        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        Vector3 fleeingForce = desiredVelocity - velocity;

        return fleeingForce;
    }

    public Vector3 Flee(GameObject target)
    {
        return Flee(target.transform.position);
    }

    protected Vector3 ObstacleAvoidance()
    {
        Vector3 avoideanceSteering = Vector3.zero;

        Vector3 toOther;
        float dotProduct;
        Vector3 right = Vector3.Cross(velocity, Vector3.up);

        avoidList.Clear();

        foreach(Wall w in Walls)
        {
            toOther = w.transform.position - transform.position;

            dotProduct = Vector3.Dot(velocity, toOther);

            if(dotProduct >= 0)
            {
                if (Vector3.Distance(transform.position, w.transform.position) < avoidanceRange + w.radius)
                {
                    dotProduct = Vector3.Dot(right.normalized, toOther);
                    if(Mathf.Abs(dotProduct) <= radius + w.radius)
                    {
                        avoidList.Add(w);
                        if(dotProduct >= 0)
                        {
                            avoideanceSteering += -right.normalized * maxSpeed;
                        }
                        else
                        {
                            avoideanceSteering += right.normalized * maxSpeed;
                        }
                    }
                    
                }    
            }
        }
        return avoideanceSteering;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, avoidanceRange);

        Gizmos.color = Color.green;
        Vector3 futurepos = transform.position + velocity;
        Gizmos.DrawWireSphere(futurepos,radius);

        Gizmos.color = Color.yellow;
        Vector3 boxSize = new Vector3(radius * 2f, radius * 2f, Vector3.Distance(transform.position, futurepos) + radius);
        Vector3 boxMidPointDelta = (futurepos - transform.position).normalized * (boxSize.z / 2f);
        Gizmos.DrawWireMesh(boxMesh, transform.position + boxMidPointDelta, Quaternion.LookRotation(velocity, Vector3.up), boxSize);

        Gizmos.color = Color.red;
        foreach(Wall w in avoidList)
        {
            Gizmos.DrawLine(transform.position, w.transform.position);
        }
    }

}
