using Antlr.Runtime.Misc;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Web;
using JDRA5.Data;
using JDRA5.Models;

// ************************************************************************************
// WEB524 Project Template V2 == 2237-4b9c1d77-4157-41a2-b42c-0abe09e7703b
//
// By submitting this assignment you agree to the following statement.
// I declare that this assignment is my own work in accordance with the Seneca Academic
// Policy. No part of this assignment has been copied manually or electronically from
// any other source (including web sites) or distributed to other students.
// ************************************************************************************

namespace JDRA5.Controllers
{
    public class Manager
    {

        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // AutoMapper instance
        public IMapper mapper;

        // Request user property...

        // Backing field for the property
        private RequestUser _user;

        // Getter only, no setter
        public RequestUser User
        {
            get
            {
                // On first use, it will be null, so set its value
                if (_user == null)
                {
                    _user = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);
                }
                return _user;
            }
        }

        // Default constructor...
        public Manager()
        {
            // If necessary, add constructor code here

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Product, ProductBaseViewModel>();

                cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();

                cfg.CreateMap<Genre, GenreBaseViewModel>();
                cfg.CreateMap<Actor, ActorBaseViewModel>();
                cfg.CreateMap<Show, ShowBaseViewModel>();
                cfg.CreateMap<Episode, EpisodeBaseViewModel>();

                cfg.CreateMap<Actor, ActorWithShowInfoViewModel>();
                cfg.CreateMap<ActorAddViewModel, Actor>();

                cfg.CreateMap<Show, ShowWithInfoViewModel>();
                cfg.CreateMap<ShowAddViewModel, Show>();

                cfg.CreateMap<Episode, EpisodeWithShowNameViewModel>();
                cfg.CreateMap<EpisodeAddViewModel, Episode>();
            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }


        // Add your methods below and call them from controllers. Ensure that your methods accept
        // and deliver ONLY view model objects and collections. When working with collections, the
        // return type is almost always IEnumerable<T>.
        //
        // Remember to use the suggested naming convention, for example:
        // ProductGetAll(), ProductGetById(), ProductAdd(), ProductEdit(), and ProductDelete().

        public IEnumerable<GenreBaseViewModel> GenreGetAll()
        {
            var query = ds.Genres.OrderBy(x => x.Name);
            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreBaseViewModel>>(query);
        }
        public IEnumerable<ActorBaseViewModel> ActorsGetAll()
        {
            var query = ds.Actors.OrderBy(x => x.Name);
            return mapper.Map<IEnumerable<Actor>, IEnumerable<ActorBaseViewModel>>(query);
        }
        public IEnumerable<ShowBaseViewModel> ShowGetAll()
        {
            var query = ds.Shows.OrderBy(x => x.Name);
            return mapper.Map<IEnumerable<Show>, IEnumerable<ShowBaseViewModel>>(query);

        }
        public IEnumerable<EpisodeBaseViewModel> EpisodesGetAll()
        {
            var query = ds.Episodes.Include("Show")
                                    .OrderBy(x => x.Show.Name)
                                    .ThenBy(x => x.SeasonNumber)
                                    .ThenBy(x => x.EpisodeNumber);

            return mapper.Map<IEnumerable<Episode>, IEnumerable<EpisodeBaseViewModel>>(query);
        }
        public Actor ActorAdd(ActorAddViewModel newItem)
        {
            var execActor = mapper.Map<Actor>(newItem);

            if (execActor == null)
            {
                return null;
            }
                var getExecName = User.Name;
                execActor.Executive = getExecName;

                ds.Actors.Add(execActor);
                ds.SaveChanges();

                return (getExecName == null) ? null : execActor;
        }

        public Show ShowAdd(ShowAddViewModel newItem)
        {
            var coordShow = mapper.Map<Show>(newItem);

            if (coordShow == null)
            {
                return null;
            }
                var getCoordName = User.Name;
                coordShow.Coordinator = getCoordName;

                ds.Shows.Add(coordShow);
                ds.SaveChanges();

                return (getCoordName == null) ? null : coordShow;
        }

        public Episode EpisodeAdd(EpisodeAddViewModel newItem)
        {
            var show = ds.Shows.Find(newItem.ShowId);

            if (show == null)
            {
                return null;
            }
            var episode = mapper.Map<Episode>(newItem);

            episode.Clerk = User.Name;

            episode.Show = show;
            ds.Episodes.Add(episode);
            ds.SaveChanges();

            return episode;
        }


        public ActorWithShowInfoViewModel ActorGetById(int id)
        {
            var query = ds.Actors.Include("Shows")
                                 .SingleOrDefault(x => x.Id == id);

            return query != null ? mapper.Map<Actor, ActorWithShowInfoViewModel>(query) : null;
        }

