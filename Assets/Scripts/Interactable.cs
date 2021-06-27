using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour
{
    public GameObject pickup;

    void Start()
    {
        //if (pickup)
        //{
        //    Instantiate(pickup, transform.position, pickup.transform.rotation, transform);
        //}
    }

    void Update()
    {

    }


    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<Interactor>())
        {

        }
    }

}