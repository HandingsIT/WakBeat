using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*                                  */
/* 가시 + 닷지 포인트 스폰 스크립트 */
/*                                  */

public class CircularMovement : MonoBehaviour
{
    public GameObject obstacleEven;               // obstacle_even  Prefab
    public GameObject obstacleOdd;                // obstacle_odd   Prefab
    public GameObject obstacleEven1;              // obstacle_even1 Prefab
    public GameObject obstacleOdd1;               // obstacle_odd1  Prefab
    public GameObject dodgePoint;                 // dodgePoint     Prefab  (초록색 닷지 포인트)
    public GameObject dodgeAuto;                  // dodgeAuto      Prefab  (오토 포인트 <- 공이랑 닿으면 궤도를 바꾸게 해줌)



    public float bpm = 133;          // 리와인드: 133, 급뱅종선언: 170
    public float bpc = 4f;           // beats per cycle, 한 바퀴에 몇 박자를 넣을 건지
    public float Angle = 0f;
    public float Radian;
    public float Radius = 4.61f;



    public bool flag_even = true;
    public bool flag_odd = true;



    public static int MAX_CYCLE = 132;            // 리와인드: 132 (66 x 2), 급뱅종선언: 104 (52 x 2)
    public static int MAX_BEAT = 4;               // 반바퀴에 들어가는 박자수 (MAX_BEAT가 4일 경우 "0.5박자, 1박자, 1.5박자, 2박자"를 나타냄)
    public float DODGE_INTERVAL = 0.1f;           // 닷지포인트 시간 간격

    public bool spawn = true;
    public bool halfCycle = true;          // 반바퀴 지났을 때 한 번만 spawn할 수 있도록

    public int cycle = 1;                         // 반바퀴 돌 때마다 1씩 추가됨 (짝수 cycle = 반바퀴, 홀수 cycle = 한바퀴)

    public int rotation = 1;                      // 안쪽 궤도에 스폰되는지 바깥쪽 궤도에 스폰되는 (1: 바깥쪽, 0: 안쪽)
    public float radius;
    public float angle = 0;
    public float radian;
    public float angle_dodge = 0;
    public float radian_dodge;
    public float radius_auto;

    public float[,] spawnAngle = new float[MAX_CYCLE, MAX_BEAT];   // 가시가 스폰되는 각도롤 저장하는 배열

