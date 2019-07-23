using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{

    public Scrollbar _thrust;
    public Button _fireButton;
    public GameObject _heli;
    public GameObject _missile;
    public Transform _spawnPos;

    private bool _spawned; //flag

    public float deltaTime = 0.0f;
    public float fps = 0.0f;
    public Text _fpcCounter;
    void Start()
    {
        _spawned = false;
        Button _btn2 = _fireButton.GetComponent<Button>();
        _btn2.onClick.AddListener(_firing);
    }

    void Update()
    {
        deltaTime += Time.deltaTime;
        deltaTime /= 2.0f;
        fps = 1.0f / deltaTime;

        _fpcCounter.text = "" + fps;

        Debug.Log(fps);
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        Scrollbar _scrl = _thrust.GetComponent<Scrollbar>();

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
        if (_spawned == true && _missile != null)
        {
            //_missile.AddForce(0, -8 * Time.deltaTime, 5 * Time.deltaTime);
        }

        if (_missile != null) // FLAGS FOR PROPPER SPAWNING
        {
            _spawned = true;
        }
        else
        {
            _spawned = false;
        }
    }

    void _firing() //function that instantiates a missile at spawning point
    {
        if (_spawned == false)
        {

            //_missileInst = Instantiate(_missileprefab, _spawnPos.position, _spawnPos.rotation) as Rigidbody;
        }
    }


}
