/*****************************************************************************
// File Name : SlimeResize.cs
// Author : Will Northrup
// Creation Date : 3/24/2026
//
// Brief Description : This is a script that is attached to the player/slime
game object. This script allows for the player to change the scale and mass 
of the player/slime game object.
*****************************************************************************/
using UnityEngine;
using UnityEngine.InputSystem;

public class SlimeResize : MonoBehaviour
{
    private PlayerMove playerMove;
    private Rigidbody slimeRb;
    private Vector3 currentPlayerScale;
    private Vector3 targetScale;
    [SerializeField] private float currentTotalPlayerSpeed;
    [SerializeField] private float currentPlayerSpeedX;
    [SerializeField] private float currentPlayerSpeedY;
    [SerializeField] private float currentPlayerSpeedZ;
    //[SerializeField] private float playerSpeedMax;
    //[SerializeField] private float playerSpeedMin;
    [SerializeField] private float xPlayerSpeedMax;
    [SerializeField] private float xPlayerSpeedMin;
    [SerializeField] private float yPlayerSpeedMax;
    [SerializeField] private float yPlayerSpeedMin;
    [SerializeField] private float zPlayerSpeedMax;
    [SerializeField] private float zPlayerSpeedMin;
    private float totalPlayerSpeedMin;
    private float totalPlayerSpeedMax;
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
    [SerializeField] private float speedChangeAmnt;
    [SerializeField] private float originalSlimeMass;
    [SerializeField] private float originalPlayerSpeed;
    
    //DO NOT TOUCH
    private Vector3 velocity;

    /// <summary>
    /// Sets the inputs when the scene reloads
    /// </summary>
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

