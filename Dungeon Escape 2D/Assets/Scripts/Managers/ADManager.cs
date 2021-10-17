using UnityEngine.Advertisements;
using UnityEngine;

namespace DCode
{
    public class ADManager : MonoBehaviour
    {
        public bool testMode = false;
        string dungeonEscapeAndroidID = "4056477";
        const string rewardedVideoPlacement = "rewardedVideo";

        ShowOptions options = new ShowOptions();

        private void Start()
        {
            Advertisement.Initialize(dungeonEscapeAndroidID, testMode);
        }

        public void ShowRewardedAD()
        {

            if (Advertisement.isInitialized && Advertisement.IsReady(rewardedVideoPlacement)) 
            {
                options.resultCallback = HandleShowResultCallback;
                Advertisement.Show(rewardedVideoPlacement , options);
            }
        }

        public void HandleShowResultCallback(ShowResult result)
        {
            switch (result)
            {
                case ShowResult.Finished:
                    GameManager.Instance.player.AddGems(100);
                    UIManager.Instance.UpdateShopGemCount(GameManager.Instance.player.Diamonds);
                    break;
            }
        }
    }

}