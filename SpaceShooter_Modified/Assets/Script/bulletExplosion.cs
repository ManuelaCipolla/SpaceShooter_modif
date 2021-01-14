using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletExplosion : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;

    public Vector3 origin;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (transform.position - origin).normalized;
        transform.position += direction * Time.deltaTime * _speed;
    }
}
