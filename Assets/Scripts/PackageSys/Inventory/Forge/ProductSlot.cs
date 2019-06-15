/***
*	
*	Title：背包系统
	       PackageSys
*	
*	Description:
*	       锻造系统成品槽，物品合成后显示在这里
*
*	Author:hongyaolee
*
*	Date:2019.6
*
*	Version:1.0
***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PackageSys
{
	public class ProductSlot : Slot 
	{
        public override void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;
            if (transform.childCount > 0)
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();
                //将当前所在的槽对应的item赋值给pickedItem
                if (!InventoryManager.Instance.IsPickedItem)
                {
                    InventoryManager.Instance.SetPickedItem(currentItemUI);
                    Destroy(transform.GetChild(0).gameObject);
                }
            }
        }

    }
}