    // 리와인드
    public int[,] stage1 = {               // cycle   실제 바퀴수   
//          1박   2박       3박   4박
        { 0, 0, 0, 0 }, { 0, 0, 0, 0 },    // 1  2    1  바퀴
        { 0, 0, 0, 0 }, { 0, 0, 0, 0 },    // 3  4    2  바퀴
        { 0, 0, 0, 0 }, { 0, 0, 0, 0 },    // 5  6    3  바퀴
        { 0, 0, 0, 0 }, { 0, 0, 0, 0 },    // 7  8    4  바퀴
        { 0, 1, 0, 0 }, { 0, 0, 0, 0 },    // 8  9    5  바퀴
        { 0, 1, 0, 0 }, { 0, 0, 0, 0 },    // 10 11   6  바퀴
        { 0, 1, 0, 0 }, { 0, 0, 0, 0 },    // 12 13   7  바퀴
        { 0, 1, 0, 0 }, { 0, 0, 0, 0 },    // 14 15   8  바퀴
        { 0, 1, 0, 0 }, { 0, 1, 0, 0 },    // 16 17   9  바퀴
        { 1, 0, 0, 0 }, { 0, 1, 0, 0 },    // 18 19   10 바퀴
        { 0, 1, 0, 0 }, { 0, 1, 0, 0 },
        { 1, 0, 0, 0 }, { 0, 1, 0, 0 },
        { 0, 1, 0, 0 }, { 0, 1, 0, 0 },
        { 1, 0, 0, 0 }, { 0, 1, 0, 0 },
        { 0, 1, 0, 0 }, { 0, 1, 0, 0 },    //         15 바퀴
        { 0, 1, 0, 1 }, { 0, 1, 0, 1 },
        { 0, 1, 0, 0 }, { 0, 1, 0, 0 },
        { 1, 0, 0, 0 }, { 0, 1, 0, 0 },
        { 0, 1, 0, 0 }, { 0, 1, 0, 0 },
        { 1, 0, 0, 0 }, { 0, 1, 0, 0 },    //         20 바퀴
        { 0, 1, 0, 0 }, { 0, 1, 0, 0 },
        { 1, 0, 0, 0 }, { 0, 1, 0, 0 },
        { 0, 1, 0, 0 }, { 1, 0, 0, 1 },
        { 0, 1, 0, 0 }, { 0, 1, 0, 0 },
        { 0, 1, 0, 1 }, { 0, 1, 0, 1 },    //         25 바퀴
        { 0, 1, 0, 1 }, { 0, 1, 0, 1 },
        { 0, 1, 0, 1 }, { 0, 1, 0, 1 },
        { 0, 1, 0, 1 }, { 0, 1, 0, 1 },
        { 0, 1, 0, 1 }, { 0, 1, 0, 1 },
        { 0, 1, 0, 1 }, { 0, 1, 0, 1 },    //         30 바퀴
        { 0, 1, 0, 1 }, { 0, 1, 0, 1 },
        { 0, 1, 0, 0 }, { 0, 1, 0, 0 },
        { 0, 1, 0, 0 }, { 0, 1, 0, 0 },
        { 1, 0, 0, 0 }, { 0, 0, 0, 0 },
        { 0, 1, 0, 0 }, { 0, 1, 0, 0 },    //         35 바퀴
        { 1, 0, 0, 0 }, { 0, 0, 0, 0 },
        { 0, 1, 0, 1 }, { 0, 1, 0, 1 },
        { 0, 1, 0, 1 }, { 0, 1, 0, 1 },
        { 0, 1, 0, 1 }, { 0, 1, 0, 1 },
        { 0, 1, 0, 0 }, { 0, 1, 0, 0 },    //         40 바퀴
        { 0, 1, 0, 0 }, { 1, 0, 1, 0 },
        { 1, 0, 0, 1 }, { 0, 1, 0, 1 },
        { 0, 1, 0, 0 }, { 0, 1, 0, 0 },
        { 0, 1, 0, 1 }, { 0, 1, 0, 1 },
        { 0, 1, 0, 1 }, { 0, 1, 0, 0 },    //         45 바퀴
        { 0, 1, 0, 1 }, { 0, 1, 0, 0 },
        { 0, 1, 0, 0 }, { 0, 1, 0, 1 },
        { 1, 0, 1, 0 }, { 1, 0, 1, 0 },
        { 0, 0, 0, 1 }, { 0, 0, 0, 1 },
        { 0, 1, 0, 1 }, { 0, 1, 0, 1 },    //         50 바퀴
        { 0, 1, 0, 1 }, { 0, 1, 0, 1 },
        { 0, 1, 0, 1 }, { 0, 1, 0, 1 },
        { 0, 1, 0, 0 }, { 0, 1, 0, 0 },
        { 0, 1, 0, 0 }, { 0, 1, 0, 0 },
        { 0, 1, 0, 1 }, { 0, 1, 0, 1 },    //         55 바퀴
        { 0, 1, 0, 0 }, { 0, 1, 0, 0 },
        { 0, 1, 0, 0 }, { 0, 0, 1, 0 },
        { 1, 0, 0, 0 }, { 0, 0, 0, 0 },
        { 0, 1, 0, 0 }, { 0, 0, 1, 0 },
        { 1, 0, 0, 0 }, { 1, 0, 0, 0 },    //         60 바퀴
        { 0, 1, 0, 0 }, { 0, 1, 0, 0 },
        { 0, 1, 0, 0 }, { 0, 1, 0, 0 },
        { 0, 1, 0, 0 }, { 0, 1, 0, 0 },
        { 0, 1, 0, 0 }, { 0, 1, 0, 0 },
        { 0, 0, 0, 0 }, { 0, 0, 0, 0 },    //         65 바퀴
        { 0, 0, 0, 0 }, { 0, 0, 0, 0 },    //     buffer 바퀴
    };

