using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    public Scrollbar _thrust;
    public Button _fireButton;
    public GameObject _heli;
    public GameObject _missile;
    public Transform _spawnPos;

    public float deltaTime = 0.0f;
    public Text _fpcCounter;

    private float fps = 0.0f;
    void Start ()
    {
        Button _btn2 = _fireButton.GetComponent<Button>();
        _btn2.onClick.AddListener(_firing);
    }

    void Update()
    {
        deltaTime += Time.deltaTime;
        deltaTime /= 2.0f;
        fps = 1.0f / deltaTime;

        _fpcCounter.text = "" + fps; // Displaying frames per second
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Scrollbar _scrl = _thrust.GetComponent<Scrollbar>();

        // Controlls for heli altitude
        if (_scrl.value > 0.5f) // If the slider is above half
        {
            _heli.GetComponent<Transform>().Translate(0, _scrl.value * Time.deltaTime, 0); // Heli goes up

            if (_scrl.value > 0.9f) // Speeds up the up speed if the slider is almost at the top
            {
                _heli.GetComponent<Transform>().Translate(0, (_scrl.value + 0.5f) * Time.deltaTime, 0); // Heli goes up
            }
        }
        if (_scrl.value < 0.5f && _heli.transform.position.y != -0.01) // If the slider is below half and it's not below the ground
        {
            _heli.GetComponent<Transform>().Translate(0, (_scrl.value + 0.1f) * (-Time.deltaTime), 0); // Heli go down

            if (_scrl.value < 0.1f && _heli.transform.position.y != -0.01) // Speeds up the down speed if the slider is almost at the botttom
            {
                _heli.GetComponent<Transform>().Translate(0, (_scrl.value + 1) * (-Time.deltaTime), 0); // Heli go down
            }
        }
        /*if (_spawned == true && _missile != null)
        {
           // _missile.AddForce(0, -8 * Time.deltaTime, 5 * Time.deltaTime);
        }*/
    }

    void _firing() // Function that instantiates a missile at spawning point.
    {
          _missile.transform.position = _spawnPos.position; // Assign missile a new position in front of the heli.

          _missile.AddComponent<Rigidbody>(); // Add rigidbody to missile to make it fall.
    
          //_missileInst = Instantiate(_missileprefab, _spawnPos.position, _spawnPos.rotation) as Rigidbody;
    }


}
