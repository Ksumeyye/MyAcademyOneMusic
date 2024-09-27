using OneMusic.EntityLayer.Entities;
namespace OneMusic.WebUI.Areas.Artist.Models
{
    public class SongViewModel
    {
        public int SongId { get; set; }
        public string SongName { get; set; }
        public string SongUrl { get; set; }
        public IFormFile SongFile { get; set; }
        public IFormFile SongImageUrl { get; set; }
        public int AlbumId { get; set; }
        public Album Album { get; set; }
    }
}
