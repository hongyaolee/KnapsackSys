/***
*	
*	Title：背包系统
	       PackageSys
*	
*	Description:
*	       物品槽
*	       
*	Author:hongyaolee
*
*	Date:2019.6
*
*	Version:1.0
***/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PackageSys
{
	public class Slot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler
	{
        public GameObject ItemUIPrefab;

        /// <summary>
        /// 把item作为自身子物体
        /// 如果本身已经存在item，则item的amount++
        /// 否则根据itemprefab实例化item，并放在下面
        /// </summary>
        /// <param name="item"></param>
        public void StoreItem(Item item)
        {
            ItemUI itemUI;
            //本身已经存在item
            if (transform.childCount != 0)
            {
                itemUI = transform.GetComponentInChildren<ItemUI>();
                
            }
            //item不存在，先实例化item对象并存到slot子物体中
            else
            {
                GameObject goItemUI = Instantiate(ItemUIPrefab);
                goItemUI.transform.parent = this.transform;
                goItemUI.transform.localPosition = Vector3.zero;
                goItemUI.transform.localScale = Vector3.one;
                itemUI = goItemUI.GetComponent<ItemUI>();
            }

            //更新UI
            itemUI.AddAmount(1);
            itemUI.SetItemUI(item);
        }
        /// <summary>
        /// 根据id放物品
        /// </summary>
        /// <param name="id"></param>
        public void StoreItemByID(int id)
        {
            Item item = InventoryManager.Instance.GetItemByID(id);
            StoreItem(item);
        }

        /// <summary>
        /// pickedItem中的物品放入到空物品槽
        /// </summary>
        /// <param name="item"></param>
        /// <param name="num"></param>
        public void StoreItemFormPickedItem(Item item,int num)
        {
            ItemUI itemUI;
            GameObject goItemUI = Instantiate(ItemUIPrefab);
            goItemUI.transform.parent = this.transform;
            goItemUI.transform.localPosition = Vector3.zero;
            goItemUI.transform.localScale = Vector3.one;
            itemUI = goItemUI.GetComponent<ItemUI>();
            itemUI.SetItemUI(item, num);
        }

        /// <summary>
        /// 获取子物体对象的itemtype
        /// </summary>
        /// <returns></returns>
        public ItemType GetItemType()
        {
            return transform.GetChild(0).GetComponent<ItemUI>().Item.ItemType;
        }
        /// <summary>
        /// 获取子物体对象id
        /// </summary>
        /// <returns></returns>
        public int GetItemID()
        {
            return transform.GetChild(0).GetComponent<ItemUI>().Item.Id;
        }


        /// <summary>
        /// 物品槽中该物品是否已经存满
        /// </summary>
        /// <returns></returns>
        public bool IsSlotFull()
        {
            ItemUI itemUI = transform.GetChild(0).GetComponent<ItemUI>();
            return itemUI.Amount >= itemUI.Item.Capacity;
        }

        /// <summary>
        /// 鼠标进入当前slot，显示tooltip
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (transform.childCount > 0)
            {
                string content = GetComponentInChildren<ItemUI>().Item.GetTextInToolTip();
                InventoryManager.Instance.DisplayToolTip(content);
            }
            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (transform.childCount > 0)
            {
                InventoryManager.Instance.HideToolTip();
            }
            
        }


        /// <summary>
        /// 处理鼠标按下事件
        /// 物品槽为空时，有两种情况：
        ///     1、pickedItem==null，不做任何处理
        ///     2、pickedItem!=null, 将pickedItem中的item放入到该物品槽中
        /// 物品槽不为空，也有两种情况：
        ///     1、pickedItem==null,此时将物品槽中的物品放入pickedItem
        ///     2、pickedItem!=null,此时又可以再分两种情况
        ///         a、pickedItem和物品槽中的item不同，则直接交换当前物品槽和pickedItem对应的物品槽中的item
        ///         b、pickedItem和槽中item相同，若槽中item的数量小于capacity,则从pickedItem中补充槽中item数量，直至补满
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (!InventoryManager.Instance.IsPickedItem && eventData.button == PointerEventData.InputButton.Right && transform.childCount > 0)
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();
                if (currentItemUI.Item is Equipment || currentItemUI.Item is Weapon)
                {
                    InventoryManager.Instance.OpenCharacter();
                    Item item=null;
                    int amount=0;
                    CharacterPanel.Instance.EquipItem(currentItemUI,out item,out amount);
                    //原本装备槽没有装备，则装备后背包中的装备销毁掉
                    if (amount == 0)
                    {
                        Destroy(currentItemUI.gameObject);
                        InventoryManager.Instance.HideToolTip();
                    }
                    //原本装备槽有装备，则与之交换
                    else
                    {
                        currentItemUI.SetItemUI(item, amount);
                    }
                }
                return;
            }
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
                        currentItemUI.SetItemUI(InventoryManager.Instance.PickedItem.Item,InventoryManager.Instance.PickedItem.Amount);
                        InventoryManager.Instance.PickedItem.SetItemUI(tempItem, tempAmount);
                    }
                    //物品槽与pickedItem的id相同，则把pickedItem补充到物品槽，直到物品槽补满
                    else
                    {
                        ReplenishSlotFormPicked(currentItemUI);
                    }
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

        /// <summary>
        /// 物品槽与pickedItem的id相同，则把pickedItem补充到物品槽，直到物品槽补满
        /// </summary>
        /// <param name="num"></param>
        private void ReplenishSlotFormPicked(ItemUI currentItemUI)
        {
            if (currentItemUI.Amount < currentItemUI.Item.Capacity)
            {
                
                //要补充的数量
                int difference = currentItemUI.Item.Capacity - currentItemUI.Amount;
                if (InventoryManager.Instance.PickedItem.Amount >= difference)
                {
                    //当前物品槽数量增加
                    currentItemUI.AddAmount(difference);
                    //pickedItem数量减少
                    InventoryManager.Instance.PickedItem.SubAmount(difference);
                }
                else
                {
                    //当前物品槽数量增加
                    currentItemUI.AddAmount(InventoryManager.Instance.PickedItem.Amount);
                    //隐藏pickedItem
                    InventoryManager.Instance.PickedItemPanelHide();
                }

                    
                
            }
        }
    }
}
