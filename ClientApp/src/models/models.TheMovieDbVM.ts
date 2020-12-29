declare namespace models {

  export interface SearchMovieVM {
    personName: string;
    details: DetailsMovieVM[];
  }


  export interface DetailsMovieVM {
    movieName: string;
    otherActors: string;
    releaseYear: string;
  }
}
