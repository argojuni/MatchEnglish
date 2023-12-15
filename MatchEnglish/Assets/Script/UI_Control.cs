using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Control : MonoBehaviour
{
    public bool IsTransisi, IsTidakPerlu;

    string SaveNamaScene;

    private void Awake()
    {
        if(IsTransisi && IsTidakPerlu)
        {
            gameObject.SetActive(false);
        }
    }

    public void btn_suara(int id)
    {
        AudioManager.instance.Sound_sfx(0);
    }

    public void btn_pindah(string nama)
    {
        this.gameObject.SetActive(true);
        SaveNamaScene = nama;
        GetComponent<Animator>().Play("OutTransisi");
        AudioManager.instance.source_BGM.Play();
    }

    public void btn_restart()
    {
        SaveNamaScene = SceneManager.GetActiveScene().name;
        GetComponent<Animator>().Play("OutTransisi");
        GameSystem.instance.ResetData();
    }

    public void pindah()
    {
        SceneManager.LoadScene(SaveNamaScene);
    }

    public void BtnQuit()
    {
        Application.Quit();
        Debug.Log("Keluar Game");
    }
}
