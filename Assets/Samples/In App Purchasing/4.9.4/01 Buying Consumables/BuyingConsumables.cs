using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.UI;

namespace Samples.Purchasing.Core.BuyingConsumables
{
    public class BuyingConsumables : MonoBehaviour, IDetailedStoreListener
    {
        IStoreController m_StoreController; // The Unity Purchasing system.

        [Header("In App Ids")]
        //Your products IDs. They should match the ids of your products in your store.
        public string NoAdsId;
        public string UnlockAllId;

        [Header("In App Buttons")]
        public Button m_NoAds;
        public Button m_UnlockAll;
        

        void Start()
        {
            InitializePurchasing();
            m_NoAds.onClick.AddListener(BuyNoAds);
            m_UnlockAll.onClick.AddListener(BuyUnloackAll);
        }

        void InitializePurchasing()
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            //Add products that will be purchasable and indicate its type.
            builder.AddProduct(NoAdsId, ProductType.Consumable);
            builder.AddProduct(UnlockAllId, ProductType.Consumable);

            UnityPurchasing.Initialize(this, builder);
        }

        public void BuyNoAds()
        {
            m_StoreController.InitiatePurchase(NoAdsId);
        }

        public void BuyUnloackAll()
        {
            m_StoreController.InitiatePurchase(UnlockAllId);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.Log("In-App Purchasing successfully initialized");
            m_StoreController = controller;
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            OnInitializeFailed(error, null);
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            var errorMessage = $"Purchasing failed to initialize. Reason: {error}.";

            if (message != null)
            {
                errorMessage += $" More details: {message}";
            }

            Debug.Log(errorMessage);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            //Retrieve the purchased product
            var product = args.purchasedProduct;

            //Add the purchased product to the players inventory
            if (product.definition.id == NoAdsId)
            {
                NoAds();
            }
            else if (product.definition.id == UnlockAllId)
            {
                UnlockAll();
            }

            Debug.Log($"Purchase Complete - Product: {product.definition.id}");

            //We return Complete, informing IAP that the processing on our side is done and the transaction can be closed.
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
            Debug.Log($"Purchase failed - Product: '{product.definition.id}'," +
                $" Purchase failure reason: {failureDescription.reason}," +
                $" Purchase failure details: {failureDescription.message}");
        }

        void NoAds()
        {
            GameEvents.NoAds.Raise();
            
        }

        void UnlockAll()
        {
            GameEvents.UnlockAll.Raise();
        }


    }
}
