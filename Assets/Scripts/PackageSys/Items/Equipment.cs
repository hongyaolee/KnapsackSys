/***
*	
*	Title：背包系统
	       PackageSys
*	
*	Description:
*	       装备类
*
*	Author:hongyaolee
*
*	Date:2019.6
*
*	Version:1.0
***/

namespace PackageSys
{
    /// <summary>
    /// 装备枚举类型
    /// </summary>
    public enum EquipmentType
    {
        None=0,
        Helmet=1,         //头盔
        Pendant=2,        //挂饰
        Shoulder=3,       //肩膀
        Clothes=4,        //上衣
        Ring=5,           //指环
        Belt=6,           //腰带
        Pants=7,          //裤子
        Boots=8,          //靴子
        OffHand=9,        //副手
        
    }

    public class Equipment : Item
    {
        private int strength;           //力量
        public int Strength
        {
            get { return strength; }
            set { strength = value; }
        }
        private int intelligence;       //智力
        public int Intelligence
        {
            get
            {
                return intelligence;
            }

            set
            {
                intelligence = value;
            }
        }
        private int agility;            //敏捷
        public int Agility
        {
            get
            {
                return agility;
            }

            set
            {
                agility = value;
            }
        }
        private int stamina;            //体力
        public int Stamina
        {
            get
            {
                return stamina;
            }

            set
            {
                stamina = value;
            }
        }

        public EquipmentType EquipmentType { get; set; }
        
        public Equipment(int id, string name, int capacity, int buy, int sell, string des, ItemType item, Quality quality,string sprite,
            int strength,int intelligence,int agility,int stamina,EquipmentType equipmentType) : base(id, name, capacity, buy, sell, des, item, quality,sprite)
        {
            this.Strength = strength;
            this.Intelligence = intelligence;
            this.Agility = agility;
            this.Stamina = stamina;
            this.EquipmentType = equipmentType;
            
        }
        /// <summary>
        /// 获取tooltip显示的信息
        /// </summary>
        /// <returns></returns>
        public override string GetTextInToolTip()
        {
            string baseText = base.GetTextInToolTip();
            string strEquipmentType = "";
            switch (EquipmentType)
            {
                case EquipmentType.Helmet:
                    strEquipmentType = "头盔";
                    break;
                case EquipmentType.Pendant:
                    strEquipmentType = "挂饰";
                    break;
                case EquipmentType.Shoulder:
                    strEquipmentType = "护肩";
                    break;
                case EquipmentType.Clothes:
                    strEquipmentType = "上衣";
                    break;
                case EquipmentType.Ring:
                    strEquipmentType = "指环";
                    break;
                case EquipmentType.Belt:
                    strEquipmentType = "腰带";
                    break;
                case EquipmentType.Pants:
                    strEquipmentType = "裤子";
                    break;
                case EquipmentType.Boots:
                    strEquipmentType = "靴子";
                    break;
                case EquipmentType.OffHand:
                    strEquipmentType = "副手";
                    break;
                default:
                    break;
            }
            string displayText = string.Format("<color=white>{0}\n力量:{1}\n智力:{2}\n敏捷:{3}\n体力:{4}\n装备类型:{5}</color>", baseText, this.Strength,this.Intelligence,this.Agility,this.Stamina,strEquipmentType);
            return displayText;
        }
    }
}