    // 급뱅종선언
    public int[,] stage2 = {               // cycle
        { 0, 0, 0, 0 }, { 0, 0, 0, 1 },    // 0  1    1  바퀴
        { 0, 0, 1, 0 }, { 0, 1, 0, 0 },    // 2  3    2  바퀴
        { 0, 0, 0, 1 }, { 0, 1, 0, 1 },    // 4  5    3  바퀴
        { 0, 0, 1, 0 }, { 0, 1, 0, 0 },    // 6  7    4  바퀴
        { 0, 1, 0, 1 }, { 0, 1, 0, 1 },    // 8  9    5  바퀴
        { 0, 0, 1, 0 }, { 0, 1, 0, 0 },    // 10 11   6  바퀴
        { 0, 0, 0, 1 }, { 0, 1, 0, 1 },    // 12 13   7  바퀴
        { 0, 0, 1, 0 }, { 0, 1, 0, 0 },    // 14 15   8  바퀴
        { 0, 1, 0, 1 }, { 0, 0, 0, 1 },    // 16 17   9  바퀴
        { 0, 0, 1, 0 }, { 0, 1, 0, 0 },    // 18 19   10 바퀴
        { 1, 0, 0, 1 }, { 0, 1, 0, 1 },
        { 0, 0, 1, 0 }, { 0, 1, 0, 0 },
        { 0, 1, 0, 1 }, { 0, 1, 0, 1 },
        { 0, 1, 0, 1 }, { 0, 1, 0, 1 },
        { 0, 0, 0, 1 }, { 0, 1, 0, 1 },    //         15 바퀴
        { 0, 0, 0, 1 }, { 0, 0, 0, 1 },
        { 0, 1, 0, 1 }, { 0, 0, 0, 1 },
        { 0, 0, 1, 0 }, { 0, 1, 0, 0 },
        { 0, 1, 0, 1 }, { 0, 1, 0, 1 },
        { 0, 0, 1, 0 }, { 0, 1, 0, 0 },    //         20 바퀴
        { 0, 1, 0, 1 }, { 0, 1, 0, 1 },
        { 0, 0, 0, 1 }, { 0, 0, 0, 1 },
        { 0, 0, 0, 1 }, { 0, 1, 0, 1 },
        { 0, 1, 0, 1 }, { 0, 1, 1, 0 },
        { 1, 0, 1, 1 }, { 0, 1, 0, 1 },    //         25 바퀴
        { 0, 1, 0, 1 }, { 0, 1, 1, 0 },
        { 1, 1, 0, 1 }, { 0, 1, 1, 1 },
        { 0, 0, 1, 1 }, { 0, 0, 1, 1 },
        { 0, 1, 0, 1 }, { 1, 1, 0, 1 },
        { 0, 1, 0, 1 }, { 0, 1, 1, 0 },    //         30 바퀴
        { 1, 1, 0, 1 }, { 0, 1, 1, 1 },
        { 0, 0, 1, 1 }, { 0, 1, 0, 1 },
        { 0, 1, 1, 1 }, { 0, 1, 0, 1 },
        { 0, 1, 0, 1 }, { 0, 1, 1, 0 },
        { 1, 1, 0, 1 }, { 0, 1, 1, 1 },    //         35 바퀴
        { 0, 0, 1, 1 }, { 0, 0, 1, 1 },
        { 0, 1, 0, 1 }, { 1, 1, 0, 1 },
        { 0, 1, 0, 1 }, { 0, 1, 1, 0 },
        { 1, 1, 0, 1 }, { 0, 1, 1, 1 },
        { 0, 0, 1, 1 }, { 0, 1, 0, 1 },    //         40 바퀴
        { 0, 1, 0, 1 }, { 0, 0, 0, 1 },
        { 0, 0, 1, 0 }, { 0, 1, 0, 0 },
        { 0, 1, 0, 1 }, { 0, 1, 0, 1 },
        { 0, 0, 1, 0 }, { 0, 1, 0, 0 },
        { 0, 1, 0, 1 }, { 0, 1, 0, 1 },    //         45 바퀴
        { 0, 0, 1, 0 }, { 0, 1, 0, 0 },
        { 0, 1, 0, 1 }, { 0, 1, 0, 1 },
        { 0, 1, 0, 1 }, { 0, 1, 0, 1 },
        { 0, 0, 0, 0 }, { 0, 0, 0, 1 },
        { 0, 0, 0, 0 }, { 0, 0, 0, 1 },    //         50 바퀴
        { 0, 0, 0, 0 }, { 0, 0, 0, 1 },
        { 0, 0, 0, 0 }, { 0, 0, 0, 0 },    //     buffer 바퀴
    };

