using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{

    public Transform orintation;

    public KeyCode RightKey = KeyCode.K;
    public KeyCode LeftKey = KeyCode.J;
    public KeyCode ResetKey = KeyCode.L;

    private float RotationSpeed = 0.1f;
    private Quaternion StartOrin ; 

    // Start is called before the first frame update
    void Start()
    {
        StartOrin = orintation.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(RightKey))
        {
            //rotate camera around the player counter clock wise
            orintation.Rotate(0.0f, -RotationSpeed, 0.0f, Space.Self);
        }
        else if (Input.GetKey(LeftKey))
        {
            //rotate camera around the player clock wise
            orintation.Rotate(0.0f, RotationSpeed, 0.0f, Space.Self);
        }
        else if (Input.GetKey(ResetKey))
        {
            //resets rotation
            orintation.rotation = StartOrin;
        }
    }
}
