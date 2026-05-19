using UnityEngine;

public class CheckGround : MonoBehaviour
{
    public bool isGrounded = false;
    [SerializeField] LayerMask _whatIsGround;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_whatIsGround & (1 << collision.gameObject.layer)) != 0)
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((_whatIsGround & (1 << collision.gameObject.layer)) != 0)
        {
            isGrounded = false;
        }
    }
}
