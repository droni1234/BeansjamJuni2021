using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Pausable))]
public class Interactor : MonoBehaviour
{
    public float actionProgressPerSecond = 1.0f;
    public Transform pickupPosition;
    public Slider progressSlider;
    public GameObject ratDead;

    private Interactable _currentInteractable;
    private GameObject _carry;
    private Pausable _pause;

    void Start()
    {
        _pause = GetComponent<Pausable>();
    }

    void Update()
    {
        if (_pause.Paused)
        {
            return;
        }
        
        if (Input.GetButtonDown("Cancel"))
        {
            return;
        }

        if (Input.GetKey(KeyCode.Space) && _currentInteractable)
        {

            SurgeryTable surgeryTable = _currentInteractable.GetComponent<SurgeryTable>();
            TeslaCoil teslaCoil = _currentInteractable.GetComponent<TeslaCoil>();
            Rat rat = _currentInteractable.GetComponent<Rat>();
            SmartRats smartRats = _currentInteractable.GetComponent<SmartRats>();
            LeverLogic leverLogic = _currentInteractable.GetComponent<LeverLogic>();
            Spawn bodyPartsSpawn = _currentInteractable.GetComponent<Spawn>();

            if (rat)
            {
                Instantiate(ratDead, rat.transform.position, Quaternion.identity, rat.transform.parent);
                Destroy(rat.gameObject);
            }
            else if (smartRats)
            {
                Instantiate(ratDead, smartRats.transform.position, Quaternion.identity);
                Destroy(smartRats.gameObject);
            }
            else if (_currentInteractable.pickup)
            {
                DestroyCarriedBodyPart();

                _carry = Instantiate(
                    _currentInteractable.pickup,
                    pickupPosition.position,
                    _currentInteractable.pickup.transform.rotation,
                    transform
                );

                _carry.transform.localScale = _currentInteractable.pickup.transform.localScale;
            }
            else if (surgeryTable && _carry)
            {
                var bodyPart = _carry.GetComponent<BodyPart>();
                if (!bodyPart)
                {
                    return;
                }

                surgeryTable.AddPart(bodyPart);
                DestroyCarriedBodyPart();
                _carry = null;
            }
            else if (teslaCoil)
            {
                surgeryTable = FindObjectOfType<SurgeryTable>();
                teslaCoil.Zap(surgeryTable);
            }

            if (leverLogic)
            {
                leverLogic.PullLever();
            }

            if (bodyPartsSpawn)
            {
                bodyPartsSpawn.CleanUp();
            }
        }
    }

    private void DestroyCarriedBodyPart()
    {
        var bodyPart = GetComponentInChildren<BodyPart>();
        if (bodyPart)
        {
            Destroy(bodyPart.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.name);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        _currentInteractable = other.GetComponent<Interactable>();
    }
}