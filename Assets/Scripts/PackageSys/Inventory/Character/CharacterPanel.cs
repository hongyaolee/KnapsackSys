/***
*	
*	Title：背包系统
	       PackageSys
*	
*	Description:
*	       角色面板，继承自Inventory类
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
using UnityEngine.UI;

namespace PackageSys
{
	public class CharacterPanel : Inventory 
	{
        private static CharacterPanel instance;
        public static CharacterPanel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.Find("Canvas").transform.Find("CharacterPanel").GetComponent<CharacterPanel>();
                }
                return instance;
            }
        }
        private Player player;
        private Text txtPlayerProperty;
        private bool isInit=false;
        //public override void Start()
        //{
        //    base.Start();
            
        //}

        private void OnEnable()
        {
            if (!isInit)
            {
                player = Player.Instance;
                txtPlayerProperty = transform.Find("HeroPropertyPanel/Text").GetComponent<Text>();
            }
           
        }

        /// <summary>
        /// 装备武器或防具
        /// </summary>
        /// <param name="itemUI"></param>
        public void EquipItem(ItemUI itemUI,out Item outItem,out int outAmount)
        {
            foreach (EquipmentSlot slot in slotList)
            {
                if (itemUI.Item is Weapon && slot.weaponType == ((Weapon)(itemUI.Item)).WeaponType||
                    itemUI.Item is Equipment && slot.equipmentType == ((Equipment)(itemUI.Item)).EquipmentType)
                {
                    //当前装备槽已经有装备，则装备后返回装备槽中原来的item
                    if (slot.transform.childCount > 0)
                    {
                        ItemUI currentItemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
                        outItem = currentItemUI.Item;
                        outAmount = currentItemUI.Amount;
                        currentItemUI.SetItemUI(itemUI.Item, itemUI.Amount);
                        //更新属性面板
                        UpdatePlayerProperty();
                        return;
                    }
                    //当前装备槽空，直接装备进去
                    else
                    {
                        slot.StoreItemFormPickedItem(itemUI.Item, itemUI.Amount);
                        outItem = itemUI.Item;
                        outAmount = 0;
                        //更新属性面板
                        UpdatePlayerProperty();
                        return;
                    }
                }
            }
            outItem = itemUI.Item;
            outAmount = 0;
        }

        /// <summary>
        /// 卸下装备
        /// </summary>
        /// <param name="itemUI"></param>
        public bool Unwield(ItemUI itemUI)
        {
            foreach (Slot slot in slotList)
            {
                //slot中没有其他物品，才能往这里存放装备
                if (slot.transform.childCount == 0)
                {
                    KnapsackPanel.Instance.StoreItem(itemUI.Item);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 显示玩家属性
        /// 1、得到玩家数据中的基础属性
        /// 2、遍历装备面板中每一个装备槽，得到每一件装备的加成属性，并与玩家基础属性相加，即得到玩家的属性面板
        /// </summary>
        public void UpdatePlayerProperty()
        {
            int attack= player.Attack, strength= player.Strength, intelligence=player.Intelligence, agility=player.Agility, stamina=player.Stamina;
            foreach (EquipmentSlot slot in slotList)
            {

                //该slot下有装备，得到该装备属性，并加成
                if (slot.transform.childCount > 0)
                {
                    Item item =slot.transform.GetChild(0).GetComponent<ItemUI>().Item;
                    if (item is Equipment)
                    {
                        strength += ((Equipment)item).Strength;
                        intelligence += ((Equipment)item).Intelligence;
                        agility += ((Equipment)item).Agility;
                        stamina += ((Equipment)item).Stamina;
                    }
                    else if (item is Weapon)
                    {
                        attack += ((Weapon)item).Attack;
                    }
                }
            }
            //玩家属性显示到属性面板
            txtPlayerProperty.text = string.Format("属性:\n力量:{0}\n智力:{1}\n敏捷:{2}\n体力:{3}\n攻击力:{4}", 
                                                    strength, intelligence, agility, stamina, attack);
        }
    }
}
