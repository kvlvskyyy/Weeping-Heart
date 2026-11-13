using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class AmputationMinigame : MonoBehaviour
{ 
    public enum Axis { X, Y, Z }
    
    private int lastScreamIndex = -1;
    
    [Header("Saw References")] 
        public Transform currentSawModel;
        public GameObject currentSawVisuals;

    [Header("Sawing settings")] 
            public float strokeDistance = 0.25f;
            public float sinkDepth = 0.1f;
            public float mouseSensitivity = 0.002f;
            public float cutSpeed = 0.4f;
            private float sawEffectTimer = 0f;
            public float effectGraceTime = 0.15f;
            
    [Header("Direction Fixes")]
    [Tooltip("Check this if the saw goes UP instead of DOWN into the limb!")]
            public bool invertDepth = true; 
            public Axis strokeAxis = Axis.Z;
            public Axis depthAxis = Axis.Y;
            
    [Header("Audio / Effects")]
            public AudioSource sawAudio;
            public ParticleSystem bloodParticles;
            public AudioSource screamAudio;
            public AudioClip[] screamClips;
            public AudioClip finalSeverSound;
            public AudioClip finalScreamSound;
    
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
        
        if (bloodParticles != null)
        {
            bloodParticles.transform.position = targetAnchor.position;
            bloodParticles.transform.rotation = targetAnchor.rotation;
        }
        
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
            Camera.main.fieldOfView = Mathf.Lerp(90f, 50f, cutProgress / 0.1f);
            
            sawEffectTimer = effectGraceTime;
            
            if (bloodParticles != null && !bloodParticles.isPlaying)
                bloodParticles.Play();
            
            if (sawAudio != null && !sawAudio.isPlaying)
            {
                if (sawAudio.time > 0f)
                    sawAudio.UnPause();
                else
                    sawAudio.Play();
            }
            
            if (screamAudio != null && screamClips != null && screamClips.Length > 0 && !screamAudio.isPlaying)
            {
                PlayRandomScream();
            }
        }
        else
        {
            sawEffectTimer -= Time.deltaTime;
            
            if (sawEffectTimer <= 0f)
            {
                if (bloodParticles != null && bloodParticles.isPlaying)
                    bloodParticles.Stop();

                if (sawAudio != null && sawAudio.isPlaying)
                    sawAudio.Pause();
            }
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
        
        if (sawAudio != null) sawAudio.Stop();
        if (screamAudio != null) screamAudio.Stop();
        
        if (sawAudio != null && finalSeverSound != null)
        {
            sawAudio.PlayOneShot(finalSeverSound);
        }

        StartCoroutine(PlayFinalAudioSequence());
        
        Camera.main.fieldOfView = 90f;
        
        onAmputationComplete?.Invoke();
        
        Debug.Log("Complete Amputation");
    }
    
    private System.Collections.IEnumerator PlayFinalAudioSequence()
    {
        float waitTime = 0.5f;
        
        if (sawAudio != null && finalSeverSound != null)
        {
            sawAudio.PlayOneShot(finalSeverSound);
            waitTime = finalSeverSound.length;
        }

        yield return new WaitForSeconds(waitTime);
        
        if (screamAudio != null && finalScreamSound != null)
        {
            screamAudio.PlayOneShot(finalScreamSound);
        }
    }
    
    private void PlayRandomScream()
    {
        if (screamAudio == null || screamClips == null || screamClips.Length == 0 || screamAudio.isPlaying) 
            return;

        int randomIndex = UnityEngine.Random.Range(0, screamClips.Length);
        
        if (screamClips.Length > 1 && randomIndex == lastScreamIndex)
        {
            randomIndex = (randomIndex + 1) % screamClips.Length;
        }

        lastScreamIndex = randomIndex;
        screamAudio.clip = screamClips[randomIndex];
        screamAudio.pitch = UnityEngine.Random.Range(0.92f, 1.08f);
        screamAudio.Play();
    }
    
}
