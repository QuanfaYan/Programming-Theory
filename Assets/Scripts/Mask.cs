using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask : MonoBehaviour
{
    // Start is called before the first frame update
    public float minPosY = 1.1f;
    public float maxPosY = 5.48f;
    private float m_progress = 0.0f;
    private Vector3 curPos;
    public float progress
    {
        get 
        { 
            return m_progress; 
        }

        set
        {
            if(value >= 0.0f && value <= 100.0f)
            {
                if(m_progress < value)
                {
                    /*
                    Vector3 distance = ( value- m_progress) * (maxPosY - minPosY) / 100f * Vector3.up;
                    Debug.Log(distance.ToString());
                    transform.Translate(distance, Space.World);
                    */
                    curPos.y = value / 100f * (maxPosY - minPosY) + minPosY;
                    transform.localPosition = curPos;
                }
                if (value == 100f)
                {
                    CastShadow();
                }
                m_progress = value;
            }
            else
            {
                Debug.Log("Invalid value:" + value);
            }
        }
    }

    void Start()
    {
        curPos = transform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    void CastShadow()
    {
        MeshRenderer[] meshRDs = transform.parent.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer rd in meshRDs)
        {
            rd.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }
    }
}
