using UnityEngine;

public class CameraController : MonoBehaviour {

    //I define singletons like this, they're handled by a static interface rather than passing the singleton instance around
    private static CameraController _s;

    //Singleton instance property
    public Transform Target => _s.target;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private Transform target;

    [SerializeField][Range(20.0f, 30.0f)]
    private float cameraTrackingSpeed = 25f;

    [SerializeField][Range(0.0f, 1.0f)]
    private float minTrackingDistance = 0.2f;

    [SerializeField]
    private AnimationCurve cameraTrackingCurve;

    private float cameraStaticZ = -4999;

    private void Awake() {
        //initialize singleton instance (i know iam able to handle this better, but for the sake of ggj i am chosing to deal with this poorly)
        _s = this;
    }

    private void FixedUpdate() {
        if( player != null ) {
            float distance = Vector3.Distance( transform.position, player.transform.position );
            if( distance > minTrackingDistance ) {
                Vector3 targetVector = player.transform.position;

                if( target != null )
                    targetVector = ( player.transform.position + target.transform.position ) * 0.5f;

                float finalSpeed = cameraTrackingSpeed * cameraTrackingCurve.Evaluate( distance ) * Time.fixedDeltaTime;

                Vector3 finalVector = Vector3.MoveTowards( transform.position, targetVector, finalSpeed );
                finalVector.z = cameraStaticZ;
                transform.position = finalVector;
            }
        }
    }
}
