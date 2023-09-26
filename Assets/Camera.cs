using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] Transform playerTransform;

    Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }
    private void Update()
    {
        cam.transform.position = playerTransform.position;
    }
}
