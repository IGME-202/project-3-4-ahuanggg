using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Vehicle
{

    [SerializeField]
    public GameObject targetZombie;

    [SerializeField]
    public List<GameObject> zombies = new List<GameObject>();

    [SerializeField]
    public GameObject targetTreasure;
    

    protected override void CalcSteeringForces()
    {

       


        for (int i = 0; i < zombies.Count; i++)
        {
            Vector3 uForce = Vector3.zero;

            uForce += ObstacleAvoidance() * 5f;

            uForce += Flee(zombies[i]);

            uForce += Seek(targetTreasure);

            uForce = Vector3.ClampMagnitude(uForce, maxForce);

            ApplyForce(uForce);
        }
        
    }

    protected override void Update()
    {
        base.Update();

        for (int i = 0; i < zombies.Count; i++)
        {
            Debug.DrawLine(transform.position, zombies[i].transform.position, Color.red, 0.01f);
        }

        Debug.DrawLine(transform.position, targetTreasure.transform.position, Color.white, 0.01f);

       

        if (transform.position.y != 0.5f)
        {
            position.y = 0.5f;
        }

        if (Vector3.Distance(position, targetTreasure.transform.position) < radius)
        {
            targetTreasure.transform.position = new Vector3(Random.Range(-75f, 75f), 0.5f, Random.Range(-75f, 75f));
        }

        
    }
}
