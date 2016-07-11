using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SpeechRecognition
{
    public class Feature
    { 
        
        private Int16[] data; //contains the wav sample data
        public string word;  //the word entered
        public double[] realData; //data after silense is removed
        private double[][] featureArray; // framewise collection of extracted features

        //gets sample data from the wav file
        public void getData(String str)
        {
            WaveFile w = new WaveFile(str);
            data = w.Read();
            data[0] = data[1];
      //      Console.WriteLine(data[0]+" "+data.Length);
          
        }

        
        //A NEW SILENCE DETECTION ALOGORITHM
        //removes silence and noise
        public void detectVoice()
        {
            realData = VoiceDetector.find(realData);
         //   Console.WriteLine(realData.Length);
       /*  for (int i = 0; i < realData.Length; i++)
           {

                Console.WriteLine(realData[i]);
          } */

        }

        //PASS THE SAMPLE THROUGH HIGH ORDER FILTER
        public void preemphasis()
        {
            realData = new double[data.Length];
            realData[1] = 0.05 * data[1];
            for (int i = 2; i < data.Length; ++i)
            {
                realData[i] = data[i] - 0.95 * data[i - 1];
            }
         
        }
        //computes mfcc features
        public void feature_computation()
        {
            int ind=realData.Length/80+1;
            featureArray = new double[ind][];
            int frameSize = 160;
            int crossSize = 80;
            // Window Parameters(Assume numSamples >= frameSize) 
            double[] win = new double[frameSize];
            constructWindow(ref win, frameSize);
            // Framing and processing 
            double[] frame = new double[frameSize];
            int frameStart = 1, frameEnd = frameSize, frameLen = frameSize, frameCount = 0;
            while (true)
            {
               //current frame
                frameCount++;
                for (int i = 0; i < frameLen; i++)
                {
                    frame[i] = realData[i + frameStart];
                }
                // Windowing
                applyWindow(ref frame, win, frameLen);
                double[] magnitude;
                // FFT Transform
                magnitude = FftAlgorithm.Calculate(frame);
                normalise(ref magnitude);
                //mel coeffiecients
                featureArray[frameCount - 1] = Mfcc.calculate(magnitude);
                //next frame
                frameStart += crossSize;
                frameEnd += crossSize;
                frameLen = frameEnd - frameStart;
                if (frameStart >= realData.Length)
                {
                    break;	// no more frames
                }
                if (frameEnd >= realData.Length)
                {
                    frameEnd = realData.Length;
                    frameLen = frameEnd - frameStart;
                    // Length of frame changed, Reconstruct Window
                    constructWindow(ref win, frameLen);
                }
            }
      /*      for (int i = 0; i < frameCount; i++)
            {
                for (int j = 0; j < 26; j++)
                    Console.WriteLine(featureArray[i][j]);
            } */
        }

        public double compare(Feature obj)
        {
            int n1=this.featureArray.GetLength(0);
            int n2=obj.featureArray.GetLength(0);
         //   Console.WriteLine(n1+" "+n2);
             double[,] distances=new double[n1+1,n2+1];
	         int i = 0, j = 0, k = 0;
             if (obj.featureArray[n2 - 1] == null)
                 n2 = n2 - 1;
            if (this.featureArray[n1 - 1] == null)
                 n1 = n1 - 1;

            // Compute the distances of all pairs of local MFCC features
	
	      for (i = 0; i < n1; i++)
	    {
	     	for (j = 0; j < n2; j++)
	    	{
			    distances[i + 1,j + 1] = 0;
			    for (k = 0; k < 26; k++)
			     	distances[i + 1,j + 1] += Math.Pow(this.featureArray[i][k] - obj.featureArray[j][k], 2);
			    distances[i + 1,j + 1] = Math.Sqrt(distances[i + 1,j + 1]);
		    } 
    	}

          // Fill the borders with infinity
          for (i = 0; i <= n1; i++)
              distances[i, 0] = double.PositiveInfinity;
          for (i = 0; i <= n2; i++)
              distances[0, i] = double.PositiveInfinity;
          // The only valid starting value at the edge
	distances[0,0] = 0;

    // Compute the cheapest way from one to the other end of the matrix
	for (i = 1; i <= n1; i++)
		for (j = 1; j <= n2; j++)
		{
            // Determine the most favorable predecessor
			double prev_min = distances[i - 1,j];
			if (distances[i - 1,j - 1] < prev_min)
				prev_min = distances[i - 1,j - 1];
			if (distances[i,j - 1] < prev_min)
				prev_min = distances[i,j - 1];
			distances[i,j] += prev_min;
		}
    // Normalize the distance
	return distances[n1,n2] / Math.Sqrt(Math.Pow(n1, 2) + Math.Pow(n2, 2));
        }


        private void normalise(ref double[] magnitude)
        {
            double mean = 0;
            for (int j = 0; j < magnitude.Length; j++)
                mean += magnitude[j];
            mean /= magnitude.Length;
            for (int j = 0; j < magnitude.Length; j++)
                magnitude[j] /= mean;
        }

        private void constructWindow(ref double[] win, int len)
        {
             	for (int i = 0; i <len; ++i)
	       {
	          	win[i] = 0.54 - 0.46*Math.Cos(2.0*(Math.PI)*i/(len-1));
	        }
        }

      private  void applyWindow(ref double[] frame, double[] win, int len)
       {
            	for (int i = 0; i <len; ++i)
         	{
	          	frame[i] = frame[i]*win[i];
	        }
        }
    }
}
