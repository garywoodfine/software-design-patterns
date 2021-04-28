using System;

namespace AdapterSample
{
    public class Bicycle
    {
      public  void Pedal()
        {
            Console.WriteLine("Pedaling");
        }

        public void Ring()
        {
            Console.WriteLine("Ringing the Bell to warn pedestrians");
        }
        
    }
}