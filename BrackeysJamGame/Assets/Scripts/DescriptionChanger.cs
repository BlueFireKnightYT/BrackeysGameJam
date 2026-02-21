using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DescriptionChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    UpgradeStructureHolder holder;
    UpgradeStructure structure;
    TextMeshProUGUI text;

    private void Start()
    {
        holder = gameObject.GetComponentInChildren<UpgradeStructureHolder>();
        structure = holder.structure;
        text = holder.descriptionText;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(holder.unlocked)
            text.text = structure.structureDescription;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.text = null;
    }
}