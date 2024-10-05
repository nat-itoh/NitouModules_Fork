using UnityEngine;
using UnityEngine.InputSystem;
using nitou;
using nitou.LevelActors.Control;

public class TestPlayer : MonoBehaviour
{
    [SerializeField] MoveControl _move;

    
    private void OnMove(InputValue value) {
        // MoveAction�̓��͒l���擾
        var axis = value.Get<Vector2>();

        // �ړ����x��ێ�
        var velocity = new Vector3(axis.x, 0, axis.y);

        //Debug_.Log($"input : {velocity}", Colors.Orange);
        _move.Move(axis);
    }
    

    //private void Update() {
    //    var velociy = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

    //    _move.Move(velociy);
    //}

}