        public ShowWithInfoViewModel ShowGetById(int id)
        {
            var query = ds.Shows.Include("Actors")
                                 .Include("Episodes")
                                 .SingleOrDefault(x => x.Id == id);

            return query != null ? mapper.Map<Show, ShowWithInfoViewModel>(query) : null;
        }

        public EpisodeWithShowNameViewModel EpisodeGetById(int id)
        {
            var query = ds.Episodes.Include("Show").SingleOrDefault(x => x.Id == id);

            return query != null ? mapper.Map<Episode, EpisodeWithShowNameViewModel>(query) : null;
        }

        // *** Add your methods ABOVE this line **

        #region Role Claims

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        #endregion

        #region Load Data Methods

        // Add some programmatically-generated objects to the data store
        // You can write one method or many methods but remember to
        // check for existing data first.  You will call this/these method(s)
        // from a controller action.
        public bool LoadData()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // *** Role claims ***
            if (ds.RoleClaims.Count() == 0)
            {
                // Add role claims here

                //LoadRoles();

                ds.SaveChanges();
                done = true;
            }

            // You may load additional entities here, or you may 
            // choose to create a new method altogether.

            return done;
        }

        public bool LoadRoles()
        {
            var user = HttpContext.Current.User.Identity.Name;

            bool done = false;

            if (ds.RoleClaims.Count() == 0)
            {
                ds.RoleClaims.Add(new Data.RoleClaim()
                {
                    Name = "Executive",
                });
                ds.RoleClaims.Add(new Data.RoleClaim()
                {
                    Name = "Coordinator",
                });
                ds.RoleClaims.Add(new Data.RoleClaim()
                {
                    Name = "Clerk",
                });
                ds.RoleClaims.Add(new Data.RoleClaim()
                {
                    Name = "Admin",
                });
                ds.SaveChanges();
                done = true;

            }

            return done;
        }

        public bool LoadGenres()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            if (ds.Genres.Count() == 0)
            {
                //Popular TV show and movie genres
                ds.Genres.Add(new Data.Genre() { Name = "Action" });
                ds.Genres.Add(new Data.Genre() { Name = "Adventure" });
                ds.Genres.Add(new Data.Genre() { Name = "Animation" });
                ds.Genres.Add(new Data.Genre() { Name = "Comedy" });
                ds.Genres.Add(new Data.Genre() { Name = "Crime" });
                ds.Genres.Add(new Data.Genre() { Name = "Drama" });
                ds.Genres.Add(new Data.Genre() { Name = "Crime" });
                ds.Genres.Add(new Data.Genre() { Name = "Fantasy" });
                ds.Genres.Add(new Data.Genre() { Name = "Horror" });
                ds.Genres.Add(new Data.Genre() { Name = "Mystery" });
                ds.Genres.Add(new Data.Genre() { Name = "Romance" });
                ds.Genres.Add(new Data.Genre() { Name = "Sci-Fi" });
                ds.Genres.Add(new Data.Genre() { Name = "Thriller" });

                ds.SaveChanges();
                done = true;
            }


            return done;
        }

        public bool LoadActors()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            var nActors = new Actor[]
            {
            new Actor
                {
                    Name = "Robert Downey Jr.",
                    AlternateName = "Iron Man",
                    BirthDate = new DateTime(1965, 4, 4),
                    Height = 1.74,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/9/94/Robert_Downey_Jr_2014_Comic_Con_%28cropped%29.jpg",
                    Executive = User.Name
                },
                new Actor
                {
                    Name = "Millie Bobby Brown",
                    AlternateName = "E11ven",
                    BirthDate = new DateTime(2004, 2, 19),
                    Height = 1.61,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/9/99/Millie_Bobby_Brown_-_MBB_-_Portrait_1_-_SFM5_-_July_10%2C_2022_at_Stranger_Fan_Meet_5_People_Convention_%28cropped%29.jpg",
                    Executive = User.Name
                },
                new Actor
                {
                    Name = "Úrsula Corberó",
                    AlternateName = "Tokyo",
                    BirthDate = new DateTime(1989, 8, 11),
                    Height = 1.63,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/3/32/%C3%9Arsula_Corber%C3%B3_Vogue_2021_2.jpg",
                    Executive = User.Name
                }
            };

            if (ds.Actors.Count() == 0)
            {
                ds.Actors.AddRange(nActors);
                ds.SaveChanges();
                done = true;
            }

            return done;
        }

        public bool LoadShows()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            var eleven = ds.Actors.SingleOrDefault(x => x.Name == "Millie Bobby Brown");

            if (ds.Shows.Count() == 0)
            {
                ds.Shows.Add(new Show
                {
                    Actors = new Actor[]
                    {
                        eleven
                    },
                    Name = "Stranger Things",
                    Genre = "Sci-Fi",
                    ReleaseDate = new DateTime(2016, 7, 15),
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/3/38/Stranger_Things_logo.png",
                    Coordinator = User.Name,
                });

                ds.Shows.Add(new Show
                {
                    Actors = new Actor[]
                    {
                        eleven
                    },
                    Name = "Enola Holmes",
                    Genre = "Adventure",
                    ReleaseDate = new DateTime(2020, 9, 23),
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/e/e6/Enola_Holmes_poster.jpeg",
                    Coordinator = User.Name
                });

                ds.SaveChanges();
                done = true;
            }
            return done;
        }

        public bool LoadEpisodes()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            var elevenShow = ds.Shows.SingleOrDefault(a => a.Name == "Stranger Things");
            var enolaShow = ds.Shows.SingleOrDefault(a => a.Name == "Enola Holmes");

            if (ds.Episodes.Count() == 0)
            {
                ds.Episodes.AddRange(new Episode[]
            {
                new Episode {
                    Show = elevenShow,
                    Name = "The Vanishing of Will Byers",
                    SeasonNumber = 1,
                    EpisodeNumber = 1,
                    AirDate = new DateTime(2016, 7, 15),
                    Genre = "Mystery",
                    ImageUrl = "https://assets.teenvogue.com/photos/63697db6a7d17699f61e55f4/16:9/w_1600,c_limit/StrangerThings_StrangerThings4_9_02_14_28_08.jpg",
                    Clerk = User.Name
                },
                new Episode {
                    Show = elevenShow,
                    Name = "The Weirdo on Maple Street",
                    SeasonNumber = 1,
                    EpisodeNumber = 2,
                    AirDate = new DateTime(2016, 7, 15),
                    Genre = "Mystery",
                    ImageUrl = "https://deadline.com/wp-content/uploads/2023/05/StrangerThings_StrangerThings4_9_01_53_00_15.jpg",
                    Clerk = User.Name
                },
                new Episode {
                    Show = elevenShow,
                    Name = "Holly, Jolly",
                    SeasonNumber = 1,
                    EpisodeNumber = 3,
                    AirDate = new DateTime(2016, 7, 15),
                    Genre = "Mystery",
                    ImageUrl = "https://media.newyorker.com/photos/62c197a67155e66883a968d9/master/w_1600,c_limit/Rothman-Stranger-Things-finale.jpg",
                    Clerk = User.Name
                }
            });

                ds.Episodes.AddRange(new Episode[]{
                new Episode {
                    Show = enolaShow,
                    Name = "Pilot",
                    SeasonNumber = 1,
                    EpisodeNumber = 1,
                    AirDate = new DateTime(2009, 9, 23),
                    Genre = "Adventure",
                    ImageUrl = "https://staticg.sportskeeda.com/editor/2022/12/807ad-16724793672593.png",
                    Clerk = User.Name
                },
                new Episode {
                    Show = enolaShow,
                    Name = "The Bicycle Thief",
                    SeasonNumber = 1,
                    EpisodeNumber = 2,
                    AirDate = new DateTime(2009, 9, 23),
                    Genre = "Adventure",
                    ImageUrl = "https://assets.popbuzz.com/2019/21/millie-bobby-brown-in-bbc-americas-intruders-1559125925-view-0.jpg",
                    Clerk = User.Name
                },
                new Episode {
                    Show = enolaShow,
                    Name = "Come Fly with Me",
                    SeasonNumber = 1,
                    EpisodeNumber = 3,
                    AirDate = new DateTime(2009, 10, 7),
                    Genre = "Adventure",
                    ImageUrl = "https://i.pinimg.com/736x/a8/2f/eb/a82feb0a5379470146974cec1ee7609b.jpg",
                    Clerk = User.Name
                }
            });
            }
            ds.SaveChanges();
            done = true;
            return done;
        }

        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.Shows)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

    #endregion

    #region RequestUser Class

    // This "RequestUser" class includes many convenient members that make it
    // easier work with the authenticated user and render user account info.
    // Study the properties and methods, and think about how you could use this class.

    // How to use...
    // In the Manager class, declare a new property named User:
    //    public RequestUser User { get; private set; }

    // Then in the constructor of the Manager class, initialize its value:
    //    User = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);

    public class RequestUser
    {
        // Constructor, pass in the security principal
        public RequestUser(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                // You can change the string value in your app to match your app domain logic
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            // Compose the nicely-formatted full names
            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }

        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }

        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }

        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }

    #endregion

}