using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DCode
{
    public class Shop : MonoBehaviour
    {
        public GameObject shopPanel;
        Player player;

        
        int Index_Selected;
        int currentSelectedItemPrice;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                player = collision.GetComponent<Player>();
                if(player != null)
                {
                    UIManager.Instance.UpdateShopGemCount(player.Diamonds);
                }
                shopPanel.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                shopPanel.SetActive(false);
            }
        }

        public void BuyItem()
        {
            if(currentSelectedItemPrice == 100)
            {
                GameManager.Instance.hasKey = true;
            }
            if(player.Diamonds >= currentSelectedItemPrice)
            {
                player.Diamonds -= currentSelectedItemPrice;
                shopPanel.SetActive(false);
            }
            else
            {
                Debug.Log("not enough cash");
                shopPanel.SetActive(false);
            }
        }

        public void SelectItem(int index)
        {
            switch (index)
            {
                case 0:
                    UIManager.Instance.UpdateSelectedItem(164);
                    Index_Selected = 0;
                    currentSelectedItemPrice = 300;
                    break;
                case 1:
                    UIManager.Instance.UpdateSelectedItem(6);
                    Index_Selected = 1;
                    currentSelectedItemPrice = 200;
                    break;
                case 2:
                    UIManager.Instance.UpdateSelectedItem(-164);
                    Index_Selected = 2;
                    currentSelectedItemPrice = 100;
                    break;
            }
                
        }
    }
}
