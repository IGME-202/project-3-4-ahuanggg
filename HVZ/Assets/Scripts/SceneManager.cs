using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
 
    [SerializeField]
    public Human Human;

    [SerializeField]
    public Zombie Zombie;

    [SerializeField]
    public GameObject PSG;

    [SerializeField]
    public Wall Wall;

    GameObject Human2, Zombie2, PSG2;

    public List<Wall> Walls = new List<Wall>();
    
    // Start is called before the first frame update
    void Start()
    {
         
        //instasiate the walls


        for(int i = 0; i < 10; i++)
        {
            Wall wall2 = (Instantiate(Wall, new Vector3(Random.Range(-75, 75), 1f, Random.Range(-75, 75)), Quaternion.identity)) ;
            Walls.Add(wall2);
        }



        //GameObject humanRef = GameObject.Find("Human");
        //Human humanScript = humanRef.GetComponent<Human>();

        //for (float i = 0; i < 2; i++)
        //{
        //    humanScript.zombies.Add(Instantiate(Zombie, new Vector3(Random.Range(-75, 75), 1f, Random.Range(-75, 75)), Quaternion.identity));
            
        //}

        Human Human2 = Instantiate(Human, new Vector3(Random.Range(-75, 75), 1f, Random.Range(-75, 75)), Quaternion.identity) as Human;
        Human2.Walls = Walls;
        
        var PSG2 = Instantiate(PSG, new Vector3(Random.Range(-75, 75), 1f, Random.Range(-75, 75)), Quaternion.identity);
    

        GameObject humanRef = GameObject.Find("Human(Clone)");
        Human humanScript = humanRef.GetComponent<Human>();

        for(int i = 0; i < 2; i++)
        {
            Zombie Zombie2 = Instantiate(Zombie, new Vector3(Random.Range(-75, 75), 1f, Random.Range(-75, 75)), Quaternion.identity) as Zombie;
            Zombie2.Walls = Walls;
        }

        
        
        foreach(GameObject zom in GameObject.FindGameObjectsWithTag("Zombie"))
        {
            Debug.Log(zom.name);
            if (zom.name == "Zombie(Clone)")
            {
                Debug.Log(zom.name);
                Zombie zombieScript = zom.GetComponent<Zombie>();
                zombieScript.targetHuman = Human2;
            }
            
        }

        // to fill up the zombie list in human
        foreach (GameObject zom in GameObject.FindGameObjectsWithTag("Zombie"))
        {
            Debug.Log(zom.name);
            if (zom.name == "Zombie(Clone)")
            {
                humanScript.zombies.Add(zom);
            }

        }


        humanScript.targetZombie = Zombie2;
        humanScript.targetTreasure = PSG2;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
