using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CSVReader
{
    public static string[][] Read(string filename)
    {
        Debug.Log(filename);

        string text = (Resources.Load(filename) as TextAsset).text;
        string[] texts = text.Split('\n');
        string[][] output = new string[texts.Length][];
        // output[][0]: 젤리 , [1]: obstacle, [2]: 발판
        for (int i = 2; i < output.Length; i++)
        {
            output[i] = texts[i].Split(',');

            /*string s = "";
            for (int j = 0; j < output[i].Length - 1; j++)
            {
                s += output[i][j];
                Debug.Log(s);
            }*/

        }
        return output;
    }
    public static int[][] ReadInt(string filename)
    {
        string text = (Resources.Load(filename) as TextAsset).text;
        string[] texts = text.Split('\n');
        string[][] output = new string[texts.Length][];
        int[][] output2 = new int[texts.Length][];
        // output[][0]: 젤리 , [1]: obstacle, [2]: 발판
        for (int i = 2; i < output.Length - 1; i++)
        {
            output[i] = texts[i].Split(',');

            //string s = "";
            for (int j = 0; j < output[i].Length; j++)
            {
                output2[i][j] = int.Parse(output[i][j]);
                /*
                s += output[i][j];
                Debug.Log(s);
                */
            }

        }
        return output2;
    }

    public static List<(int a, int b, int c)> ReadList(string filename)
    {
        string text = (Resources.Load(filename) as TextAsset).text;
        string[] texts = text.Split('\n');
        string[][] output = new string[texts.Length][];
        List<(int a, int b, int c)> output2 = new List<(int, int, int)>();
        // output[][0]: 젤리 , [1]: obstacle, [2]: 발판
        for (int i = 2; i < output.Length - 1; i++)
        {
            output[i] = texts[i].Split(',');
            output2.Add((int.Parse(output[i][0]), int.Parse(output[i][1]), int.Parse(output[i][2])));

        }
        //Debug.Log(output2.Count);
        return output2;
    }
}