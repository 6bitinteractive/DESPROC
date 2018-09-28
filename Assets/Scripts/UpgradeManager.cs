using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public Text PickupSpeed;
    public Text Inventory;
    public Text MovementSpeed;
    public Text Luck;
    public Text Clock;
    public int UpgradeCost = 30;

    private int maxMovementSpeed = 2;
    private int maxPickupSpeed = 5;
    private int maxLuck = 5;
    private int maxClock = 5;

    // Update is called once per frame
    void Update()
    {
        UpdateValues();
    }

    void UpdateValues()
    {
        PickupSpeed.text = "Pickup Speed: " + GlobalData.Instance.PickupSpeed.ToString();
        Inventory.text = "Inventory: " + GlobalData.Instance.InventoryCapacity.ToString();
        MovementSpeed.text = "Movement Speed: " + GlobalData.Instance.MovementSpeed.ToString();
        Luck.text = "Luck: " + GlobalData.Instance.Luck.ToString();
        Clock.text = "Clock: " + GlobalData.Instance.Clock.ToString();
    }

    public void UpgradPickupSpeed()
    {
       // Insert pick up Speed code here
       // GlobalData.Instance.PickupSpeed -= .25f;
    }

    public void UpgradeInventory()
    {
        // Insert upgrade Inventory code here
        // GlobalData.Instance.InventoryCapacity = Mathf.Round(GlobalData.Instance.InventoryCapacity + 5);
    }

    public void UpgradeMovementSpeed()
    {
        // If tortgold is less than 0 or less than the required upgrade cost or exceeded the max possible upgrades return
        if (GlobalData.Instance.Tortgold <= 0 || GlobalData.Instance.Tortgold <= UpgradeCost || maxMovementSpeed <= 0)
        {
            return;
        }

        GlobalData.Instance.MovementSpeed = Mathf.Round(GlobalData.Instance.MovementSpeed + 1);
        GlobalData.Instance.Tortgold -= UpgradeCost;
        maxMovementSpeed--;
    }

    public void UpgradeLuck()
    {
        if (GlobalData.Instance.Tortgold <= 0 || GlobalData.Instance.Tortgold <= UpgradeCost || maxLuck <= 0)
        {
            return;
        }

        GlobalData.Instance.Luck += .5f;
        GlobalData.Instance.Tortgold -= UpgradeCost;
        maxLuck--;
    }

    public void UpgradeClock()
    {
        if (GlobalData.Instance.Tortgold <= 0 || GlobalData.Instance.Tortgold <= UpgradeCost || maxClock <= 0)
        {
            return;
        }

        GlobalData.Instance.Clock += .5f;
        GlobalData.Instance.Tortgold -= UpgradeCost;
        maxClock--;
    }
}
