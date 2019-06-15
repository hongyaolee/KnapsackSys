/***
*	
*	Title：背包系统
	       PackageSys
*	
*	Description:
*	       Json的序列化和反序列化
*
*	Author:hongyaolee
*
*	Date:2019.6
*
*	Version:1.0
***/

using System.Collections.Generic;
using System;
using UnityEngine;

namespace PackageSys
{
    [Serializable]
	public class ItemForJson 
	{
        public int Id;
        public string Name;
        public int Capacity;
        public int BuyPrice;
        public int SellPrice;
        public string Description;
        public int ItemType;
        public int Quality;
        public string Sprite;
        public int HP;
        public int MP;
        public int Strength;
        public int Intelligence;
        public int Agility;
        public int Stamina;
        public int Attack;
        public int WeaponType;
        public int EquipmentType;

        public static void JsonParse(List<Item> ItemListAll)
        {
            TextAsset jsonAsset = Resources.Load<TextAsset>("Items");
            ItemList itemlist = JsonUtility.FromJson<ItemList>(jsonAsset.text);

            foreach (var item in itemlist.ItemsList)
            {
                int id = item.Id;
                string name = item.Name;
                int capacity = item.Capacity;
                int buyPrice = item.BuyPrice;
                int sellPrice = item.SellPrice;
                string description = item.Description;
                Quality quality = (Quality)item.Quality;
                string sprite = item.Sprite;
                switch (item.ItemType)
                {
                    case 0:
                        int hp = item.HP;
                        int mp = item.MP;
                        Item ConsumableItem = new Consumable(id, name, capacity, buyPrice, sellPrice, description,(ItemType)item.ItemType, quality,sprite, hp, mp);
                        ItemListAll.Add(ConsumableItem);
                        break;
                    case 1:
                        int attack = item.Attack;
                        WeaponType weaponType=(WeaponType)item.WeaponType;
                        Item WeaponItem = new Weapon(id, name, capacity, buyPrice, sellPrice, description, (ItemType)item.ItemType, quality, attack, weaponType,sprite);
                        ItemListAll.Add(WeaponItem);
                        break;
                    case 2:
                        int strength = item.Strength;
                        int intelligence = item.Intelligence;
                        int agility = item.Agility;
                        int stamina = item.Stamina;
                        EquipmentType equipmentType = (EquipmentType)item.EquipmentType;
                        Item EquipmentItem = new Equipment(id, name, capacity, buyPrice, sellPrice, description, (ItemType)item.ItemType, quality,sprite,strength,intelligence,agility,stamina,equipmentType);
                        ItemListAll.Add(EquipmentItem);
                        break;
                    case 3:
                        Item MaterialItem = new Material(id, name, capacity, buyPrice, sellPrice, description, (ItemType)item.ItemType, quality, sprite);
                        ItemListAll.Add(MaterialItem);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    [Serializable]
    public class ItemList
    {
        public List<ItemForJson> ItemsList = new List<ItemForJson>();
    }


}
