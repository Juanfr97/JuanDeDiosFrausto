using JuanDeDiosFrausto.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JuanDeDiosFrausto.Controllers
{
    [Route("api/songs")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ApplicationDBContext context;

        public SongController(ApplicationDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Song>>> GetSongs()
        {
            var songs = await context.Songs.ToListAsync();
            return Ok(songs);
        }

        [HttpGet("ismn")]
        public async Task<ActionResult> GetSongByISMN(string ismn)
        {
            var song = await context.Songs.FirstOrDefaultAsync(s => s.ISMN == ismn);
            if(song == null)
                return NotFound($"The song {ismn} was not found");
            return Ok(song);
        }

        [HttpPost]
        public async Task<ActionResult> CreateSong(Song song)
        {
            var songExists = await context.Songs.AnyAsync(s=>s.ISMN==song.ISMN);
            if (songExists)
                return BadRequest($"The song {song.ISMN} already exists");

            context.Add(song);
            await context.SaveChangesAsync();
            return Ok($"Song {song.Name} added properly");
        }

        [HttpPut("ismn")]
        public async Task<ActionResult> UpdateSong(string ismn,[FromBody]Song song)
        {
            var songExists = await context.Songs.AnyAsync(s => s.ISMN == song.ISMN);
            if (!songExists)
                return NotFound($"The song {ismn} was not found");

            Song updatedSong = new Song()
            {
                ISMN = ismn,
                Name = song.Name,
                Album = song.Album,
                Artist = song.Artist,
                AlbumYear = song.AlbumYear,
                Image = song.Image,
                Duration = song.Duration,
            };
            context.Songs.Update(updatedSong);
            await context.SaveChangesAsync();
            return Ok($"The song {ismn} was updated properly");
        }

        [HttpDelete("ismn")]
        public async Task<ActionResult> DeleteSong(string ismn)
        {
            var songExists = await context.Songs.AnyAsync(s => s.ISMN == ismn);
            if (!songExists)
                return BadRequest($"The song {ismn} was not found");

            context.Remove(new Song() { ISMN = ismn });
            await context.SaveChangesAsync();
            return Ok($"The song {ismn} was elimanted properly");
        }
    }
}
