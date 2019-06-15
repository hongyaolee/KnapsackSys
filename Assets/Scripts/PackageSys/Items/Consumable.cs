/***
*	
*	Title：背包系统
	       PackageSys
*	
*	Description:
*	       消耗品类，包括红瓶、蓝瓶
*
*	Author:hongyaolee
*
*	Date:2019.6
*
*	Version:1.0
***/

namespace PackageSys
{
    public class Consumable : Item
    {
        private int hp;
        public int HP
        {
            get { return hp; }
            set { hp = value; }
        }
        private int mp;
        public int MP
        {
            get { return mp; }
            set { mp = value; }
        }

        public Consumable(int id, string name, int capacity, int buy, int sell, string des, ItemType item, Quality quality,string sprite,int hp,int mp) : base(id, name, capacity, buy, sell, des, item, quality,sprite)
        {
            this.HP = hp;
            this.MP = mp;
        }

        /// <summary>
        /// 获取tooltip显示的信息
        /// </summary>
        /// <returns></returns>
        public override string GetTextInToolTip()
        {
            string baseText = base.GetTextInToolTip(); 
            string displayText = string.Format("<color=white>{0}\nHP:{1}\nMP:{2}</color>", baseText, hp,mp);
            return displayText;
        }
    }
}
