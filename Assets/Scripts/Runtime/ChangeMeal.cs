using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UnityEngine.XR.ARFoundation.Samples
{
    public class ChangeMeal : MonoBehaviour
    {
        // Define Buttons and their corresponding prefabs
        public Button PhoButton;
        public Button PokeBowlButton;
        public GameObject backButton;
        public GameObject addButton;
        public GameObject pho;
        public GameObject pokebowl;
        public GameObject NewMealPrefab;
        public GameObject homeScreen;
        public bool ARStarted;
        public bool destroyMeal;
        private PlaceMeal placeMeal;

        public void UpdateMealToPho()
        {
            
            homeScreen.SetActive(false);
            NewMealPrefab = pho;
            NewMealPrefab.tag = "NewMealPrefab";
            Invoke("WaitAndInitializeAR", 1f);
        }

        public void UpdateMealToPokeBowl()
        {
            
            homeScreen.SetActive(false);
            NewMealPrefab = pokebowl;
            NewMealPrefab.tag = "NewMealPrefab";
            Invoke("WaitAndInitializeAR", 1f);

        }

        public void ReturnToMenu()
        {
            homeScreen.SetActive(true);
            backButton.SetActive(false);
            addButton.SetActive(false);
            ARStarted = false;
            destroyMeal = true;
            //SceneManager.LoadScene("ARMenu");
        }
        public void AddItems()
        {
            homeScreen.SetActive(true);
            backButton.SetActive(false);
            addButton.SetActive(false);
            ARStarted = false;
        }

        void WaitAndInitializeAR()
        {
            print("activating AR");
            ARStarted = true;
            backButton.SetActive(true);
            addButton.SetActive(true);
        }

        void Start()
        {
            
            // Add listeners to the TextMeshPro buttons to update the prefab
            PhoButton.GetComponent<Button>().onClick.AddListener(UpdateMealToPho);
            PokeBowlButton.GetComponent<Button>().onClick.AddListener(UpdateMealToPokeBowl);
            
        }

    }
}
