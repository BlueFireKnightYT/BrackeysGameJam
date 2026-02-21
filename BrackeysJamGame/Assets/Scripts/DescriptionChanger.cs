using TMPro;
using UnityEngine;

using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

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
        text.text = structure.structureDescription;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.text = null;
    }
}