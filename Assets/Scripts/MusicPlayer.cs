using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    //singleton pattern.
    // burada yaptığımız hali hazırda oynayan bir müzik varsa diğerleri başlamadan sonlandırıyor. Oyunun içindeki müzik böylece sahne baştan başlasa bile hiç durmadan devam ediyor.
    private void Awake()
    {
        // stops the music start itself every scene reload even we die after and after.
        int numMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        if (numMusicPlayers > 1)
        {
            Destroy(gameObject);

        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

}
