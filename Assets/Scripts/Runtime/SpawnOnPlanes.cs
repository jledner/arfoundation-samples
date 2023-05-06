using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARFoundation.Samples;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class SpawnOnPlanes : MonoBehaviour
{
    [SerializeField]
    GameObject PlacedPrefab;
    [SerializeField]
    GameObject MealPrefab;
    GameObject spawnObject;
    GameObject mealObject;
    bool mealPlaced = false;
    private ChangeMeal changeMeal;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    ARRaycastManager m_raycastmanager;

    // Start is called before the first frame update
    void Awake()
    {
        m_raycastmanager = GetComponent<ARRaycastManager>();
    }
    void Start()
    {
        changeMeal = FindObjectOfType<ChangeMeal>();
    }

    bool GetTouch(out Vector2 touch_pos)
    {
        if(Input.touchCount > 0)
        {
            touch_pos = Input.GetTouch(0).position;
            return true;
        }
        touch_pos = default;
        return false;
    }

    public void SpawnPlacemat(Pose hitPose)
    {
        spawnObject = Instantiate(PlacedPrefab, hitPose.position, hitPose.rotation);
        spawnObject.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
    }

    // Update is called once per frame
    void Update()
    {
        if (changeMeal.ARStarted) { 

            if (GetTouch(out Vector2 touch_pos) == false)
            {
                return;
            }

            if (m_raycastmanager.Raycast(touch_pos, hits, TrackableType.AllTypes))
            {
                var hitPose = hits[0].pose;

                if (spawnObject == null)
                {
                    SpawnPlacemat(hitPose);
                
                    //spawnObject = Instantiate(PlacedPrefab, hitPose.position, hitPose.rotation);
                    //spawnObject.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
                }

                    //else
                    //{


                    //    print("checking for placed meal");
                    //    if (mealPlaced)
                    //    {
                    //        // Drop what we're holding
                    //        Destroy(mealObject);
                    //        mealPlaced = false;
                    //    }
                    //    else 
                    //    {
                    //        print(hits[0]);
                    //        print("Trackable tag: " + hits[0].trackable.gameObject.tag);
                    //        print("Trackable name: " + hits[0].trackable.gameObject.name);
                    //        print("Trackable pose: " + hits[0].trackable.transform.position + ", " + hits[0].trackable.gameObject.transform.rotation);

                    //        if (hits[0].trackable.gameObject.CompareTag("PlaceableSurface")) {
                    //            print("Trying to place meal on placeable surface");
                    //            // Place the object
                    //            mealObject = Instantiate(MealPrefab, hits[0].trackable.gameObject.transform);
                    //            //spawnObject.transform.position = hitPose.position;
                    //            mealPlaced = true;
                    //        }

                    //    }
                    //}
                }
        }
    }
}
