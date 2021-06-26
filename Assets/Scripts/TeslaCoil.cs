using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TeslaCoil : MonoBehaviour
{
    public float energyPercentage = 30.0f;

    public float energyPercentagePerSecond = 1.0f;

    public TextMeshPro txt;
    
    void Start()
    {
    }

    void Update()
    {
        if (energyPercentage < 100.0f)
        {
            energyPercentage += energyPercentagePerSecond * Time.deltaTime;
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
