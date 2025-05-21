using UnityEngine;

public class JumpPad : MonoBehaviour
{

    public float jumpPower = 15;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rigid= other.gameObject.GetComponent<Rigidbody>();
            if (rigid != null)
            {
                rigid.velocity=Vector3.zero;
                rigid.AddForce(Vector3.up.normalized * jumpPower, ForceMode.Impulse);
            }
        }
    }
}
