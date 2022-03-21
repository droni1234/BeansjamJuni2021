using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Pausable))]
public class TeslaCoil : Interactable
{
    public SurgeryTable surgeryTable;
    public float energyPercentage = 30.0f;
    public float energyPercentagePerSecond = 1.0f;
    public float overChargePercentage = 120.0f;
    public float overChargeMinus = 40.0f;

    [SerializeField]
    private Image charge;

    private bool _won = false;
    private Pausable _pause;

    public float coolDown = 10F;

    [SerializeField]
    private Animator animator;

    private float time = 0F;

    private bool isPulled = false;

    private void Update()
    {
        if (GetComponent<Pausable>().Paused)
        {
            return;
        }
        
        if (energyPercentage < overChargePercentage)
        {
            energyPercentage += energyPercentagePerSecond * Time.deltaTime;
        }
        else
        {
            GetComponent<AudioSource>().Play();
            surgeryTable.ZapRandom();
            energyPercentage -= overChargeMinus;
        }

        charge.fillAmount = energyPercentage / overChargePercentage;

        //Pull Cooldown
        if (time > 0F)
        {
            time -= Time.deltaTime;
        }
        else if (isPulled)
        {
            isPulled = false;
            animator.SetBool("isDown", isPulled);
        }
    }

    public void Zap(SurgeryTable surgeryTable)
    {
        if (!_won && surgeryTable)
        {
            _won = surgeryTable.Zap(energyPercentage);
            //energyPercentage = 0.0f;
        }
    }

    public void PullLever()
    {
        isPulled = true;
        animator.SetBool("isDown", isPulled);
        
        if (time > 0F) 
            return;
        
        FindObjectOfType<SpawnMaster>().SpawnBodies();
        time = coolDown;
    }

    public override void Trigger(Interactor interactor)
    {
        surgeryTable = FindObjectOfType<SurgeryTable>();
        Zap(surgeryTable);
    }
}