    private void OnTriggerEnter2D(Collider2D other)
    {
        switchSides();
    }

    public void switchSides()
    {
        if (Radius > 4.2f)
        {
            Radius = 4.18f;
        }
        else
        {
            Radius = 4.61f;
        }
    }
    void Start()
    {
        for (int j = 0; j < MAX_CYCLE; j++)       // spawnAngle 초기화
        {
            for (int i = 0; i < MAX_BEAT; i++)
            {
                spawnAngle[j, i] = -1f;  // -1.0으로 초기화
            }
        }

        int x;
        for (int j = 0; j < MAX_CYCLE; j++)       // spawnAngle 각도 배정
        {
            x = 0;
            while (spawnAngle[j, x] > -1f)
            {
                x++;
            }
            for (int i = 0; i < MAX_BEAT; i++)
            {      /*~~~~*/
                if (stage1[j, i] == 1)  // <--------------------------------------------------------------------------------------------------------------------------------- 스테이지 바꿀 시 여기서 배열 이름 변경 (MAX_CYCLE, bpm도 변경 필요)
                {
                    angle = ((i + 1) / (bpc * 2) * 360) + (j % 2) * 180 + (6 * bpm * DODGE_INTERVAL / bpc);
                    if (angle > 180 * (j % 2 + 1))
                    {
                        x = 0;
                        while (spawnAngle[j + 1, x] > -1f)
                        {
                            x++;
                        }
                        spawnAngle[j + 1, x] = angle;
                    }
                    else
                    {
                        spawnAngle[j, x++] = angle;
                    }
                }
            }
        }
    }

