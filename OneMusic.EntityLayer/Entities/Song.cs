using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.EntityLayer.Entities
{
    public class Song
    {
        public int SongId { get; set; }
        public string SongName { get; set; }
        public string SongUrl { get; set; }
        public int AlbumId { get; set; }
        public Album Album { get; set; }
    }
}
