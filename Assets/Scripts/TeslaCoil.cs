using TMPro;
using UnityEngine;

public class TeslaCoil : MonoBehaviour
{
    public SurgeryTable surgeryTable;
    public float energyPercentage = 30.0f;
    public float energyPercentagePerSecond = 1.0f;
    public float overChargePercentage = 120.0f;
    public float overChargeMinus = 40.0f;

    public TextMeshPro txt;
    
    void Start()
    {
    }

    void Update()
    {
        if (energyPercentage < overChargePercentage)
        {
            energyPercentage += energyPercentagePerSecond * Time.deltaTime;
        }
        else
        {
            surgeryTable.ZapRandom();
            energyPercentage -= overChargeMinus;
        }

        txt.text = energyPercentage.ToString("0.0") + "%";
    }

    public void Zap(SurgeryTable surgeryTable)
    {
        if (surgeryTable)
        {
            surgeryTable.Zap(energyPercentage);
            energyPercentage = 0.0f;
        }
    }
}
