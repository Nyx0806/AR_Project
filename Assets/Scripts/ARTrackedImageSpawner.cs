using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// Quét ảnh → spawn model tại vị trí ảnh.
/// Sau khi spawn, model được tách khỏi ảnh → tự do di chuyển độc lập.
/// </summary>
public class ARTrackedImageSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    private GameObject _spawnedModel;
    private ARTrackedImageManager _trackedImageManager;
    private bool _hasSpawned = false;

    private void OnEnable()
    {
        _trackedImageManager = GetComponent<ARTrackedImageManager>();
        _trackedImageManager.trackablesChanged.AddListener(OnImageChanged);
    }

    private void OnDisable()
    {
        _trackedImageManager.trackablesChanged.RemoveListener(OnImageChanged);
    }

    private void OnImageChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        // Chỉ spawn 1 lần duy nhất
        if (_hasSpawned) return;

        foreach (var image in eventArgs.added)
        {
            // Spawn tại đúng vị trí & rotation của ảnh
            _spawnedModel = Instantiate(prefab, image.transform.position, image.transform.rotation);
            
            // KHÔNG set parent → model độc lập hoàn toàn, không bám theo ảnh
            _spawnedModel.SetActive(true);
            _hasSpawned = true;
            break;
        }
    }

    public GameObject SpawnedModel => _spawnedModel;
}