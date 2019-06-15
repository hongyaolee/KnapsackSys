/***
*	
*	Title：背包系统
	       PackageSys
*	
*	Description:
*	       角色相关控制
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
	public class Player : MonoBehaviour 
	{
        private static Player instance=null;
        public static Player Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.Find("Player").GetComponent<Player>();
                }
                return instance;
            }
        }
        
#region player property
        private int attack=10;
        public int Attack
        {
            get
            {
                return attack;
            }
            private set
            {
                attack = value;
            }
        }
        private int strength=10;           //力量
        public int Strength
        {
            get { return strength; }
            private set{ strength = value; }
        }
        private int intelligence=10;       //智力
        public int Intelligence
        {
            get
            {
                return intelligence;
            }
            private set { intelligence = value; }
        }
        private int agility=10;            //敏捷
        public int Agility
        {
            get
            {
                return agility;
            }
            private set { agility = value; }
        }
        private int stamina=10;            //体力
        public int Stamina
        {
            get
            {
                return stamina;
            }
            private set { stamina = value; }
        }

        #endregion
        private int coinAmount = 10000;
        public int CoinAmount
        {
            get { return coinAmount; }
            private set { coinAmount = value; }
        }

        private Text txtCoin;
        private void Start()
        {
            txtCoin = GameObject.Find("Coin").GetComponentInChildren<Text>();
            txtCoin.text = CoinAmount.ToString();
        }

        /// <summary>
        /// 消费金币
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public bool ConsumeCoin(int num)
        {
            if (CoinAmount >= num)
            {
                CoinAmount -= num;
                txtCoin.text = CoinAmount.ToString();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取金币
        /// </summary>
        /// <param name="num"></param>
        public void GetCoin(int num)
        {
            CoinAmount += num;
            txtCoin.text = CoinAmount.ToString();
        }

        /// <summary>
        /// 保存角色数据
        /// </summary>
        public void Save()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Attack + "," + Strength + "," + Intelligence + "," + Agility + "," + Stamina + "," + CoinAmount);
            PlayerPrefs.SetString(this.gameObject.name, sb.ToString());
        }
        /// <summary>
        /// 加载角色数据
        /// </summary>
        public void Load()
        {
            if (!PlayerPrefs.HasKey(this.gameObject.name)) return;
            string[] strProperty = PlayerPrefs.GetString(this.gameObject.name).Split(',');
            Attack = int.Parse(strProperty[0]);
            strength = int.Parse(strProperty[1]);
            Intelligence = int.Parse(strProperty[2]);
            Agility = int.Parse(strProperty[3]);
            Stamina= int.Parse(strProperty[4]);
            CoinAmount = int.Parse(strProperty[5]);
        }
    }
}
