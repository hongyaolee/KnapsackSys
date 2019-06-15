/***
*	
*	Title：背包系统
	       PackageSys
*	
*	Description:
*	       锻造系统锻造槽，锻造需要的材料放在这里
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
	public class ForgeSlot : Slot 
	{
        /// <summary>
        /// 减少当前slot中物品数量
        /// </summary>
        /// <param name="num"></param>
        public void SubItemAmount(int num)
        {
            
            if (transform.childCount > 0)
            {
                ItemUI itemUI = transform.GetChild(0).GetComponent<ItemUI>();
                itemUI.SubAmount(num);
                //数量减少到0，销毁item
                if (itemUI.Amount <= 0)
                {
                    DestroyImmediate(itemUI.gameObject);
                }
            }
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;
            //物品槽不为空
            if (transform.childCount > 0)
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();
                //将当前所在的槽对应的item赋值给pickedItem
                if (!InventoryManager.Instance.IsPickedItem)
                {
                    InventoryManager.Instance.SetPickedItem(currentItemUI);
                    Destroy(transform.GetChild(0).gameObject);
                }
                //
                else
                {
                    //当前物品槽与pickedItem不同id，交换
                    if (currentItemUI.Item.Id != InventoryManager.Instance.PickedItem.Item.Id)
                    {
                        Item tempItem = currentItemUI.Item;
                        int tempAmount = currentItemUI.Amount;
                        currentItemUI.SetItemUI(InventoryManager.Instance.PickedItem.Item, InventoryManager.Instance.PickedItem.Amount);
                        InventoryManager.Instance.PickedItem.SetItemUI(tempItem, tempAmount);
                    }
                    ////物品槽与pickedItem的id相同，则把pickedItem补充到物品槽，直到物品槽补满
                    //else
                    //{
                    //    ReplenishSlotFormPicked(currentItemUI);
                    //}
                }
            }
            //物品槽为空
            else
            {
                //pickedItem放入到物品槽中
                if (InventoryManager.Instance.IsPickedItem)
                {
                    //物品槽的处理
                    StoreItemFormPickedItem(InventoryManager.Instance.PickedItem.Item, InventoryManager.Instance.PickedItem.Amount);
                    //pickedItem的处理
                    InventoryManager.Instance.PickedItemPanelHide();
                }
            }
        }

    }

   
}
