using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeStructure", menuName = "Scriptable Objects/UpgradeStructure")]
public class UpgradeStructure : ScriptableObject
{
    public string structureName;
    public GameObject structurePrefab;
    public int structureID;
    public bool isUpgrade;
    public int basePrice;
    public int realPrice;
    public int unlockAmount;
    public Sprite upgradeImage;
    public string structureDescription;
}
