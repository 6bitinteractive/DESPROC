using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public Text PickupSpeed;
    public Text MovementSpeed;
    public int UpgradeCost = 30;

    // Update is called once per frame
    void Update()
    {
        UpdateValues();
    }

    void UpdateValues()
    {
        PickupSpeed.text = "Pickup Speed: " + GlobalData.Instance.PickupSpeed.ToString();
        MovementSpeed.text = "Speed: " + GlobalData.Instance.MovementSpeed.ToString();
    }

    
    public void UpgradPickupSpeed()
    {
        /*
        if (GlobalData.Instance.Tortgold <= 0)
        {
            return;
        }

        GlobalData.Instance.MovementSpeed = Mathf.Round(GlobalData.Instance.MovementSpeed + 1);
        GlobalData.Instance.Tortgold -= UpgradeCost;
        */
    }
    

    public void UpgradeMovementSpeed()
    {
        // If tortgold is less than 0 or less than the required upgrade cost return
        if (GlobalData.Instance.Tortgold <= 0 || GlobalData.Instance.Tortgold <= UpgradeCost)
        {
            return;
        }

        GlobalData.Instance.MovementSpeed = Mathf.Round(GlobalData.Instance.MovementSpeed + 1);
        GlobalData.Instance.Tortgold -= UpgradeCost;
    }
}
