/*****************************************************************************
// File Name : Comments.cs
// Author : John P. Doran
// Creation Date : February 19, 2020
//
// Brief Description : This is a sample document that teaches students how to
comment. Students have to follow this commenting style
exactly so that they don't get points deducted.
*****************************************************************************/
using UnityEngine;
using UnityEngine.InputSystem;

public class SlimeResize : MonoBehaviour
{
    private Rigidbody slimeRb;
    private Vector3 currentPlayerScale;
    private Vector3 targetScale;
    [SerializeField] private float currentTotalSlimeMass;
    [SerializeField] private float currentSlimeMassX;
    [SerializeField] private float currentSlimeMassY;
    [SerializeField] private float currentSlimeMassZ;
    private InputAction increaseXScale;
    private InputAction increaseYScale;
    private InputAction increaseZScale;
    private InputAction decreaseXScale;
    private InputAction decreaseYScale;
    private InputAction decreaseZScale;
    private InputAction resetAllScale;
    [SerializeField] private float xScaleMax;
    [SerializeField] private float yScaleMax;
    [SerializeField] private float zScaleMax;
    [SerializeField] private float xScaleMin;
    [SerializeField] private float yScaleMin;
    [SerializeField] private float zScaleMin;
    [SerializeField] private float scaleIncreaseAmnt;
    [SerializeField] private float scaleDecreaseAmnt;
    private float totalSlimeMassMin;
    private float totalSlimeMassMax;
    [SerializeField] private float slimeMassMaxX;
    [SerializeField] private float slimeMassMinX;
    [SerializeField] private float slimeMassMaxY;
    [SerializeField] private float slimeMassMinY;
    [SerializeField] private float slimeMassMaxZ;
    [SerializeField] private float slimeMassMinZ;
    [SerializeField] private float massChangeAmnt;
    [SerializeField] private float originalSlimeMass;
    
    //DO NOT TOUCH
    private Vector3 velocity;

    private void Awake()
    {
        //Increase scale inputs
        increaseXScale = InputSystem.actions.FindAction("IncreaseScaleX");
        increaseYScale = InputSystem.actions.FindAction("IncreaseScaleY");
        increaseZScale = InputSystem.actions.FindAction("IncreaseScaleZ");

        //Decrease scale inputs
        decreaseXScale = InputSystem.actions.FindAction("DecreaseScaleX");
        decreaseYScale = InputSystem.actions.FindAction("DecreaseScaleY");
        decreaseZScale = InputSystem.actions.FindAction("DecreaseScaleZ");

        //reset scale inputs
        resetAllScale = InputSystem.actions.FindAction("Jump");

        increaseXScale.performed += IncreaseXScalePerformed;
        increaseYScale.performed += IncreaseYScalePerformed;
        increaseZScale.performed += IncreaseZScalePerformed;
        decreaseXScale.performed += DecreaseXScalePerformed;
        decreaseYScale.performed += DecreaseYScalePerformed;
        decreaseZScale.performed += DecreaseZScalePerformed;
        resetAllScale.performed += ResetAllScalePerformed;
    }

    void Start()
    {
        slimeRb = GetComponent<Rigidbody>();
        currentPlayerScale = transform.localScale;
        currentTotalSlimeMass = slimeRb.mass;
        currentSlimeMassZ = 1.0f;
        currentSlimeMassY = 1.0f;
        currentSlimeMassX = 1.0f;
        targetScale = new Vector3(1.5f, 1.5f, 1.5f);

        
    }

    private void DecreaseZScalePerformed(InputAction.CallbackContext obj)
    {
        if (currentPlayerScale.z >= zScaleMin)
        {
            targetScale = new Vector3(currentPlayerScale.x, currentPlayerScale.y, currentPlayerScale.z *
                scaleDecreaseAmnt);

            currentSlimeMassZ -= massChangeAmnt;

            currentTotalSlimeMass = currentSlimeMassZ + currentSlimeMassX + currentSlimeMassY;
        }
        else if (currentPlayerScale.z < zScaleMin)
        {
            currentPlayerScale.z = zScaleMin;
            currentTotalSlimeMass = slimeMassMinZ + currentSlimeMassX + currentSlimeMassY;
        }
    }

    private void DecreaseYScalePerformed(InputAction.CallbackContext obj)
    {
        if (currentPlayerScale.y >= yScaleMin)
        {
            targetScale = new Vector3(currentPlayerScale.x, currentPlayerScale.y * scaleDecreaseAmnt,
                currentPlayerScale.z);

            currentSlimeMassY -= massChangeAmnt;

            currentTotalSlimeMass = currentSlimeMassZ + currentSlimeMassX + currentSlimeMassY;
        }
        else if (currentPlayerScale.y < yScaleMin)
        {
            currentPlayerScale.y = yScaleMin;
            currentTotalSlimeMass = currentSlimeMassZ + currentSlimeMassX + slimeMassMinY;
        }
    }

