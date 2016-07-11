using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//A New Silence Removal and Endpoint Detection Algorithm
namespace SpeechRecognition
{
 public static class VoiceDetector
    {
     public static double[] find(double[] data)
     {
         double mean, sum, sd;
         sum = 0;
         // 1. calculation of mean
         for (int i = 1; i <= 1600; i++)
         {
             sum += data[i];
         }
         mean = sum / 1600;
         sum = 0;
         // 2. calculation of Standard Deviation
         for (int i = 1; i <= 1600; i++)
         {
             sum += (data[i] - mean) * (data[i] - mean);
         }
         sd = Math.Sqrt(sum / 1600);
      //   Console.WriteLine(sd + " " + mean);
         double[] y = new double[data.Length];
         // 3. identifying whether one-dimensional Mahalanobis distance function
         // i.e. |x-u|/s greater than #3 or not,
         for (int i = 1; i < data.Length; i++)
         {
             if (Math.Abs((data[i] - mean)) / sd > 3)
                 y[i] = 1;
             else
                 y[i] = 0;
         }
         int count = 1; int one = 0; int zero = 0;
         // 4. calculation of voiced and unvoiced signals
         // mark each frame to be voiced or unvoiced frame
         for (int i = 1; i < data.Length; i++)
         {
             if (y[i] == 0)
                 zero++;
             else
                 one++;
             count++;
             if (count == 80)
             {
                 count = 0;
                 if (one > zero)
                 {
                     for (int j = i - 79; j < i; j++)
                         y[j] = 1;
                 }
                 else
                     for (int j = i - 79; j < i; j++)
                         y[j] = 0;
                 one = 0;
                 zero = 0;
             }
         }
        double[] nz = new double[data.Length];
         count = 0;
         // 5. silence removal
         for (int i = 1; i < data.Length; i++)
         {
             if (y[i] == 1)
             {
                 nz[count] = data[i];
                 count++;
             }
         }
         double[] d = new double[count];
         for (int i = 1; i < count; i++)
             d[i] = nz[i];
             return d;
     }
   }
}
