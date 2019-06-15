/***
*	
*	Title：背包系统
	       PackageSys
*	
*	Description:
*	       仓库基类，背包、箱子等仓库类型都继承自此类
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
using System.Text;

namespace PackageSys
{
	public class Inventory : MonoBehaviour 
	{
        public Slot[] slotList;
        private Button btnClose;

        public virtual void Start()
        {
            btnClose = gameObject.transform.Find("btn_Close").GetComponent<Button>();
            btnClose.onClick.AddListener(Close);

            slotList=GetComponentsInChildren<Slot>();
        }

        /// <summary>
        /// 存储物品
        /// </summary>
        /// <param name="id"></param>
        public bool StoreItem(int id)
        {
            Item item = InventoryManager.Instance.GetItemByID(id);
            return StoreItem(item);
        }

        public bool StoreItem(Item item)
        {
            Slot slot;
            if (item == null)
            {
                Debug.LogWarning("存储的物品id不存在");
                return false;
            }
            //容量为1，表示一个物品槽只能存放一个该物品，如武器装备类物品
            if (item.Capacity == 1)
            {
                slot = FindEmptySlot();
            }
            //容量不为1，表示一个物品槽可以存放多个该物品，如消耗品、材料
            else
            {
                slot = FindTheSameSlot(item);

            }
            if (slot == null)
            {
                Debug.LogWarning("背包已满，不能再放入物品");
                return false;
            }
            else
            {
                slot.StoreItem(item);
            }
            return true;
        }

        /// <summary>
        /// 查找空的物品槽
        /// </summary>
        /// <returns></returns>
        public Slot FindEmptySlot()
        {
            foreach (Slot slot in slotList)
            {
                if (slot.transform.childCount == 0)
                {
                    return slot;
                }
            }
            return null;
        }

        /// <summary>
        /// 查找存有相同item的物品槽，
        /// 找到则返回该slot，
        /// 没找到则返回一个空的物品槽
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Slot FindTheSameSlot(Item item)
        {

            foreach (Slot slot in slotList)
            {
                if (slot.transform.childCount >= 1 && item.Id == slot.GetItemID() && !slot.IsSlotFull())
                {
                    return slot;
                }
            }
            return FindEmptySlot();
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
        public void Open()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// 保存
        /// 1、物品id
        /// 2、物品数量amount
        /// 3、物品所在格子位置
        /// </summary>
        public void Save()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Slot slot in slotList)
            {
                if (slot.transform.childCount > 0)
                {
                    ItemUI itemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
                    sb.Append(itemUI.Item.Id + "," + itemUI.Amount + "-");
                }
                else
                {
                    sb.Append("0-");
                }
            }
            PlayerPrefs.SetString(this.gameObject.name, sb.ToString());
        }
        /// <summary>
        /// 加载
        /// </summary>
        public void Load()
        {
            if (!PlayerPrefs.HasKey(this.gameObject.name)) return;
            string strInventory = PlayerPrefs.GetString(this.gameObject.name);
            string[] strSlots = strInventory.Split('-');
            for (int i = 0; i < strSlots.Length-1; i++)
            {
                //该位置不为空
                if (strSlots[i] != "0")
                {
                    //解析每一个slot数据项
                    string[] strItems = strSlots[i].Split(',');
                    int id = int.Parse(strItems[0]);
                    Item item = InventoryManager.Instance.GetItemByID(id);
                    int amount = int.Parse(strItems[1]);
                    //当前该slot有数据，将其修改为加载出来的数据
                    if (slotList[i].transform.childCount > 0)
                    {
                        ItemUI itemUI = slotList[i].transform.GetChild(0).GetComponent<ItemUI>();
                        itemUI.SetItemUI(item, amount);
                    }
                    //当前slot没有数据，直接将加载出来的数据存进去
                    else
                    {
                        for (int j = 0; j < amount; j++)
                        {
                            slotList[i].StoreItemByID(id);
                        }
                    }
                }
                //该位置为空
                else
                {
                    //如果该位置有其他数据，则将该位置置空
                    if (slotList[i].transform.childCount > 0)
                    {
                        DestroyImmediate(slotList[i].transform.GetChild(0).gameObject);
                    }
                    
                }
            }
        }

    }

    
}
