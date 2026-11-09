using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class AmputationMinigame : MonoBehaviour
{ 
    public enum Axis { X, Y, Z }
    
    [Header("Saw References")] 
        public Transform currentSawModel;
        public GameObject currentSawVisuals;

    [Header("Sawing settings")] 
            public float strokeDistance = 0.25f;
            public float sinkDepth = 0.1f;
            public float mouseSensitivity = 0.002f;
            public float cutSpeed = 0.4f;
            
    [Header("Direction Fixes")]
    [Tooltip("Check this if the saw goes UP instead of DOWN into the limb!")]
            public bool invertDepth = true; 
            public Axis strokeAxis = Axis.Z;
            public Axis depthAxis = Axis.Y;
            
    /*[Header("Audio / Effects (Optional)")]
            public AudioSource sawAudio;
            public ParticleSystem bloodParticles;*/
    
    public bool isSawing = false;
    private float currentStrokePos = 0f;
    private float cutProgress = 0f;
    private int lastStrokeDirection = 0;
    
    private Action onAmputationComplete;
    
    private Vector3 initialSawLocalPos;

    public void StartSawing(Transform targetAnchor, Action onCompleteCallback)
    {
        if (targetAnchor.childCount > 0)
        {
            currentSawModel = targetAnchor.GetChild(0);
            currentSawVisuals = currentSawModel.gameObject;
            currentSawVisuals.SetActive(true);
        }
        
        initialSawLocalPos = currentSawModel.localPosition;
        
        cutProgress = 0f;
        currentStrokePos = 0f;
        onAmputationComplete = onCompleteCallback;
        lastStrokeDirection  = 0;
        isSawing = true;
        
        if (currentSawVisuals != null) 
            currentSawVisuals.SetActive(true);
        
        Cursor.lockState =  CursorLockMode.Locked;
        Cursor.visible = false;
        
        
    }

    void Update()
    {
        if (!isSawing || currentSawModel == null) return;
        
        float mouseX = Mouse.current.delta.ReadValue().x;
        
        float moveDelta = mouseX * mouseSensitivity;
        float previousStrokePos = currentStrokePos;

        currentStrokePos = Mathf.Clamp(currentStrokePos + moveDelta, -strokeDistance, strokeDistance);

        float actualMove = Mathf.Abs(currentStrokePos - previousStrokePos);

        if (actualMove > 0.0001f)
        {
            cutProgress += actualMove * cutSpeed;
            Debug.Log(cutProgress);
            Camera.main.fieldOfView = Mathf.Lerp(90f, 50f, cutProgress);
        }
        
        float targetDepth = invertDepth ? sinkDepth : -sinkDepth;
        float currentDepth = Mathf.Lerp(0f, targetDepth, cutProgress);
        
        Vector3 offset = Vector3.zero;
        offset = SetAxisValue(offset, strokeAxis, currentStrokePos);
        offset = SetAxisValue(offset, depthAxis, currentDepth);
        
        currentSawModel.localPosition = initialSawLocalPos + offset;
        
        if (cutProgress >= 0.1f)
        {
            CompleteAmputation();
        }
    }
    
    private Vector3 SetAxisValue(Vector3 vector, Axis axis, float value)
    {
        switch (axis)
        {
            case Axis.X: vector.x = value; break;
            case Axis.Y: vector.y = value; break;
            case Axis.Z: vector.z = value; break;
        }
        return vector;
    }

    private void CompleteAmputation()
    {
        isSawing = false;
        
        if (currentSawVisuals != null) currentSawVisuals.SetActive(false);
        
        Camera.main.fieldOfView = 90f;
        
        onAmputationComplete?.Invoke();
        
        Debug.Log("Complete Amputation");
    }
    
}
