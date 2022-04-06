using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Organi.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organi.Domain.DataContext
{
    public static class OrganiSeedData
    {
        static public IApplicationBuilder Seed(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<OrganiDbContext>();

                InitAppInfo(db);
                InitCategories(db);
                InitProducts(db);
                InitPosts(db);
            }
            return app;
        }

        private static void InitPosts(OrganiDbContext db)
        {
            if (!db.Posts.Any())
            {
                for (int i = 1; i < 6; i++)
                {
                    db.Posts.Add(new Post
                    {
                        Name = $"{i} Blog Demo",
                        Author = "Code Academy",
                        Body = $"{i}-lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum ",
                        ImagePath = $"blog-{i}.jpg"
                    });
                    db.SaveChanges();
                }
            }
        }

        private static void InitAppInfo(OrganiDbContext db)
        {
            if (!db.AppInfos.Any())
            {
                db.AppInfos.Add(new AppInfo
                {
                    AppTitle = "Organi",
                    Address = "Baku",
                    Description = "Abbas yaradib yene",
                    Email = "abbas_abasov@list.ru",
                    ImageLink = "logo.png",
                    HashTag = "#developerleriqoruyaq",
                    Phone = "0703011132",
                    OpenTime = "10:00 to 20:00",
                    FacebookLink = "http://facebook.com",
                    Githublink = "http://github.com",
                    InstagramLink = "http://instagram.com",
                    TwitterLink = "http://twitter.com"
                });
            }
        }

        private static void InitProducts(OrganiDbContext db)
        {
            if (!db.Products.Any())
            {
                for (int i = 1; i < 12; i++)
                {
                    int CategoryId = 1;
                    if (i % 2 == 0)
                    {
                        CategoryId = 2;
                    }
                    else if (i % 3 == 0)
                    {
                        CategoryId = 3;
                    }
                    var product = new Product
                    {
                        Name = $"Product-{i}",
                        CategoryId = CategoryId,
                        Price = 50,
                        Quantity = 300,
                        Unit = "Kq",
                        Description = " lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum ",
                    };
                    product.Images = new List<Image>();
                    product.Images.Add(
                    new Image
                    {
                        Path = $"product-{i}.jpg",
                        IsMain = true
                    });
                    product.Images.Add(
                    new Image
                    {
                        Path = "thumb-1.jpg",
                    });
                    product.Images.Add(
                    new Image
                    {
                        Path = "thumb-2.jpg",
                    });
                    product.Images.Add(
                    new Image
                    {
                        Path = "thumb-3.jpg",
                    });
                    product.Images.Add(
                    new Image
                    {
                        Path = "thumb-4.jpg",
                    });
                    db.Products.Add(product);
                }
                db.SaveChanges();
                
            }
        }

        private static void InitCategories(OrganiDbContext db)
        {
            if (!db.Categories.Any())
            {
                db.Categories.AddRange(new Category
                {
                    Name = "Fresh Meat",
                    IsFeatured = true
                }, new Category
                {
                    Name = "Vegetables",
                    IsFeatured = true
                }, new Category
                {
                    Name = "Fruits"
                }, new Category
                {
                    Name = "Ocean Foods"
                }, new Category
                {
                    Name = "Butter & Eggs"
                }, new Category
                {
                    Name = "Fastfood",
                    IsFeatured = true
                }, new Category
                {
                    Name = "Fresh Onion"
                }, new Category
                {
                    Name = "Crips"
                }, new Category
                {
                    Name = "Fresh Bananas"
                });

                db.SaveChanges();
            }
        }
    }
}
