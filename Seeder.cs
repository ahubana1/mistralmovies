using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics.CodeAnalysis;
using Movies_Mistral.Models; 

namespace Movies_Mistral.Seeder
{
    public class ActorHelper
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class MovieHelper
    {
        public string Title { get; set; }
        public string Plot { get; set; }
        public string Poster { get; set; }
        public string imdbID { get; set; }
        public string imdbRating { get; set; }
        public string imdbVotes { get; set; }
        public string Released { get; set; }
        public string Genre { get; set; }
        public string Actors { get; set; }
        public DateTimeOffset ReleaseDate => DateTimeOffset.Parse(Released);
        public double Rating => Double.Parse(imdbRating);
        public List<string> Genres => Genre.Split(",").Select(p => p.Trim()).ToList();
        public List<ActorHelper> AllActors => ConvertToActors(Actors).ToList();
        public int Votes => int.Parse(imdbVotes.Replace(",", ""));

        public IEnumerable<ActorHelper> ConvertToActors(string actors)
        {
            var allActors = Actors.Split(",").Select(p => p.Trim()).ToList();

            foreach (var a in allActors)
            {
                var id = ComputeSha256Hash(a).Substring(0, 8);
                var name = a;
                yield return new ActorHelper { Id = id, Name = name };
            }
        }

        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }

    public class SeasonHelper
    {
        public int Id { get; set; }
        public string StringId => ShowHelper.ComputeSha256Hash(Id.ToString()).Substring(0, 8);
        public int SeasonNumber { get; set; }
        public List<EpisodeHelper> Episodes { get; set; }
    }

    public class EpisodeHelper
    {
        public int Id { get; set; }
        public string StringId => ShowHelper.ComputeSha256Hash(Id.ToString()).Substring(0, 8);
        public int EpisodeNumber { get; set; }
        public string Title { get; set; }
        public string Plot { get; set; }
        public double Rating => Math.Round(new Random().NextDouble() + 3, 1);
        public int RatingCount => new Random().Next(50, 250);
        public List<string> Cast { get; set; }
        public string AirDate { get; set; }
        public DateTimeOffset? Aired => string.IsNullOrWhiteSpace(AirDate) ? null : DateTimeOffset.Parse(AirDate);
    }

    public class ShowHelper
    {
        public int Id { get; set; }
        public string StringId => ComputeSha256Hash(Id.ToString()).Substring(0, 8);
        public string Title { get; set; }
        public string Plot { get; set; }
        public string PosterPath { get; set; }
        public double Rating { get; set; }
        public int RatingCount { get; set; }
        public List<string> Cast { get; set; }

        public List<SeasonHelper> Seasons { get; set; }
        public DateTimeOffset FirstAired { get; set; }
        public List<ActorHelper> AllActors => ConvertToActors().ToList();

        public IEnumerable<ActorHelper> ConvertToActors()
        {
            var allActors = Cast.Select(p => p.Trim()).ToList();

            foreach (var a in allActors)
            {
                var id = ComputeSha256Hash(a).Substring(0, 8);
                var name = a;
                yield return new ActorHelper { Id = id, Name = name };
            }
        }


        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }


    public class SeederClass
    {
        public static string jsonString = File.ReadAllText("imdbtop250.json");
        public static List<MovieHelper> movieHelpers = JsonSerializer.Deserialize<List<MovieHelper>>(jsonString);

        public static IEnumerable<Movie> SeedMovies(MoviesContext context)
        {
            List<Movie> movies = new List<Movie>();

            foreach (var mh in movieHelpers)
            {
                List<Actor> actors = new List<Actor>();
                foreach (var ah in mh.AllActors)
                {
                    actors.Add(context.Actors.Single(a => a.Id == ah.Id));
                }

                movies.Add(new Movie
                {
                    Id = mh.imdbID,
                    Title = mh.Title,
                    Plot = mh.Plot,
                    ReleaseDate = mh.ReleaseDate,
                    Rating = mh.Rating/2,
                    NumberOfRatings = mh.Votes,
                    CoverImagePath = "wwwroot/images/" + mh.imdbID.Substring(0, 5) + "/" + mh.imdbID + ".jpg",
                    Actors = actors
                });
            }

            return movies;
        }



        public static IEnumerable<Actor> SeedActors()
        {
            List<Actor> actors = new List<Actor>();

            foreach (var mh in movieHelpers)
            {
                foreach (var ah in mh.AllActors)
                {
                    actors.Add(new Actor
                    {
                        Id = ah.Id,
                        FirstName = ah.Name.Split(" ")[0],
                        LastName = ah.Name.Substring(ah.Name.IndexOf(' ') + 1)
                    });
                }
            }
            return actors.Distinct(new EqualityComparer());
        }



