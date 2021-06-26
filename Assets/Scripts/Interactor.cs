using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
    public float actionProgressPerSecond = 1.0f;
    public Transform pickupPosition;
    public Slider progressSlider;

    private float _progress = 0.0f;
    private GameObject _currentInteractable;
    private GameObject _carry;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (_progress <= 0.0f)
        {
            _progress += actionProgressPerSecond * Time.deltaTime;
        }
        
        if (_progress <= 0.05f)
        {
            progressSlider.transform.gameObject.SetActive(false);
        }
        else
        {
            progressSlider.transform.gameObject.SetActive(true);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (!_currentInteractable)
            {
                return;
            }

            var interactable = _currentInteractable.GetComponent<Interactable>();
            if (!interactable)
            {
                return;
            }

            _progress += actionProgressPerSecond * Time.deltaTime;
            progressSlider.value = _progress / interactable.actionCost;

            if (_progress < interactable.actionCost)
            {
                return;
            }

            var surgeryTable = _currentInteractable.GetComponent<SurgeryTable>();
            var teslaCoil = _currentInteractable.GetComponent<TeslaCoil>();
            var rat = _currentInteractable.GetComponent<Rat>();
            if (rat)
            {
                Destroy(rat.gameObject);
                _progress = 0.0f;
            }
            else if (interactable.pickup)
            {
                DestroyCarriedBodyPart();

                _carry = Instantiate(
                    interactable.pickup,
                    pickupPosition.position,
                    interactable.pickup.transform.rotation,
                    transform
                );

                _carry.transform.localScale = interactable.pickup.transform.localScale;
                _progress = -0.25f;
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
                _progress = -0.25f;
            }
            else if (teslaCoil)
            {
                surgeryTable = FindObjectOfType<SurgeryTable>();
                teslaCoil.Zap(surgeryTable);
                _progress = -0.25f;
            }
        }
        else
        {
            _progress = 0.0f;
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
        _currentInteractable = other.gameObject;
    }
}