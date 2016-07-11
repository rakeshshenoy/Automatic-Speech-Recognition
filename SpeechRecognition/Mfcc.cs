using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeechRecognition
{
  public static  class Mfcc
    {
       const int N_FILTER=26;
       const int N_Mfcc = 26;
       // Asymmetric triangle filter
       // List of center frequencies; Bandwidth of the left adjacent to the right adjacent center frequency
      static int[] mel_filters = new int[] {150, 200, 250, 300, 350, 400, 450,	//Linear
				490, 560, 640, 730, 840, 960, 1100,	//500-1000Hz Logarithmic
				1210, 1340, 1480, 1640, 1810, 2000,	//1000-2000Hz Logarithmic
				2250, 2520, 2840, 3190, 3580, 4020};	//2000-4000Hz Logarithmic
      public static double[] calculate(double[] magnitude)
      { 
             int i = 0, j = 0, k = 0;
             double[] features = new double[N_FILTER];
	         double[] filterOutput=new double[N_FILTER];
             double[,] filterSpectrum = new double[N_FILTER, magnitude.Length];
	         double c0 = Math.Sqrt(1.0 / N_FILTER);
	         double cn = Math.Sqrt(2.0 / N_FILTER);
	         double pi = 4 * Math.Atan(1.0);
             // Create for each filter its spectrum.
             for (i = 0; i < N_FILTER; i++)
             {
                 double maxFreq = 0, minFreq = 0, centerFreq = 0;
                 // The first Mel-filter has no bottom neighbor filter, so it is made symmetrical.
                 if (i == 0)
                     minFreq = mel_filters[0] - (mel_filters[1] - mel_filters[0]);
                 else
                     minFreq = mel_filters[i - 1];
                 centerFreq = mel_filters[i];
                 // The last mel filter has no neighbors above filter, so it is made symmetrical.
                 if (i == N_FILTER - 1)
                     maxFreq = mel_filters[N_FILTER - 1] + (mel_filters[N_FILTER - 1] - mel_filters[N_FILTER - 2]);
                 else
                     maxFreq = mel_filters[i + 1];
                 // Calculate the coefficients of the filter for each frequency.
                 for (j = 0; j < magnitude.Length; j++)
                 {
                     double freq = 1.0 * j * 8000 / magnitude.Length;
                     // Is the frequency within the filter range?
                     if (freq > minFreq && freq < maxFreq)
                         // When ascending or descending flank?
                         if (freq < centerFreq)
                             filterSpectrum[i,j] = 1.0 * (freq - minFreq) / (centerFreq - minFreq);
                         else
                             filterSpectrum[i,j] = 1 - 1.0 * (freq - centerFreq) / (maxFreq - centerFreq);
                     else
                         filterSpectrum[i,j] = 0;
                 }
             }

             // Set each filter a and calculate the sum.
            
                 for (j = 0; j < N_FILTER; j++)
                 {
                     filterOutput[j] = 0;
                     for (k = 0; k < magnitude.Length; k++)
                         filterOutput[j] += magnitude[k] * filterSpectrum[j,k];
                 }
                 // Compute the MFCC features using the sums and the discrete cosine transform.
                 for (j = 0; j < N_Mfcc; j++)
                 {
                       features[j] = 0;

                     for (k = 0; k < N_FILTER; k++)
                         features[j] += Math.Log(Math.Abs(filterOutput[k]) + 1e-10) * Math.Cos((pi * (2 * k + 1) * j) / (2 * N_FILTER));

                     if (j!=0)
                        features[j] *= cn;
                     else
                        features[j] *= c0;
                 }

                 return features;

       }

    }
}
