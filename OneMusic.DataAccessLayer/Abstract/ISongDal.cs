using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.DataAccessLayer.Abstract
{
    public interface ISongDal : IGenericDal<Song>
    {
        List<Song> GetSongsWithAlbumAndArtist(); //Şarkıları albümleri ve sanatçılarıyla birlikte getir.
        List<Song> GetSongWithAlbum();
        List<Song> GetSongswithAlbumByUserId(int id); //Kullanıcının idsine göre albumdeki şarkıları getirme
        List<Song> GetSongsByAlbumId(int id);
    }
}
