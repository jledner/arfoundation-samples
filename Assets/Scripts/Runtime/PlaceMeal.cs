using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

namespace UnityEngine.XR.ARFoundation.Samples
{
    public class PlaceMeal : MonoBehaviour
    {
        public GameObject focusedObject;
        public float rayLength = 1000f;
        public Color rayColor = Color.white;
        //private LineRenderer lineRenderer;
        public GameObject MealPrefab;
        public GameObject mealObject;
        private ChangeMeal changeMeal;
        public GameObject[] objectsToDelete;

        public bool ARStarted;

        void Start()
        {
            changeMeal = FindObjectOfType<ChangeMeal>();
        }

        public void InstantiateMeal(RaycastHit hit)
        {
            MealPrefab =changeMeal.NewMealPrefab;
            //MealPrefab = GameObject.FindWithTag("NewMealPrefab");
            mealObject = Instantiate(MealPrefab, hit.point, Quaternion.identity);
            mealObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }


        // Update is called once per frame
        void FixedUpdate()
        {
            objectsToDelete = GameObject.FindGameObjectsWithTag("Meal");
            // Compute the player's forward direction
            Vector3 fwd = transform.TransformDirection(Vector3.forward);

            if (changeMeal.destroyMeal == true)
            {
                foreach (GameObject obj in objectsToDelete)
                {
                    print("Attempting to destroy meal object");
                    Destroy(obj);
                }                
                changeMeal.destroyMeal = false;
            }

            // conduct raycast
            if (changeMeal.ARStarted)
            {         
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    print("touch event");
                    // create a ray from the touch point
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    // cast the ray and get the hit result
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, rayLength))
                    {
                        print("Raycast hit detected");
                        focusedObject = hit.collider.gameObject;
                        if (focusedObject.CompareTag("PlaceableSurface"))
                        {
                            print("PlaceableSurface click detected");
                            MealPrefab = changeMeal.NewMealPrefab;
                            mealObject = Instantiate(MealPrefab, hit.point, Quaternion.identity);
                            mealObject.transform.localScale = new Vector3(.2f, .2f, .2f);
                            //mealObject.transform.SetParent(parentTransform);
                            mealObject.tag = "Meal";
                        }

                        Debug.DrawRay(transform.position, ray.direction * hit.distance, Color.yellow);
                    }
                    else
                    {
                        focusedObject = null;
                    }
                }



            }
        }
    }
}
