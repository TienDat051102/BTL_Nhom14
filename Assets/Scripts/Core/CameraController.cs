using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Room camera
    [SerializeField] private float speed;// tốc độ của camera
    private float currentPosX;// vị trị hiện tại của camera
    private Vector3 velocity = Vector3.zero;

    //Follow player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position/*vị trí htai*/, new Vector3(currentPosX, transform.position.y, transform.position.z)/*đích*/, ref velocity, speed /*tốc độ chuyển động*/);       //thay đổi vị trí của máy ảnh,SmoothDamp: thay đổi dần vị trí của vector,

    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        print("here");
        currentPosX = _newRoom.position.x;
    }
}