using UnityEngine;
using UnityEngine.UI;

public class Binder : MonoBehaviour
{
    public Rigidbody2D myriRigidbody2D;
    public Transform myTransform;
    public Animator muAnim;
    public float speed;
    public Image water;
    public Image hunger;
    public Image invade;

    private void Update()
    {
        PlayerController.Instance.Update();
        
    }

    private void FixedUpdate()
    {
        PlayerController.Instance.InputControl();
        PlayerProperty.Instance.Update();
    }

    private void Start()
    {
        PlayerController.Instance._rigidbody2D = myriRigidbody2D;
        PlayerController.Instance._transform = myTransform;
        PlayerController.Instance.movespeed = speed;
        PlayerController.Instance._anim = muAnim;
        PlayerProperty.Instance.waterBar = water;
        PlayerProperty.Instance.hungerBar = hunger;
        PlayerProperty.Instance.invadeBar = invade;
        
        
        PlayerProperty.Instance.InitProperties();
    }

    private void UpdatebyTenSec()
    {
        
    }
}
