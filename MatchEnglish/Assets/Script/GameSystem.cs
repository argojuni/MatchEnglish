using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    public static GameSystem instance;

    [Header("Data Game")]
    public bool isGameActive;
    public int Target;
    public int DataLevel, DataScore, DataWaktu, DataDarah;

    [Header("Komponen UI")]
    public Text Text_Level,Text_Waktu,Text_Score;
    public RectTransform ui_Darah;


    public bool SistemAcak;

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
        AcakSoal();
    }

    float s;
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            AcakSoal();

        if (isGameActive)
        {
            if(DataWaktu > 0)
            {
                s += Time.deltaTime;
                if (s >= 1)
                {
                    DataWaktu--;
                    s = 0;
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

        for(int i=0; i<_AcakSoal.Count; i++)
        {
            rand = Random.Range(1, DataPermainan.Length);
            while (_AcakSoal.Contains(rand))
                rand = Random.Range(1, DataPermainan.Length);

            _AcakSoal[i] = rand;

            Drag_Obj[i].ID = rand - 1;  
            Drag_Obj[i].Texts.text = DataPermainan[rand-1].Nama;

        }

        _AcakPos = new List<int>(new int[Drop_Tempat.Length]);

        for (int i = 0; i < _AcakPos.Count; i++)
        {
            rand = Random.Range(1, _AcakSoal.Count+1);
            while (_AcakPos.Contains(rand))
                rand = Random.Range(1, _AcakSoal.Count + 1); 

            _AcakPos[i] = rand;

            Drop_Tempat[i].Drop.ID = _AcakSoal[rand - 1]-1;
            Drop_Tempat[i].Gambar.sprite = DataPermainan[Drop_Tempat[i].Drop.ID].Gambar;
        }

    }

    public void SetInfoUI()
    {
        Text_Level.text = (DataLevel + 1).ToString();

        int Menit = Mathf.FloorToInt(DataWaktu / 60);//01
        int Detik = Mathf.FloorToInt(DataWaktu % 60);//30
        Text_Waktu.text = Menit.ToString("00") + ":" + Detik.ToString("00");

        Text_Score.text = DataScore.ToString();

        ui_Darah.sizeDelta = new Vector2(316f * DataDarah, 286f);
    }
}
