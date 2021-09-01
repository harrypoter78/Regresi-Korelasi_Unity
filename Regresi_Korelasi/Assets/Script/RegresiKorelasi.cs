using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

public class RegresiKorelasi : MonoBehaviour
{
    public GameObject textBerat;
    public GameObject textSepatu;

    string filename = "";
    static int ukuransepatu;
    static double korelasi, konstanta, koef, regresi, determinasi, fktlain;
    static string persamaan, skala;

    // Start is called before the first frame update
    void Start()
    {
        filename = Application.dataPath + "/dataReport.csv";
    }

    // Update is called once per frame
    void Update()
    {
        //nilai X
        ukuransepatu = int.Parse(Menu._ukuransepatu);

        //korelasi
        korelasi = Perhitungan._getKorelasi;
        skala = Perhitungan._getskala;

        //regresi
        konstanta = Perhitungan._getA;
        koef = Perhitungan._getB;
        persamaan = ("Persamaan Y = (" + konstanta + ") + (" + koef + ") X");
        regresi = Perhitungan._getRegresi;

        //writeCSV
        if (Input.GetKeyDown(KeyCode.Space))
        {
            WriteCSV(ukuransepatu, korelasi, regresi);
            Debug.Log("CSV Saved");
        }

        //SetText Output
        setSepatu(Menu._ukuransepatu);
        setBerat(regresi.ToString());
    }

    public void WriteCSV(int uk, double kor, double regresi)
    {
        string hub = "";
        if (kor > 0)
        {
            hub = "Lurus/Linier";
        }
        else
        {
            hub = "Terbalik";
        }

        determinasi = ((kor * kor) * 100);
        fktlain = 100 - determinasi;

        TextWriter tw = new StreamWriter(filename, false);
        tw.WriteLine("Ukuran Sepatu = " + uk);
        tw.WriteLine("Berat Badan = " + regresi + " KG");

        tw.WriteLine("\n");
        tw.WriteLine("SUM X = " + Perhitungan._getSumX);
        tw.WriteLine("SUM Y = " + Perhitungan._getSumY);
        tw.WriteLine("SUM XY = " + Perhitungan._getSumXY);
        tw.WriteLine("SUM X^2 = " + Perhitungan._getSumX2);
        tw.WriteLine("SUM Y^2 = " + Perhitungan._getSumY2);

        tw.WriteLine("\n");
        tw.WriteLine("Korelasi");
        tw.WriteLine("Hubungan dimiliki = " + hub);
        tw.WriteLine("Skala Guilford = " + skala);
        tw.WriteLine("Koefisien Determinasi = " + determinasi + "%");
        tw.WriteLine("Kontribusi dari faktor lain = " + fktlain + "%");
        tw.WriteLine("Korelasi Ukuran Sepatu dan Berat Badan = " + kor);

        tw.WriteLine("\n");
        tw.WriteLine("Regresi");
        tw.WriteLine("Nilai X = " + uk);
        tw.WriteLine("Nilai Konstanta = " + konstanta);
        tw.WriteLine("Nilai Koefisien = " + koef);
        tw.WriteLine(persamaan);
        tw.WriteLine("Hasil Regresi = " + regresi + " KG");

        tw.Close();

    }

    public void setBerat(string input)
    {
        textBerat.GetComponent<Text>().text = input + " KG";

    }

    public void setSepatu(string input)
    {
        textSepatu.GetComponent<Text>().text = input;

    }
}
