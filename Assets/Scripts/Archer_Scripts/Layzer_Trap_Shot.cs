using System.Collections;
using UnityEngine;

public class Layzer_Trap_Shot : ArcherShot
{
    //양쪽벽에 트랩을 설치해서 범위에 들어오는 적들에게 일정 데미지를 준다.

    private GameObject Lazer;
    private int speed = 100;
    private int MaxLength = 26;
    private WaitForSeconds SustainmentTime = new WaitForSeconds(2f);

    public  bool Is_Lazer = false;


    public override void Shot(){ }

    public Layzer_Trap_Shot(GameObject Bolt, GameObject Shot_Spawn, GameObject Shot_Spawn_Point, int Shot_Level,GameObject Lazer)
    :base(Bolt,Shot_Spawn,Shot_Spawn_Point,Shot_Level)
    {
        this.Lazer = Lazer;
    }



    public IEnumerator Set_Lazer()
    {
        Is_Lazer = true;//참 값을 줘서 다른 레이저의 발생을 막는다.

        //레이저 복제
        GameObject Lazer_Clone = MonoBehaviour.Instantiate(Lazer, Shot_Spawn_Point.transform.position + new Vector3(0,0,2), Shot_Spawn_Point.transform.rotation);


        //레이저가 생성되고 루프를 통해서 길이를 확장해나가는 부분
        for (float i= 0; i< MaxLength; i = i + Time.deltaTime * speed)
        {
            Lazer_Clone.transform.localScale = new Vector3(i, Lazer.transform.localScale.y, Lazer.transform.localScale.z);
            yield return null;
        }

        //이 부분이 레이저의 지속시간이 된다.
        yield return SustainmentTime;


        //지속시간이 끝나고 레이저가 사라지게 만드는 루프
        for (float i = MaxLength; i > 0f; i = i - Time.deltaTime * speed/2)
        {
            Lazer_Clone.transform.localScale = new Vector3(i, Lazer.transform.localScale.y, Lazer.transform.localScale.z);

            //깜빡깜빡을 표현
            Lazer_Clone.SetActive(false);
            yield return null;
            Lazer_Clone.SetActive(true);
            yield return null;
        }
        MonoBehaviour.Destroy(Lazer_Clone);
        Is_Lazer = false;
        
    }
}
