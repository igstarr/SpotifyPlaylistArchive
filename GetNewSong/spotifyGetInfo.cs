using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetNewSong
{
    public class spotifyGetInfo
    {

        public Item[] Items { get; set; }
        public class Item
        {
            public DateTime AddedAt { get; set; }
            public Track Track { get; set; }
        }
        public class Track
        {
            public Artist[] Artists { get; set; }
            public string Id { get; set; }
            public string Name { get; set; }
        }
        public class Artist
        {
            public string Name { get; set; }
        }

    }
}
