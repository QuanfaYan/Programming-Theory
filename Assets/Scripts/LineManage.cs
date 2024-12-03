using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManage : MonoBehaviour
{
    private int buildProgress = 0;
    const int SUCCESS = 100;
    const float SPAWN_POS_X = 8.5f;
    const float RANGE_POS_X = 1.5f;
    private bool defendState = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateProgress(int modVal)
    {
        buildProgress += modVal;
        if(buildProgress >= SUCCESS)
        {
            StartDefend();
        }
    }

    void StartDefend()
    {
        // destroy all the monster on the line
        Monster[] monsters = GetComponentsInChildren<Monster>();
        foreach (Monster monster in monsters)
        {
            Destroy(monster.gameObject);
        }

        // set defendState True
        defendState = true;

        // Soldiers and builders on the line stop working to celebrate;
        Soldier[] soldiers = GetComponentsInChildren<Soldier>();
        foreach(Soldier soldier in soldiers)
        {
            soldier.Celebrate();
        
        }
        Builder[] builders = GetComponentsInChildren<Builder>();
        foreach(Builder builder in builders)
        {
            builder.Celebrate();
        }

    }

    public bool GetDefendState()
    {
        return defendState;
    }

    public int GetBuildProgress()
    {
        return buildProgress;
    }

    public Vector3 GetSpawnPos()
    {
        Monster[] monsters = GetComponentsInChildren<Monster>();
        if(monsters.Length > 0)
        {
            float posX = monsters[monsters.Length - 1].gameObject.transform.position.x;
            if(posX +  RANGE_POS_X > SPAWN_POS_X)
            {
                return new Vector3(posX + RANGE_POS_X, 0, transform.position.z);
            }
        }
        return new Vector3(SPAWN_POS_X, 0, transform.position.z);
    }
}
