using UnityEngine;
using UnityEngine.UIElements;

public class RadialController : MonoBehaviour
{
    public PanelRenderer panelRenderer;
    public InventoryController inventoryController;
    bool updateData = false;
    ItemInstance[] data = new ItemInstance[8];

    void OnEnable()
    {
        panelRenderer.RegisterUIReloadCallback(OnUIReload);
        updateData = true;
    }

    void OnDisable()
    {
        panelRenderer.UnregisterUIReloadCallback(OnUIReload);
    }

    void OnUIReload(PanelRenderer renderer, VisualElement rootElement, int version)
    {
        if (updateData)
        {
            int i = 0;
            foreach (ItemInstance item in data)
            {
                data[i] = inventoryController.inv.GetItem(i);
                i++;

            }


        }


    }

}