        public class EqualityComparer : IEqualityComparer<Actor>
        {
            public bool Equals([AllowNull] Actor x, [AllowNull] Actor y)
            {
                if (x == null || y == null) return false;
                else if (x == null && y == null) return true;
                return x.Id == y.Id;
            }

            public int GetHashCode([DisallowNull] Actor obj)
            {
                return obj.Id.GetHashCode();
            }
        }

        public static IEnumerable<Actor> SeedActorsFromShow(int show)
        {
            var jsonString = File.ReadAllText("ShowsSeedData/tvshow" + show + ".json");
            var showHelper = JsonSerializer.Deserialize<ShowHelper>(jsonString);
            List<Actor> actors = new List<Actor>();

            foreach (var ah in showHelper.AllActors)
            {
                actors.Add(new Actor
                {
                    Id = ah.Id,
                    FirstName = ah.Name.Split(" ")[0],
                    LastName = ah.Name.Substring(ah.Name.IndexOf(' ') + 1),
                });
            }

            return actors.Distinct(new EqualityComparer());
        }

        public static IEnumerable<Episode> SeedEpisodesFromShow(int show, MoviesContext context)
        {
            var jsonString = File.ReadAllText("ShowsSeedData/tvshow" + show + ".json");
            var showHelper = JsonSerializer.Deserialize<ShowHelper>(jsonString);

            List<Episode> episodes = new List<Episode>();

            List<Actor> actors = new List<Actor>();
            foreach (var ah in showHelper.AllActors)
            {
                actors.Add(context.Actors.Single(a => a.Id == ah.Id));
            }

            foreach (var seasonHelper in showHelper.Seasons)
            {
                foreach(var episodeHelper in seasonHelper.Episodes)
                {
                    var dtoffset = string.IsNullOrWhiteSpace(episodeHelper.AirDate) || string.IsNullOrEmpty(episodeHelper.AirDate) ? DateTimeOffset.Now : episodeHelper.Aired;
                    episodes.Add(new Episode {
                        Id = episodeHelper.StringId,
                        Title = episodeHelper.Title,
                        Plot = episodeHelper.Plot,
                        ReleaseDate = (DateTimeOffset)dtoffset,
                        Rating = episodeHelper.Rating,
                        NumberOfRatings = episodeHelper.RatingCount,
                        CoverImagePath = "wwwroot/images/" + showHelper.StringId + ".jpg",
                        Actors = actors,
                        EpisodeNumber = episodeHelper.EpisodeNumber,
                    });
                }
            }

            return episodes;
        }

        public static IEnumerable<Season> SeedSeasonsFromShow(int show, MoviesContext context)
        {
            var jsonString = File.ReadAllText("ShowsSeedData/tvshow" + show + ".json");
            var showHelper = JsonSerializer.Deserialize<ShowHelper>(jsonString);

            List<Season> seasons = new List<Season>();

            List<Episode> episodes = new List<Episode>();
            foreach (var seasonHelper in showHelper.Seasons)
            {
                foreach(var eh in seasonHelper.Episodes)
                {
                    episodes.Add(context.Episodes.Single(e => e.Id == eh.StringId));
                }
            }

            foreach (var seasonHelper in showHelper.Seasons)
            {
                seasons.Add(new Season { 
                    Id = seasonHelper.StringId,
                    SeasonNumber = seasonHelper.SeasonNumber,
                    Episodes = episodes
                });
            }

            return seasons;
        }

        public static Show SeedShow(int show, MoviesContext context)
        {
            var jsonString = File.ReadAllText("ShowsSeedData/tvshow" + show + ".json");
            var showHelper = JsonSerializer.Deserialize<ShowHelper>(jsonString);

            List<Actor> actors = new List<Actor>();
            List<Season> seasons = new List<Season>();
            foreach (var ah in showHelper.AllActors)
            {
                actors.Add(context.Actors.Single(a => a.Id == ah.Id));
            }

            foreach(var seasonHelper in showHelper.Seasons)
            {
                seasons.Add(context.Seasons.Single(s => s.Id == seasonHelper.StringId));
            }

            return new Show
            {
                Id = showHelper.StringId,
                Title = showHelper.Title,
                Plot = showHelper.Plot,
                ReleaseDate = showHelper.FirstAired,
                Rating = showHelper.Rating,
                NumberOfRatings = showHelper.RatingCount,
                CoverImagePath = "wwwroot/images/" + showHelper.StringId + ".jpg",
                Actors = actors,
                Seasons = seasons
            };
        }
    }
}
