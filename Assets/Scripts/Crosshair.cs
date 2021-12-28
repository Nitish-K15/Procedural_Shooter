using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private Image image;
    private Interactable currentObject = null;
    private RaycastHit hitinfo;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        Ray rayOrigin = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        //Debug.DrawRay(Camera.current.transform.position, Camera.current.transform.forward);
        if (Physics.Raycast(rayOrigin, out hitinfo))
        {
            Interactable i = hitinfo.collider.GetComponent<Interactable>();
            if (i != currentObject)
            {
                currentObject = i;
            }

            if (currentObject != null)
            {
                if (currentObject.isActive(playerTransform))
                {
                    image.color = Color.green;
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        currentObject.Interact();
                        currentObject = null;
                    }
                }
                else
                {
                    image.color = Color.white;
                }
            }
            else
            {
                image.color = Color.white;
                currentObject = null;
            }
        }
    }
}
