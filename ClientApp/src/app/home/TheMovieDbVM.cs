using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Movies.ClientApp.src.app.home
{
  [DataContract]
  public class KnownFor
  {
    [DataMember]
    public bool adult { get; set; }
    [DataMember]
    public string backdrop_path { get; set; }
    [DataMember]
    public List<int> genre_ids { get; set; }
    [DataMember]
    public int id { get; set; }
    [DataMember]
    public string media_type { get; set; }
    [DataMember]
    public string original_language { get; set; }
    [DataMember]
    public string original_title { get; set; }
    [DataMember]
    public string overview { get; set; }
    [DataMember]
    public string poster_path { get; set; }
    [DataMember]
    public string release_date { get; set; }
    [DataMember]
    public string title { get; set; }
    [DataMember]
    public bool video { get; set; }
    [DataMember]
    public double vote_average { get; set; }
    [DataMember]
    public int vote_count { get; set; }
    [DataMember]
    public string first_air_date { get; set; }
    [DataMember]
    public string name { get; set; }
    [DataMember]
    public List<string> origin_country { get; set; }
    [DataMember]
    public string original_name { get; set; }
  }
  [DataContract]
  public class Result
  {
    [DataMember]
    public bool adult { get; set; }
    [DataMember]
    public int gender { get; set; }
    [DataMember]
    public int id { get; set; }
    [DataMember]
    public List<KnownFor> known_for { get; set; }
    [DataMember]
    public string known_for_department { get; set; }
    [DataMember]
    public string name { get; set; }
    [DataMember]
    public double popularity { get; set; }
    [DataMember]
    public string profile_path { get; set; }
  }
  [DataContract]
  public class PersonVM
  {
    [DataMember]
    public int page { get; set; }
    [DataMember]
    public List<Result> results { get; set; }
    [DataMember]
    public int total_pages { get; set; }
    [DataMember]
    public int total_results { get; set; }
  }

}
