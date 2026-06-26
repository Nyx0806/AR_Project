// using UnityEngine;
// using UnityEngine.XR.ARFoundation;
// using UnityEngine.XR.ARSubsystems;

// /// <summary>
// /// Hiển thị trạng thái AR tracking lên màn hình.
// /// Gắn script này lên bất kỳ GameObject nào trong scene.
// /// </summary>
// public class ARDebugUI : MonoBehaviour
// {
//     [SerializeField] private ARTrackedImageManager trackedImageManager;
//     [SerializeField] private ARTrackedImageSpawner spawner;

//     private string _status = "Chưa quét ảnh...";
//     private GUIStyle _style;

//     private void Start()
//     {
//         if (trackedImageManager == null)
//             trackedImageManager = FindFirstObjectByType<ARTrackedImageManager>();
//         if (spawner == null)
//             spawner = FindFirstObjectByType<ARTrackedImageSpawner>();
//     }

//     private void OnGUI()
//     {
//         // Setup style lần đầu
//         if (_style == null)
//         {
//             _style = new GUIStyle(GUI.skin.box);
//             _style.fontSize = Mathf.RoundToInt(Screen.height * 0.035f);
//             _style.alignment = TextAnchor.UpperLeft;
//             _style.normal.textColor = Color.white;
//             _style.padding = new RectOffset(20, 20, 15, 15);
//         }

//         // Thu thập thông tin
//         string trackingInfo = "❌ Không thấy ảnh";
//         Color boxColor = new Color(0.8f, 0.1f, 0.1f, 0.85f);

//         if (trackedImageManager != null)
//         {
//             foreach (var image in trackedImageManager.trackables)
//             {
//                 if (image.trackingState == TrackingState.Tracking)
//                 {
//                     trackingInfo = $"✅ Đang tracking: {image.referenceImage.name}";
//                     boxColor = new Color(0.1f, 0.6f, 0.1f, 0.85f);
//                 }
//                 else if (image.trackingState == TrackingState.Limited)
//                 {
//                     trackingInfo = $"⚠️ Tracking yếu: {image.referenceImage.name}";
//                     boxColor = new Color(0.8f, 0.5f, 0.0f, 0.85f);
//                 }
//             }
//         }

//         bool modelActive = spawner != null
//                            && spawner.SpawnedModel != null
//                            && spawner.SpawnedModel.activeInHierarchy;

//         string modelInfo  = modelActive ? "✅ Model đã spawn" : "❌ Chưa spawn model";
//         string spawnedPos = (spawner != null && spawner.SpawnedModel != null)
//                             ? $"Vị trí: {spawner.SpawnedModel.transform.position:F2}"
//                             : "";

//         string text = $"{trackingInfo}\n{modelInfo}";
//         if (!string.IsNullOrEmpty(spawnedPos))
//             text += $"\n{spawnedPos}";

//         // Vẽ box debug góc trên bên trái
//         float w = Screen.width * 0.75f;
//         float h = Screen.height * 0.18f;
//         float x = Screen.width * 0.025f;
//         float y = Screen.height * 0.025f;

//         GUI.backgroundColor = boxColor;
//         GUI.Box(new Rect(x, y, w, h), text, _style);
//     }
// }
