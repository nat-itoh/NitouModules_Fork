using UnityEngine;
using UnityEngine.InputSystem;
using nitou;
using nitou.LevelActors.Control;

public class TestPlayer : MonoBehaviour
{
    [SerializeField] MoveControl _move;
    [SerializeField] Animator _animator;

    
    private void OnMove(InputValue value) {
        // MoveAction�̓��͒l���擾
        var axis = value.Get<Vector2>();

        // �ړ����x��ێ�
        var velocity = new Vector3(axis.x, 0, axis.y);

        //Debug_.Log($"input : {velocity}", Colors.Orange);
        _move.Move(axis);
    }



    private void Update() {
        _animator.SetFloat("Speed", _move.CurrentSpeed);
    }

    //private void Update() {
    //    var velociy = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

    //    _move.Move(velociy);
    //}

}
