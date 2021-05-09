using UnityEngine;

public class Binder : MonoBehaviour
{
    public Rigidbody2D myriRigidbody2D;
    public Transform myTransform;
    public Animator muAnim;
    public float speed;

    private void Update()
    {
        PlayerController.Instance.Update();
    }

    private void FixedUpdate()
    {
        PlayerController.Instance.InputControl();
    }

    private void Start()
    {
        PlayerController.Instance._rigidbody2D = myriRigidbody2D;
        PlayerController.Instance._transform = myTransform;
        PlayerController.Instance.movespeed = speed;
        PlayerController.Instance._anim = muAnim;

        PlayerProperty.Instance.InitProperties();
    }
}
