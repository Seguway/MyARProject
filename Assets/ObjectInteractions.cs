using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class RAMAndMOBOTracker : MonoBehaviour
{
    public GameObject RAM;
    public GameObject GPU;
    public GameObject CPU;
    public GameObject M2;
    public GameObject MOBO;
    public GameObject RAMImageTarget;
    public GameObject SecondRAMImageTarget;
    public GameObject GPUImageTarget;
    public GameObject ThirdGPUImageTarget;
    public GameObject CPUImageTarget;
    public GameObject FourthCPUImageTarget;
    public GameObject M2ImageTarget;
    public GameObject FifthM2ImageTarget;
    public Button rotateButton; 
    public float snapDistance = 0.3f;
    public float sizeChangeDistance = 0.15f;
    public float moveSpeed = 3f;


    private bool rotateMode = false;
    private bool isRAMTargetFound = false;
    private bool isGPUTargetFound = false;
    private bool isCPUTargetFound = false;
    private bool isM2TargetFound = false;
    private Vector3 originalRAMLocalPosition;
    private Vector3 originalRAMLocalScale;
    private Quaternion originalRAMLocalRotation;
    private Transform originalRAMParent;
    private Quaternion originalGPULocalRotation;
    private Vector3 originalGPULocalPosition;
    private Transform originalGPUParent;
    private Quaternion originalCPULocalRotation;
    private Vector3 originalCPULocalPosition;
    private Transform originalCPUParent;
    private Quaternion originalM2LocalRotation;
    private Vector3 originalM2LocalPosition;
    private Transform originalM2Parent;
    private GameObject selectedObject;

    void Start()
    {
        originalRAMLocalPosition = RAM.transform.localPosition;
        originalRAMLocalScale = RAM.transform.localScale;
        originalRAMLocalRotation = RAM.transform.localRotation;
        originalRAMParent = RAM.transform.parent;

        originalGPULocalRotation = GPU.transform.localRotation;
        originalGPULocalPosition = GPU.transform.localPosition;
        originalGPUParent = GPU.transform.parent;

        originalCPULocalRotation = CPU.transform.localRotation;
        originalCPULocalPosition = CPU.transform.localPosition;
        originalCPUParent = CPU.transform.parent;

        originalM2LocalRotation = M2.transform.localRotation;
        originalM2LocalPosition = M2.transform.localPosition;
        originalM2Parent = M2.transform.parent;

        rotateButton.gameObject.SetActive(false); // Initially hide the button
        rotateButton.onClick.AddListener(ToggleRotateMode); // Add listener to the button
        
    }

    void Update()
    {
        // RAM Movement Logic
      if (isRAMTargetFound && RAM != null && RAMImageTarget != null && SecondRAMImageTarget != null)
        {
            float ramDistance = Vector3.Distance(RAMImageTarget.transform.position, SecondRAMImageTarget.transform.position);

            if (ramDistance <= snapDistance && ramDistance > sizeChangeDistance) // Modified Condition
            {
                RAM.transform.localPosition = Vector3.Lerp(RAM.transform.localPosition, new Vector3(RAM.transform.localPosition.x, 0.2f, RAM.transform.localPosition.z), Time.deltaTime * moveSpeed);
                rotateButton.gameObject.SetActive(true);
                selectedObject = RAM;

                RAM.transform.SetParent(RAMImageTarget.transform);
                RAM.transform.localScale = originalRAMLocalScale;
                RAM.transform.localRotation = originalRAMLocalRotation;
            }
            else if (ramDistance <= sizeChangeDistance)
            {
                RAM.transform.SetParent(SecondRAMImageTarget.transform);
                RAM.transform.localPosition = Vector3.Lerp(RAM.transform.localPosition, Vector3.zero, Time.deltaTime * moveSpeed);
                RAM.transform.localScale = Vector3.Lerp(RAM.transform.localScale, Vector3.one * 1.5f, Time.deltaTime * moveSpeed);
                RAM.transform.localRotation = Quaternion.Lerp(RAM.transform.localRotation, Quaternion.Euler(-90, 180, 0), Time.deltaTime * moveSpeed);
                rotateButton.gameObject.SetActive(true);
                selectedObject = RAM;
            }
            else
            {
                RAM.transform.localPosition = Vector3.Lerp(RAM.transform.localPosition, originalRAMLocalPosition, Time.deltaTime * moveSpeed);
                RAM.transform.localScale = Vector3.Lerp(RAM.transform.localScale, originalRAMLocalScale, Time.deltaTime * moveSpeed);
                RAM.transform.localRotation = Quaternion.Lerp(RAM.transform.localRotation, originalRAMLocalRotation, Time.deltaTime * moveSpeed);
                RAM.transform.SetParent(originalRAMParent);
                if (!rotateMode) {
                    rotateButton.gameObject.SetActive(false);
                    selectedObject = null;
                }
            }
        }
        else
        {
            rotateButton.gameObject.SetActive(false);
            selectedObject = null;
        }


        // GPU Movement Logic
         if (isGPUTargetFound && GPU != null && GPUImageTarget != null && ThirdGPUImageTarget != null)
        {
            float gpuDistance = Vector3.Distance(GPUImageTarget.transform.position, ThirdGPUImageTarget.transform.position);

            if (gpuDistance <= snapDistance && gpuDistance > sizeChangeDistance) // Modified Condition
            {
                GPU.transform.localPosition = Vector3.Lerp(GPU.transform.localPosition, new Vector3(GPU.transform.localPosition.x, 0.2f, GPU.transform.localPosition.z), Time.deltaTime * moveSpeed);
                GPU.transform.localRotation = Quaternion.Lerp(GPU.transform.localRotation, Quaternion.Euler(90, 0, 0), Time.deltaTime * moveSpeed);
                rotateButton.gameObject.SetActive(true);
                selectedObject = GPU;

                GPU.transform.SetParent(originalGPUParent);
                GPU.transform.localRotation = Quaternion.Lerp(GPU.transform.localRotation, originalGPULocalRotation, Time.deltaTime * moveSpeed);
                GPU.transform.localPosition = Vector3.Lerp(GPU.transform.localPosition, originalGPULocalPosition, Time.deltaTime * moveSpeed);
            }
            else if (gpuDistance <= sizeChangeDistance)
            {
                GPU.transform.SetParent(ThirdGPUImageTarget.transform);
                GPU.transform.localRotation = Quaternion.Lerp(GPU.transform.localRotation, Quaternion.Euler(90, 0, 0), Time.deltaTime * moveSpeed);
                GPU.transform.localPosition = Vector3.Lerp(GPU.transform.localPosition, Vector3.zero, Time.deltaTime * moveSpeed);
                rotateButton.gameObject.SetActive(true);
                selectedObject = GPU;
            }
            else
            {
                GPU.transform.SetParent(originalGPUParent);
                GPU.transform.localRotation = Quaternion.Lerp(GPU.transform.localRotation, originalGPULocalRotation, Time.deltaTime * moveSpeed);
                GPU.transform.localPosition = Vector3.Lerp(GPU.transform.localPosition, originalGPULocalPosition, Time.deltaTime * moveSpeed);
                if (!rotateMode) {
                    rotateButton.gameObject.SetActive(false);
                    selectedObject = null;
                }
            }
        }
        else
        {
            rotateButton.gameObject.SetActive(false);
            selectedObject = null;
        }

        // CPU Movement Logic (Revised)
        if (isCPUTargetFound && CPU != null && CPUImageTarget != null && FourthCPUImageTarget != null)
        {
            float cpuDistance = Vector3.Distance(CPUImageTarget.transform.position, FourthCPUImageTarget.transform.position);

            if (cpuDistance <= snapDistance && cpuDistance > sizeChangeDistance) // Modified Condition
            {
                CPU.transform.localPosition = Vector3.Lerp(CPU.transform.localPosition, new Vector3(CPU.transform.localPosition.x, 0.2f, CPU.transform.localPosition.z), Time.deltaTime * moveSpeed);
                CPU.transform.localRotation = Quaternion.Lerp(CPU.transform.localRotation, Quaternion.Euler(90, 0, 0), Time.deltaTime * moveSpeed);
                rotateButton.gameObject.SetActive(true);
                selectedObject = CPU;

                CPU.transform.SetParent(originalCPUParent);
                CPU.transform.localRotation = Quaternion.Lerp(CPU.transform.localRotation, originalCPULocalRotation, Time.deltaTime * moveSpeed);
                CPU.transform.localPosition = Vector3.Lerp(CPU.transform.localPosition, originalCPULocalPosition, Time.deltaTime * moveSpeed);
                CPU.transform.localScale = Vector3.Lerp(CPU.transform.localScale, Vector3.one, Time.deltaTime * moveSpeed);
            }
            else if (cpuDistance <= sizeChangeDistance)
            {
                CPU.transform.SetParent(FourthCPUImageTarget.transform);
                CPU.transform.localRotation = Quaternion.Lerp(CPU.transform.localRotation, Quaternion.Euler(90, 0, 0), Time.deltaTime * moveSpeed);
                CPU.transform.localPosition = Vector3.Lerp(CPU.transform.localPosition, Vector3.zero, Time.deltaTime * moveSpeed);
                CPU.transform.localScale = Vector3.Lerp(CPU.transform.localScale, Vector3.one * 3f, Time.deltaTime * moveSpeed);
                rotateButton.gameObject.SetActive(true);
                selectedObject = CPU;
            }
            else
            {
                CPU.transform.SetParent(originalCPUParent);
                CPU.transform.localRotation = Quaternion.Lerp(CPU.transform.localRotation, originalCPULocalRotation, Time.deltaTime * moveSpeed);
                CPU.transform.localPosition = Vector3.Lerp(CPU.transform.localPosition, originalCPULocalPosition, Time.deltaTime * moveSpeed);
                CPU.transform.localScale = Vector3.Lerp(CPU.transform.localScale, Vector3.one, Time.deltaTime * moveSpeed);
                if (!rotateMode) {
                    rotateButton.gameObject.SetActive(false);
                    selectedObject = null;
                }
            }
        }
        else
        {
            rotateButton.gameObject.SetActive(false);
            selectedObject = null;
        }

         if (isM2TargetFound && M2 != null && M2ImageTarget != null && FifthM2ImageTarget != null)
        {
            float m2Distance = Vector3.Distance(M2ImageTarget.transform.position, FifthM2ImageTarget.transform.position);

            if (m2Distance <= snapDistance && m2Distance > sizeChangeDistance)
            {
                M2.transform.localPosition = Vector3.Lerp(M2.transform.localPosition, new Vector3(M2.transform.localPosition.x, 0.2f, M2.transform.localPosition.z), Time.deltaTime * moveSpeed);
                M2.transform.localRotation = Quaternion.Lerp(M2.transform.localRotation, Quaternion.Euler(90, 0, 0), Time.deltaTime * moveSpeed);
            }
            else if (m2Distance <= sizeChangeDistance)
            {
                M2.transform.SetParent(FifthM2ImageTarget.transform);
                M2.transform.localRotation = Quaternion.Lerp(M2.transform.localRotation, Quaternion.Euler(-180, 180, 0), Time.deltaTime * moveSpeed);
                M2.transform.localPosition = Vector3.Lerp(M2.transform.localPosition, Vector3.zero, Time.deltaTime * moveSpeed);
                M2.transform.localScale = Vector3.Lerp(M2.transform.localScale, Vector3.one * 3f, Time.deltaTime * moveSpeed);

                rotateButton.gameObject.SetActive(true); // Show the button
                selectedObject = M2; // Set object to rotate
            }
            else if(!rotateMode)
            {
                M2.transform.SetParent(originalM2Parent);
                M2.transform.localRotation = Quaternion.Lerp(M2.transform.localRotation, originalM2LocalRotation, Time.deltaTime * moveSpeed);
                M2.transform.localPosition = Vector3.Lerp(M2.transform.localPosition, originalM2LocalPosition, Time.deltaTime * moveSpeed);
                M2.transform.localScale = Vector3.Lerp(M2.transform.localScale, Vector3.one, Time.deltaTime * moveSpeed);

                rotateButton.gameObject.SetActive(false); // Hide the button
                selectedObject = null;
            } else {
                rotateButton.gameObject.SetActive(true);
                selectedObject = M2;
            }
            
        } else {
            rotateButton.gameObject.SetActive(false);
            selectedObject = null;
        }

        if (rotateMode && selectedObject != null)
        {
            float rotationX = Input.GetAxis("Mouse X") * 5f;
            float rotationY = Input.GetAxis("Mouse Y") * 5f;
            selectedObject.transform.Rotate(Vector3.up, -rotationX);
            selectedObject.transform.Rotate(Vector3.right, rotationY);
        }
    }

    void ToggleRotateMode()
    {
        rotateMode = !rotateMode;
    }

    // RAM Target Found/Lost Events
    public void OnRAMTargetFound() { isRAMTargetFound = true; }
    public void OnRAMTargetLost()
    {
        isRAMTargetFound = false;
        RAM.transform.localPosition = originalRAMLocalPosition;
        RAM.transform.localScale = originalRAMLocalScale;
        RAM.transform.localRotation = originalRAMLocalRotation;
        RAM.transform.SetParent(originalRAMParent);
    }

    // GPU Target Found/Lost Events
    public void OnGPUTargetFound() { isGPUTargetFound = true; }
    public void OnGPUTargetLost()
    {
        isGPUTargetFound = false;
        GPU.transform.localRotation = originalGPULocalRotation;
        GPU.transform.localPosition = originalGPULocalPosition;
        GPU.transform.SetParent(originalGPUParent);
    }

    // CPU Target Found/Lost Events
    public void OnCPUTargetFound() { isCPUTargetFound = true; }
    public void OnCPUTargetLost()
    {
        isCPUTargetFound = false;
        CPU.transform.localRotation = originalCPULocalRotation;
        CPU.transform.localPosition = originalCPULocalPosition;
        CPU.transform.SetParent(originalCPUParent);
    }

    public void OnM2TargetFound() { isM2TargetFound = true; }
    public void OnM2TargetLost()
    {
        isM2TargetFound = false;
        M2.transform.localRotation = originalM2LocalRotation;
        M2.transform.localPosition = originalM2LocalPosition;
        M2.transform.SetParent(originalM2Parent);
        M2.transform.localScale = Vector3.one;
    }
}