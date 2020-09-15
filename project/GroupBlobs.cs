using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coin_Detections
{
    class GroupBlobs
    {
        List<Blob> blobs;
        public GroupBlobs()
        {
            blobs = new List<Blob>();
        }
        public void groupAdd(Blob b)
        {
            blobs.Add(b);
        }
        public int getCount()
        {
            return blobs.Count;
        }
        public Blob getZero()
        {
            return blobs[0];
        }
    }
}
