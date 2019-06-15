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
    public enum WeaponType
    {
        None,
        MainHand,       //主手
        OffHand,        //副手
        
    }
    public class Weapon : Item
    {
        private int attack;
        public int Attack
        {
            get
            {
                return attack;
            }

            set
            {
                attack = value;
            }
        }
        
        public WeaponType WeaponType { get; set; }

        public Weapon(int id, string name, int capacity, int buy, int sell, string des, ItemType item, Quality quality,int attack,WeaponType weapontype,string sprite) : base(id, name, capacity, buy, sell, des, item, quality,sprite)
        {
            this.Attack = attack;
            this.WeaponType = weapontype;
        }

        /// <summary>
        /// 获取tooltip显示的信息
        /// </summary>
        /// <returns></returns>
        public override string GetTextInToolTip()
        {
            string baseText = base.GetTextInToolTip();
            string strWeaponType="";
            switch (WeaponType)
            {
                case WeaponType.MainHand:
                    strWeaponType = "主手";
                    break;
                case WeaponType.OffHand:
                    strWeaponType = "副手";
                    break;
            }
            string displayText = string.Format("<color=white>{0}\n攻击力:{1}\n武器类型:{2}</color>", baseText, this.Attack, strWeaponType);
            return displayText;
        }
    }
}
