//using UnityEngine;

//public class RopeBalance : MonoBehaviour
//{
//    public float balanceOffset = 0f;  // Balance state
//    public float balanceSensitivity = 2f;  // How quickly balance is affected
//    public float recoveryRate = 1f;  // How quickly the player can recover
//    public float maxBalance = 10f;  // Maximum balance offset before falling

//    void Update()
//    {
//        // External forces
//        balanceOffset += Random.Range(-0.1f, 0.1f);

//        // Player input to counteract
//        //if (Input.GetKey(KeyCode.A))
//        //    balanceOffset -= balanceSensitivity * Time.deltaTime;
//        //if (Input.GetKey(KeyCode.D))
//        //    balanceOffset += balanceSensitivity * Time.deltaTime;

//        // Recovery (stabilizing naturally over time)
//        balanceOffset = Mathf.Lerp(balanceOffset, 0, recoveryRate * Time.deltaTime);

//        // Clamp balance to avoid infinite values
//        balanceOffset = Mathf.Clamp(balanceOffset, -maxBalance, maxBalance);

//        // Check if the player falls
//        if (Mathf.Abs(balanceOffset) >= maxBalance)
//        {
//            Debug.Log("Player Fell!");            
//        }

//        Camera.main.transform.localRotation = Quaternion.Euler(balanceOffset * 2, 0, -balanceOffset);
//    }
//}
