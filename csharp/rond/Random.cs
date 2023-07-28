using System;

namespace GenerationDonjon2
{
    class random
    {
        public int randint(int min,int max)
        {
            return (new Random()).Next(min,max+1);
        }
    }
}