using SistemasWeb01.Enums;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Implementations;
using SistemasWeb01.Repository.Interfaces;
using System.IO.Pipelines;

namespace SistemasWeb01.DataAccess
{
    public class DbInitializer
    {

        private readonly IUserRepository _userRepository;
        public DbInitializer( IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            ShoppingDbContext context = applicationBuilder.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<ShoppingDbContext>();
            
            //Categories
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(Categories.Select(c => c.Value));
            }
            //SubCategories
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(Categories.Select(s => s.Value));
            }
            //Brands
            if (!context.Brands.Any())
            {
                context.Brands.AddRange(Brands.Select(b => b.Value));
            }

            //Tallas
            if (!context.Tallas.Any())
            {
                context.Tallas.AddRange(Tallas.Select(t => t.Value));
            }

            //Products
            if (!context.Products.Any())
            {
                context.Products.AddRange(Products.Select(p => p.Value));
            }

            //Pictures
            if (!context.Pictures.Any())
            {
                context.Pictures.AddRange(Pictures.Select(p => p.Value));
            }
            //Countries
            if (!context.Countries.Any())
            {
                context.Countries.AddRange(Countries.Select(c => c.Value));
            }
            //States
            if (!context.States.Any())
            {
                context.States.AddRange(States.Select(s => s.Value));
            }
            //Cities
            if (!context.Cities.Any())
            {
                context.Cities.AddRange(Cities.Select(c => c.Value));
            }

