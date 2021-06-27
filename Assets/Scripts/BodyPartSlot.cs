using UnityEngine;

public class BodyPartSlot : MonoBehaviour
{
    public BodyPart.BodyPartType forType;
    public BodyPart BodyPart => _part;
    
    private BodyPart _part;
    
    void Start()
    {
    }

    void Update()
    {
    }

    public void SetBodyPart(BodyPart bodyPart)
    {
        DestroyChildren();

        _part = Instantiate(
            bodyPart,
            transform.position,
            bodyPart.transform.rotation,
            transform
        );

        _part.transform.localScale = bodyPart.transform.localScale;
    }

    public void ClearSlot()
    {
        _part = null;
        DestroyChildren();
    }
    
    private void DestroyChildren()
    {
        foreach (Transform child in GetComponentInChildren<Transform>())
        {
            Destroy(child.gameObject);
        }
    }

    public bool HasBodyPart()
    {
        return _part != null;
    }
}
