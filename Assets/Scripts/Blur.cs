using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class Blur : MonoBehaviour
{
    [SerializeField]
    private Material material;

    [SerializeField][Range(0f,5f)]
    private float maxValue;

    [SerializeField][Range(0f,1f)]
    private float speed = 1f;

    private float value = 0f;

    [SerializeField]
    private AnimationCurve animationCurve;

    [SerializeField]
    private bool isActive = true;

    private float target = 0f;

    [SerializeField]
    private UnityEvent OnBlurOn, OnBlurOff;

    public bool IsActive {
        get => isActive;
        set {
            if( value != isActive ) {
                isActive = value;
                if( isActive ) {
                    target = 1f;
                    OnBlurOn?.Invoke();
                }
                else {
                    target = 0f;
                    OnBlurOff?.Invoke();
                }
            }
        }
    }

    private void FixedUpdate() {
        if( value != target ) {
            value = Mathf.MoveTowards( value, target, speed * Time.fixedDeltaTime );
            material.SetFloat( "_Blursize", animationCurve.Evaluate( value ) * maxValue );
            Debug.Log( value );
        }
    }

    private void OnRenderImage( RenderTexture source, RenderTexture destination ) {
        if(value > 0f)
            Graphics.Blit( source, destination, material );
        else
            Graphics.Blit( source, destination );
    }
}
