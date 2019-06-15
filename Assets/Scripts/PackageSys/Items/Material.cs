/***
*	
*	Title：背包系统
	       PackageSys
*	
*	Description:
*	       材料类
*          材料类完全继承Item类，不需要自己特有的字段和方法
*	Author:hongyaolee
*
*	Date:2019.6
*
*	Version:1.0
***/

namespace PackageSys
{

	public class Material : Item 
	{
        public Material() { }
        public Material(int id, string name, int capacity, int buyPrice, int sellPrice, string description, ItemType type, Quality quality, string sprite) 
            : base(id, name, capacity, buyPrice, sellPrice, description, type, quality, sprite)
        { }

	}
}
