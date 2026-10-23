using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public static readonly int HeshMove = Animator.StringToHash("Move");
    public float moveSpeed = 5f;
    public float rotateSpeed = 180f;
    private Animator playerAnimator;
    private PlayerInput playerInput;
    private Rigidbody playerRigidbody;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerInput= GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()  // 매 프레임마다 업데이트.
    {
        playerAnimator.SetFloat(HeshMove, playerInput.Move);
    }

    private void FixedUpdate() // 매 프래임마다 물리 관련 업데이트.
    {
        float angle = playerInput.Rotate *rotateSpeed * Time.deltaTime;
        playerRigidbody.MoveRotation(playerRigidbody.rotation * Quaternion.Euler(0f, angle, 0f));
        
        Vector3 delta = playerInput.Move * transform.forward * moveSpeed * Time.deltaTime;
        // 사용자 입력값 * 캐릭터가 바라보는 방향 * 속도 * 시간.
        playerRigidbody.MovePosition(playerRigidbody.position + delta);
    }
}
