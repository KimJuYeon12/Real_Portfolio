using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layzer_Trap_Shot : ArcherShot
{
    //양쪽벽에 트랩을 설치해서 범위에 들어오는 적들에게 일정 데미지를 준다.

    GameObject Lazer;
    public static bool Is_Lazer = false;

    public override void Shot(){ }

    public Layzer_Trap_Shot(GameObject Bolt, GameObject Shot_Spawn, GameObject Shot_Spawn_Point, int Shot_Level,GameObject Lazer)
    :base(Bolt,Shot_Spawn,Shot_Spawn_Point,Shot_Level)
    {
        this.Lazer = Lazer;
    }



    public IEnumerator Set_Lazer()
    {
        Is_Lazer = true;  
        GameObject Lazer_Clone = MonoBehaviour.Instantiate(Lazer, Shot_Spawn_Point.transform.position+new Vector3(0,0,2), Shot_Spawn_Point.transform.rotation);
        for (float i= 0; i<26f; i = i + Time.deltaTime*100)
        {
            Debug.Log("x= "+i);
            Lazer_Clone.transform.localScale = new Vector3(i, Lazer.transform.localScale.y, Lazer.transform.localScale.z);
            yield return null;
        }


        yield return new WaitForSeconds(2f);
        for (float i = 26; i > 0f; i = i - Time.deltaTime * 50)
        {
            Debug.Log("x= " + i);
            Lazer_Clone.transform.localScale = new Vector3(i, Lazer.transform.localScale.y, Lazer.transform.localScale.z);
            Lazer_Clone.SetActive(false);
            yield return null;
            Lazer_Clone.SetActive(true);
            yield return null;
        }
        MonoBehaviour.Destroy(Lazer_Clone);
        Is_Lazer = false;
    }




}
