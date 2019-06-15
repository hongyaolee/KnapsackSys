/***
*	
*	Title：背包系统
	       PackageSys
*	
*	Description:
*	       装备槽，继承自Slot，单个槽只能存放一件对应种类的装备
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
	public class EquipmentSlot : Slot 
	{
        public EquipmentType equipmentType;
        public WeaponType weaponType;

        /// <summary>
        /// 点击装备槽事件
        /// 1、装备槽不为空
        ///     a、pickedItem有物品
        ///         pickedItem上的物品是当前装备槽对应的装备类型，则交换装备槽和pickedItem的装备
        ///     b、pickedItem没有物品
        ///         卸下装备槽的装备到pickedItem
        /// 2、装备槽为空
        ///     a、pickedItem有物品
        ///         pickedItem上的物品是当前装备槽对应的装备类型，则将pickedItem装备到装备槽
        ///         pickedItem上的物品和当前装备槽的装备类型不一致，则不能交换，不做其他处理
        ///     b、pickedItem没有物品，不做其他操作
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnPointerDown(PointerEventData eventData)
        {
            if (!InventoryManager.Instance.IsPickedItem && eventData.button == PointerEventData.InputButton.Right && transform.childCount > 0)
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();
             
                InventoryManager.Instance.OpenPacksack();
                //是否卸装成功
                bool isUnwield = CharacterPanel.Instance.Unwield(currentItemUI);
                //卸装成功，装备槽中的装备销毁
                if (isUnwield)
                {
                    DestroyImmediate(currentItemUI.gameObject);
                    InventoryManager.Instance.HideToolTip();
                    CharacterPanel.Instance.UpdatePlayerProperty();
                }
                //背包满，无法将卸下的装备存进背包
                else
                {
                    Debug.LogWarning("背包已满，不能卸载装备");
                }
                
                return;
            }
            if (eventData.button != PointerEventData.InputButton.Left) return;
            ItemUI pickedItemUI = InventoryManager.Instance.PickedItem;                         //pickedItemUI
            if (transform.childCount > 0)
            {
                ItemUI equipItemUI = transform.GetChild(0).GetComponent<ItemUI>();              //当前装备槽的ItemUI
                //武器或装备类型
                if (pickedItemUI.Item is Equipment)
                {
                    //装备类型相同,交换
                    if (equipmentType != EquipmentType.None && ((Equipment)pickedItemUI.Item).EquipmentType == equipmentType)
                    {
                        SwapEquipment(pickedItemUI, equipItemUI);
                    }

                }
                else if ( pickedItemUI.Item is Weapon)
                {
                    //武器类型相同，交换
                    if (weaponType != WeaponType.None && ((Weapon)pickedItemUI.Item).WeaponType == weaponType)
                    {
                        SwapEquipment(pickedItemUI, equipItemUI);
                    }
                }
                //卸下装备
                if (InventoryManager.Instance.IsPickedItem==false)
                {
                    InventoryManager.Instance.SetPickedItem(equipItemUI);
                    Destroy(equipItemUI.gameObject);
                }
            }
            else
            {
                //pickedItem是装备或者武器
                if (pickedItemUI.Item is Equipment && equipmentType == ((Equipment)pickedItemUI.Item).EquipmentType
                    || pickedItemUI.Item is Weapon && weaponType == ((Weapon)pickedItemUI.Item).WeaponType)
                {
                    //物品槽的处理
                    StoreItemFormPickedItem(pickedItemUI.Item, pickedItemUI.Amount);
                    //pickedItem的处理
                    InventoryManager.Instance.PickedItemPanelHide();
                }
                else
                {
                    return;
                }
            }
            //更新属性面板
            CharacterPanel.Instance.UpdatePlayerProperty();
        }
        /// <summary>
        /// 交换同类型装备（pickedItem和装备槽）
        /// </summary>
        /// <param name="itemUI1"></param>
        /// <param name="itemUI2"></param>
        private void SwapEquipment(ItemUI itemUI1, ItemUI itemUI2)
        {
            Item item = itemUI1.Item;
            int amount = itemUI1.Amount;
            itemUI1.SetItemUI(itemUI2.Item, itemUI2.Amount);
            itemUI2.SetItemUI(item, amount);
        }
    }
}
