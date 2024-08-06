using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public class CanvasResize : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private CanvasScaler canvasScaler;

        void Awake()
        {
            if (canvasScaler == null)
            {
                canvasScaler = GetComponent<CanvasScaler>();
            }

            canvasScaler.matchWidthOrHeight = cam.aspect < 0.55f ? 0 : 1;
        }
    }
