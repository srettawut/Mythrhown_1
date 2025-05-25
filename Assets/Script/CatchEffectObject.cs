using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchEffectObject : MonoBehaviour
{
    public float lifetime = 1f;
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0f, speed*Time.deltaTime, 0f);
        Destroy(gameObject,lifetime);
    }
}
