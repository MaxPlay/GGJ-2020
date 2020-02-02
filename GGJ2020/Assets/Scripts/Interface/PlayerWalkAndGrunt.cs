using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkAndGrunt : MonoBehaviour
{

    private AudioSource audioSource;
    [SerializeField]
    private AudioClip sound;

bool soundOn = true;

    void Start()
    {
          audioSource = GetComponent<AudioSource> ();


   StartCoroutine (PlayerHasMoved ());

    }



 IEnumerator PlayerHasMoved () {

        while (soundOn) {

Vector3 earlier = transform.position;

float bla = 0.1f;

yield return new WaitForSeconds (bla);

Vector3 later = transform.position;

if (earlier != later) {

     audioSource.clip = sound;
            audioSource.Play ();



} 



           

        }

    }








}
