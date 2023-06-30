using Microsoft.EntityFrameworkCore;
using SistemasWeb01.Enums;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Interfaces;

namespace SistemasWeb01.DataAccess
{
    public class DatabaseInitializer
    {
        private readonly IUserRepository _userRepository;
        private readonly ShoppingDbContext _shoppingDbContext;
        public DatabaseInitializer(ShoppingDbContext shoppingDbContext, IUserRepository userRepository)
        {
            _shoppingDbContext = shoppingDbContext;
            _userRepository = userRepository;
        }

        public async Task SeedAsync()
        {
            await _shoppingDbContext.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckCategoriesAsync();
            await CheckBrandsAsync();
            await CheckTallasAsync();
            await CheckProductsAsync();

            await CheckRolesAsync();

            await CheckUserAsync("7210049", "Wilfredo", "Yelma", "yelma@gmail.com", "64435282", "Calle 13, B/ Plan 4 mil", "fotow.webp", UserType.Admin, "Admin");
            await CheckUserAsync("6525331", "Beth", "Shop", "beth@gmail.com", "78522456", "Calle Paris, B/ San Pedro", "beth.webp", UserType.User, "User");

            await CheckShoppingCartItem();

        }

        private async Task CheckRolesAsync()
        {
            await _userRepository.CheckRoleAsync(UserType.Admin.ToString());
            await _userRepository.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, string imageName, UserType userType, string type)
        {
            User user = await _userRepository.GetUserAsync(email);
            
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = _shoppingDbContext.Cities.FirstOrDefault(),
                    ImageName = imageName,
                    UserType = userType,
                    TypeUser = type
                };

                await _userRepository.AddUserAsync(user, "123456");
                await _userRepository.AddUserToRole(user, type);
            }

