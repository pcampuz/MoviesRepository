using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Movies.ClientApp.src.app.home
{
  [DataContract]
  public class SearchMovieVM
  {
    [DataMember]
    public string PersonName { get; set; }

    [DataMember]
    public List<DetailsMovieVM> Details { get; set; }
  }

  [DataContract]
  public class DetailsMovieVM : SearchMovieVM
  {
    [DataMember]
    public string MovieName { get; set; }

    [DataMember]
    public string DirectorName { get; set; }

    [DataMember]
    public bool IsDirector { get; set; }

    [DataMember]
    public string OtherActors { get; set; }

    [DataMember]
    public string ReleaseYear { get; set; }

    [DataMember]
    public string PosterBase64 { get; set; }
  }
}
