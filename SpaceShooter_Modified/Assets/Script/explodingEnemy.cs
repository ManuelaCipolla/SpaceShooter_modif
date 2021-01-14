using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explodingEnemy : MonoBehaviour
{
[SerializeField]
private float _speed = 3.5f;

[SerializeField]
private GameObject _bulletExplosion;

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
        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject,2);
        }

        if(other.tag == "Player")
        {
            Vector3 enemyPosition = this.gameObject.transform.position;
            Instantiate(_bulletExplosion, enemyPosition, Quaternion.identity);
            
            /*Player player = other.GetComponent<Player>();
            if(player!= null)
            {
                player.Damage();
            }*/
            Destroy(this.gameObject);
        }
    }
}
