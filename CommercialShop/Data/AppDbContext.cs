using CommercialShop.Models;
using CommercialShop.Models.ProductModel;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Data;
using System.Security.Claims;
using System.Security.Principal;

namespace CommercialShop.Data
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
          // Product model DbSets
    public DbSet<Product> Products { get; set; }
    public DbSet<ItemVariant> ItemVariants { get; set; }
    public DbSet<Size> Sizes { get; set; }
    public DbSet<Categories> Categories { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<ProductDetails> ProductDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        base.OnModelCreating(modelBuilder); // preserve Identity configuration, Without it: Identity tables(AspNetUsers, Roles, Claims…) break.

        // Category (one) -> Product (many)
        modelBuilder.Entity<Categories>()
            .HasMany(c => c.Products)
            .WithOne(p => p.categories)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Size: unique code
        modelBuilder.Entity<Size>()
            .HasIndex(s => s.CodeSize)
            .IsUnique();

        // Product -> Images
        modelBuilder.Entity<Product>()
            .HasMany(p => p.images)
            .WithOne(i => i.product)
            .HasForeignKey(i => i.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // Product -> Details
        modelBuilder.Entity<Product>()
            .HasMany(p => p.details)
            .WithOne(d => d.product)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // ItemVariant config (do all variant rules here)
        modelBuilder.Entity<ItemVariant>(entity =>
        {
            // Product (1) -> (Many) Variants
            entity.HasOne(v => v.product)
                  .WithMany(p => p.itemVariants)
                  .HasForeignKey(v => v.ProductId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Size (1) -> (Many) Variants
            entity.HasOne(v => v.size)
                  .WithMany(s => s.itemVariants)
                  .HasForeignKey(v => v.SizeId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Unique variant per Product+Size
            entity.HasIndex(v => new { v.ProductId, v.SizeId })
                  .IsUnique();
        });

        // Store Gender enum as string
        modelBuilder.Entity<Categories>()
            .Property(c => c.gender) // change to c.Gender if that’s your property name
            .HasConversion<string>()
            .HasMaxLength(20);

            // replace the Guid initializers with valid, hex-only GUIDs
            var productID1 = Guid.Parse("2b6f0a30-1f7a-4d7f-b9a7-111111111111");
            var productID2 = Guid.Parse("3c7f1b41-2f8b-5e8f-c8b8-222222222222");

            var variantId1 = Guid.Parse("4d8f2c52-3f9c-6f90-d9c9-333333333333");
            var variantId2 = Guid.Parse("5e9f3d63-4fad-7fa1-eada-444444444444");
            var variantId3 = Guid.Parse("6faf4e74-5bbe-8fb2-fbeb-555555555555");
            var variantId4 = Guid.Parse("70a05f85-6ccf-9fc3-0cbc-666666666666");
            var variantId5 = Guid.Parse("81b16a96-7dd0-afd4-1cdc-777777777777");
            var variantId6 = Guid.Parse("92c27ba7-8ee1-bfe5-2ded-888888888888");

            // Seed Categories for Male and Female
            modelBuilder.Entity<Categories>().HasData(
                // --- FEMALE CATEGORIES ---
                new Categories { CategoryId = 1, CategoryName = "Jackets", gender = Gender.Female },
                new Categories { CategoryId = 2, CategoryName = "Jerseys", gender = Gender.Female },
                new Categories { CategoryId = 3, CategoryName = "Scarves", gender = Gender.Female },
                new Categories { CategoryId = 4, CategoryName = "Tops", gender = Gender.Female },
                new Categories { CategoryId = 5, CategoryName = "Training", gender = Gender.Female },
                new Categories { CategoryId = 6, CategoryName = "Footwear", gender = Gender.Female },

                // --- MALE CATEGORIES ---
                new Categories { CategoryId = 7, CategoryName = "Jackets", gender = Gender.Male },
                new Categories { CategoryId = 8, CategoryName = "Jerseys", gender = Gender.Male },
                new Categories { CategoryId = 9, CategoryName = "Tops", gender = Gender.Male },
                new Categories { CategoryId = 10, CategoryName = "Training", gender = Gender.Male },
                new Categories { CategoryId = 11, CategoryName = "Footwear", gender = Gender.Male },
                new Categories { CategoryId = 12, CategoryName = "Trousers & Shorts", gender = Gender.Male }
            );


            // 2. Seed Sizes
            modelBuilder.Entity<Size>().HasData(
                new Size { SizeId = 1, CodeSize = "S" },
                new Size { SizeId = 2, CodeSize = "M" },
                new Size { SizeId = 3, CodeSize = "L" },
                new Size { SizeId = 4, CodeSize = "XL" },
                new Size { SizeId = 5, CodeSize = "2XL" },
                new Size { SizeId = 6, CodeSize = "3XL" }
             );

            // 3. Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = productID1,
                    ProductName = "Chelsea Nike Away Stadium Shirt 2025-26",
                    Price = 172.00m,
                    CategoryId = 4, // Links to top for female
                    ProductDescription = "Official 2025-26 Away Stadium Shirt."
                },

                 new Product
                 {
                     ProductId = productID2,
                     ProductName = "Chelsea Nike Stadium Shirt 2025-26",
                     Price = 172.00m,
                     CategoryId = 9, // Links to top for male
                     ProductDescription = "Official 2025-26 Away Stadium Shirt."
                 }
            );

            modelBuilder.Entity<ItemVariant>().HasData(
                //product1 size 1
               new ItemVariant
               {
                   VariantId = variantId1,
                   ProductId = productID1,
                   SizeId = 1,
                   StockQuantity = 21
               },

               //product1 size 2
               new ItemVariant
               {
                   VariantId = variantId2,
                   ProductId = productID1,
                   SizeId = 2,
                   StockQuantity = 22
               },

               //product1 size 3
               new ItemVariant
               {
                   VariantId = variantId3,
                   ProductId = productID1,
                   SizeId = 3,
                   StockQuantity = 29
               },
               

                //product2 size1
                new ItemVariant
                {
                    VariantId = variantId4,
                    ProductId = productID2,
                    SizeId = 1,
                    StockQuantity = 23
                },

                 //product2 size2
                new ItemVariant
                {
                    VariantId = variantId5,
                    ProductId = productID2,
                    SizeId = 2,
                    StockQuantity = 25
                },

                //product2 size3
                new ItemVariant
                {
                    VariantId = variantId6,
                    ProductId = productID2,
                    SizeId = 3,
                    StockQuantity = 20
                }           
           );

            modelBuilder.Entity<ProductImage>().HasData(
                //image for female
                new ProductImage
                {
                    ImageId = 1,
                    ImagePath = "/Image/Women/Top/womenHomeShirt.png",
                    ProductId = productID1,
                    IsShownFirst = true,

                },

                new ProductImage
                {
                    ImageId = 3,
                    ImagePath = "/Image/HomePage/game.png",
                    ProductId = productID1,
                },
                 //image for male
                 new ProductImage
                 {
                     ImageId = 2,
                     ImagePath = "/Image/Men/Top/ChelseaAwayTshirt.png",
                     ProductId = productID2,
                     IsShownFirst = true,

                 },

                  //image for male
                 new ProductImage
                 {
                     ImageId = 4,
                     ImagePath = "/Image/HomePage/kit1.png",
                     ProductId = productID2,
                     IsShownFirst = true,

                 }
            );

            
        }
}
    }
        

