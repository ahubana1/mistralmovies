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

        // public static IEnumerable<ActorMovie> SeedActorMovies()
        // {
        //     List<Movie> movies = new List<Movie>();
        //     List<ActorMovie> actorMovies = new List<ActorMovie>();

        //     foreach (var mh in movieHelpers)
        //     {
        //         List<Actor> actors = new List<Actor>();
        //         foreach (var ah in mh.AllActors)
        //         {
        //             actorMovies.Add(new ActorMovie
        //             {
        //                 CastId = ah.Id,
        //                 MoviesId = mh.imdbID
        //             });
        //         }
        //     }

        //     return actorMovies;
        // }

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
    }
}
