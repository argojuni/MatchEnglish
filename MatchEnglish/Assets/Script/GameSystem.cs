using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Data
{
    public static int DataLevel, DataScore, DataWaktu, DataDarah;
}
public class GameSystem : MonoBehaviour
{
    public static GameSystem instance;

    public string nameOldGameScene;

    public string nameNewGameScene,nameGameEnd;

    int maxLevel = 3;

    [Header("Data Game")]
    public bool isGameActive;
    public bool isGameSelesai;
    public bool SistemAcak;

    public int Target, DataSaatIni;

    [Header("Komponen UI")]
    public Text Text_Level, Text_Waktu, Text_Score;
    public RectTransform ui_Darah;

    [Header("Obj GUI")]
    public GameObject Gui_Pause;
    public GameObject GUI_Transisi;


    [System.Serializable]
    public class DataGame
    {
        public string Nama;
        public Sprite Gambar;
    }

    [Header("Setingan Standar")]
    public DataGame[] DataPermainan;

    public Obj_TempatDrop[] Drop_Tempat;
    public ObjDrag[] Drag_Obj;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        isGameActive = false;
        isGameSelesai = false;

        ResetData();

        Target = Drop_Tempat.Length;

        if (SistemAcak)
        {
            AcakSoal();
        }

        DataSaatIni = 0;

        isGameActive = true;
    }

    public void ResetData()
    {
        Data.DataWaktu = 60 * 3;

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == nameOldGameScene)
        {
            Data.DataWaktu = 60 * 3;
            Data.DataScore = 0;
            Data.DataDarah = 5;
            Data.DataLevel = 0;
        }
    }

    float s;
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            AcakSoal();

        if (isGameActive && !isGameSelesai)
        {
            if (Data.DataWaktu > 0)
            {
                s += Time.deltaTime;
                if (s >= 1)
                {
                    Data.DataWaktu--;
                    s = 0;
                }
            }
            if (Data.DataWaktu <= 0)
            {
                isGameActive = false;
                isGameSelesai = true;

                // Game Over
                GUI_Transisi.GetComponent<UI_Control>().btn_pindah(nameGameEnd);
                AudioManager.instance.Sound_sfx(3);
                AudioManager.instance.source_BGM.Stop();

            }

            if (Data.DataDarah <= 0)
            {
                isGameActive = false;
                isGameSelesai = true;

                //Fungsi Kalah
                GUI_Transisi.GetComponent<UI_Control>().btn_pindah(nameGameEnd);
                AudioManager.instance.Sound_sfx(3);
                AudioManager.instance.source_BGM.Stop();
            }

            if (DataSaatIni >= Target)
            {
                isGameSelesai = true;
                isGameActive = false;

                //Game Win
                if (Data.DataLevel < (maxLevel - 1))
                {
                    Debug.Log(Data.DataLevel < (maxLevel - 1));

                    Data.DataLevel++;
                    //Pindah ke next level

                    UnityEngine.SceneManagement.SceneManager.LoadScene(nameNewGameScene + Data.DataLevel);
                    //GUI_Transisi.GetComponent<UI_Control>().btn_pindah("Game"+Data.DataLevel);
                    AudioManager.instance.Sound_sfx(5);
                }
                else
                {
                    //Game selesai pindah ke menu selesai
                    GUI_Transisi.GetComponent<UI_Control>().btn_pindah(nameGameEnd);
                    AudioManager.instance.Sound_sfx(4);
                    AudioManager.instance.source_BGM.Stop();

                }

            }
        }
        SetInfoUI();
    }

    public List<int> _AcakSoal = new List<int>();
    public List<int> _AcakPos = new List<int>();
    int rand;

    public void AcakSoal()
    {
        _AcakSoal.Clear();
        _AcakPos.Clear();

        _AcakSoal = new List<int>(new int[Drag_Obj.Length]);

        for (int i = 0; i < _AcakSoal.Count; i++)
        {
            rand = Random.Range(1, DataPermainan.Length);
            while (_AcakSoal.Contains(rand))
                rand = Random.Range(1, DataPermainan.Length);

            _AcakSoal[i] = rand;

            Drag_Obj[i].ID = rand - 1;
            Drag_Obj[i].Texts.text = DataPermainan[rand - 1].Nama;

        }

        _AcakPos = new List<int>(new int[Drop_Tempat.Length]);

        for (int i = 0; i < _AcakPos.Count; i++)
        {
            rand = Random.Range(1, _AcakSoal.Count + 1);
            while (_AcakPos.Contains(rand))
                rand = Random.Range(1, _AcakSoal.Count + 1);

            _AcakPos[i] = rand;

            Drop_Tempat[i].Drop.ID = _AcakSoal[rand - 1] - 1;
            Drop_Tempat[i].Gambar.sprite = DataPermainan[Drop_Tempat[i].Drop.ID].Gambar;
        }

    }

    public void SetInfoUI()
    {
        Text_Level.text = (Data.DataLevel + 1).ToString();

        int Menit = Mathf.FloorToInt(Data.DataWaktu / 60);//01
        int Detik = Mathf.FloorToInt(Data.DataWaktu % 60);//30
        Text_Waktu.text = Menit.ToString("00") + ":" + Detik.ToString("00");

        Text_Score.text = Data.DataScore.ToString();

        ui_Darah.sizeDelta = new Vector2(316f * Data.DataDarah, 286f);
    }

    public void Btn_Pause(bool pause)
    {
        if (pause)
        {
            isGameActive = false;
            Gui_Pause.SetActive(true);
        }
        else
        {
            isGameActive = true;
            Gui_Pause.SetActive(false);
        }
    }

}