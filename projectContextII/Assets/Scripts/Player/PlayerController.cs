using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    private CharacterController _charCon;
    private Transform _cam;

    [Header("Variables")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float turnSmoothgTime = 0.1f;
    private float _turnSmoothVelocity;


    private void Start()
    {
        _charCon = GetComponent<CharacterController>();
        _cam = Camera.main.GetComponent<Transform>();
    }

    void Update()
    {
        Move(); 
    }

    private void Move()
    {
        float horInput = Input.GetAxisRaw("Horizontal");
        float verInput = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(horInput, 0f, verInput).normalized;

        if (dir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + _cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothgTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _charCon.Move(moveDir.normalized * walkSpeed * Time.deltaTime);
        }
    }
}
