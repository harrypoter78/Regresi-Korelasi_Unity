using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Perhitungan : MonoBehaviour
{
    public static double _getKorelasi, _getRegresi, _getA, _getB, _getSumX, _getSumY, _getSumXY, _getSumX2, _getSumY2;
    public static string _getskala;

    List<IsiData> datas = new List<IsiData>();
    List<IsiData> dataX = new List<IsiData>();
    List<IsiData> dataY = new List<IsiData>();

    void Start()
    {
        TextAsset data = Resources.Load<TextAsset>("data");
        string[] dataset = data.text.Split(new char[] { '\n' });

        for (int i = 1; i < dataset.Length - 1; i++)
        {
            string[] row = dataset[i].Split(new char[] { ',' });

            IsiData q = new IsiData();
            int.TryParse(row[0], out q.id);
            int.TryParse(row[1], out q.UkuranSepatu);
            int.TryParse(row[2], out q.BeratBadan);

            IsiData x = new IsiData();
            int.TryParse(row[1], out x.UkuranSepatu);

            IsiData y = new IsiData();
            int.TryParse(row[2], out y.BeratBadan);

            datas.Add(q);
            dataX.Add(x);
            dataY.Add(y);

        }

        List<int> elementx = new List<int>();
        foreach (IsiData x in dataX)
        {
            elementx.Add(x.UkuranSepatu);

        }

        List<int> elementy = new List<int>();
        foreach (IsiData x in dataY)
        {
            elementy.Add(x.BeratBadan);

        }

        int n = dataset.Length - 2;
        Debug.Log("nilai n = " + n);

        // Function call to correlation
        _getKorelasi = Math.Round(korelasi(elementx, elementy, n) * 1000000.0) / 1000000.0;

        //Skalaguilford
        _getskala = skalaguilford(_getKorelasi);

        // Function call to reggesi
        var tuple = regresi(elementx, elementy, n);
        _getA = tuple.Item1;
        _getB = tuple.Item2;
        _getRegresi = Math.Round(((int.Parse(Menu._ukuransepatu) * _getB) + _getA));

    }

    static float korelasi(List<int> X, List<int> Y, int n)
    {
        int sum_X = 0, sum_Y = 0, sum_XY = 0, squareSum_X = 0, squareSum_Y = 0;

        for (int i = 0; i < n; i++)
        {
            // sum of elements of array X.
            sum_X = sum_X + X[i];

            // sum of elements of array Y.
            sum_Y = sum_Y + Y[i];

            // sum of X[i] * Y[i].
            sum_XY = sum_XY + X[i] * Y[i];

            // sum of square of array elements.
            squareSum_X = squareSum_X + X[i] * X[i];
            squareSum_Y = squareSum_Y + Y[i] * Y[i];
        }
        // getVar to Pass
        _getSumX = sum_X;
        _getSumY = sum_Y;
        _getSumXY = sum_XY;
        _getSumX2 = squareSum_X;
        _getSumY2 = squareSum_Y;

        float atas = (float)(n * sum_XY - sum_X * sum_Y);
        float bawah1 = (float)(n * squareSum_X);
        float bawah2 = (float)(sum_X * sum_X);
        float bawah3 = (float)(n * squareSum_Y);
        float bawah4 = (float)(sum_Y * sum_Y);
        float bawahakar = (float)(Math.Sqrt((bawah1 - bawah2) * (bawah3 - bawah4)));

        // formula for calculating correlation coefficient.
        float corr = atas / bawahakar;

        return corr;
    }

    static string skalaguilford(double input)
    {
        string skala = "";

        if (input < 0.2 && input >= 0)
        {
            skala = "Sangat Lemah";
        }
        else if (input < 0.4 && input >= 0.2)
        {
            skala = "Lemah";
        }
        else if (input < 0.6 && input >= 0.4)
        {
            skala = "Sedang";
        }
        else if (input < 0.8 && input >= 0.6)
        {
            skala = "Kuat";
        }
        else if (input <= 1 && input >= 0.8)
        {
            skala = "Sangat Kuat";
        }

        return skala;
    }

    static Tuple<float, float> regresi(List<int> X, List<int> Y, int n)
    {
        int sumX = 0, sumY = 0, sumXY = 0, sumX2 = 0, sumY2 = 0;

        for (int i = 0; i < n; i++)
        {
            sumX = sumX + X[i];
            sumY = sumY + Y[i];
            sumXY = sumXY + X[i] * Y[i];
            sumX2 = sumX2 + X[i] * X[i];
            sumY2 = sumY2 + Y[i] * Y[i];
        }

        //KONSTANTA A
        float a_atas = (float)(sumY * sumX2) - (sumX * sumXY);
        float a_bawah = (float)((n * sumX2) - (sumX * sumX));
        float a = a_atas / a_bawah;

        //KOEF B
        float b_atas = (float)(n * sumXY) - (sumX * sumY);
        float b_bawah = (float)((n * sumX2) - (sumX * sumX));
        float b = b_atas / b_bawah;

        return new Tuple<float, float>(a, b);
    }
}

public class IsiData
{
    public int id;
    public int UkuranSepatu;
    public int BeratBadan;

}