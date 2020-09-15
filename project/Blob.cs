using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Coin_Detections
{
    class Blob
    {
        int x, y;

        public int Getx()
        {
            return x;
        }
        public int Gety()
        {
            return y;
        }
        

        public Blob(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public float distSq(int x1, int y1, int x, int y)
        {
            float dist = (x - x1) * (x - x1) + (y - y1) * (y - y1);
            return dist;
        }

        public bool isNear(int x1, int y1)
        {
            int cx = (x + x1) / 2;
            int cy = (y + y1) / 2;
            float d = distSq(cx, cy, x1, y1);
            if (d < 6000)
            {
                return true;
            }
            else
                return false;
        }
        
    }
}
