/***
*	
*	Title：背包系统
	       PackageSys
*	
*	Description:
*	       物品类基类
*
*	Author:hongyaolee
*
*	Date:2019.6
*
*	Version:1.0
***/
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PackageSys
{
    /// <summary>
    /// 物品类型
    /// </summary>
    public enum ItemType
    {
        Consumable=0,             //消耗品
        Weapon=1,                 //武器
        Equipment=2,              //装备
        Material=3                //材料
    }

    /// <summary>
    /// 装备稀有度
    /// </summary>
    public enum Quality
    {
        Common=0,                 //普通的
        Good=1,                   //良好的
        Rare=2,                   //稀有的
        Epic=3,                   //史诗的
        Legendary=4               //传说的
    }
	public class Item 
	{
        #region 属性
        private int id;         //物品id
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;    //物品名称
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private int capacity;   //单个格子物品容量
        public int Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }
        private int buyPrice;   //购买价格
        public int BuyPrice
        {
            get { return buyPrice; }
            set { buyPrice = value; }
        }
        private int sellPrice;  //销售价格
        public int SellPrice
        {
            get { return sellPrice; }
            set { sellPrice = value; }
        }
        private string description;     //物品描述
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// 物品种类
        /// </summary>
        private ItemType itemType;
        public ItemType ItemType
        {
            get { return itemType; }
            set { itemType = value; }
        }
        /// <summary>
        /// 物品品质
        /// </summary>
        private Quality quality;
        public Quality Quality
        {
            get { return quality; }
            set { quality = value; }
        }
        //物品sprite图标
        private string sprite;
        public string Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }
        #endregion

        public Item()
        {
            this.Id = -1;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">物品id</param>
        /// <param name="name">物品名字</param>
        /// <param name="capacity">格子容量</param>
        /// <param name="buy">购买价格</param>
        /// <param name="sell">销售价格</param>
        /// <param name="des">描述</param>
        /// <param name="item">物品类型</param>
        /// <param name="quality">物品品质</param>
        public Item(int id, string name, int capacity, int buy, int sell, string des, ItemType item, Quality quality,string sprite)
        {
            this.Id = id;
            this.Name = name;
            this.Capacity = capacity;
            this.BuyPrice = buy;
            this.SellPrice = sell;
            this.Description = des;
            this.ItemType = item;
            this.Quality = quality;
            this.Sprite = sprite;
        }

        public virtual string GetTextInToolTip()
        {
            string color="";
            switch (Quality)
            {
                case Quality.Common:
                    color = "white";
                    break;
                case Quality.Good:
                    color = "lime";
                    break;
                case Quality.Rare:
                    color = "blue";
                    break;
                case Quality.Epic:
                    color = "magenta";
                    break;
                case Quality.Legendary:
                    color = "red";
                    break;

            }

            string content = "";
            content = string.Format("<color={4}>{0}\n描述:{1}\n购买:{2}\n出售:{3}</color>", Name, Description, BuyPrice, SellPrice, color);
            return content;
        }
	}
    
}
