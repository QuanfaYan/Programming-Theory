using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricLines;

public class Laser : VolumetricLineBehavior
{
    // Start is called before the first frame update
    public float laserLength;
    public float increaseRate;
    private Vector3 currentEndPos;
    

    // Update is called once per frame
    void Update()
    {
        if(Application.isPlaying)
        {
            Shot();
        }

        if (transform.hasChanged)
        {
            UpdateLineScale();
            UpdateBounds();
        }        
    }

    private void Shot()
    {
        if (EndPos.z < laserLength)
        {
            currentEndPos = EndPos;
            currentEndPos.z += increaseRate * Time.deltaTime;
            EndPos = currentEndPos;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
