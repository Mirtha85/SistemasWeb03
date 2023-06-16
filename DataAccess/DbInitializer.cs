using SistemasWeb01.Models;
using System.IO.Pipelines;

namespace SistemasWeb01.DataAccess
{
    public class DbInitializer
    {
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

            context.SaveChanges();
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
                        new Category { Name = "Ropa" },
                        new Category { Name = "Calzados" },
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
                        new Brand { Name = "Zara" },
                        new Brand { Name = "Armani" },
                        new Brand { Name = "Calvin Klein" },
                        new Brand { Name = "Chanel" },
                        new Brand { Name = "LaCoste" },
                        new Brand { Name = "Gucci" },
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

                        new Product { Name = "Biker Taran - Verde", Description = "Inauguramos por todo lo alto la temporada de bikers. En un tono verde ideal y de suave antelina elástica, esta es la biker que más vas a amar. Combínala con vestido, con vaqueros o con faldas, tienes el rollazo asegurado. Lleva bolsillos. La modelo de la talla 34 mide 1,62 m y la modelo de la talla 44 mide 1,58 m.", Price = 55, InStock = 10, IsDeleted = false, IsNew = true, SubCategory = SubCategories["Blazers y chaquetas"] },
                        new Product { Name = "Biker Taran - Negro", Description = "Inauguramos por todo lo alto la temporada de bikers. Imprescindible en negro y de suave antelina elastica, esta es la biker que más vas a amar. Combínala con vestido, con vaqueros o con faldas, tienes el rollazo asegurado. Lleva bolsillos. La modelo de la talla 34 mide 1,62 m y la modelo de la talla 44 mide 1,58 m. 95%Poliéster 5%Elastán", Price = 55, InStock = 10, IsDeleted = false, IsNew = true, SubCategory = SubCategories["Blazers y chaquetas"] },
                        new Product { Name = "Cárdigan Bego - Beige", Description = "Con la forma de un trench y la fluidez de un cárdigan, hemos creado el mix perfecto. Finito, no pesa y no se arruga, tu compañero perfecto para cuando se esconde el sol. Lleva cinturón del mismo tejido y manga sardineta, juega con su largo. La modelo de la talla 36 mide 1,57 m y la modelo de la talla 44 mide 1,58 m. 100%Poliéster", Price = 55, InStock = 10, IsDeleted = false, IsNew = true, SubCategory = SubCategories["Blazers y chaquetas"] }

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

    }
}
