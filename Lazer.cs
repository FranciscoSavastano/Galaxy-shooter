using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField]
    private int _LazerVel = 8;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _LazerVel * Time.deltaTime);

        if(transform.position.y >= 4)
        {
            DestroyImmediate(this.gameObject);
        }
    }
}
