using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class CSVReader
{
    public static List<(int a, int b, int c, int d)> ReadList(string filename)
    {
        StreamReader sr = new StreamReader($"{Application.dataPath}/Resources/Excel/map.csv"); 
        List<(int a, int b, int c, int d)> output2 = new List<(int, int, int, int)>();

        sr.ReadLine();
        sr.ReadLine();
        bool _isEOF = false;
        while (!_isEOF)
        {
            string data = sr.ReadLine();
            Debug.Log(data);
            if (data == null)
            {
                _isEOF = true;
                break;
            }
            var data_values = data.Split(',');
            output2.Add((int.Parse(data_values[0]), int.Parse(data_values[1]), int.Parse(data_values[2]), int.Parse(data_values[3])));

        }

        return output2;
    }
}