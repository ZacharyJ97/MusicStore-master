using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;
using MusicStore.Data_Contexts;

namespace MetalMania.Controllers
{
    public class HomeController : Controller
    {
        private EFDbContext db = new EFDbContext();
        

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Albums()
        {
            // TO-DO: Call GetAlbums() instead of db.Albums
            ViewBag.Albums = GetAlbums().OrderBy(album => album.Title);
            return View();
        }


        [HttpPost]
        public ActionResult Search(string searchBy, string searchText)
        {
            var albums = GetAlbums();

            if (!String.IsNullOrEmpty(searchText))
            {
                List<AlbumListModel> results;

                searchText = searchText.ToLower();

                if (searchBy == "Artist")
                {
                    results = albums.Where(x => x.Artist.ToLower().Contains(searchText)).ToList();
                }
                else if (searchBy == "Title")
                {
                    results = albums.Where(x => x.Title.ToLower().Contains(searchText)).ToList();
                }
                else if (searchBy == "Year")
                {
                    results = albums.Where(x => x.Year == Int32.Parse(searchText)).ToList();
                }
                else
                {
                    results = albums.Where(x => x.Title.ToLower().Contains(searchText)
                                            || x.Artist.ToLower().Contains(searchText))
                                            .ToList();
                }

                return View(results);
            }

            return View(albums.OrderBy(x => x.Year).ToList());

        }

        private IQueryable<AlbumListModel> GetAlbums()
        {
            var albums = from alb in db.Albums
                         join art in db.Artists on alb.ArtistId equals art.ArtistId
                         select new AlbumListModel
                         {
                             AlbumId = alb.AlbumId,
                             Artist = art.Name,
                             Title = alb.Title,
                             Year = alb.Year
                         };

            return albums;
        }
      
    }
}