    /// <summary>
    /// Sets the initial slime mass and initial target scale on start
    /// </summary>
    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        slimeRb = GetComponent<Rigidbody>();
        currentPlayerScale = transform.localScale;
        currentTotalSlimeMass = slimeRb.mass;
        currentSlimeMassZ = 1.0f;
        currentSlimeMassY = 1.0f;
        currentSlimeMassX = 1.0f;
        targetScale = new Vector3(1.5f, 1.5f, 1.5f);
        currentTotalPlayerSpeed = playerMove.PlayerSpeed;
        currentPlayerSpeedZ = 7;
        currentPlayerSpeedY = 7;
        currentPlayerSpeedX = 7;
        
    }

    /// <summary>
    /// Decreases slime mass and scale on the Z axis when performed
    /// </summary>
    /// <param name="obj"></param>
    private void DecreaseZScalePerformed(InputAction.CallbackContext obj)
    {
        //Decreases the target scale and mass if it is greater than the scale minimum
        if (currentPlayerScale.z >= zScaleMin)
        {
            targetScale = new Vector3(currentPlayerScale.x, currentPlayerScale.y, currentPlayerScale.z *
                scaleDecreaseAmnt);

            currentSlimeMassZ -= massChangeAmnt;

            currentTotalSlimeMass = currentSlimeMassZ + currentSlimeMassX + currentSlimeMassY;

            currentPlayerSpeedZ -= speedChangeAmnt;

            currentTotalPlayerSpeed = currentPlayerSpeedZ + currentPlayerSpeedX + currentPlayerSpeedY;
        }
        else if (currentPlayerScale.z < zScaleMin)
        {
            currentPlayerScale.z = zScaleMin;
            currentTotalSlimeMass = slimeMassMinZ + currentSlimeMassX + currentSlimeMassY;
            currentTotalPlayerSpeed = zPlayerSpeedMin + currentPlayerSpeedX + currentPlayerSpeedY;
        }
    }

    /// <summary>
    /// Decreases slime mass and scale on the Y axis when performed
    /// </summary>
    /// <param name="obj"></param>
    private void DecreaseYScalePerformed(InputAction.CallbackContext obj)
    {
        //Decreases the target scale and mass if it is greater than the scale minimum
        if (currentPlayerScale.y >= yScaleMin)
        {
            targetScale = new Vector3(currentPlayerScale.x, currentPlayerScale.y * scaleDecreaseAmnt,
                currentPlayerScale.z);

            currentSlimeMassY -= massChangeAmnt;

            currentTotalSlimeMass = currentSlimeMassZ + currentSlimeMassX + currentSlimeMassY;

            currentPlayerSpeedY -= speedChangeAmnt;

            currentTotalPlayerSpeed = currentPlayerSpeedZ + currentPlayerSpeedX + currentPlayerSpeedY;
        }
        else if (currentPlayerScale.y < yScaleMin)
        {
            currentPlayerScale.y = yScaleMin;
            currentTotalSlimeMass = currentSlimeMassZ + currentSlimeMassX + slimeMassMinY;
        }
    }

    /// <summary>
    /// Decreases slime mass and scale on the X axis when performed
    /// </summary>
    /// <param name="obj"></param>
    private void DecreaseXScalePerformed(InputAction.CallbackContext obj)
    {
        //Decreases the target scale and mass if it is greater than the scale minimum
        if (currentPlayerScale.x >= xScaleMin)
        {
            targetScale = new Vector3(currentPlayerScale.x * scaleDecreaseAmnt, currentPlayerScale.y,
                currentPlayerScale.z);

            currentSlimeMassX -= massChangeAmnt;

            currentTotalSlimeMass = currentSlimeMassZ + currentSlimeMassX + currentSlimeMassY;

            currentPlayerSpeedX -= speedChangeAmnt;

            currentTotalPlayerSpeed = currentPlayerSpeedZ + currentPlayerSpeedX + currentPlayerSpeedY;
        }
        else if (currentPlayerScale.x < xScaleMin)
        {
            currentPlayerScale.x = xScaleMin;

            currentTotalSlimeMass = currentSlimeMassZ + slimeMassMinX + currentSlimeMassY;
        }
    }

    /// <summary>
    /// Resets the target scale and slime mass to their default values
    /// </summary>
    /// <param name="obj"></param>
    private void ResetAllScalePerformed(InputAction.CallbackContext obj)
    {
        targetScale = new Vector3(1.5f, 1.5f, 1.5f);
        currentTotalSlimeMass = originalSlimeMass;
        currentSlimeMassY = 1;
        currentSlimeMassX = 1;
        currentSlimeMassZ = 1;
        
        currentPlayerSpeedX = 7;
        currentPlayerSpeedZ = 7;
        currentPlayerSpeedY = 7;
        currentTotalPlayerSpeed = originalPlayerSpeed;
    }

    /// <summary>
    /// Increases slime mass and scale on the Z axis when performed
    /// </summary>
    /// <param name="obj"></param>
    private void IncreaseZScalePerformed(InputAction.CallbackContext obj)
    {
        //Increases the target scale and mass if it is less than the scale maximum
        if (currentPlayerScale.z <= zScaleMax)
        {
            targetScale = new Vector3(currentPlayerScale.x, currentPlayerScale.y, currentPlayerScale.z *
                scaleIncreaseAmnt);

            currentSlimeMassZ += massChangeAmnt;

            currentTotalSlimeMass = currentSlimeMassZ + currentSlimeMassX + currentSlimeMassY;

            currentPlayerSpeedZ += speedChangeAmnt;

            currentTotalPlayerSpeed = currentPlayerSpeedZ + currentPlayerSpeedX + currentPlayerSpeedY;
        }
        else if (currentPlayerScale.z > zScaleMax)
        {
            currentPlayerScale.z = zScaleMax;

            currentTotalSlimeMass = slimeMassMaxZ + currentSlimeMassX + currentSlimeMassY;
        }
    }

    /// <summary>
    /// Increases slime mass and scale on the Y axis when performed
    /// </summary>
    /// <param name="obj"></param>
    private void IncreaseYScalePerformed(InputAction.CallbackContext obj)
    {
        //Increases the target scale and mass if it is less than the scale maximum
        if (currentPlayerScale.y <= yScaleMax)
        {
            targetScale = new Vector3(currentPlayerScale.x, currentPlayerScale.y * scaleIncreaseAmnt,
                currentPlayerScale.z);

            currentSlimeMassY += massChangeAmnt;

            currentTotalSlimeMass = currentSlimeMassZ + currentSlimeMassX + currentSlimeMassY;

            currentPlayerSpeedY += speedChangeAmnt;

            currentTotalPlayerSpeed = currentPlayerSpeedZ + currentPlayerSpeedX + currentPlayerSpeedY;
        }
        else if (currentPlayerScale.y > yScaleMax)
        {
            currentPlayerScale.y = yScaleMax;

            currentTotalSlimeMass = currentSlimeMassZ + currentSlimeMassX + slimeMassMaxY;
        }
    }

    /// <summary>
    /// Increases slime mass and scale on the X axis when performed
    /// </summary>
    /// <param name="obj"></param>
    private void IncreaseXScalePerformed(InputAction.CallbackContext obj)
    {
        //Increases the target scale and mass if it is less than the scale maximum
        if (currentPlayerScale.x <= xScaleMax)
        {
            targetScale = new Vector3(currentPlayerScale.x * scaleIncreaseAmnt, currentPlayerScale.y,
                currentPlayerScale.z);

            currentSlimeMassX += massChangeAmnt;

            currentTotalSlimeMass = currentSlimeMassZ + currentSlimeMassX + currentSlimeMassY;

            currentPlayerSpeedX += speedChangeAmnt;

            currentTotalPlayerSpeed = currentPlayerSpeedZ + currentPlayerSpeedX + currentPlayerSpeedY;
        }
        else if (currentPlayerScale.z >  xScaleMax)
        {
            currentPlayerScale.x = xScaleMax;

            currentTotalSlimeMass = currentSlimeMassZ + slimeMassMaxX + currentSlimeMassY;
        }
    }

    /// <summary>
    /// Allows for the slime scale to smoothly grow and shrink to the target scale
    /// </summary>
    private void FixedUpdate()
    {
        transform.localScale = Vector3.SmoothDamp(transform.localScale, targetScale, ref velocity, 0.5f);
    }

    /// <summary>
    /// Updates the mass and scale values from the performed actions, also clamps the mass and scale of the slime to
    /// not go beyond a specific point
    /// </summary>
    void Update()
    {
        totalSlimeMassMin = slimeMassMinX + slimeMassMinY + slimeMassMinZ;
        totalSlimeMassMax = slimeMassMaxX + slimeMassMaxY + slimeMassMaxZ;

        totalPlayerSpeedMin = xPlayerSpeedMin + yPlayerSpeedMin + zPlayerSpeedMin;
        totalPlayerSpeedMax = xPlayerSpeedMax + yPlayerSpeedMax + zPlayerSpeedMax;

        transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x, xScaleMin, xScaleMax), 
            Mathf.Clamp(transform.localScale.y, yScaleMin, yScaleMax), Mathf.Clamp(transform.localScale.z, zScaleMin,
            zScaleMax));
        slimeRb.mass = Mathf.Clamp(slimeRb.mass, totalSlimeMassMin, totalSlimeMassMax);
        currentTotalSlimeMass = Mathf.Clamp(currentTotalSlimeMass, totalSlimeMassMin, totalSlimeMassMax);
        currentSlimeMassX = Mathf.Clamp(currentSlimeMassX, slimeMassMinX, slimeMassMaxX);
        currentSlimeMassY = Mathf.Clamp(currentSlimeMassY, slimeMassMinY, slimeMassMaxY);
        currentSlimeMassZ = Mathf.Clamp(currentSlimeMassZ, slimeMassMinZ, slimeMassMaxZ);
        currentTotalPlayerSpeed = Mathf.Clamp(currentTotalPlayerSpeed, totalPlayerSpeedMin, totalPlayerSpeedMax);
        currentPlayerSpeedX = Mathf.Clamp(currentPlayerSpeedX, xPlayerSpeedMin, xPlayerSpeedMax);
        currentPlayerSpeedY = Mathf.Clamp(currentPlayerSpeedY, yPlayerSpeedMin, yPlayerSpeedMax);
        currentPlayerSpeedZ = Mathf.Clamp(currentPlayerSpeedZ, zPlayerSpeedMin, zPlayerSpeedMax);
        
        currentPlayerScale = transform.localScale;
        slimeRb.mass = currentTotalSlimeMass;
        playerMove.PlayerSpeed = currentTotalPlayerSpeed;
    }

    /// <summary>
    /// This is a function that resets the slime scale and mass to default values when called
    /// </summary>
    public void ResetSlimeScaleAndMass()
    {
        targetScale = new Vector3(1.5f, 1.5f, 1.5f);
        currentTotalSlimeMass = originalSlimeMass;
        currentSlimeMassY = 1;
        currentSlimeMassX = 1;
        currentSlimeMassZ = 1;

        currentPlayerSpeedX = 7;
        currentPlayerSpeedZ = 7;
        currentPlayerSpeedY = 7;
        currentTotalPlayerSpeed = originalPlayerSpeed;
    }

    /// <summary>
    /// Disconnects the input actions for the scale increases and decreases when destroyed.
    /// </summary>
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
