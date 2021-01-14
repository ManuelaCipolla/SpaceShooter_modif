using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
private float _speed = 3.5f;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -6f)
        {
            float randomX = Random.Range(-9.2f, 9.2f);
            transform.position = new Vector3 (randomX, 7.5f, 0);

        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Fuck move away " + other.transform.name);
        if(other.tag == "Laser")
        {
            //Detroy laser
            Destroy(other.gameObject);
            //destory enemy
            Destroy(this.gameObject);
        }

        if (other.tag == "Player")
        {
            //damage player, you get the player script(component) so we can access its variables
            Player player = other.GetComponent<Player>();
            // != not equal
            if(player!= null )
            {
            player.Damage();
            }
            
            //Destroy enemy
            Destroy(this.gameObject);
        }
    }
}
