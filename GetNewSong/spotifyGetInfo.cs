using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetNewSong
{
    public class spotifyGetInfo
    {

    
            public Item[] items { get; set; }

        public class Item
        {
            public DateTime added_at { get; set; }
            public Track track { get; set; }
        }

        public class Track
        {
            public Artist[] artists { get; set; }
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Artist
        {
            public string name { get; set; }
        }

    }
}