    void Update()
    {


        // bpm과 bpc 이용해 공의 속도 계산
        Angle += 6 * bpm * Time.deltaTime / bpc;
        Radian = (90 - Angle) * Mathf.PI / 180;
        transform.position = new Vector3(Radius * Mathf.Cos(Radian), Radius * Mathf.Sin(Radian), 0);

        // 스페이스 누르면 궤도 변경
        if (Input.GetKeyDown("space"))
        {
            switchSides();
        }



        if (Angle > 360)                        // 360도 클리어
        {
            cycle++;
            Angle = 0;

            spawn = true;
            halfCycle = true;
        }
        else if ((Angle > 180) && halfCycle)    // 180도 클리어
        {
            cycle++;

            halfCycle = false;
            spawn = true;
        }



        if (flag_even)
        {
            if (halfCycle)
            {
                flag_even = false;
            }
        }
        else
        {
            if (!halfCycle)
            {
                flag_even = true;
                GameObject[] even = GameObject.FindGameObjectsWithTag("even");
                for (int i = 0; i < even.Length; i++)
                {
                    Destroy(even[i]);
                }
                GameObject[] even1 = GameObject.FindGameObjectsWithTag("even1");
                for (int i = 0; i < even1.Length; i++)
                {
                    Destroy(even1[i]);
                }
            }
        }

        if (flag_odd)
        {
            if (!halfCycle)
            {
                flag_odd = false;
            }
        }
        else
        {
            if (halfCycle)
            {
                flag_odd = true;
                GameObject[] odd = GameObject.FindGameObjectsWithTag("odd");
                for (int i = 0; i < odd.Length; i++)
                {
                    Destroy(odd[i]);
                }
                GameObject[] odd1 = GameObject.FindGameObjectsWithTag("odd1");
                for (int i = 0; i < odd1.Length; i++)
                {
                    Destroy(odd1[i]);
                }
            }
        }



        if (spawn)
        {
            for (int i = 0; cycle < MAX_CYCLE && i < MAX_BEAT; i++)
            {
                angle = spawnAngle[cycle, i];
                if (angle > -1f)
                {
                    if (rotation > 0)          // 바깥쪽 궤도
                    {
                        rotation = 0;
                        radius = 4.619f;
                        radius_auto = 4.61f;
                    }
                    else                       // 안쪽 궤도
                    {
                        rotation = 1;
                        radius = 4.157f;
                        radius_auto = 4.18f;
                    }
                    // 가시 스폰 각도
                    radian = (90 - angle) * Mathf.PI / 180;
                    // 가시 스폰 rotation
                    angle += 180 * rotation;
                    // 닷지 포인트 스폰 각도
                    radian_dodge = (90 - (spawnAngle[cycle, i] - (6 * bpm * DODGE_INTERVAL / bpc))) * Mathf.PI / 180;

                    switch (cycle % 4)
                    {
                        case 0:
                            // 클론 생성
                            GameObject cloneEven = Instantiate(obstacleEven, new Vector3(radius * Mathf.Cos(radian), radius * Mathf.Sin(radian), 0), Quaternion.Euler(new Vector3(0, 0, -angle))) as GameObject;   // 가시        instantiate
                            GameObject dodgeEven = Instantiate(dodgePoint, new Vector3(4.39f * Mathf.Cos(radian_dodge), 4.39f * Mathf.Sin(radian_dodge), 0), Quaternion.identity) as GameObject;                   // 닷지 포인트 instantiate
                            GameObject dodgeAutoEven = Instantiate(dodgeAuto, new Vector3(radius_auto * Mathf.Cos(radian_dodge), radius_auto * Mathf.Sin(radian_dodge), 0), Quaternion.identity) as GameObject;    // 오토 포인트 instantiate
                            // 태그 설정 (태그들은 destroy~~ 코드에서 클론을 삭제할 때 사용됨)
                            cloneEven.tag = "even";
                            dodgeEven.tag = "even";
                            dodgeAutoEven.tag = "even";
                            break;
                        case 1:
                            GameObject cloneOdd = Instantiate(obstacleOdd, new Vector3(radius * Mathf.Cos(radian), radius * Mathf.Sin(radian), 0), Quaternion.Euler(new Vector3(0, 0, -angle))) as GameObject;
                            GameObject dodgeOdd = Instantiate(dodgePoint, new Vector3(4.39f * Mathf.Cos(radian_dodge), 4.39f * Mathf.Sin(radian_dodge), 0), Quaternion.identity) as GameObject;
                            GameObject dodgeAutoOdd = Instantiate(dodgeAuto, new Vector3(radius_auto * Mathf.Cos(radian_dodge), radius_auto * Mathf.Sin(radian_dodge), 0), Quaternion.identity) as GameObject;
                            cloneOdd.tag = "odd";
                            dodgeOdd.tag = "odd";
                            dodgeAutoOdd.tag = "odd";
                            break;
                        case 2:
                            GameObject cloneEven1 = Instantiate(obstacleEven1, new Vector3(radius * Mathf.Cos(radian), radius * Mathf.Sin(radian), 0), Quaternion.Euler(new Vector3(0, 0, -angle))) as GameObject;
                            GameObject dodgeEven1 = Instantiate(dodgePoint, new Vector3(4.39f * Mathf.Cos(radian_dodge), 4.39f * Mathf.Sin(radian_dodge), 0), Quaternion.identity) as GameObject;
                            GameObject dodgeAutoEven1 = Instantiate(dodgeAuto, new Vector3(radius_auto * Mathf.Cos(radian_dodge), radius_auto * Mathf.Sin(radian_dodge), 0), Quaternion.identity) as GameObject;
                            cloneEven1.tag = "even1";
                            dodgeEven1.tag = "even1";
                            dodgeAutoEven1.tag = "even1";
                            break;
                        case 3:
                            GameObject cloneOdd1 = Instantiate(obstacleOdd1, new Vector3(radius * Mathf.Cos(radian), radius * Mathf.Sin(radian), 0), Quaternion.Euler(new Vector3(0, 0, -angle))) as GameObject;
                            GameObject dodgeOdd1 = Instantiate(dodgePoint, new Vector3(4.39f * Mathf.Cos(radian_dodge), 4.39f * Mathf.Sin(radian_dodge), 0), Quaternion.identity) as GameObject;
                            GameObject dodgeAutoOdd1 = Instantiate(dodgeAuto, new Vector3(radius_auto * Mathf.Cos(radian_dodge), radius_auto * Mathf.Sin(radian_dodge), 0), Quaternion.identity) as GameObject;
                            cloneOdd1.tag = "odd1";
                            dodgeOdd1.tag = "odd1";
                            dodgeAutoOdd1.tag = "odd1";
                            break;
                    }
                }
            }
            spawn = false;
        }
    }
}
