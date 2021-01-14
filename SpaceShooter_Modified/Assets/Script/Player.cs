using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //this variable contains the speed of the player
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private GameObject _tripleShotPrefab; 

    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleShotActive = false;
    
    // Start is called before the first frame update
    void Start()
    {
        //Vector3(float x, y, z) in our case Vector3(0,0,0)
        transform.position = new Vector3(0, 0, 0);
        //store the spawn manager inside. In order to find the spawn we can use the name of the object
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        //check if the SpawnManager is empty
        if(_spawnManager == null)
        {
            //if empty then show the below error
            Debug.LogError("The Spawn Manager is null trottola");
        }
    }


    void Update()
    {
        CalculateMovement();

        //Time.time = 0 _canFire = -1 (when we start the game)
        // Time.time = 0.5 _canFire = -1 (right after shooting laser)
        // Time.time = 0.5 _canFire = 0.5 ()
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); //cuz it updates bitch
        float verticalInput = Input.GetAxis("Vertical");
        //(x,y,z)
        //Vector3(1,0,0) 1* fps 0* fps 0*fps = 1* 1sec 0* 1sec 0* 1sec 1m/s (this with deltatime)
        //Vector3(1,0,0) 1 * horizontalInput( -1 -1) * _speed * Time.deltaTime (for horizontal)
        //transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime); _not code anymore
        //vector3(0,1,0) 1y * verticalInput (-1 -1) * _speed * Time.deltaTime (for vertical)
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime); _not code anymore
        //transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime); //for both x and y_ not code anymore
        //the more we go the cleaner it gets, now we got a variable down there and the cleaner string
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        
        //all this to not have the player exit the camera view
        //>= is greater or equal
        /*
        if(transform.position.y >= 0)
        {
            //stop player from goin higher, writin transform.position.x is for the x position to be modified
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        //if player y positiong is <= (smaller or equal) to -3.8f
        else if(transform.position.y <= -3.8f)
        {
            //stop player from going lower than -3.8f
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }
        */
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f,0),0);

        // > is bigger than 
        if(transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        // < is smaller than 
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        Debug.Log("it alive!");
        _canFire = Time.time + _fireRate;
        if(_isTripleShotActive == true) //powerup
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            //create a clone on the player's position using the default rotation
            Instantiate(_laser, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        }


    }
    
    public void Damage()
    {
        //-lives = _lives -1
        //_lives--;
        _lives -= 1;

        if(_lives < 1)
        {
            _spawnManager.OnPlayerDeath(); 
            Destroy(this.gameObject);

        }
    }
        public void ActiveTripleShot()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _isTripleShotActive = false;

    }
}