            return user;
        }

        // crear shopping cart item
        private async Task CheckShoppingCartItem()
        {
            if (!_shoppingDbContext.ShoppingCartItems.Any())
            {
                _shoppingDbContext.ShoppingCartItems.Add( new ShoppingCartItem { ProductId= 1, ProductSizeId = 1, Amount = 1, ShoppingCartId = "1a2b3c4d" });
                _shoppingDbContext.ShoppingCartItems.Add( new ShoppingCartItem { ProductId = 2, ProductSizeId = 2, Amount = 2, ShoppingCartId = "4a3b2c1d" });
            }
            await _shoppingDbContext.SaveChangesAsync();
        }




        private async Task CheckBrandsAsync()
        {
            if (!_shoppingDbContext.Brands.Any())
            {
                _shoppingDbContext.Brands.Add(new Brand { Name = "Zara", ThumbnailImage = "brand_5.png" });
                _shoppingDbContext.Brands.Add(new Brand { Name = "Armani", ThumbnailImage = "brand_4.png" });
                _shoppingDbContext.Brands.Add(new Brand { Name = "Calvin Klein", ThumbnailImage = "brand_1.png" });
                _shoppingDbContext.Brands.Add(new Brand { Name = "Chanel", ThumbnailImage = "brand_2.png" });
                _shoppingDbContext.Brands.Add(new Brand { Name = "LaCoste", ThumbnailImage = "brand_6.png" });
                _shoppingDbContext.Brands.Add(new Brand { Name = "Gucci", ThumbnailImage = "brand_3.png" });
                _shoppingDbContext.Brands.Add(new Brand { Name = "Puma", ThumbnailImage = "brand_7.png" });
            }
            await _shoppingDbContext.SaveChangesAsync();
        }



        private async Task CheckCategoriesAsync()
        {
            if (!_shoppingDbContext.Categories.Any())
            {
                _shoppingDbContext.Categories.Add(new Category
                {
                    Name = "Ropa",
                    ThumbnailImage = "cat_rop.webp",
                    SubCategories = new List<SubCategory>()
                    {
                        new SubCategory { Name = "Vestidos"}, //→ 1
                        new SubCategory { Name = "Blusas"}, //→ 2
                        new SubCategory { Name = "Pantalones y monos"}, //→ 3
                        new SubCategory { Name = "Blazers y Chaquetas"}, //→ 4
                        new SubCategory { Name = "Faldas"} //→ 5
                    }
                });


                _shoppingDbContext.Categories.Add(new Category
                {
                    Name = "Zapatos",
                    ThumbnailImage = "cat_zapatos.webp",
                    SubCategories = new List<SubCategory>()
                    {
                        new SubCategory { Name = "Zapatos de tacon"}, //→ 6
                        new SubCategory { Name = "Sandalias"}, //→ 7
                        new SubCategory { Name = "Botas"}, //→ 8
                        new SubCategory { Name = "Zapatillas Tenis"} //→ 9
                    }
                });
            }
            await _shoppingDbContext.SaveChangesAsync();
        }

        private async Task CheckTallasAsync()
        {
            if (!_shoppingDbContext.Tallas.Any())
            {
                _shoppingDbContext.Tallas.Add(new Talla { Name = "Pequeña", ShortName = "P", SizeNumber = "36" }); //1
                _shoppingDbContext.Tallas.Add(new Talla { Name = "Mediana", ShortName = "M", SizeNumber = "38" });  //2
                _shoppingDbContext.Tallas.Add(new Talla { Name = "Grande", ShortName = "L", SizeNumber = "40" }); //3
                _shoppingDbContext.Tallas.Add(new Talla { Name = "Extra Grande", ShortName = "XL", SizeNumber = "42" }); //4
                _shoppingDbContext.Tallas.Add(new Talla { Name = "Extra Extra Grande", ShortName = "XXL", SizeNumber = "44" }); //5
                _shoppingDbContext.Tallas.Add(new Talla { Name = "Ninguna", ShortName = "-", SizeNumber = "-" }); //5

            }
            await _shoppingDbContext.SaveChangesAsync();
        }
        private async Task CheckProductsAsync()
        {
            if (!_shoppingDbContext.Products.Any())
            {
                _shoppingDbContext.Products.Add(new Product
                {
                    Name = "Vestido Bea - Crudo",
                    Description = "El vestido más buscado de la temporada. Largo y de punto calado con apariencia croché. Perfectísimo para pasear junto al mar después de una jornada de playa, o para sentarte en una terraza con tus mejillas sonrosadas mientras brindas por la vida. Un vestido cómodo y con rollazo de edición limitada. Lleva vestidito de forro interior. La modelo de la talla 36/38 mide 1,62 m y la modelo de la talla 44/46 mide 1,58 m.  49%Algodón 49%Poliéster 2%Elastán",
                    Price = 208,
                    InStock = 7,
                    IsNew = true,
                    IsBestSeller = true,
                    PercentageDiscount = 0,
                    SubCategoryId = 1,
                    Pictures = new List<Picture>{
                        new Picture {PictureName = "p1a.webp"},
                        new Picture {PictureName = "p1b.webp"},
                        new Picture {PictureName = "p1c.webp"},
                        new Picture {PictureName = "p1d.webp"}
                    },
                    ProductSizes = new List<ProductSize>{
                        new ProductSize { ProductId = 1, TallaId = 2, Quantity = 5},
                        new ProductSize { ProductId = 1, TallaId = 4, Quantity = 5}
                    }

                });

                _shoppingDbContext.Products.Add(new Product
                {
                    Name = "Vestido Nasu Ef. Lino - Rosa",
                    Description = "Ha llegado EL VESTIDO más bonito en el color rosa que más nos gusta. Con escote en forma de corazón y tirantes con los que podrás ajustar a tu gusto dando una sensación de halter; en la espalda lleva panal de abeja y su tejido es efecto lino. Es el vestido perfecto para ir a una tarde con amigas. ¡NOS ENCANTA! La modelo de la talla 36 mide 1,57 m y la modelo de la talla 44 mide 1,58 m. escote corte corazon con tirastes para ajustar efecto lino rosa que nos gusta. 97%Algodón 3%Eslastano ",
                    Price = 215,
                    InStock = 10,
                    IsNew = true,
                    IsBestSeller = true,
                    PercentageDiscount = 0,
                    SubCategoryId = 1,
                    Pictures = new List<Picture>{
                        new Picture {PictureName = "p2a.webp"},
                        new Picture {PictureName = "p2b.webp"},
                        new Picture {PictureName = "p2c.webp"}
                    },
                    ProductSizes = new List<ProductSize>{
                        new ProductSize { ProductId = 2, TallaId = 2, Quantity = 10},
                        new ProductSize { ProductId = 2, TallaId = 3, Quantity = 12}
                    }

                });
                _shoppingDbContext.Products.Add(new Product
                {
                    Name = "Vestido Martinica - Negro",
                    Description = " Lo buscabas y aquí lo tienes. Un little black dress diseñado para disfrutarlo. COn manga de capa, escote en pico y cinturilla engomada. Probablemente el vestido que más utilices esta temporada. Con bambas, sandalias y con un cuñita para una noche de verano. Apto para lactancia. La modelo de la talla 34 mide 1,62m y la modelo de la talla 44 mide 1,58 m. 100%Poliéster",
                    Price = 230,
                    InStock = 5,
                    IsNew = true,
                    IsBestSeller = true,
                    PercentageDiscount = 0,
                    SubCategoryId = 1,
                    Pictures = new List<Picture>{
                        new Picture {PictureName = "p3a.webp"},
                        new Picture {PictureName = "p3b.webp"},
                        new Picture {PictureName = "p3c.webp"},
                        new Picture {PictureName = "p3d.webp"}
                    },
                    ProductSizes = new List<ProductSize>{
                        new ProductSize { ProductId = 3, TallaId = 1, Quantity = 5},
                        new ProductSize { ProductId = 3, TallaId = 2, Quantity = 10}
                    }

                });
                _shoppingDbContext.Products.Add(new Product
                {
                    Name = "Vestido Lopez - Marino",
                    Description = " ¡Ha llegado el momoento de los vestidos fresquitos! Y en eso el vestido López es experto. De manguita corta y cuello redondo, su tejido es súper elástico y fluido. Cómo, comodísimo y súper ponible. ¿El vestido de la temporada? Llévalo con bambas súper informal o compleméntalo para elevarlo. La modelo de la talla 34 mide 1,62 m y la modelo de la talla 44 mide 1,58 m. 92%Poliéster 8%Elastán",
                    Price = 210,
                    InStock = 12,
                    IsNew = true,
                    IsBestSeller = true,
                    PercentageDiscount = 0,
                    SubCategoryId = 1,
                    Pictures = new List<Picture>{
                        new Picture {PictureName = "p4a.webp"},
                        new Picture {PictureName = "p4b.webp"},
                        new Picture {PictureName = "p4c.webp"},
                        new Picture {PictureName = "p4d.webp"}
                    },
                    ProductSizes = new List<ProductSize>{
                        new ProductSize { ProductId = 4, TallaId = 1, Quantity = 5},
                        new ProductSize { ProductId = 4, TallaId = 2, Quantity = 5},
                        new ProductSize { ProductId = 4, TallaId = 3, Quantity = 5}
                    }

                });
                _shoppingDbContext.Products.Add(new Product
                {
                    Name = "Vestido Sochi - Kaki",
                    Description = "Este vestido nos ha dejado en shock. Y es que demuestra que menos es más. De partón recto, su tejido lo dice todo. Es de un plisado con brillo que no puede ser más bonito. Lleva un cinturón del mismo tejido para que puedas ceñirlo a tu cintura. Un auténtico espectáculo. La modelo de la talla 34 mide 1,62 m y la modelo de la talla 44 mide 1,58 m. 100%Poliéster ",
                    Price = 170,
                    InStock = 7,
                    IsNew = true,
                    IsBestSeller = true,
                    PercentageDiscount = 0,
                    SubCategoryId = 1,
                    Pictures = new List<Picture>{
                        new Picture {PictureName = "p5a.webp"},
                        new Picture {PictureName = "p5b.webp"},
                        new Picture {PictureName = "p5c.webp"}
                    },
                    ProductSizes = new List<ProductSize>{
                        new ProductSize { ProductId = 5, TallaId = 2, Quantity = 5},
                        new ProductSize { ProductId = 5, TallaId = 4, Quantity = 5}
                    }

                });
                _shoppingDbContext.Products.Add(new Product
                {
                    Name = "Vestido Keit - Marino",
                    Description = "¡Sorpresa! Seguro que no tenías previsto enamorarte hoy, pero ha llegado a tu vida el vestido Keit. De crepe, con un escote ideal y volante en el bajo. Un vestido que es energía pura, que favorece y que te puedes poner para tus planazos con bamba, sandalia plana o tacón. ¿Lo amas? La modelo de la talla 36 mide 1,57 m y la modelo de la talla 44 mide 1,58 m.",
                    Price = 157,
                    InStock = 7,
                    IsNew = true,
                    SubCategoryId = 1,
                    IsBestSeller = true,
                    PercentageDiscount = 0,
                    Pictures = new List<Picture>{
                        new Picture {PictureName = "p6a.webp"},
                        new Picture {PictureName = "p6b.webp"},
                        new Picture {PictureName = "p6c.webp"}
                    },
                    ProductSizes = new List<ProductSize>{
                        new ProductSize { ProductId = 6, TallaId = 2, Quantity = 5},
                        new ProductSize { ProductId = 6, TallaId = 4, Quantity = 5}
                    }

                });
                _shoppingDbContext.Products.Add(new Product
                {
                    Name = "Blusa Rizi - Verde",
                    Description = "La blusa más mona ahora en el color que más amamos, VERDE. Segurmante la prenda que más combines esta temporada. De plumeti y volantes románticos. ¡Es perfecta para combinar de mil formas! La modelo la del a talla 36 mide 1,57 m. 100% Poliéster ",
                    Price = 90,
                    InStock = 7,
                    IsNew = true,
                    IsBestSeller = true,
                    PercentageDiscount = 0,
                    SubCategoryId = 2,
                    Pictures = new List<Picture>{
                        new Picture {PictureName = "p7a.webp"},
                        new Picture {PictureName = "p7b.webp"}
                    },
                    ProductSizes = new List<ProductSize>{
                        new ProductSize { ProductId = 7, TallaId = 2, Quantity = 5},
                        new ProductSize { ProductId = 7, TallaId = 3, Quantity = 5}
                    }

                });
                _shoppingDbContext.Products.Add(new Product
                {
                    Name = "Camiseta Millan - Blanco",
                    Description = "Un tank top es un imprescindible durante todo el año. La prenda básica por excelencia. De un patrón semi entallado que queda perfecto y un canalé que no marca. La camiseta ideal para combinar absolutamente con todo. La modelo mide 1,57 m y lleva la talla 36. 95%Algodón 5%Elastán ",
                    Price = 125,
                    InStock = 7,
                    IsNew = true,
                    IsBestSeller = true,
                    PercentageDiscount = 0,
                    SubCategoryId = 2,
                    Pictures = new List<Picture>{
                        new Picture {PictureName = "p8a.webp"},
                        new Picture {PictureName = "p8b.webp"},
                        new Picture {PictureName = "p8c.webp"}
                    },
                    ProductSizes = new List<ProductSize>{
                        new ProductSize { ProductId = 8, TallaId = 2, Quantity = 5},
                        new ProductSize { ProductId = 8, TallaId = 3, Quantity = 5}
                    }

                });
                _shoppingDbContext.Products.Add(new Product
                {
                    Name = "Camiseta Boule - Morado",
                    Description = " Las mangas mariposa están de moda y nos sumamos porque nos encantan. Dale protagonismo a tus hombros con esta camiseta de canalé y cuello redondo que combina con todo y sienta increíble.  La modelo de la talla 34 mide 1,58 m y la modelo de la talla 44 mide 1,58 m. 93%Poliéster 7%Elastán",
                    Price = 230,
                    InStock = 7,
                    IsNew = true,
                    IsBestSeller = true,
                    PercentageDiscount = 0,
                    SubCategoryId = 2,
                    Pictures = new List<Picture>{
                        new Picture {PictureName = "p9a.webp"},
                        new Picture {PictureName = "p9b.webp"},
                        new Picture {PictureName = "p9c.webp"},
                        new Picture {PictureName = "p9d.webp"}
                    },
                    ProductSizes = new List<ProductSize>{
                        new ProductSize { ProductId = 9, TallaId = 2, Quantity = 5},
                        new ProductSize { ProductId = 9, TallaId = 3, Quantity = 5}
                    }

                });
                _shoppingDbContext.Products.Add(new Product
                {
                    Name = "Mono Mulens - Negro",
                    Description = "Es que no hay palabras para describir esta perfección hecha mono. Aparte de que es monísimo, es multiposición, adáptalo a la ocasión y en todo momento triunfarás. Tejido bámbula, escote en la espalda, y corte recto y fluido en el bajo. REPITO: ES PERFECTO. La modelo de la talla 36 mide 1,57 m y la modelo de la talla 44 mide 1,58 m. 85%Viscosa 15%Nylon ",
                    Price = 175,
                    InStock = 7,
                    IsNew = true,
                    IsBestSeller = true,
                    PercentageDiscount = 0,
                    SubCategoryId = 3,
                    Pictures = new List<Picture>{
                        new Picture {PictureName = "p10a.webp"},
                        new Picture {PictureName = "p10b.webp"},
                        new Picture {PictureName = "p10c.webp"},
                        new Picture {PictureName = "p10d.webp"}
                    },
                    ProductSizes = new List<ProductSize>{
                        new ProductSize { ProductId = 10, TallaId = 2, Quantity = 5},
                        new ProductSize { ProductId = 10, TallaId = 3, Quantity = 5}
                    }

                });
                _shoppingDbContext.Products.Add(new Product
                {
                    Name = "Pantalón Leale - Escarlata",
                    Description = "Fans absolutas de los pantalones estampados y este de estampado animal print, fue un flechazo al segundo. Con gomita en la cintura y un tejido súper fluido; puedes ir comodísima y con rollazo. La modelo de la talla 36 mide 1,57 m y la modelo de la talla 34 mide 1,61 m. 100%Poliéster ",
                    Price = 185,
                    InStock = 7,
                    IsNew = true,
                    IsBestSeller = true,
                    PercentageDiscount = 0,
                    SubCategoryId = 3,
                    Pictures = new List<Picture>{
                        new Picture {PictureName = "p11a.webp"},
                        new Picture {PictureName = "p11b.webp"},
                        new Picture {PictureName = "p11c.webp"}
                    },
                    ProductSizes = new List<ProductSize>{
                        new ProductSize { ProductId = 11, TallaId = 2, Quantity = 5},
                        new ProductSize { ProductId = 11, TallaId = 3, Quantity = 5}
                    }

                });
                _shoppingDbContext.Products.Add(new Product
                {
                    Name = "Mono Aliseda Bicolor - Kaki",
                    Description = "¿Fan de los monos? Estás de enhorabuena. Con escote cruzado y pernecha recta, necesitas este mono. El estampado bicolor nos encanta. Llévalo con calzado a contraste y triunfa. Tu tejido de crepe no se arruga. La modelo de la talla 36 mide 1,72 m y la modelo de la talla 46 mide 1,58 m. 100%Poliéster ",
                    Price = 208,
                    InStock = 7,
                    IsNew = true,
                    IsBestSeller = true,
                    PercentageDiscount = 14,
                    SubCategoryId = 3,
                    Pictures = new List<Picture>{
                        new Picture {PictureName = "p12a.webp"},
                        new Picture {PictureName = "p12b.webp"},
                        new Picture {PictureName = "p12c.webp"}
                    },
                    ProductSizes = new List<ProductSize>{
                        new ProductSize { ProductId = 12, TallaId = 2, Quantity = 5},
                        new ProductSize { ProductId = 12, TallaId = 3, Quantity = 5}
                    }

                });
                _shoppingDbContext.Products.Add(new Product
                {
                    Name = "Biker Taran - Fucsia",
                    Description = "Inauguramos por todo lo alto la temporada de bikers. En un tono fucsia potente y de suave antelina elastica, esta es la biker que más vas a amar. Combínala con vestido, con vaqueros o con faldas, tienes el rollazo asegurado. Lleva bolsillos. La modelo de la talla 34 mide 1,62 m y la modelo de la talla 44 mide 1,58 m. 95%Poliéster 5%Elastán ",
                    Price = 170,
                    InStock = 10,
                    IsNew = true,
                    IsBestSeller = true,
                    PercentageDiscount = 0,
                    SubCategoryId = 4,
                    Pictures = new List<Picture>{
                        new Picture {PictureName = "p13a.webp"},
                        new Picture {PictureName = "p13b.webp"},
                        new Picture {PictureName = "p13c.webp"},
                        new Picture {PictureName = "p13d.webp"}
                    },
                    ProductSizes = new List<ProductSize>{
                        new ProductSize { ProductId = 13, TallaId = 2, Quantity = 5},
                        new ProductSize { ProductId = 13, TallaId = 3, Quantity = 5}
                    }

                });
                _shoppingDbContext.Products.Add(new Product
                {
                    Name = "Blazer Dike Ef. Lino - Blanco",
                    Description = "ESTILO en mayúsculas. Esta blazer crop lo tiene todo. De corte recto, solapas y hombreras, es sobria y elegante a la vez que trendy. De tejido de algodón de efecto lino, lleva forro interior. La modelo de la talla 36 mide 1,57 m y la modelo de la talla 44 mide 1,58 m. Edición exclusiva online. 97%Algodón 3%Elastán ",
                    Price = 190,
                    InStock = 8,
                    IsNew = true,
                    IsBestSeller = true,
                    PercentageDiscount = 0,
                    SubCategoryId = 4,
                    Pictures = new List<Picture>{
                        new Picture {PictureName = "p14a.webp"},
                        new Picture {PictureName = "p14b.webp"},
                        new Picture {PictureName = "p14c.webp"},
                        new Picture {PictureName = "p14d.webp"}
                    },
                    ProductSizes = new List<ProductSize>{
                        new ProductSize { ProductId = 14, TallaId = 2, Quantity = 5},
                        new ProductSize { ProductId = 14, TallaId = 3, Quantity = 5}
                    }

                });
                _shoppingDbContext.Products.Add(new Product
                {
                    Name = "Falda Salor Calado - Blanco",
                    Description = "Diseño ha creado la falda más bonita de la temporada. De calado, corte sirena y fruncida en los laterales ¿Puede ser más mona?. Es perfecta para una tarde con amigas, una cena, brunch y para ese viaje que estás deseando. Spoiler: Combínala con el top Ugada y arrasarás. La modelo mide 1,57 m y lleva la talla 36. 100%Algodón ",
                    Price = 185,
                    InStock = 8,
                    IsNew = true,
                    IsBestSeller = true,
                    PercentageDiscount = 0,
                    SubCategoryId = 5,
                    Pictures = new List<Picture>{
                        new Picture {PictureName = "p15a.webp"},
                        new Picture {PictureName = "p15b.webp"},
                        new Picture {PictureName = "p15c.webp"},
                        new Picture {PictureName = "p15d.webp"}
                    },
                    ProductSizes = new List<ProductSize>{
                        new ProductSize { ProductId = 15, TallaId = 2, Quantity = 5},
                        new ProductSize { ProductId = 15, TallaId = 3, Quantity = 5}
                    }

                });
                _shoppingDbContext.Products.Add(new Product
                {
                    Name = "Falda Reguie - Beige",
                    Description = "Hemos unido 2 tendencias en una prenda. diseñamos la falda pareo con estampado boho con la que aegurarás un lookazo. En un lateral con un lacito del mismo tejido. La modelo de la talla 34 mide 1,62, la modelo de la talla 36 mide 1,57 m y la modelo de la talla 48 mide 1,58 m. 00% Poliéster ",
                    Price = 195,
                    InStock = 8,
                    IsNew = true,
                    IsBestSeller = true,
                    PercentageDiscount = 0,
                    SubCategoryId = 5,
                    Pictures = new List<Picture>{
                        new Picture {PictureName = "p16a.webp"},
                        new Picture {PictureName = "p16b.webp"},
                        new Picture {PictureName = "p16c.webp"},
                        new Picture {PictureName = "p16d.webp"}
                    },
                    ProductSizes = new List<ProductSize>{
                        new ProductSize { ProductId = 16, TallaId = 2, Quantity = 5},
                        new ProductSize { ProductId = 16, TallaId = 3, Quantity = 5}
                    }

                });
                _shoppingDbContext.Products.Add(new Product
                {
                    Name = "Falda Talina Ef. Lino - Arena",
                    Description = "Monería de falda. De algodón con aspecto lino, esta falda de corte pareo será tu gran aliada para looks cómodos, fresquitos y de aire natural. Combínala con blancos, negros, kakis o azules. Sienta genial. La modelo de la talla 36 mide 1,57 m y la modelo de la talla 48 mide 1,58 m. 97%Algodón 3%Elastán ",
                    Price = 200,
                    InStock = 8,
                    IsNew = true,
                    IsBestSeller = true,
                    PercentageDiscount = 0,
                    SubCategoryId = 5,
                    Pictures = new List<Picture>{
                        new Picture {PictureName = "p17a.webp"},
                        new Picture {PictureName = "p17b.webp"},
                        new Picture {PictureName = "p17c.webp"},
                        new Picture {PictureName = "p17d.webp"}
                    },
                    ProductSizes = new List<ProductSize>{
                        new ProductSize { ProductId = 17, TallaId = 2, Quantity = 5},
                        new ProductSize { ProductId = 17, TallaId = 3, Quantity = 5}
                    }

                });
                _shoppingDbContext.Products.Add(new Product
                {
                    Name = "Falda Maretan - Marino",
                    Description = "Falda midi, plisada y de topitos. Es perfecta para usar en todas las temporadas LLévala con bambas, palas o taconazo. Tejido de crepe muy fluido y con gomita. La modelo mide 1,57 m y lleva la talla 36. 100% Poliéster ",
                    Price = 190,
                    InStock = 8,
                    IsNew = true,
                    IsBestSeller = true,
                    PercentageDiscount = 0,
                    SubCategoryId = 5,
                    Pictures = new List<Picture>{
                        new Picture {PictureName = "p18a.webp"},
                        new Picture {PictureName = "p18b.webp"},
                        new Picture {PictureName = "p18c.webp"}
                    },
                    ProductSizes = new List<ProductSize>{
                        new ProductSize { ProductId = 18, TallaId = 2, Quantity = 5},
                        new ProductSize { ProductId = 18, TallaId = 3, Quantity = 5}
                    }

                });
                _shoppingDbContext.Products.Add(new Product
                {
                    Name = "Zapato Silvie - Oro",
                    Description = "Zapato de Tacon Fino para Mujer Comodo Estilo Salon con Strass Punta Fina. Tacón: 7cm, Plataforma: 0cm ",
                    Price = 208,
                    InStock = 8,
                    IsNew = true,
                    IsBestSeller = true,
                    PercentageDiscount = 0,
                    SubCategoryId = 6,
                    Pictures = new List<Picture>{
                        new Picture {PictureName = "p19a.webp"},
                        new Picture {PictureName = "p19b.webp"},
                        new Picture {PictureName = "p19c.webp"},
                        new Picture {PictureName = "p19d.webp"}
                    },
                    ProductSizes = new List<ProductSize>{
                        new ProductSize { ProductId = 19, TallaId = 2, Quantity = 5}
                    }

                });
                _shoppingDbContext.Products.Add(new Product
                {
                    Name = "Zapato Jollie - Negro",
                    Description = "Zapato de Tacon Fino para Mujer Comodo Estilo Salon con Strass Punta Fina. Tacón: 9,5cm, Plataforma: 0cm",
                    Price = 212,
                    InStock = 8,
                    IsNew = true,
                    IsBestSeller = true,
                    PercentageDiscount = 0,
                    SubCategoryId = 6,
                    Pictures = new List<Picture>{
                        new Picture {PictureName = "p20a.webp"},
                        new Picture {PictureName = "p20b.webp"},
                        new Picture {PictureName = "p20c.webp"},
                        new Picture {PictureName = "p20d.webp"}
                    },
                    ProductSizes = new List<ProductSize>{
                        new ProductSize { ProductId = 20, TallaId = 2, Quantity = 5},
                        new ProductSize { ProductId = 20, TallaId = 3, Quantity = 5}
                    }

                });
                _shoppingDbContext.Products.Add(new Product
                {
                    Name = "Sandalia Tacón Tere - Beige",
                    Description = "Sandalia de tacón medio para mujer. Ultra cómodo, forrado de yute, pala fina y cierre de hebilla.",
                    Price = 207,
                    InStock = 8,
                    IsNew = true,
                    IsBestSeller = true,
                    PercentageDiscount = 0,
                    SubCategoryId = 7,
                    Pictures = new List<Picture>{
                        new Picture {PictureName = "p21a.webp"},
                        new Picture {PictureName = "p21b.webp"},
                        new Picture {PictureName = "p21c.webp"}
                    },
                    ProductSizes = new List<ProductSize>{
                        new ProductSize { ProductId = 21, TallaId = 2, Quantity = 5},
                        new ProductSize { ProductId = 21, TallaId = 3, Quantity = 5}
                    }
                });
                _shoppingDbContext.Products.Add(new Product
                {
                    Name = "Sandalia Bea - Negro",
                    Description = "Sandalia de tacón fino para mujer. Cómodo con tiras finas cruzadas y cierre de hebilla.",
                    Price = 250,
                    InStock = 10,
                    IsNew = true,
                    IsBestSeller = true,
                    PercentageDiscount = 0,
                    SubCategoryId = 7,
                    Pictures = new List<Picture>{
                        new Picture {PictureName = "p22a.webp"},
                        new Picture {PictureName = "p22b.webp"},
                        new Picture {PictureName = "p22c.webp"}
                    },
                    ProductSizes = new List<ProductSize>{
                        new ProductSize { ProductId = 22, TallaId = 2, Quantity = 5},
                        new ProductSize { ProductId = 22, TallaId = 3, Quantity = 5}
                    }
                });
                _shoppingDbContext.Products.Add(new Product
                {
                    Name = "Sandalia Tacón Pavi - Oro",
                    Description = "Sandalia de tacón para mujer. Cómoda con pala fina, estilo Ankle Strap y cierre de hebilla.",
                    Price = 225,
                    InStock = 8,
                    IsNew = true,
                    IsBestSeller = true,
                    PercentageDiscount = 0,
                    SubCategoryId = 7,
                    Pictures = new List<Picture>{
                        new Picture {PictureName = "p23a.webp"},
                        new Picture {PictureName = "p23b.webp"},
                        new Picture {PictureName = "p23c.webp"}
                    },
                    ProductSizes = new List<ProductSize>{
                        new ProductSize { ProductId = 23, TallaId = 2, Quantity = 5},
                        new ProductSize { ProductId = 23, TallaId = 3, Quantity = 5}
                    }
                });
                _shoppingDbContext.Products.Add(new Product
                {
                    Name = "Botín Colina - Negro",
                    Description = "Botin de Tacon Campero para Mujer Comodo con Perforado Mil Puntos en Caña Bordado y Cierre de Cremallera ",
                    Price = 280,
                    InStock = 8,
                    IsNew = true,
                    IsBestSeller = true,
                    PercentageDiscount = 0,
                    SubCategoryId = 8,
                    Pictures = new List<Picture>{
                        new Picture {PictureName = "p24a.webp"},
                        new Picture {PictureName = "p24b.webp"},
                        new Picture {PictureName = "p24c.webp"}
                    },
                    ProductSizes = new List<ProductSize>{
                        new ProductSize { ProductId = 24, TallaId = 2, Quantity = 5},
                        new ProductSize { ProductId = 24, TallaId = 3, Quantity = 5}
                    }
                });
                _shoppingDbContext.Products.Add(new Product
                {
                    Name = "Bota Itza - Camel",
                    Description = "Bota de tacón súper ideal con estampado liso y detalle con tachuela en la caña. Cierre con cremallera.",
                    Price = 350,
                    InStock = 8,
                    IsNew = true,
                    IsBestSeller = true,
                    PercentageDiscount = 0,
                    SubCategoryId = 8,
                    Pictures = new List<Picture>{
                        new Picture {PictureName = "p25a.webp"},
                        new Picture {PictureName = "p25b.webp"},
                        new Picture {PictureName = "p25c.webp"}
                    },
                    ProductSizes = new List<ProductSize>{
                        new ProductSize { ProductId = 25, TallaId = 2, Quantity = 5},
                        new ProductSize { ProductId = 25, TallaId = 3, Quantity = 5}
                    }
                });
                _shoppingDbContext.Products.Add(new Product
                {
                    Name = "Botín Colina - Marrón",
                    Description = "Botin de Tacon Campero para Mujer Comodo con Perforado Mil Puntos en Caña Bordado y Cierre de Cremallera",
                    Price = 320,
                    InStock = 12,
                    IsNew = true,
                    IsBestSeller = true,
                    PercentageDiscount = 0,
                    SubCategoryId = 8,
                    Pictures = new List<Picture>{
                        new Picture {PictureName = "p26a.webp"},
                        new Picture {PictureName = "p26b.webp"},
                        new Picture {PictureName = "p26c.webp"}
                    },
                    ProductSizes = new List<ProductSize>{
                        new ProductSize { ProductId = 26, TallaId = 2, Quantity = 5},
                        new ProductSize { ProductId = 26, TallaId = 3, Quantity = 5}
                    }
                });
                _shoppingDbContext.Products.Add(new Product
                {
                    Name = "Botín Colina - Lila",
                    Description = "Botin de Tacon Campero para Mujer Comodo con Perforado Mil Puntos en Caña Bordado y Cierre de Cremallera",
                    Price = 280,
                    InStock = 12,
                    IsNew = true,
                    IsBestSeller = true,
                    PercentageDiscount = 0,
                    SubCategoryId = 8,
                    Pictures = new List<Picture>{
                        new Picture {PictureName = "p27a.webp"},
                        new Picture {PictureName = "p27b.webp"},
                        new Picture {PictureName = "p27c.webp"},
                        new Picture {PictureName = "p27d.webp"}
                    },
                    ProductSizes = new List<ProductSize>{
                        new ProductSize { ProductId = 27, TallaId = 2, Quantity = 5},
                        new ProductSize { ProductId = 27, TallaId = 3, Quantity = 5}
                    }
                });
                _shoppingDbContext.Products.Add(new Product
                {
                    Name = "Botín Valle - Beige",
                    Description = "Botin de Tacon para Mujer Comodos de Punta Fina Estampado Liso y Cierre de Cremallera",
                    Price = 305,
                    InStock = 9,
                    IsNew = true,
                    IsBestSeller = true,
                    PercentageDiscount = 0,
                    SubCategoryId = 8,
                    Pictures = new List<Picture>{
                        new Picture {PictureName = "p28a.webp"},
                        new Picture {PictureName = "p28b.webp"},
                        new Picture {PictureName = "p28c.webp"}
                    },
                    ProductSizes = new List<ProductSize>{
                        new ProductSize { ProductId = 28, TallaId = 2, Quantity = 5},
                        new ProductSize { ProductId = 28, TallaId = 3, Quantity = 5}
                    }
                });
                _shoppingDbContext.Products.Add(new Product
                {
                    Name = "Bamba Noci - Blanco",
                    Description = "Zapatilla plana para mujer. Diseñada con material bordado y calado. Son ultra cómodas y ligeras. Cierre de cordones.",
                    Price = 175,
                    InStock = 8,
                    IsNew = true,
                    IsBestSeller = true,
                    PercentageDiscount = 0,
                    SubCategoryId = 9,
                    Pictures = new List<Picture>{
                        new Picture {PictureName = "p29a.webp"},
                        new Picture {PictureName = "p29b.webp"},
                        new Picture {PictureName = "p29c.webp"}
                    },
                    ProductSizes = new List<ProductSize>{
                        new ProductSize { ProductId = 29, TallaId = 2, Quantity = 5},
                        new ProductSize { ProductId = 29, TallaId = 3, Quantity = 5}
                    }
                });

            }
            await _shoppingDbContext.SaveChangesAsync();
        }






        private async Task CheckCountriesAsync()
        {
            if (!_shoppingDbContext.Countries.Any())
            {
                _shoppingDbContext.Countries.Add(new Country
                {
                    Name = "Bolivia",
                    States = new List<State>()
                    {
                        new State()
                        {
                            Name = "Santa Cruz",
                            Cities = new List<City>() {
                                new City { Name = "Ascención de Guarayos"},
                                new City { Name = "Porongo"},
                                new City { Name = "Boyuibe"},
                                new City { Name = "Buena Vista"},
                                new City { Name = "Cabezas"},
                                new City { Name = "Camiri"},
                                new City { Name = "Carmen Rivero Torres"},
                                new City { Name = "Charagua"},
                                new City { Name = "Colpa Bélgica"},
                                new City { Name = "Comarapa"},
                                new City { Name = "Concepción"},
                                new City { Name = "Cotoca"},
                                new City { Name = "Cuatro Cañadas"},
                                new City { Name = "Cuevo"},
                                new City { Name = "El Puente"},
                                new City { Name = "El Torno"},
                                new City { Name = "Gral. Saavedra"},
                                new City { Name = "Gutiérrez"},
                                new City { Name = "La Guardia "},
                                new City { Name = "Lagunillas"},
                                new City { Name = "Mairana"},
                                new City { Name = "Mineros"},
                                new City { Name = "Montero"},
                                new City { Name = "Moro Moro"},
                                new City { Name = "Okinawa"},
                                new City { Name = "Pailón"},
                                new City { Name = "Pampa Grande"},
                                new City { Name = "Portachuelo"},
                                new City { Name = "Postrer Valle"},
                                new City { Name = "Pucara"},
                                new City { Name = "Puerto Fernández Alonso"},
                                new City { Name = "Puerto Quijarro"},
                                new City { Name = "Puerto Suárez"},
                                new City { Name = "Quirusillas"},
                                new City { Name = "Roboré"},
                                new City { Name = "Saipina"},
                                new City { Name = "Samaipata "},
                                new City { Name = "San Antonio del Lomerío"},
                                new City { Name = "San Carlos"},
                                new City { Name = "San Ignacio"},
                                new City { Name = "San Javier"},
                                new City { Name = "San José"},
                                new City { Name = "San Juan"},
                                new City { Name = "San Julián"},
                                new City { Name = "San Matías"},
                                new City { Name = "San Miguel"},
                                new City { Name = "San Pedro"},
                                new City { Name = "San Rafae"},
                                new City { Name = "San Ramón"},
                                new City { Name = "Santa Cruz de la Sierra"},
                                new City { Name = "Santa Rosa del Sara"},
                                new City { Name = "Trigal "},
                                new City { Name = "Urubichá"},
                                new City { Name = "Valle Grande"},
                                new City { Name = "Warnes"},
                                new City { Name = "Yapacaní"}
                            }
                        },
                        new State()
                        {
                            Name = "Cochabamba",
                            Cities = new List<City>() {
                                new City { Name = "Aiquile"},
                                new City { Name = "Alalay"},
                                new City { Name = "Anzaldo"},
                                new City { Name = "Arani "},
                                new City { Name = "Arbieto"},
                                new City { Name = "Arque"},
                                new City { Name = "Bolívar"},
                                new City { Name = "Capinota"},
                                new City { Name = "Chimoré"},
                                new City { Name = "Cliza"},
                                new City { Name = "Cochabamba"},
                                new City { Name = "Colcapirhua"},
                                new City { Name = "Colomi"},
                                new City { Name = "Cuchumuela"},
                                new City { Name = "Entre Ríos (Bulo Bulo)"},
                                new City { Name = "Independencia"},
                                new City { Name = "Mizque"},
                                new City { Name = "Morochata"},
                                new City { Name = "Omereque"},
                                new City { Name = "Pasorapa"},
                                new City { Name = "Pocona"},
                                new City { Name = "Pojo"},
                                new City { Name = "Puerto Villarroel"},
                                new City { Name = "Punata "},
                                new City { Name = "Quillacollo"},
                                new City { Name = "Sacaba"},
                                new City { Name = "Sacabamba"},
                                new City { Name = "San Benito"},
                                new City { Name = "Santivañez"},
                                new City { Name = "Sicaya"},
                                new City { Name = "Sipe Sipe"},
                                new City { Name = "Tacachi"},
                                new City { Name = "Tacopaya"},
                                new City { Name = "Tapacarí"},
                                new City { Name = "Tarata"},
                                new City { Name = "Tiquipaya"},
                                new City { Name = "Tiraque"},
                                new City { Name = "Toco"},
                                new City { Name = "Tolata"},
                                new City { Name = "Totora"},
                                new City { Name = "Vacas"},
                                new City { Name = "Vila Vila"},
                                new City { Name = "Villa Rivero"},
                                new City { Name = "Villa Tunari "},
                                new City { Name = "Vinto"},
                                new City { Name = "Cocapata"},
                                new City { Name = "Shinahota"}
                            }
                        },
                        new State()
                        {
                            Name = "Oruro",
                            Cities = new List<City>() {
                                new City { Name = "Andamarca"},
                                new City { Name = "Antequera"},
                                new City { Name = "Belén de Andamarca"},
                                new City { Name = "Caracollo"},
                                new City { Name = "Carangas"},
                                new City { Name = "Challapata"},
                                new City { Name = "Chipaya"},
                                new City { Name = "Choquecota"},
                                new City { Name = "Coipasa"},
                                new City { Name = "Corque"},
                                new City { Name = "Cruz de Machacamarca"},
                                new City { Name = "Curahuara de Carangas"},
                                new City { Name = "El Choro"},
                                new City { Name = "Escara"},
                                new City { Name = "Esmeralda"},
                                new City { Name = "Eucaliptus"},
                                new City { Name = "Huachacalla"},
                                new City { Name = "La Rivera"},
                                new City { Name = "Machacamarca"},
                                new City { Name = "Oruro"},
                                new City { Name = "Pampa Aullagas"},
                                new City { Name = "Pazña"},
                                new City { Name = "Sabaya"},
                                new City { Name = "Salinas de Garci Mendoza"},
                                new City { Name = "Santiago de Huari"},
                                new City { Name = "Santiago de Huayllamarca"},
                                new City { Name = "Santuario de Quillacas"},
                                new City { Name = "Soracachi"},
                                new City { Name = "Todos Santos"},
                                new City { Name = "Toledo"},
                                new City { Name = "Totora Oruro"},
                                new City { Name = "Turco"},
                                new City { Name = "Villa Huanuni"},
                                new City { Name = "Villa Poopó "},
                                new City { Name = "Yunguyo del Litora"},
                            }
                        },
                        new State()
                        {
                            Name = "Potosí",
                            Cities = new List<City>() {
                                new City { Name = "Acasio"},
                                new City { Name = "Arampampa"},
                                new City { Name = "Atocha"},
                                new City { Name = "Belén de Urmiri"},
                                new City { Name = "Betanzos"},
                                new City { Name = "Caiza D"},
                                new City { Name = "Caripuyo"},
                                new City { Name = "Chaquí"},
                                new City { Name = "Chayanta"},
                                new City { Name = "Colcha K"},
                                new City { Name = "Colquechaca"},
                                new City { Name = "Cotagaita"},
                                new City { Name = "Llallagua"},
                                new City { Name = "Llica"},
                                new City { Name = "Mojinete"},
                                new City { Name = "Ocurí"},
                                new City { Name = "Pocoata"},
                                new City { Name = "Porco"},
                                new City { Name = "Potosí"},
                                new City { Name = "Puna"},
                                new City { Name = "Ravelo"},
                                new City { Name = "Sacaca"},
                                new City { Name = "San Agustín"},
                                new City { Name = "San Antonio de Esmoruco"},
                                new City { Name = "San Pablo de Lipez"},
                                new City { Name = "San Pedro de Buena Vista"},
                                new City { Name = "San Pedro de Quemes"},
                                new City { Name = "Tacobamba"},
                                new City { Name = "Tahua"},
                                new City { Name = "Tinguipaya (4)"},
                                new City { Name = "Tomave"},
                                new City { Name = "Toro Toro"},
                                new City { Name = "Tupiza"},
                                new City { Name = "Uncía"},
                                new City { Name = "Uyuni"},
                                new City { Name = "Villa de Yocalla"},
                                new City { Name = "Villazón"},
                                new City { Name = "Vitichi"},
                                new City { Name = "Chuquiuta"},
                                new City { Name = "Ckochas"}
                            }
                        },
                        new State()
                        {
                            Name = "Chuquisaca",
                            Cities = new List<City>() {
                                new City { Name = "Camargo"},
                                new City { Name = "Camataqui"},
                                new City { Name = "Culpina "},
                                new City { Name = "El Villar"},
                                new City { Name = "Huacaya"},
                                new City { Name = "Icla"},
                                new City { Name = "Incahuasi"},
                                new City { Name = "Las Carreras"},
                                new City { Name = "Macharetí"},
                                new City { Name = "Monteagudo"},
                                new City { Name = "Padilla"},
                                new City { Name = "Poroma"},
                                new City { Name = "Presto"},
                                new City { Name = "San Lucas"},
                                new City { Name = "San Pablo de Huacareta"},
                                new City { Name = "Sopachuy"},
                                new City { Name = "Sucre"},
                                new City { Name = "Tarabuco"},
                                new City { Name = "Tarvita"},
                                new City { Name = "Tomina"},
                                new City { Name = "Villa Alcalá"},
                                new City { Name = "Villa Azurduy"},
                                new City { Name = "Villa Mojocoya"},
                                new City { Name = "Villa Serrano"},
                                new City { Name = "Villa Vaca Guzmán"},
                                new City { Name = "Villa Zudáñez"},
                                new City { Name = "Yamparáez"}
                            }
                        },
                        new State()
                        {
                            Name = "Tarija",
                            Cities = new List<City>() {
                                new City { Name = "Bermejo"},
                                new City { Name = "Caraparí"},
                                new City { Name = "El Puente"},
                                new City { Name = "Entre Ríos"},
                                new City { Name = "Padcaya"},
                                new City { Name = "San Lorenzo"},
                                new City { Name = "Tarija"},
                                new City { Name = "Uriondo"},
                                new City { Name = "Villamontes"},
                                new City { Name = "Yacuiba"},
                                new City { Name = "Yunchará"}
                            }
                        },
                        new State()
                        {
                            Name = "Pando",
                            Cities = new List<City>() {
                                new City { Name = "Bella Flor"},
                                new City { Name = "Blanca Flor"},
                                new City { Name = "Bolpebra"},
                                new City { Name = "Cobija"},
                                new City { Name = "El Sena"},
                                new City { Name = "Filadelfia"},
                                new City { Name = "Humaita"},
                                new City { Name = "Nueva Esperanza"},
                                new City { Name = "Porvenir"},
                                new City { Name = "Puerto Gonzalo Moreno"},
                                new City { Name = "Puerto Rico"},
                                new City { Name = "San Pedro Pando"},
                                new City { Name = "Santa Rosa del Abuná"},
                                new City { Name = "Santos Mercado (Reserva)"},
                                new City { Name = "Villa Nueva (Loma Alta) "}
                            }
                        },
                        new State()
                        {
                            Name = "Beni",
                            Cities = new List<City>() {
                                new City { Name = "Baures"},
                                new City { Name = "Exaltación"},
                                new City { Name = "Guayaramerín"},
                                new City { Name = "Huacaraje"},
                                new City { Name = "Loreto"},
                                new City { Name = "Magdalena"},
                                new City { Name = "Puerto Siles"},
                                new City { Name = "Reyes"},
                                new City { Name = "Riberalta"},
                                new City { Name = "Rurrenabaque "},
                                new City { Name = "San Andrés"},
                                new City { Name = "San Borja"},
                                new City { Name = "San Ignacio Beni"},
                                new City { Name = "San Javier Beni"},
                                new City { Name = "San Joaquín"},
                                new City { Name = "San Ramón Beni"},
                                new City { Name = "Santa Ana"},
                                new City { Name = "Santa Rosa"},
                                new City { Name = "Trinidad"}
                            }
                        },
                        new State()
                        {
                            Name = "La Paz",
                            Cities = new List<City>() {
                                new City { Name = "Achacachi"},
                                new City { Name = "Achocalla"},
                                new City { Name = "Ancoraimes"},
                                new City { Name = "Andrés de Machaca"},
                                new City { Name = "Apolo"},
                                new City { Name = "Aucapata"},
                                new City { Name = "Ayata"},
                                new City { Name = "Ayo Ayo"},
                                new City { Name = "Batallas"},
                                new City { Name = "Cairoma"},
                                new City { Name = "Cajuata"},
                                new City { Name = "Calacoto"},
                                new City { Name = "Calamarca"},
                                new City { Name = "Caquiaviri"},
                                new City { Name = "Caranavi"},
                                new City { Name = "Catacora"},
                                new City { Name = "Chacarilla"},
                                new City { Name = "Charaña"},
                                new City { Name = "Chulumani"},
                                new City { Name = "Chuma"},
                                new City { Name = "Collana"},
                                new City { Name = "Colquencha"},
                                new City { Name = "Colquiri"},
                                new City { Name = "Comanche"},
                                new City { Name = "Combaya"},
                                new City { Name = "Copacabana"},
                                new City { Name = "Coripata"},
                                new City { Name = "Coro Coro"},
                                new City { Name = "Coroico"},
                                new City { Name = "Curva"},
                                new City { Name = "Desaguadero"},
                                new City { Name = "El Alto"},
                                new City { Name = "Gral. Juan José Perez"},
                                new City { Name = "Guanay"},
                                new City { Name = "Guaqui"},
                                new City { Name = "Ichoca"},
                                new City { Name = "Inquisivi"},
                                new City { Name = "Irupana"},
                                new City { Name = "Ixiamas"},
                                new City { Name = "Jesús de Machaca"},
                                new City { Name = "La Asunta"},
                                new City { Name = "La Paz"},
                                new City { Name = "Laja"},
                                new City { Name = "Licoma Pampa"},
                                new City { Name = "Luribay"},
                                new City { Name = "Malla"},
                                new City { Name = "Mapiri"},
                                new City { Name = "Mecapaca (1)"},
                                new City { Name = "Mocomoco"},
                                new City { Name = "Nazacara de Pacajes"},
                                new City { Name = "Palca (1)"},
                                new City { Name = "Palos Blancos"},
                                new City { Name = "Papel Pampa"},
                                new City { Name = "Patacamaya"},
                                new City { Name = "Pelechuco"},
                                new City { Name = "Pucarani"},
                                new City { Name = "Puerto Acosta"},
                                new City { Name = "Puerto Carabuco"},
                                new City { Name = "Puerto Pérez"},
                                new City { Name = "Quiabaya"},
                                new City { Name = "Quime"},
                                new City { Name = "San Buena Ventura"},
                                new City { Name = "San Pedro de Curahuara"},
                                new City { Name = "San Pedro de Tiquina"},
                                new City { Name = "Santiago de Callapa"},
                                new City { Name = "Santiago de Machaca"},
                                new City { Name = "Sapahaqui"},
                                new City { Name = "Sica Sica"},
                                new City { Name = "Sorata"},
                                new City { Name = "Tacacoma"},
                                new City { Name = "Taraco"},
                                new City { Name = "Teoponte"},
                                new City { Name = "Tiahuanacu"},
                                new City { Name = "Tipuani"},
                                new City { Name = "Tito Yupanqui"},
                                new City { Name = "Umala"},
                                new City { Name = "Viacha"},
                                new City { Name = "Waldo Ballivián"},
                                new City { Name = "Yaco"},
                                new City { Name = "Yanacachi"},
                                new City { Name = "Huarina"},
                                new City { Name = "Santiago de Huata"},
                                new City { Name = "Escoma"},
                                new City { Name = "Humanata"},
                                new City { Name = "Alto Beni"}
                            }
                        },
                    }
                });



                _shoppingDbContext.Countries.Add(new Country
                {
                    Name = "Colombia",
                    States = new List<State>()
                    {
                        new State()
                        {
                            Name = "Antioquia",
                            Cities = new List<City>() {
                                new City() { Name = "Medellín" },
                                new City() { Name = "Itagüí" },
                                new City() { Name = "Envigado" },
                                new City() { Name = "Bello" },
                                new City() { Name = "Rionegro" },
                            }
                        },
                        new State()
                        {
                            Name = "Bogotá",
                            Cities = new List<City>() {
                                new City() { Name = "Usaquen" },
                                new City() { Name = "Champinero" },
                                new City() { Name = "Santa fe" },
                                new City() { Name = "Useme" },
                                new City() { Name = "Bosa" },
                            }
                        },
                    }
                });
                _shoppingDbContext.Countries.Add(new Country
                {
                    Name = "Estados Unidos",
                    States = new List<State>()
                    {
                        new State()
                        {
                            Name = "Florida",
                            Cities = new List<City>() {
                                new City() { Name = "Orlando" },
                                new City() { Name = "Miami" },
                                new City() { Name = "Tampa" },
                                new City() { Name = "Fort Lauderdale" },
                                new City() { Name = "Key West" },
                            }
                        },
                        new State()
                        {
                            Name = "Texas",
                            Cities = new List<City>() {
                                new City() { Name = "Houston" },
                                new City() { Name = "San Antonio" },
                                new City() { Name = "Dallas" },
                                new City() { Name = "Austin" },
                                new City() { Name = "El Paso" },
                            }
                        },
                    }
                });
            }

            await _shoppingDbContext.SaveChangesAsync();
        }



    }
}
