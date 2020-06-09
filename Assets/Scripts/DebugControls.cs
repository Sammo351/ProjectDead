using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugControls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Y))
            Time.timeScale = Mathf.Clamp(Time.timeScale + 0.1f, 0f, 1f);
        
        

        if (Input.GetKey(KeyCode.T))
            Time.timeScale = Mathf.Clamp(Time.timeScale - 0.1f, 0.1f, 1f);


    }
}