    private void DecreaseXScalePerformed(InputAction.CallbackContext obj)
    {
        if (currentPlayerScale.x >= xScaleMin)
        {
            targetScale = new Vector3(currentPlayerScale.x * scaleDecreaseAmnt, currentPlayerScale.y,
                currentPlayerScale.z);

            currentSlimeMassX -= massChangeAmnt;

            currentTotalSlimeMass = currentSlimeMassZ + currentSlimeMassX + currentSlimeMassY;
        }
        else if (currentPlayerScale.x < xScaleMin)
        {
            currentPlayerScale.x = xScaleMin;

            currentTotalSlimeMass = currentSlimeMassZ + slimeMassMinX + currentSlimeMassY;
        }
    }

    private void ResetAllScalePerformed(InputAction.CallbackContext obj)
    {
        targetScale = new Vector3(1.5f, 1.5f, 1.5f);
        currentTotalSlimeMass = originalSlimeMass;
        currentSlimeMassY = 1;
        currentSlimeMassX = 1;
        currentSlimeMassZ = 1;
    }

    private void IncreaseZScalePerformed(InputAction.CallbackContext obj)
    {
        if (currentPlayerScale.z <= zScaleMax)
        {
            targetScale = new Vector3(currentPlayerScale.x, currentPlayerScale.y, currentPlayerScale.z *
                scaleIncreaseAmnt);

            currentSlimeMassZ += massChangeAmnt;

            currentTotalSlimeMass = currentSlimeMassZ + currentSlimeMassX + currentSlimeMassY;
        }
        else if (currentPlayerScale.z > zScaleMax)
        {
            currentPlayerScale.z = zScaleMax;

            currentTotalSlimeMass = slimeMassMaxZ + currentSlimeMassX + currentSlimeMassY;
        }
    }

    private void IncreaseYScalePerformed(InputAction.CallbackContext obj)
    {
        if (currentPlayerScale.y <= yScaleMax)
        {
            targetScale = new Vector3(currentPlayerScale.x, currentPlayerScale.y * scaleIncreaseAmnt,
                currentPlayerScale.z);

            currentSlimeMassY += massChangeAmnt;

            currentTotalSlimeMass = currentSlimeMassZ + currentSlimeMassX + currentSlimeMassY;
        }
        else if (currentPlayerScale.y > yScaleMax)
        {
            currentPlayerScale.y = yScaleMax;

            currentTotalSlimeMass = currentSlimeMassZ + currentSlimeMassX + slimeMassMaxY;
        }
    }

    private void IncreaseXScalePerformed(InputAction.CallbackContext obj)
    {
        if (currentPlayerScale.x <= xScaleMax)
        {
            targetScale = new Vector3(currentPlayerScale.x * scaleIncreaseAmnt, currentPlayerScale.y,
                currentPlayerScale.z);

            currentSlimeMassX += massChangeAmnt;

            currentTotalSlimeMass = currentSlimeMassZ + currentSlimeMassX + currentSlimeMassY;
        }
        else if (currentPlayerScale.z >  xScaleMax)
        {
            currentPlayerScale.x = xScaleMax;

            currentTotalSlimeMass = currentSlimeMassZ + slimeMassMaxX + currentSlimeMassY;
        }
    }

    private void FixedUpdate()
    {
        transform.localScale = Vector3.SmoothDamp(transform.localScale, targetScale, ref velocity, 0.5f);
    }

    void Update()
    {
        totalSlimeMassMin = slimeMassMinX + slimeMassMinY + slimeMassMinZ;
        totalSlimeMassMax = slimeMassMaxX + slimeMassMaxY + slimeMassMaxZ;

        transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x, xScaleMin, xScaleMax), 
            Mathf.Clamp(transform.localScale.y, yScaleMin, yScaleMax), Mathf.Clamp(transform.localScale.z, zScaleMin,
            zScaleMax));
        slimeRb.mass = Mathf.Clamp(slimeRb.mass, totalSlimeMassMin, totalSlimeMassMax);
        currentTotalSlimeMass = Mathf.Clamp(currentTotalSlimeMass, totalSlimeMassMin, totalSlimeMassMax);
        currentSlimeMassX = Mathf.Clamp(currentSlimeMassX, slimeMassMinX, slimeMassMaxX);
        currentSlimeMassY = Mathf.Clamp(currentSlimeMassY, slimeMassMinY, slimeMassMaxY);
        currentSlimeMassZ = Mathf.Clamp(currentSlimeMassZ, slimeMassMinZ, slimeMassMaxZ);
        currentPlayerScale = transform.localScale;
        slimeRb.mass = currentTotalSlimeMass;

    }

    public void ResetSlimeScaleAndMass()
    {
        targetScale = new Vector3(1.5f, 1.5f, 1.5f);
        currentTotalSlimeMass = originalSlimeMass;
        currentSlimeMassY = 1;
        currentSlimeMassX = 1;
        currentSlimeMassZ = 1;
    }

    private void OnDestroy()
    {
        increaseXScale.performed -= IncreaseXScalePerformed;
        increaseYScale.performed -= IncreaseYScalePerformed;
        increaseZScale.performed -= IncreaseZScalePerformed;
        decreaseXScale.performed -= DecreaseXScalePerformed;
        decreaseYScale.performed -= DecreaseYScalePerformed;
        decreaseZScale.performed -= DecreaseZScalePerformed;
        resetAllScale.performed -= ResetAllScalePerformed;
    }
}
