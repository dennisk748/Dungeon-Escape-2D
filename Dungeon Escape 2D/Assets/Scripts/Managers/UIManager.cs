using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DCode
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager _instance;

        public static UIManager Instance { get { return _instance; } }

        public Text ShopGemCountstext;
        public Text HUDGemCountText;
        public Image selectionImage;
        public Image[] lifeUnits;

        public int Gems;

        private void Awake()
        {
            if(_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        public void UpdateSelectedItem(int ypos)
        {
            selectionImage.rectTransform.anchoredPosition = new Vector2(selectionImage.rectTransform.anchoredPosition.x, ypos);
        }

        public void UpdateShopGemCount(int gems)
        {
            Gems = gems;
            ShopGemCountstext.text = "" + gems + " G";
        }
        public void HUDGemCount(int gems)
        {
            HUDGemCountText.text = "" + gems + " G";
        }
        public void UIHealth(int lives)
        {
            for(int i = 0; i <= lives; i++)
            {
                if(i == lives)
                {
                    lifeUnits[i].enabled = false;
                }
            }
        }
    }

}