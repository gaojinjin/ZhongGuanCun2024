using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

namespace UnityEngine.XR.ARFoundation.Samples
{
    /// <summary>
    /// Listens for touch events and performs an AR raycast from the screen touch point.
    /// AR raycasts will only hit detected trackables like feature points and planes.
    ///
    /// If a raycast hits a trackable, the <see cref="placedPrefab"/> is instantiated
    /// and moved to the hit position.
    /// </summary>
    [RequireComponent(typeof(ARRaycastManager))]
    public class PlaceOnPlane : PressInputBase
    {
        [SerializeField]
        [Tooltip("Instantiates this prefab on a plane at the touch location.")]
        GameObject m_PlacedPrefab;

        /// <summary>
        /// The prefab to instantiate on touch.
        /// </summary>
        public GameObject placedPrefab
        {
            get { return m_PlacedPrefab; }
            set { m_PlacedPrefab = value; }
        }

        /// <summary>
        /// The object instantiated as a result of a successful raycast intersection with a plane.
        /// </summary>
        public GameObject spawnedObject { get; private set; }

        bool m_Pressed;

        private bool isMoving = false;
        public Button moveBut;
        protected override void Awake()
        {
            base.Awake();
            m_RaycastManager = GetComponent<ARRaycastManager>();
            //注册按钮事件，点击按钮的时候可以重置位置，否则不可以
            moveBut.onClick.AddListener(()=> {
                isMoving = !isMoving;
            });
            //禁止息屏
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        void Update()
        {

            if (Pointer.current == null || m_Pressed == false)
                return;

            var touchPosition = Pointer.current.position.ReadValue();

            if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
            {
                // Raycast hits are sorted by distance, so the first one
                // will be the closest hit.
                var hitPose = s_Hits[0].pose;

                if (spawnedObject == null)
                {
                    spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
                    //点击地面的时候 隐藏线条现实   set line  enable  false
                }
                else
                {
                    if (isMoving)
                    {
                        spawnedObject.transform.position = hitPose.position;
                    }
                    
                }
            }
        }

        protected override void OnPress(Vector3 position) => m_Pressed = true;

        protected override void OnPressCancel() => m_Pressed = false;

        static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

        ARRaycastManager m_RaycastManager;
    }
}
