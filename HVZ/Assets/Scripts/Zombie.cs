using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Vehicle
{
    [SerializeField]
    public Human targetHuman;


    [SerializeField]
    public GameObject targetZombie;

    [SerializeField]
    public List<GameObject> humans = new List<GameObject>();

    protected override void CalcSteeringForces()
    {
        for(int i = 0; i < humans.Count; i++)
        {
            ApplyForce(Seek(humans[i].transform.position));
        }
        
        //for (int i = 0; i < humans.Count; i++)
        //{
        //    ApplyForce(Seek(humans[i]));
        //}
    }

    protected override void Update()
    {
        base.Update();

        Debug.DrawLine(targetHuman.transform.position, transform.position, Color.white, 0.01f);


        if (transform.position.y != 1)
        {
            position.y = 1f;
        }

        if (Vector3.Distance(position, targetHuman.transform.position) < radius)
        {
            Vector3 oldpos = targetHuman.transform.position;
            Destroy(targetHuman);
            Instantiate(targetZombie, oldpos, Quaternion.identity);
            
        }
    }
}

