using UnityEngine;

public class Collectable : MonoBehaviour
{
	public BoxCollider2D c;

    void Awake()
    {
        
    }

    void Update()
    {
        
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("Collectable Touched");
	}
}
