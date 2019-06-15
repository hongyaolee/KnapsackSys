/***
*	
*	Title：背包系统
	       PackageSys
*	
*	Description:
*	       商店面板的物品槽，继承自Slot
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
	public class ShopSlot : Slot 
	{
        /// <summary>
        /// 商店的物品槽不需要与其他类型的物品面板交互
        /// 所以只需要重写OnPointerDown，并不继承base的方法即可
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnPointerDown(PointerEventData eventData)
        {
            //右键购买物品
            if (!InventoryManager.Instance.IsPickedItem && eventData.button == PointerEventData.InputButton.Right && transform.childCount > 0)
            {
                Item item = transform.GetChild(0).GetComponent<ItemUI>().Item;
                ShopPanel.Instance.BuyItem(item);
            }
            //pickedItem不为空时点击左键出售该物品
            if (InventoryManager.Instance.IsPickedItem && eventData.button==PointerEventData.InputButton.Left)
            {
                ShopPanel.Instance.SellItem();
            }
        }

    }
}