            context.SaveChanges();
        }


        //User
        private static Dictionary<string, User>? users;

        public static Dictionary<string, User> Users
        {

            get
            {
                if (users == null)
                {
                    var genresList = new User[]
                    {
                        new User { UserName = "will" }
                    };

                    users = new Dictionary<string, User>();

                    foreach (User genre in genresList)
                    {
                        users.Add(genre.UserName, genre);
                    }
                }

                return users;
            }
        }






        private static Dictionary<string, Category>? categories;

        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (categories == null)
                {
                    var genresList = new Category[]
                    {
                        new Category { Name = "Ropa",  ThumbnailImage = "cat_rop.webp" },
                        new Category { Name = "Calzados",  ThumbnailImage = "cat_zapatos.webp" },
                    };

                    categories = new Dictionary<string, Category>();

                    foreach (Category genre in genresList)
                    {
                        categories.Add(genre.Name, genre);
                    }
                }

                return categories;
            }
        }

        private static Dictionary<string, SubCategory>? subcategories;

        public static Dictionary<string, SubCategory> SubCategories
        {
            get
            {
                if (subcategories == null)
                {
                    var genresList = new SubCategory[]
                    {
                        new SubCategory{ Name = "Vestidos", Category = Categories["Ropa"]},
                        new SubCategory{ Name = "Camisetas y camisas", Category = Categories["Ropa"]},
                        new SubCategory{ Name = "Pantalones y monos", Category = Categories["Ropa"]},
                        new SubCategory{ Name = "Blazers y Chaquetas", Category = Categories["Ropa"]},
                        new SubCategory{ Name = "Ropa deportiva", Category = Categories["Ropa"]},
                        new SubCategory{ Name = "Faldas", Category = Categories["Ropa"]},

                        new SubCategory{ Name = "Zapatos de tacon", Category = Categories["Calzados"]},
                        new SubCategory{ Name = "Sandalias", Category = Categories["Calzados"]},
                        new SubCategory{ Name = "Botas", Category = Categories["Calzados"]},
                        new SubCategory{ Name = "Zapatillas Tenis", Category = Categories["Calzados"]},
                    };

                    subcategories = new Dictionary<string, SubCategory>();

                    foreach (SubCategory genre in genresList)
                    {
                        subcategories.Add(genre.Name, genre);
                    }
                }

                return subcategories;
            }
        }

        private static Dictionary<string, Brand>? brands;

        public static Dictionary<string, Brand> Brands
        {
            get
            {
                if (brands == null)
                {
                    var genresList = new Brand[]
                    {
                        new Brand { Name = "Zara", ThumbnailImage = "brand_5.png" },
                        new Brand { Name = "Armani", ThumbnailImage = "brand_4.png" },
                        new Brand { Name = "Calvin Klein", ThumbnailImage = "brand_1.png" },
                        new Brand { Name = "Chanel", ThumbnailImage = "brand_2.png" },
                        new Brand { Name = "LaCoste" , ThumbnailImage = "brand_6.png"},
                        new Brand { Name = "Gucci" , ThumbnailImage = "brand_3.png"},
                        new Brand { Name = "Puma" , ThumbnailImage = "brand_7.png"}
                    };

                    brands = new Dictionary<string, Brand>();

                    foreach (Brand genre in genresList)
                    {
                        brands.Add(genre.Name, genre);
                    }
                }

                return brands;
            }
        }

        private static Dictionary<string, Talla>? tallas;

        public static Dictionary<string, Talla> Tallas
        {
            get
            {
                if (tallas == null)
                {
                    var genresList = new Talla[]
                    {
                        new Talla { Name = "Pequeña", ShortName = "P" , SizeNumber = "36"},
                        new Talla { Name = "Mediana" , ShortName = "M", SizeNumber = "38"},
                        new Talla { Name = "Grande", ShortName = "L", SizeNumber = "40" },
                        new Talla { Name = "Extra Grande", ShortName = "XL", SizeNumber = "42" }

                    };

                    tallas = new Dictionary<string, Talla>();

                    foreach (Talla genre in genresList)
                    {
                        tallas.Add(genre.Name, genre);
                    }
                }

                return tallas;
            }
        }

        private static Dictionary<string, Product>? products;

        public static Dictionary<string, Product> Products
        {
            get
            {
                if (products == null)
                {
                    var genresList = new Product[]
                    {
                        new Product { Name = "Vestido Ariel - Marino", Description = "Cuando el sol sale nos apetecen los estampados alegres y a todo color. Por eso hemos diseñado esta versión del vestido Ariel que es puro verano. Su patrón ya lo conoces, es nuestro top ventas máximo porque es perfecto para todas. Cruzado, con escote de pico y manguita corta. Ideal. Ya sabes, llévalo a la oficina, de compras o una cena de verano. La modelo mide 1,62m y lleva la talla 36.", Price = 36, InStock = 15,  IsNew = true,IsBestSeller = true,  SubCategory = SubCategories["Vestidos"] },
                        new Product { Name = "Vestido Bea - Crudo", Description = "El vestido más buscado de la temporada. Largo y de punto calado con apariencia croché. Perfectísimo para pasear junto al mar después de una jornada de playa, o para sentarte en una terraza con tus mejillas sonrosadas mientras brindas por la vida. Un vestido cómodo y con rollazo de edición limitada. Lleva vestidito de forro interior. La modelo de la talla 36/38 mide 1,62 m y la modelo de la talla 44/46 mide 1,58 m. 49%Algodón 49%Poliéster 2%Elastán", Price = 45, InStock = 10, IsDeleted = false, IsNew = true, SubCategory = SubCategories["Vestidos"]},
                        new Product { Name = "Vestido Ariel - Fucsia", Description = "Cuando el sol sale nos apetecen los estampados alegres y a todo color. Por eso hemos diseñado esta versión del vestido Ariel que es puro verano. Su patrón ya lo conoces, es nuestro top ventas máximo porque es perfecto para todas. Cruzado, con escote de pico y manguita corta. Ideal. Ya sabes, llévalo a la oficina, de compras o una cena de verano.", Price = 37, InStock = 12, IsNew = true, SubCategory = SubCategories["Vestidos"]},
                        new Product { Name = "Vestido Pretty Lunares - Marrón", Description = "El vestido de la temporada, no tenemos pruebas pero tampoco dudas. De un patrón top y un estampado icónico, solo estamos pensando en estrenarlo. Su tejido es de crepe del que no se arruga, lleva bolsillos y puedes elegir si llevar los botones delante o en la espalda. Amamos. La modelo de la talla 36 mide 1,57 m y la modelo de la talla 44 mide 1,58 m. 100%Poliéste", Price = 36, InStock = 9, IsDeleted = false, IsNew = true, SubCategory = SubCategories["Vestidos"]},
                        new Product { Name = "Vestido Alka - Negro", Description = "Hay vestidos y VESTIDOS. Ideal para una vichy addict como nosotras, este vestido es el look de la primavera. Nos encanta con complementos rojos, verdes, fucsias o amarillos. De corte recto y manguita estilo volante, lleva un botón en la espalda. ¡Qué ganas de estrenarlo! La modelo de la talla 36 mide 1,7 m y la modelo de la talla 44 mide 1,58 m.100%Viscosa", Price = 55, InStock = 10, IsNew = true, SubCategory = SubCategories["Vestidos"]},
                        
                        new Product { Name = "Falda Pantalón Allace - Mostaza", Description = "Dale estilo y color a tus looks con esta falda pantalón. Su tejido es una sarguita fluida elástica, ideal. Lleva cremallera lateral. La modelo de la talla 36 mide 1,57 m. 95%Poliéster 5%Elastán", Price = 55, InStock = 10, IsDeleted = false, IsNew = true, SubCategory = SubCategories["Faldas"]},
                        new Product { Name = "Falda Ovar Lunares - Marrón", Description = "Verano, verbenas y fiesta. Esta falda invita a pasarlo bien y a bailar. La prenda perfecta para cerrar un look perfecto. Es de crepe y está toda fruncida por lo que sienta de maravilla y queda pegadita sin marcar. Lleva cremallera invisible en un lateral. La modelo de la talla 36 mide 1,57 m y la modelo de la talla 48 mide 1,58 m.  100%Poliéster", Price = 55, InStock = 10, IsDeleted = false, IsNew = true, SubCategory = SubCategories["Faldas"]},
                        new Product { Name = "Falda Atria - Kaki", Description = "Ya sabes que esta temporada es (casi) obligatorio tener una falda denim midi. Reinventa la tendencia y pásate al color kaki. Nos flipa. Combínala con mil colores, es un tono neutro que queda de maravilla. Es de corte recto, con abertura en la parte de delante y bolsillos. Lista para dar rienda suelta a tus looks. La modelo de la talla 36 mide 1,57 m y la modelo de la talla 44 mide 1,58 m. 95%Algodón 5%Elastán", Price = 55, InStock = 10, IsDeleted = false, IsNew = true, SubCategory = SubCategories["Faldas"] },
                        new Product { Name = "Falda Pantalón Lino Galis - Marrón", Description = "No nos gusta, ¡nos flipa! Hemos logrado la falda pantalón perfecta. Su color lima nos ha calado hondo y se ha convertido en nuestra prenda favorita. Estilo, fresca y cómoda. Lleva los botones decorativos al tono y cremallera invisible. La modelo de la talla 36 mide 1,57 m y la modelo de la talla 48 mide 1,58 m. 97%Algodón 3%Elastán", Price = 55, InStock = 10, IsDeleted = false, IsNew = true,SubCategory = SubCategories["Faldas"]},
                        new Product { Name = "Falda Leresa Print - Multicolor", Description = "Sí rotundo a las faldas pareo. Nos apetece estrenar ya esta falda de crepe de un estampado que nos ha robado el corazón. Con lazada lateral, está lista para ser la prenda clave en tus looks tops de la temporada. La modelo de la talla 34 mide 1,54 m y la modelo de la talla 44 mide 1,58 m. 100%Poliéster", Price = 55, InStock = 10, IsDeleted = false, IsNew = true, SubCategory = SubCategories["Faldas"] },
                        
                        new Product { Name = "Camisa Idare - Blanco", Description = "Camisa blanca puño rizadito perfecta para llevarla suelta o para acompañar a tus jerséis favoritos. Gustosa y suave, a demás es apta para lactancia por sus botones reales. La modelo de la talla 34 mide 1,62 m y la modelo de la talla 44 mide 1,58 m. 97%Algodón 3%Elastán", Price = 55, InStock = 10, IsDeleted = false, IsNew = true, SubCategory = SubCategories["Camisetas y camisas"] },
                        new Product { Name = "Top Lencero Nosdi - Negro", Description = "El top lencero es fondo de armario obligatorio. Y con esta preciosidad de top ni nos lo pensamos. Su tejido en crepe tiene mucha caída y movimiento, es de tirantitos y con una blonda en el escote como detalle al tono del top. Lo puedes combinar con pantalón o falda, tacón o zapatillas que irás espectacular. La modelo mide 1,57 m y lleva la talla 36. 100%Poliéster", Price = 55, InStock = 10, IsDeleted = false, IsNew = true, SubCategory = SubCategories["Camisetas y camisas"] },
                        new Product { Name = "Top Lefilo - Marino", Description = "Para las amantes de los escotes halter y las prendas de punto este top es lo más. Muy versátil por su color y patrón, llévalo en todo tipo de ocasiones. La modelo de la talla 36 mide 1,57 m y la modelo de la talla 44 mide 1,58 m. 50% Acrilico 50% Algodón", Price = 55, InStock = 10, IsDeleted = false, IsNew = true, SubCategory = SubCategories["Camisetas y camisas"] },
                        new Product { Name = "Top Lencero Nosdi - Bugambilia", Description = "El top lencero es fondo de armario obligatorio. Y con esta preciosidad de top ni nos lo pensamos. Su tejido en crepe tiene mucha caída y movimiento, es de tirantitos y con una blonda en el escote como detalle al tono del top. Lo puedes combinar con pantalón o falda, tacón o zapatillas que irás espectacular. La modelo mide 1,57 m y lleva la talla 36. 100%Poliéster", Price = 55, InStock = 10, IsDeleted = false, IsNew = true, SubCategory = SubCategories["Camisetas y camisas"] },

                        new Product { Name = "Mono Aliseda - Negro", Description = "El mono perfecto. Lo decimos sin lugar a dudas. Con el escote cruzado del que somos absolutas fans, manguita caída y tejido súper fluido que no necesita plancha. Una prenda que no puede faltar en tu armario y que utilizarás para lookazos de evento y para tu día a día. La modelo de la talla 36 mide 1,57 m y la modelo de la talla 34 mide 1,61 m. 100%Poliéster", Price = 55, InStock = 10, IsDeleted = false, IsNew = true, SubCategory = SubCategories["Pantalones y monos"] },
                        new Product { Name = "Pantalón Gois - Marino", Description = "Sabemos que un pantalón marino es un imprescindible, y si este pantalón tiene ese toque especial, mejor que mejor. Fluído, con un tejido abambulado que es una pasada y con goma en la cintura. Una pasada de pantalón perfecto para tus días de oficina y tus vacaciones. La modelo de la talla 36 mide 1,58 m y la modelo de la talla 48 mide 1,58 m. 86%Rayon 14%Nylon", Price = 55, InStock = 10, IsDeleted = false, IsNew = true, SubCategory = SubCategories["Pantalones y monos"] },
                        new Product { Name = "Pantalón Gois - Beige", Description = "Sabemos que un pantalón marino es un imprescindible, y si este pantalón tiene ese toque especial, mejor que mejor. Fluído, con un tejido abambulado que es una pasada y con goma en la cintura. Una pasada de pantalón perfecto para tus días de oficina y tus vacaciones. La modelo de la talla 36 mide 1,58 m y la modelo de la talla 48 mide 1,58 m.  84%Viscosa 14%Nylon", Price = 55, InStock = 10, IsDeleted = false, IsNew = true, SubCategory = SubCategories["Pantalones y monos"] },
                        new Product { Name = "Short Ertin - Jeans", Description = "Ya están aquí las bermudas de la temporada. De un tejido súper elástico y cómodo, goma en la cintura y bolsillos. Nosotras ya nos hemos enamorado. El pantalón más versátil del momento tiene que ser tuyo. La modelo de la talla 36 mide 1,57 m y la modelo de la talla 48 mide 1,58 m. 76%Viscosa 21%Nylon 3%Elastán", Price = 55, InStock = 10, IsDeleted = false, IsNew = true, SubCategory = SubCategories["Pantalones y monos"] },
                        new Product { Name = "Falda Pantalón Romal Vichy - Verde", Description = "Un toque fresco y chic para tus looks de temporada. Esta falda pantalón de cuadro vichy es lap renda que buscabas. Tiene la parte delantera de corte pareo con botoncitos nacarados. Lleva cremallera invisible en un lateral. La modelo mide 1,62 m y lleva la talla 34. 100%Algodón", Price = 55, InStock = 10, IsDeleted = false, IsNew = true, SubCategory = SubCategories["Pantalones y monos"]},
                        new Product { Name = "Pantalón Gabriel - Kaki", Description = "La opción de colores tierra y naturales nos parece ideal también para primavera. Crea tus looks en estos tonos con este pantalón que es una pasada. Favorece 100%, su tiro es alto, lleva bolsillos y es de corte flare. Una auténtica pasada. La modelo de la talla 36 mide 1,57 m y la modelo de la talla 48 mide 1,58 m. 95%Algodón 5%Elastán", Price = 55, InStock = 10, IsDeleted = false, IsNew = true, SubCategory = SubCategories["Pantalones y monos"] },

                        new Product { Name = "Biker Taran - Verde", Description = "Inauguramos por todo lo alto la temporada de bikers. En un tono verde ideal y de suave antelina elástica, esta es la biker que más vas a amar. Combínala con vestido, con vaqueros o con faldas, tienes el rollazo asegurado. Lleva bolsillos. La modelo de la talla 34 mide 1,62 m y la modelo de la talla 44 mide 1,58 m.", Price = 55, InStock = 10, IsDeleted = false, IsNew = true, SubCategory = SubCategories["Blazers y Chaquetas"] },
                        new Product { Name = "Biker Taran - Negro", Description = "Inauguramos por todo lo alto la temporada de bikers. Imprescindible en negro y de suave antelina elastica, esta es la biker que más vas a amar. Combínala con vestido, con vaqueros o con faldas, tienes el rollazo asegurado. Lleva bolsillos. La modelo de la talla 34 mide 1,62 m y la modelo de la talla 44 mide 1,58 m. 95%Poliéster 5%Elastán", Price = 55, InStock = 10, IsDeleted = false, IsNew = true, SubCategory = SubCategories["Blazers y Chaquetas"] },
                        new Product { Name = "Cárdigan Bego - Beige", Description = "Con la forma de un trench y la fluidez de un cárdigan, hemos creado el mix perfecto. Finito, no pesa y no se arruga, tu compañero perfecto para cuando se esconde el sol. Lleva cinturón del mismo tejido y manga sardineta, juega con su largo. La modelo de la talla 36 mide 1,57 m y la modelo de la talla 44 mide 1,58 m. 100%Poliéster", Price = 55, InStock = 10, IsDeleted = false, IsNew = true, SubCategory = SubCategories["Blazers y Chaquetas"] }

                    };

                    products = new Dictionary<string, Product>();

                    foreach (Product genre in genresList)
                    {
                        products.Add(genre.Name, genre);
                    }
                }

                return products;
            }
        }

        private static Dictionary<string, Picture>? pictures;

        public static Dictionary<string, Picture> Pictures
        {
            get
            {
                if (pictures == null)
                {
                    var genresList = new Picture[]
                    {
                        new Picture {PictureName = "v1.webp", Product = Products["Vestido Ariel - Marino"] },
                        new Picture {PictureName = "v2.webp", Product = Products["Vestido Bea - Crudo"] },
                        new Picture {PictureName = "v3.webp", Product = Products["Vestido Ariel - Fucsia"] },
                        new Picture {PictureName = "v4.webp", Product = Products["Vestido Pretty Lunares - Marrón"] },
                        new Picture {PictureName = "v5.webp", Product = Products["Vestido Alka - Negro"] },

                        new Picture {PictureName = "f1.webp", Product = Products["Falda Pantalón Allace - Mostaza"] },
                        new Picture {PictureName = "f2.webp", Product = Products["Falda Ovar Lunares - Marrón"] },
                        new Picture {PictureName = "f3.webp", Product = Products["Falda Atria - Kaki"] },
                        new Picture {PictureName = "f4.webp", Product = Products["Falda Pantalón Lino Galis - Marrón"] },
                        new Picture {PictureName = "f5.webp", Product = Products["Falda Leresa Print - Multicolor"] },

                        new Picture {PictureName = "c1.webp", Product = Products["Camisa Idare - Blanco"] },
                        new Picture {PictureName = "c2.webp", Product = Products["Top Lencero Nosdi - Negro"] },
                        new Picture {PictureName = "c3.webp", Product = Products["Top Lefilo - Marino"] },
                        new Picture {PictureName = "c4.webp", Product = Products["Top Lencero Nosdi - Bugambilia"] },

                        new Picture {PictureName = "m1.webp", Product = Products["Mono Aliseda - Negro"] },
                        new Picture {PictureName = "m2.webp", Product = Products["Pantalón Gois - Marino"] },
                        new Picture {PictureName = "m3.webp", Product = Products["Pantalón Gois - Beige"] },
                        new Picture {PictureName = "m4.webp", Product = Products["Short Ertin - Jeans"] },
                        new Picture {PictureName = "m5.webp", Product = Products["Falda Pantalón Romal Vichy - Verde"] },
                        new Picture {PictureName = "m6.webp", Product = Products["Pantalón Gabriel - Kaki"] },

                        new Picture {PictureName = "ch1.webp", Product = Products["Biker Taran - Verde"] },
                        new Picture {PictureName = "ch2.webp", Product = Products["Biker Taran - Negro"] },
                        new Picture {PictureName = "ch3.webp", Product = Products["Cárdigan Bego - Beige"] },
                    };

                    pictures = new Dictionary<string, Picture>();

                    foreach (Picture genre in genresList)
                    {
                        pictures.Add(genre.PictureName, genre);
                    }
                }

                return pictures;
            }
        }


        private static Dictionary<string, Country>? countries;

        public static Dictionary<string, Country> Countries
        {
            get
            {
                if (countries == null)
                {
                    var genresList = new Country[]
                    {
                        new Country { Name = "Bolivia"},
                        new Country { Name = "Brasil"},
                        new Country { Name = "Argentina"},
                        new Country { Name = "Chile"},
                        new Country { Name = "Paraguay"},
                        new Country { Name = "Uruguay"},
                        new Country { Name = "Perú"},
                        new Country { Name = "Venezuela"},
                        new Country { Name = "Colombia"},
                        new Country { Name = "Ecuador"},
                        new Country { Name = "México"},
                        new Country { Name = "Extranjero"},

                    };

                    countries = new Dictionary<string, Country>();

                    foreach (Country genre in genresList)
                    {
                        countries.Add(genre.Name, genre);
                    }
                }

                return countries;
            }
        }

        private static Dictionary<string, State>? states;

        public static Dictionary<string, State> States
        {
            get
            {
                if (states == null)
                {
                    var genresList = new State[]
                    {
                        new State { Name = "La Paz", Country = Countries["Bolivia"]},
                        new State { Name = "Oruro", Country = Countries["Bolivia"]},
                        new State { Name = "Potosi", Country = Countries["Bolivia"]},
                        new State { Name = "Cochabamba", Country = Countries["Bolivia"]},
                        new State { Name = "Chuquisaca", Country = Countries["Bolivia"]},
                        new State { Name = "Tarija", Country = Countries["Bolivia"]},
                        new State { Name = "Pando", Country = Countries["Bolivia"]},
                        new State { Name = "Beni", Country = Countries["Bolivia"]},
                        new State { Name = "Santa Cruz", Country = Countries["Bolivia"]},

                    };

                    states = new Dictionary<string, State>();

                    foreach (State genre in genresList)
                    {
                        states.Add(genre.Name, genre);
                    }
                }

                return states;
            }
        }

        private static Dictionary<string, City>? cities;

        public static Dictionary<string, City> Cities
        {
            get
            {
                if (cities == null)
                {
                    var genresList = new City[]
                    {
                        new City { Name = "Achacachi", State = States["La Paz"]},
                        new City { Name = "Achocalla", State = States["La Paz"]},
                        new City { Name = "Ancoraimes", State = States["La Paz"]},
                        new City { Name = "Andrés de Machaca", State = States["La Paz"]},
                        new City { Name = "Apolo", State = States["La Paz"]},
                        new City { Name = "Aucapata", State = States["La Paz"]},
                        new City { Name = "Ayata", State = States["La Paz"]},
                        new City { Name = "Ayo Ayo", State = States["La Paz"]},
                        new City { Name = "Batallas", State = States["La Paz"]},
                        new City { Name = "Cairoma", State = States["La Paz"]},
                        new City { Name = "Cajuata", State = States["La Paz"]},
                        new City { Name = "Calacoto", State = States["La Paz"]},
                        new City { Name = "Calamarca", State = States["La Paz"]},
                        new City { Name = "Caquiaviri", State = States["La Paz"]},
                        new City { Name = "Caranavi", State = States["La Paz"]},
                        new City { Name = "Catacora", State = States["La Paz"]},
                        new City { Name = "Chacarilla", State = States["La Paz"]},
                        new City { Name = "Charaña", State = States["La Paz"]},
                        new City { Name = "Chulumani", State = States["La Paz"]},
                        new City { Name = "Chuma", State = States["La Paz"]},
                        new City { Name = "Collana", State = States["La Paz"]},
                        new City { Name = "Colquencha", State = States["La Paz"]},
                        new City { Name = "Colquiri", State = States["La Paz"]},
                        new City { Name = "Comanche", State = States["La Paz"]},
                        new City { Name = "Combaya", State = States["La Paz"]},
                        new City { Name = "Copacabana", State = States["La Paz"]},
                        new City { Name = "Coripata", State = States["La Paz"]},
                        new City { Name = "Coro Coro", State = States["La Paz"]},
                        new City { Name = "Coroico", State = States["La Paz"]},
                        new City { Name = "Curva", State = States["La Paz"]},
                        new City { Name = "Desaguadero", State = States["La Paz"]},
                        new City { Name = "El Alto", State = States["La Paz"]},
                        new City { Name = "Gral. Juan José Perez", State = States["La Paz"]},
                        new City { Name = "Guanay", State = States["La Paz"]},
                        new City { Name = "Guaqui", State = States["La Paz"]},
                        new City { Name = "Ichoca", State = States["La Paz"]},
                        new City { Name = "Inquisivi", State = States["La Paz"]},
                        new City { Name = "Irupana", State = States["La Paz"]},
                        new City { Name = "Ixiamas", State = States["La Paz"]},
                        new City { Name = "Jesús de Machaca", State = States["La Paz"]},
                        new City { Name = "La Asunta", State = States["La Paz"]},
                        new City { Name = "La Paz", State = States["La Paz"]},
                        new City { Name = "Laja", State = States["La Paz"]},
                        new City { Name = "Licoma Pampa", State = States["La Paz"]},
                        new City { Name = "Luribay", State = States["La Paz"]},
                        new City { Name = "Malla", State = States["La Paz"]},
                        new City { Name = "Mapiri", State = States["La Paz"]},
                        new City { Name = "Mecapaca (1)", State = States["La Paz"]},
                        new City { Name = "Mocomoco", State = States["La Paz"]},
                        new City { Name = "Nazacara de Pacajes", State = States["La Paz"]},
                        new City { Name = "Palca (1)", State = States["La Paz"]},
                        new City { Name = "Palos Blancos", State = States["La Paz"]},
                        new City { Name = "Papel Pampa", State = States["La Paz"]},
                        new City { Name = "Patacamaya", State = States["La Paz"]},
                        new City { Name = "Pelechuco", State = States["La Paz"]},
                        new City { Name = "Pucarani", State = States["La Paz"]},
                        new City { Name = "Puerto Acosta", State = States["La Paz"]},
                        new City { Name = "Puerto Carabuco", State = States["La Paz"]},
                        new City { Name = "Puerto Pérez", State = States["La Paz"]},
                        new City { Name = "Quiabaya", State = States["La Paz"]},
                        new City { Name = "Quime", State = States["La Paz"]},
                        new City { Name = "San Buena Ventura", State = States["La Paz"]},
                        new City { Name = "San Pedro de Curahuara", State = States["La Paz"]},
                        new City { Name = "San Pedro de Tiquina", State = States["La Paz"]},
                        new City { Name = "Santiago de Callapa", State = States["La Paz"]},
                        new City { Name = "Santiago de Machaca", State = States["La Paz"]},
                        new City { Name = "Sapahaqui", State = States["La Paz"]},
                        new City { Name = "Sica Sica", State = States["La Paz"]},
                        new City { Name = "Sorata", State = States["La Paz"]},
                        new City { Name = "Tacacoma", State = States["La Paz"]},
                        new City { Name = "Taraco", State = States["La Paz"]},
                        new City { Name = "Teoponte", State = States["La Paz"]},
                        new City { Name = "Tiahuanacu", State = States["La Paz"]},
                        new City { Name = "Tipuani", State = States["La Paz"]},
                        new City { Name = "Tito Yupanqui", State = States["La Paz"]},
                        new City { Name = "Umala", State = States["La Paz"]},
                        new City { Name = "Viacha", State = States["La Paz"]},
                        new City { Name = "Waldo Ballivián", State = States["La Paz"]},
                        new City { Name = "Yaco", State = States["La Paz"]},
                        new City { Name = "Yanacachi", State = States["La Paz"]},
                        new City { Name = "Huarina", State = States["La Paz"]},
                        new City { Name = "Santiago de Huata", State = States["La Paz"]},
                        new City { Name = "Escoma", State = States["La Paz"]},
                        new City { Name = "Humanata", State = States["La Paz"]},
                        new City { Name = "Alto Beni", State = States["La Paz"]},
                        new City { Name = "Aiquile", State = States["Cochabamba"]},
                        new City { Name = "Alalay", State = States["Cochabamba"]},
                        new City { Name = "Anzaldo", State = States["Cochabamba"]},
                        new City { Name = "Arani ", State = States["Cochabamba"]},
                        new City { Name = "Arbieto", State = States["Cochabamba"]},
                        new City { Name = "Arque", State = States["Cochabamba"]},
                        new City { Name = "Bolívar", State = States["Cochabamba"]},
                        new City { Name = "Capinota", State = States["Cochabamba"]},
                        new City { Name = "Chimoré", State = States["Cochabamba"]},
                        new City { Name = "Cliza", State = States["Cochabamba"]},
                        new City { Name = "Cochabamba", State = States["Cochabamba"]},
                        new City { Name = "Colcapirhua", State = States["Cochabamba"]},
                        new City { Name = "Colomi", State = States["Cochabamba"]},
                        new City { Name = "Cuchumuela", State = States["Cochabamba"]},
                        new City { Name = "Entre Ríos (Bulo Bulo)", State = States["Cochabamba"]},
                        new City { Name = "Independencia", State = States["Cochabamba"]},
                        new City { Name = "Mizque", State = States["Cochabamba"]},
                        new City { Name = "Morochata", State = States["Cochabamba"]},
                        new City { Name = "Omereque", State = States["Cochabamba"]},
                        new City { Name = "Pasorapa", State = States["Cochabamba"]},
                        new City { Name = "Pocona", State = States["Cochabamba"]},
                        new City { Name = "Pojo", State = States["Cochabamba"]},
                        new City { Name = "Puerto Villarroel", State = States["Cochabamba"]},
                        new City { Name = "Punata ", State = States["Cochabamba"]},
                        new City { Name = "Quillacollo", State = States["Cochabamba"]},
                        new City { Name = "Sacaba", State = States["Cochabamba"]},
                        new City { Name = "Sacabamba", State = States["Cochabamba"]},
                        new City { Name = "San Benito", State = States["Cochabamba"]},
                        new City { Name = "Santivañez", State = States["Cochabamba"]},
                        new City { Name = "Sicaya", State = States["Cochabamba"]},
                        new City { Name = "Sipe Sipe", State = States["Cochabamba"]},
                        new City { Name = "Tacachi", State = States["Cochabamba"]},
                        new City { Name = "Tacopaya", State = States["Cochabamba"]},
                        new City { Name = "Tapacarí", State = States["Cochabamba"]},
                        new City { Name = "Tarata", State = States["Cochabamba"]},
                        new City { Name = "Tiquipaya", State = States["Cochabamba"]},
                        new City { Name = "Tiraque", State = States["Cochabamba"]},
                        new City { Name = "Toco", State = States["Cochabamba"]},
                        new City { Name = "Tolata", State = States["Cochabamba"]},
                        new City { Name = "Totora", State = States["Cochabamba"]},
                        new City { Name = "Vacas", State = States["Cochabamba"]},
                        new City { Name = "Vila Vila", State = States["Cochabamba"]},
                        new City { Name = "Villa Rivero", State = States["Cochabamba"]},
                        new City { Name = "Villa Tunari ", State = States["Cochabamba"]},
                        new City { Name = "Vinto", State = States["Cochabamba"]},
                        new City { Name = "Cocapata", State = States["Cochabamba"]},
                        new City { Name = "Shinahota", State = States["Cochabamba"]},
                        new City { Name = "Andamarca", State = States["Oruro"]},
                        new City { Name = "Antequera", State = States["Oruro"]},
                        new City { Name = "Belén de Andamarca", State = States["Oruro"]},
                        new City { Name = "Caracollo", State = States["Oruro"]},
                        new City { Name = "Carangas", State = States["Oruro"]},
                        new City { Name = "Challapata", State = States["Oruro"]},
                        new City { Name = "Chipaya", State = States["Oruro"]},
                        new City { Name = "Choquecota", State = States["Oruro"]},
                        new City { Name = "Coipasa", State = States["Oruro"]},
                        new City { Name = "Corque", State = States["Oruro"]},
                        new City { Name = "Cruz de Machacamarca", State = States["Oruro"]},
                        new City { Name = "Curahuara de Carangas", State = States["Oruro"]},
                        new City { Name = "El Choro", State = States["Oruro"]},
                        new City { Name = "Escara", State = States["Oruro"]},
                        new City { Name = "Esmeralda", State = States["Oruro"]},
                        new City { Name = "Eucaliptus", State = States["Oruro"]},
                        new City { Name = "Huachacalla", State = States["Oruro"]},
                        new City { Name = "La Rivera", State = States["Oruro"]},
                        new City { Name = "Machacamarca", State = States["Oruro"]},
                        new City { Name = "Oruro", State = States["Oruro"]},
                        new City { Name = "Pampa Aullagas", State = States["Oruro"]},
                        new City { Name = "Pazña", State = States["Oruro"]},
                        new City { Name = "Sabaya", State = States["Oruro"]},
                        new City { Name = "Salinas de Garci Mendoza", State = States["Oruro"]},
                        new City { Name = "Santiago de Huari", State = States["Oruro"]},
                        new City { Name = "Santiago de Huayllamarca", State = States["Oruro"]},
                        new City { Name = "Santuario de Quillacas", State = States["Oruro"]},
                        new City { Name = "Soracachi", State = States["Oruro"]},
                        new City { Name = "Todos Santos", State = States["Oruro"]},
                        new City { Name = "Toledo", State = States["Oruro"]},
                        new City { Name = "Totora Oruro", State = States["Oruro"]},
                        new City { Name = "Turco", State = States["Oruro"]},
                        new City { Name = "Villa Huanuni", State = States["Oruro"]},
                        new City { Name = "Villa Poopó ", State = States["Oruro"]},
                        new City { Name = "Yunguyo del Litora", State = States["Oruro"]},
                        new City { Name = "Acasio", State = States["Potosi"]},
                        new City { Name = "Arampampa", State = States["Potosi"]},
                        new City { Name = "Atocha", State = States["Potosi"]},
                        new City { Name = "Belén de Urmiri", State = States["Potosi"]},
                        new City { Name = "Betanzos", State = States["Potosi"]},
                        new City { Name = "Caiza D", State = States["Potosi"]},
                        new City { Name = "Caripuyo", State = States["Potosi"]},
                        new City { Name = "Chaquí", State = States["Potosi"]},
                        new City { Name = "Chayanta", State = States["Potosi"]},
                        new City { Name = "Colcha K", State = States["Potosi"]},
                        new City { Name = "Colquechaca", State = States["Potosi"]},
                        new City { Name = "Cotagaita", State = States["Potosi"]},
                        new City { Name = "Llallagua", State = States["Potosi"]},
                        new City { Name = "Llica", State = States["Potosi"]},
                        new City { Name = "Mojinete", State = States["Potosi"]},
                        new City { Name = "Ocurí", State = States["Potosi"]},
                        new City { Name = "Pocoata", State = States["Potosi"]},
                        new City { Name = "Porco", State = States["Potosi"]},
                        new City { Name = "Potosí", State = States["Potosi"]},
                        new City { Name = "Puna", State = States["Potosi"]},
                        new City { Name = "Ravelo", State = States["Potosi"]},
                        new City { Name = "Sacaca", State = States["Potosi"]},
                        new City { Name = "San Agustín", State = States["Potosi"]},
                        new City { Name = "San Antonio de Esmoruco", State = States["Potosi"]},
                        new City { Name = "San Pablo de Lipez", State = States["Potosi"]},
                        new City { Name = "San Pedro de Buena Vista", State = States["Potosi"]},
                        new City { Name = "San Pedro de Quemes", State = States["Potosi"]},
                        new City { Name = "Tacobamba", State = States["Potosi"]},
                        new City { Name = "Tahua", State = States["Potosi"]},
                        new City { Name = "Tinguipaya (4)", State = States["Potosi"]},
                        new City { Name = "Tomave", State = States["Potosi"]},
                        new City { Name = "Toro Toro", State = States["Potosi"]},
                        new City { Name = "Tupiza", State = States["Potosi"]},
                        new City { Name = "Uncía", State = States["Potosi"]},
                        new City { Name = "Uyuni", State = States["Potosi"]},
                        new City { Name = "Villa de Yocalla", State = States["Potosi"]},
                        new City { Name = "Villazón", State = States["Potosi"]},
                        new City { Name = "Vitichi", State = States["Potosi"]},
                        new City { Name = "Chuquiuta", State = States["Potosi"]},
                        new City { Name = "Ckochas", State = States["Potosi"]},
                        new City { Name = "Bermejo", State = States["Tarija"]},
                        new City { Name = "Caraparí", State = States["Tarija"]},
                        new City { Name = "El Puente", State = States["Tarija"]},
                        new City { Name = "Entre Ríos", State = States["Tarija"]},
                        new City { Name = "Padcaya", State = States["Tarija"]},
                        new City { Name = "San Lorenzo", State = States["Tarija"]},
                        new City { Name = "Tarija", State = States["Tarija"]},
                        new City { Name = "Uriondo", State = States["Tarija"]},
                        new City { Name = "Villamontes", State = States["Tarija"]},
                        new City { Name = "Yacuiba", State = States["Tarija"]},
                        new City { Name = "Yunchará", State = States["Tarija"]},
                        new City { Name = "Ascención de Guarayos", State = States["Santa Cruz"]},
                        new City { Name = "Ayacucho - Porongo", State = States["Santa Cruz"]},
                        new City { Name = "Boyuibe", State = States["Santa Cruz"]},
                        new City { Name = "Buena Vista", State = States["Santa Cruz"]},
                        new City { Name = "Cabezas", State = States["Santa Cruz"]},
                        new City { Name = "Camiri", State = States["Santa Cruz"]},
                        new City { Name = "Carmen Rivero Torres", State = States["Santa Cruz"]},
                        new City { Name = "Charagua", State = States["Santa Cruz"]},
                        new City { Name = "Colpa Bélgica", State = States["Santa Cruz"]},
                        new City { Name = "Comarapa", State = States["Santa Cruz"]},
                        new City { Name = "Concepción", State = States["Santa Cruz"]},
                        new City { Name = "Cotoca", State = States["Santa Cruz"]},
                        new City { Name = "Cuatro Cañadas", State = States["Santa Cruz"]},
                        new City { Name = "Cuevo", State = States["Santa Cruz"]},
                        new City { Name = "El Puente sc", State = States["Santa Cruz"]},
                        new City { Name = "El Torno", State = States["Santa Cruz"]},
                        new City { Name = "Gral. Saavedra", State = States["Santa Cruz"]},
                        new City { Name = "Gutiérrez", State = States["Santa Cruz"]},
                        new City { Name = "La Guardia ", State = States["Santa Cruz"]},
                        new City { Name = "Lagunillas", State = States["Santa Cruz"]},
                        new City { Name = "Mairana", State = States["Santa Cruz"]},
                        new City { Name = "Mineros", State = States["Santa Cruz"]},
                        new City { Name = "Montero", State = States["Santa Cruz"]},
                        new City { Name = "Moro Moro", State = States["Santa Cruz"]},
                        new City { Name = "Okinawa", State = States["Santa Cruz"]},
                        new City { Name = "Pailón", State = States["Santa Cruz"]},
                        new City { Name = "Pampa Grande", State = States["Santa Cruz"]},
                        new City { Name = "Portachuelo", State = States["Santa Cruz"]},
                        new City { Name = "Postrer Valle", State = States["Santa Cruz"]},
                        new City { Name = "Pucara", State = States["Santa Cruz"]},
                        new City { Name = "Puerto Fernández Alonso", State = States["Santa Cruz"]},
                        new City { Name = "Puerto Quijarro", State = States["Santa Cruz"]},
                        new City { Name = "Puerto Suárez", State = States["Santa Cruz"]},
                        new City { Name = "Quirusillas", State = States["Santa Cruz"]},
                        new City { Name = "Roboré", State = States["Santa Cruz"]},
                        new City { Name = "Saipina", State = States["Santa Cruz"]},
                        new City { Name = "Samaipata ", State = States["Santa Cruz"]},
                        new City { Name = "San Antonio del Lomerío", State = States["Santa Cruz"]},
                        new City { Name = "San Carlos", State = States["Santa Cruz"]},
                        new City { Name = "San Ignacio", State = States["Santa Cruz"]},
                        new City { Name = "San Javier", State = States["Santa Cruz"]},
                        new City { Name = "San José", State = States["Santa Cruz"]},
                        new City { Name = "San Juan", State = States["Santa Cruz"]},
                        new City { Name = "San Julián", State = States["Santa Cruz"]},
                        new City { Name = "San Matías", State = States["Santa Cruz"]},
                        new City { Name = "San Miguel", State = States["Santa Cruz"]},
                        new City { Name = "San Pedro", State = States["Santa Cruz"]},
                        new City { Name = "San Rafae", State = States["Santa Cruz"]},
                        new City { Name = "San Ramón", State = States["Santa Cruz"]},
                        new City { Name = "Santa Cruz de la Sierra", State = States["Santa Cruz"]},
                        new City { Name = "Santa Rosa del Sara", State = States["Santa Cruz"]},
                        new City { Name = "Trigal ", State = States["Santa Cruz"]},
                        new City { Name = "Urubichá", State = States["Santa Cruz"]},
                        new City { Name = "Valle Grande", State = States["Santa Cruz"]},
                        new City { Name = "Warnes", State = States["Santa Cruz"]},
                        new City { Name = "Yapacaní", State = States["Santa Cruz"]},
                        new City { Name = "Baures", State = States["Beni"]},
                        new City { Name = "Exaltación", State = States["Beni"]},
                        new City { Name = "Guayaramerín", State = States["Beni"]},
                        new City { Name = "Huacaraje", State = States["Beni"]},
                        new City { Name = "Loreto", State = States["Beni"]},
                        new City { Name = "Magdalena", State = States["Beni"]},
                        new City { Name = "Puerto Siles", State = States["Beni"]},
                        new City { Name = "Reyes", State = States["Beni"]},
                        new City { Name = "Riberalta", State = States["Beni"]},
                        new City { Name = "Rurrenabaque ", State = States["Beni"]},
                        new City { Name = "San Andrés", State = States["Beni"]},
                        new City { Name = "San Borja", State = States["Beni"]},
                        new City { Name = "San Ignacio Beni", State = States["Beni"]},
                        new City { Name = "San Javier Beni", State = States["Beni"]},
                        new City { Name = "San Joaquín", State = States["Beni"]},
                        new City { Name = "San Ramón Beni", State = States["Beni"]},
                        new City { Name = "Santa Ana", State = States["Beni"]},
                        new City { Name = "Santa Rosa", State = States["Beni"]},
                        new City { Name = "Trinidad", State = States["Beni"]},
                        new City { Name = "Bella Flor", State = States["Pando"]},
                        new City { Name = "Blanca Flor", State = States["Pando"]},
                        new City { Name = "Bolpebra", State = States["Pando"]},
                        new City { Name = "Cobija", State = States["Pando"]},
                        new City { Name = "El Sena", State = States["Pando"]},
                        new City { Name = "Filadelfia", State = States["Pando"]},
                        new City { Name = "Humaita", State = States["Pando"]},
                        new City { Name = "Nueva Esperanza", State = States["Pando"]},
                        new City { Name = "Porvenir", State = States["Pando"]},
                        new City { Name = "Puerto Gonzalo Moreno", State = States["Pando"]},
                        new City { Name = "Puerto Rico", State = States["Pando"]},
                        new City { Name = "San Pedro Pando", State = States["Pando"]},
                        new City { Name = "Santa Rosa del Abuná", State = States["Pando"]},
                        new City { Name = "Santos Mercado (Reserva)", State = States["Pando"]},
                        new City { Name = "Villa Nueva (Loma Alta) ", State = States["Pando"]},
                        new City { Name = "Camargo", State = States["Chuquisaca"]},
                        new City { Name = "Camataqui", State = States["Chuquisaca"]},
                        new City { Name = "Culpina ", State = States["Chuquisaca"]},
                        new City { Name = "El Villar", State = States["Chuquisaca"]},
                        new City { Name = "Huacaya", State = States["Chuquisaca"]},
                        new City { Name = "Icla", State = States["Chuquisaca"]},
                        new City { Name = "Incahuasi", State = States["Chuquisaca"]},
                        new City { Name = "Las Carreras", State = States["Chuquisaca"]},
                        new City { Name = "Macharetí", State = States["Chuquisaca"]},
                        new City { Name = "Monteagudo", State = States["Chuquisaca"]},
                        new City { Name = "Padilla", State = States["Chuquisaca"]},
                        new City { Name = "Poroma", State = States["Chuquisaca"]},
                        new City { Name = "Presto", State = States["Chuquisaca"]},
                        new City { Name = "San Lucas", State = States["Chuquisaca"]},
                        new City { Name = "San Pablo de Huacareta", State = States["Chuquisaca"]},
                        new City { Name = "Sopachuy", State = States["Chuquisaca"]},
                        new City { Name = "Sucre", State = States["Chuquisaca"]},
                        new City { Name = "Tarabuco", State = States["Chuquisaca"]},
                        new City { Name = "Tarvita", State = States["Chuquisaca"]},
                        new City { Name = "Tomina", State = States["Chuquisaca"]},
                        new City { Name = "Villa Alcalá", State = States["Chuquisaca"]},
                        new City { Name = "Villa Azurduy", State = States["Chuquisaca"]},
                        new City { Name = "Villa Mojocoya", State = States["Chuquisaca"]},
                        new City { Name = "Villa Serrano", State = States["Chuquisaca"]},
                        new City { Name = "Villa Vaca Guzmán", State = States["Chuquisaca"]},
                        new City { Name = "Villa Zudáñez", State = States["Chuquisaca"]},
                        new City { Name = "Yamparáez", State = States["Chuquisaca"]},







                    };

                    cities = new Dictionary<string, City>();

                    foreach (City genre in genresList)
                    {
                        cities.Add(genre.Name, genre);
                    }
                }

                return cities;
            }
        }

    }
}
