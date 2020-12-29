using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Movies.ClientApp.src.app.home
{
  [ApiController]
  [Route("api/[controller]/[action]")]
  public class HomeController : ControllerBase
  {
    public async Task<List<DetailsMovieVM>> GetInfoByActorOrDirector(string person)
    {
      try
      {
        const string director = "Directing";
        const string actor = "Acting";
        string theMovieDbApiKey = ConfigurationManager.AppSettings["apiKeyTheMovieDb"];
        const string theMovieDbApi = "http://api.themoviedb.org/3/search/person?api_key={0}&language=en-US&query={1}&include_adult=false";

        var resultMovieDb = await HTTPClient<PersonVM>.GetApi(theMovieDbApi, theMovieDbApiKey, person);

        var listActorDirectors = resultMovieDb.results
                                    .Where(x => x.name.ToUpper().Contains(person.ToUpper()))
                                    .Where(x => x.known_for_department == director || x.known_for_department == actor)
                                    .Select(a => new SearchMovieVM()
                                    {
                                      PersonName = a.name,
                                      Details = a.known_for.Select(y => new DetailsMovieVM()
                                      {
                                        PersonName = a.name,
                                        DirectorName = a.name,
                                        IsDirector = a.known_for_department == director,
                                        MovieName = y.title != null ? y.title : y.original_name
                                      }).ToList()
                                    }).ToList();


        const string omdApi = "http://www.omdbapi.com/?apikey={0}&t={1}";
        string omdApiKey = ConfigurationManager.AppSettings["apiKeyOmdb"];

        //not good pratice loop with data in memory, but is the way to use OmdbApi
        var listMovieDetails = listActorDirectors.SelectMany(x => x.Details).ToList();
        foreach (var movie in listMovieDetails)
        {
          try
          {
            var resultOmdb = await HTTPClient<OmdbVM>.GetApi(omdApi, omdApiKey, movie.MovieName);

            movie.DirectorName = !movie.IsDirector ? resultOmdb.Director : movie.DirectorName;
            movie.OtherActors = resultOmdb.Actors;
            movie.ReleaseYear = resultOmdb.Year;
            movie.PersonName = movie.PersonName;

            using (var webClient = new WebClient())
            {
              byte[] imageBytes = webClient.DownloadData(resultOmdb.Poster);
              movie.PosterBase64 = "data:image/png;base64," + Convert.ToBase64String(imageBytes);
            }
          }
          catch (Exception)
          {
            //if the image link is invalid, process next image
            continue;
          }
        }
        return listMovieDetails;
      }
      catch (Exception)
      {
        throw;
      }
    }
  }

  public static class HTTPClient<T> where T : class
  {
    public static async Task<T> GetApi(string urlApi, string apiKey, string filter)
    {
      try
      {
        var http = new HttpClient();
        var url = string.Format(urlApi, apiKey, filter);
        var response = await http.GetAsync(url);
        var result = await response.Content.ReadAsStringAsync();
        var serializer = new DataContractJsonSerializer(typeof(T));

        var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
        var data = (T)serializer.ReadObject(ms);

        return data;
      }
      catch (Exception)
      {
        throw;
      }
    }
  }
}
