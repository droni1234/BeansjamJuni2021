using TMPro;
using UnityEngine;

[RequireComponent(typeof(Pausable))]
public class TeslaCoil : MonoBehaviour
{
    public SurgeryTable surgeryTable;
    public float energyPercentage = 30.0f;
    public float energyPercentagePerSecond = 1.0f;
    public float overChargePercentage = 120.0f;
    public float overChargeMinus = 40.0f;

    public TextMeshPro txt;

    private bool _won = false;
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
        if (!_won && surgeryTable)
        {
            _won = surgeryTable.Zap(energyPercentage);
            energyPercentage = 0.0f;
        }
    }
}
