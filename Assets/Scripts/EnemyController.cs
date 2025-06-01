using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Enemy Movement controlls
    [SerializeField]
    private Vector2 initialMoveSpeed = new Vector2(2f, 6f);
    [SerializeField]
    private float chaseSpeed = 4f;
    private float currentMoveSpeed;


}
