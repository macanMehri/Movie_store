using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieDataBase
{
    class MyMovies
    {
        public List<Movie> myMovies;
        public MyMovies()
        {
            myMovies = new List<Movie>();
        }
        public void addMovie(Movie m)
        {
            if(!myMovies.Contains(m))
                myMovies.Add(m);
        }
        public void removeMovie(string name)
        {
            var check = 
                from item in myMovies
                where item.movieName == name
                select item;
            if(check.Count() > 0)
                myMovies.Remove(check.First());
        }
        public List<Movie> searchForMovieByGenre(string genre)
        {
            var filtered =
                from item in myMovies
                where item.genre == genre
                select item;
            return filtered.ToList();
        }
        public List<Movie> searchForMovieByImdb(double imdb)
        {
            var filtered =
                from item in myMovies
                where item.Imdb >= imdb
                select item;
            return filtered.ToList();
        }
        public List<Movie> searchForMovieByName(string name)
        {
            var filtered =
                from item in myMovies
                where item.movieName == name
                select item;
            return filtered.ToList();
        }
        public List<Movie> searchForMovieByCreater(string name)
        {
            var filtered =
                from item in myMovies
                where item.filmMakerName == name
                select item;
            return filtered.ToList();
        }
        public List<Movie> searchForAnimations()
        {
            var filtered =
                from item in myMovies
                where item.isAnimation == true
                select item;
            return filtered.ToList();
        }
        public List<Movie> searchForSerials()
        {
            var filtered =
                from item in myMovies
                where item is Serial
                select item;

            return filtered.ToList();
        }
        public void show()
        {
            foreach (var movie in myMovies)
            {
                Console.WriteLine(movie);
            }
        }
    }
   
    class Movie
    {
        public string movieName { get; private set; }
        public string filmMakerName { get; private set; }  
        private double imdb;    
        public string genre { get; private set; }
        private int ages;
        public bool isAnimation;
        private double movieTime;
        public double MovieTime
        {
            get { return movieTime; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Movie time", value, "Time must be higher than zero!!!");
                movieTime = value;
            }
        }  //movieTime property
        public int Ages
        {
            get { return ages; }
            set
            {
                if (ages < 0)
                    throw new ArgumentOutOfRangeException("Ages", value, "Age must be older!!!");
                ages = value;
            }
        }          //ages property
        public double Imdb
        {
            get { return imdb; }
            set
            {
                if (value < 0 || value > 10)
                    throw new ArgumentOutOfRangeException("IMDB", value, "IMDB must be zero or greater than zero and cannot be greater than 10!!!");
                imdb = value;
            }
        }       //imdb property
        //Constructor function
        public Movie(string movieName, string genre,string filmMakerName
            ,double IMDB, int ages,double movieTime,bool isAnimation)
        {
            this.movieName = movieName;
            this.genre = genre;
            this.filmMakerName = filmMakerName;
            Imdb = IMDB;
            Ages = ages;
            MovieTime = movieTime;
            this.isAnimation = isAnimation;
        }
        public static Movie creatMovie()
        {
            try
            {
                Console.Write("Please enter movie name: ");
                string n = Console.ReadLine();
                Console.Write("Please enter movie genre: ");
                string g = Console.ReadLine();
                Console.Write("Please enter movie imdb: ");
                double i = Convert.ToDouble(Console.ReadLine());
                Console.Write("Please enter movie maker name: ");
                string mn = Console.ReadLine();
                Console.Write("Please enter movie time: ");
                double t = Convert.ToDouble(Console.ReadLine());
                Console.Write("Please enter movie ages: ");
                int a = Convert.ToInt32(Console.ReadLine());
                Console.Write("Is this an animation? true/false ");
                bool b = Convert.ToBoolean(Console.ReadLine());
                Console.Write("Please enter number of episodes: ");
                int ne = Convert.ToInt32(Console.ReadLine());
                if (ne == 1)
                    return new Movie(n, g, mn, i, a, t, b);
                return new Serial(n, g, mn, i, a, t, b, ne);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  
                return null;
            }
        }
        public override string ToString()
        {
            return string.Format("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}{5,-15}{6,-15}", "Movie Name", "Creater Name", "IMDB", "Genre", "Movie Time", "Ages", "Animation") + "\n" 
                + string.Format("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}{5,-15}{6,-15}", movieName, filmMakerName, Imdb, genre, MovieTime, Ages, isAnimation);
        }
    }
    class Serial : Movie
    {
        private int episodes;
        public int Episodes
        {
            get { return episodes; }
            set
            {
                if (value <= 1)
                    throw new ArgumentOutOfRangeException("Episodes", value, "Number of episodes must be at least 2!!!");
                episodes = value;
            }
        }
        //Constructor function
        public Serial(string movieName, string genre, string filmMakerName
            , double IMDB, int ages, double movieTime, bool isAnimation, int episodes) 
            : base(movieName,genre,filmMakerName,IMDB,ages,movieTime,isAnimation)
        {
            Episodes = episodes;
        }
        public override string ToString()
        {
            return base.ToString() + "\nNumber of episodes: " + Episodes;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            MyMovies macan = new MyMovies();
            string s;
            double i;

            try
            {
                int order = 0;
                while (order != 11)
                {
                    Console.WriteLine("1: Add new movie" + '\n' + "2: Remove a movie" + '\n' + "3: Search a movie by genre" + '\n' +
                        "4: Movies which imdb higher than 5" + '\n' + "5: Movies which imdb higher than a number" + '\n' + "6: Search a movie by name" + '\n' +
                        "7: Search for animations" + '\n' + "8: Search for serials" + '\n' + "9: Search a movie by creater name" + '\n' + "10: Show my movies lists" + '\n' + "11: Quit!");
                    order = Convert.ToInt32(Console.ReadLine());
                    switch (order)
                    {
                        case 1:
                            Console.Clear();
                            macan.addMovie(Movie.creatMovie());
                            Console.Clear();
                            break;
                        case 2:
                            Console.Clear();
                            Console.Write("Please enter a movie name which you want to remove: ");
                            s = Console.ReadLine();
                            macan.removeMovie(s);
                            Console.Clear();
                            break;
                        case 3:
                            Console.Clear();
                            Console.Write("Please enter a genre: ");
                            s = Console.ReadLine();
                            Console.Clear();
                            print_list(macan.searchForMovieByGenre(s));
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 4:
                            Console.Clear();
                            print_list(macan.searchForMovieByImdb(5));
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 5:
                            Console.Clear();
                            Console.Write("Please enter imdb: ");
                            i = Convert.ToDouble(Console.ReadLine());
                            print_list(macan.searchForMovieByImdb(i));
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 6:
                            Console.Clear();
                            Console.Write("Please enter movie name: ");
                            s = Console.ReadLine();
                            print_list(macan.searchForMovieByName(s));
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 7:
                            Console.Clear();
                            print_list(macan.searchForAnimations());
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 8:
                            Console.Clear();
                            print_list(macan.searchForSerials());
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 9:
                            Console.Clear();
                            Console.Write("Please enter name of the creater: ");
                            s = Console.ReadLine();
                            print_list(macan.searchForMovieByCreater(s));
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 10:
                            Console.Clear();
                            macan.show();
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            void print_list(List<Movie> list)
            {
                foreach (Movie movie in list)
                    Console.WriteLine(movie);
            }

        }
    }
}
