using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Video;

public class MultipleImageTracker : MonoBehaviour
{
    private ARTrackedImageManager trackedImageManager;
    public VideoPlayer video1 = new VideoPlayer();

    [SerializeField]
    private GameObject[] placeablePrefabs;

    private Dictionary<string, GameObject> spawnedObjects;

    private void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
        spawnedObjects = new Dictionary<string, GameObject>();

        foreach (GameObject obj in placeablePrefabs)
        {
            GameObject newObject = Instantiate(obj);
            newObject.name = obj.name;
            newObject.SetActive(false);

            spawnedObjects.Add(newObject.name, newObject);
        }
    }
    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImageChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImageChanged;
    }

    void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateSpawnObject(trackedImage);
            GetComponent<VideoPlayer>().SetDirectAudioVolume(0, 1);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateSpawnObject(trackedImage);
            GetComponent<VideoPlayer>().SetDirectAudioVolume(0, 1);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            //spawnedObjects[trackedImage.referenceImage.name].SetActive(false);
            //spawnedObjects.Remove(trackedImage.referenceImage.name);
            Destroy(spawnedObjects[trackedImage.referenceImage.name]);
            GetComponent<VideoPlayer>().SetDirectAudioVolume(0, 0);
            //spawnedObjects[trackedImage.referenceImage.name] = video1.SetDirectAudioVolume(0, 0);
            GameObject.Find(spawnedObjects[trackedImage.referenceImage.name]).GetComponent<VideoPlayer>().SetDirectAudioVolume(0, 0);
        }
    }

    void UpdateSpawnObject(ARTrackedImage trackedImage)
    {
        string referenceImageName = trackedImage.referenceImage.name;

        spawnedObjects[referenceImageName].transform.position = trackedImage.transform.position;
        spawnedObjects[referenceImageName].transform.rotation = trackedImage.transform.rotation;

        spawnedObjects[referenceImageName].SetActive(true);
    }

    void Start()
    {
        
    }

    void Update()
    {
        Debug.Log($"There are {trackedImageManager.trackables.count} images being tracked");

        foreach(var tarckedImage in trackedImageManager.trackables)
        {
            Debug.Log($"Image: {tarckedImage.referenceImage.name} is at" +
                        $"{trackedImageManager.transform.position}");
        }

    }
}
