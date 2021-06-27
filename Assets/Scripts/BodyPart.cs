using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public enum BodyPartType
    {
        Head,
        Torso,
        LeftArm,
        RightArm,
        LeftLeg,
        RightLeg
    }

    public Quaternion getTableRotation()
    {
        switch (type)
        {
            case BodyPartType.Head:
                return Quaternion.identity;
            case BodyPartType.Torso:
                return Quaternion.identity;
            case BodyPartType.LeftArm:
                return Quaternion.Euler(0F, 0F, 10F);
            case BodyPartType.RightArm:
                return Quaternion.Euler(0F, 0F, -10F);
            case BodyPartType.LeftLeg:
                return Quaternion.Euler(0F, 0F, 10F);
            case BodyPartType.RightLeg:
                return Quaternion.Euler(0F, 0F, -10F);
                
        }
        return Quaternion.identity;
    }

    public BodyPartType type;
}
