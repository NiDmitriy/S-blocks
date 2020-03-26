using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_lab
{

    class Program
    {
        static void Main(string[] args)
        {
            int[,] S_block = {
            { 10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8 },
            { 13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1 },
            { 13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7 },
            { 1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12 }
            };
            string text = Console.ReadLine();
            FileStream fstream = File.OpenRead(text);
            byte[] ByteArray = new byte[fstream.Length];
            fstream.Read(ByteArray, 0, ByteArray.Length);
            BitArray bits = new BitArray(ByteArray);
            BitArray bits_2 = new BitArray(2);
            BitArray bits_4 = new BitArray(4);
            bool[] ar = new bool[4];
            const int a = 3, b = 4;
            byte stolb = 0, result = 0, result_1 = 0;

            for (int i = 0; i < bits.Length / 6; i++)
            {

                bits_4[0] = bits[6 * i];
                bits_4[1] = bits[6 * i + 1];
                bits_4[2] = bits[6 * i + 2];
                bits_4[3] = bits[6 * i + 5];
                bits_2[0] = bits[6 * i + 3];
                bits_2[1] = bits[6 * i + 4];
                for (byte index = 0, m = 1; index < 4; index++, m *= 2)
                {
                    if (bits_4[index])
                    {
                        result += m;
                    }
                }
                for (byte index = 0, m = 1; index < 2; index++, m *= 2)
                {
                    if (bits_2[index])
                    {
                        result_1 += m;
                    }
                }
                bits_4 = new BitArray(new byte[] { (byte)(S_block[result_1, result]) });
                bits[i * 6] = bits_2[1];
                bits[i * 6 + 1] = bits_2[0];
                bits[i * 6 + 2] = bits_4[3];
                bits[i * 6 + 3] = bits_4[2];
                bits[i * 6 + 4] = bits_4[1];
                bits[i * 6 + 5] = bits_4[0];
                result = 0;
                result_1 = 0;
            }
            bits_4 = new BitArray(8);
            byte[] by = new byte[1];
            int schet = 0;
            Console.WriteLine();
            Console.WriteLine("READY");
            bits_4 = new BitArray(4);
            
            for (int i = 0; i < bits.Length/6; i++)
            {
                result = 0;
                result_1 = 0;
                bits_2[0] = bits[i * 6 + 1];
                bits_2[1] = bits[i * 6 + 0];
                bits_4[3] = bits[i * 6 + 2];
                bits_4[2] = bits[i * 6 + 3];
                bits_4[1] = bits[i * 6 + 4];
                bits_4[0] = bits[i * 6 + 5];
                for (byte index = 0, m = 1; index < 2; index++, m *= 2)
                {
                    if(bits_2[index])
                    {
                        result += m;
                    }
                }
                for (byte index = 0, m = 1; index < 4; index++, m *= 2)
                {
                    if (bits_4[index])
                    {
                        result_1 += m;
                    }
                }
                for (int l = 0; l < 16; l++)
                {
                    if (S_block[result, l] == result_1)
                    {
                        result_1 = (byte)l;
                        break;
                    }
                }
                bits_4 = new BitArray(new byte[] { (byte)(result_1) });
                schet = 0;
                bits[i * 6 + 3] = bits_2[0];
                bits[i * 6 + 4] = bits_2[1];
                bits[i * 6] = bits_4[0];
                bits[i * 6+1] = bits_4[1];
                bits[i * 6+2] = bits_4[2];
                bits[i * 6+5] = bits_4[3];
                schet++;
            }
            
            bits_4 = new BitArray(8);
            by = new byte[bits.Count / 8];
            schet = 0;
            for (int i = 0; i < bits.Length / 8; i++)
            {
                result = 0;
                for (int k = 0; k < 8; k++)
                {
                    bits_4[k] = bits[i * 8 + k];
                }
                for (byte index = 0, m = 1; index < 8; index++, m *= 2)
                {
                    if(bits_4[index])
                    {
                        result += m;
                    }
                }
                by[schet] = result;
                schet++;
            }
            FileStream outStream1 = File.Create(@"file path");
            outStream1.Write(by, 0, by.Length);
            Console.WriteLine("Текст записан в файл");
            outStream1.Close();
            Console.ReadKey();
        }      
    }
}
