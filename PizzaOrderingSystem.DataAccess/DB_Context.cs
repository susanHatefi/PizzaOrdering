using Microsoft.EntityFrameworkCore;
using PizzaOrderSystem.DataAccess.Model.Entities;
using System.Collections.Generic;

namespace PizzaOrderSystem.DataAccess;

public class DB_Context:DbContext
{
    public DbSet<Order> Order { get; set; }
    public DbSet<OrderItem> OrderItem { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<Topping> Topping { get; set; }
    public DbSet<ToppingUnit> ToppingUnit { get; set; }
    public DbSet<Promotion> Promotion { get; set; }


    public DB_Context(DbContextOptions dbContextOptions) :base(dbContextOptions)
    {
        
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasMany<OrderItem>(e => e.OrderItems).WithOne(e => e.Order).HasForeignKey(e => e.OrderId).OnDelete(DeleteBehavior.Cascade);
            entity.Property(o => o.TotalPrice).IsRequired().HasColumnType("decimal(22,2)");

        });
        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasOne<Order>(oi=>oi.Order).WithMany(e => e.OrderItems).HasForeignKey(e => e.OrderId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne<Promotion>(oi=>oi.Promotion).WithMany(e => e.OrderItems).HasForeignKey(e => e.PromotionId);
            entity.HasOne<Product>(oi=>oi.Product).WithMany(e => e.OrderItems).HasForeignKey(e => e.ProductId);
            entity.HasMany<Topping>(e => e.Toppings).WithMany(e => e.OrderItems).UsingEntity<Dictionary<string,object>>("OrderItem_Topping",
                e=>e.HasOne<Topping>().WithMany().HasForeignKey("ToppingId"),
                e=>e.HasOne<OrderItem>().WithMany().HasForeignKey("OrderItemId")
                
                );
            entity.Property(oi => oi.TotalPrice).HasColumnType("decimal(22,2)").IsRequired();
            entity.Property(oi => oi.CreatedDate).IsRequired().HasDefaultValueSql("GETDATE()");


        });
        modelBuilder.Entity<Product>(entity =>
        {
           
            entity.HasIndex(e => e.Size).IsUnique();
            entity.Property(p => p.Size).IsRequired().HasConversion<string>();
            entity.Property(oi => oi.Price).HasColumnType("decimal(22,2)").IsRequired();
            entity.HasData(new Product()
            {
                Id = Guid.NewGuid(),
                Price = 5,
                Size = DataAccess.Model.Enumerations.ProductSizeEnum.Small
            },
                        new Product()
                        {
                            Id = Guid.NewGuid(),
                            Price = 7,
                            Size = DataAccess.Model.Enumerations.ProductSizeEnum.Medium
                        },
                        new Product()
                        {
                            Id = Guid.NewGuid(),
                            Price = 8,
                            Size = DataAccess.Model.Enumerations.ProductSizeEnum.Large
                        },
                        new Product()
                        {
                            Id = Guid.NewGuid(),
                            Price = 9,
                            Size = DataAccess.Model.Enumerations.ProductSizeEnum.ExtraLarge
                        }
            );
        });

        modelBuilder.Entity<Topping>(entity =>
        {
            
            entity.HasIndex(e => new { e.Name, e.Category }).IsUnique();
            entity.Property(t => t.Category).HasConversion<string>();
            entity.Property(t => t.Price).HasColumnType("decimal(22,2)").IsRequired();
            entity.HasData(new Topping()
            {
                Category = DataAccess.Model.Enumerations.ToppingCategoryEnum.Veg,
                Id = Guid.NewGuid(),
                Name = "Tomatoes",
                Price = 1

            },
                        new Topping()
                        {
                            Category = DataAccess.Model.Enumerations.ToppingCategoryEnum.Veg,
                            Id = Guid.NewGuid(),
                            Name = "Onions",
                            Price = 0.5M

                        },
                        new Topping()
                        {
                            Category = DataAccess.Model.Enumerations.ToppingCategoryEnum.Veg,
                            Id = Guid.NewGuid(),
                            Name = "Bell Pepper",
                            Price = 1

                        },
                        new Topping()
                        {
                            Category = DataAccess.Model.Enumerations.ToppingCategoryEnum.Veg,
                            Id = Guid.NewGuid(),
                            Name = "Mushrooms",
                            Price = 1.2M

                        },
                        new Topping()
                        {
                            Category = DataAccess.Model.Enumerations.ToppingCategoryEnum.Veg,
                            Id = Guid.NewGuid(),
                            Name = "Pineapple",
                            Price = 0.75M

                        },
                        new Topping()
                        {
                            Category = DataAccess.Model.Enumerations.ToppingCategoryEnum.NonVeg,
                            Id = Guid.NewGuid(),
                            Name = "Sausage",
                            Price = 1

                        },
                        new Topping()
                        {
                            Category = DataAccess.Model.Enumerations.ToppingCategoryEnum.NonVeg,
                            Id = Guid.NewGuid(),
                            Name = "Pepperoni",
                            Price = 2

                        },
                        new Topping()
                        {
                            Category = DataAccess.Model.Enumerations.ToppingCategoryEnum.NonVeg,
                            Id = Guid.NewGuid(),
                            Name = "Barbecue Chicken",
                            Price = 3

                        });

        }
        );
                var promotionId= Guid.NewGuid();

            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.HasIndex(p => p.Name).IsUnique();
                entity.Property(p => p.ProductSize).HasConversion<string>();

                entity.Property(t => t.Price).HasColumnType("decimal(22,2)").HasDefaultValue(0);
                entity.HasData(new Promotion()
                
                {
                    Id = Guid.NewGuid(),
                    Price = 5,
                    Name = "Offer1",
                    ProductSize = DataAccess.Model.Enumerations.ProductSizeEnum.Medium,
                    TotalToppings = 2,
                },
                new Promotion()
                {
                    Id = Guid.NewGuid(),
                    Price = 9,
                    Name = "Offer2",
                    ProductSize = DataAccess.Model.Enumerations.ProductSizeEnum.Medium,
                    TotalToppings = 4,
                    Quantity = 2
                },
                new Promotion()
                {
                    Id = promotionId,
                    Price = 0,
                    Discount = 50,
                    Name = "Offer3",
                    ProductSize = DataAccess.Model.Enumerations.ProductSizeEnum.Large,
                    TotalToppings = 4
                }
                );
            });

        modelBuilder.Entity<ToppingUnit>(entity =>
        {
            entity.HasOne<Promotion>(tu=>tu.Promotion).WithMany(e => e.TotalToppingsUnit).HasForeignKey(e => e.PromotionId).OnDelete(DeleteBehavior.Cascade);
            entity.HasData(new ToppingUnit()
            {
                Id = Guid.NewGuid(),
                ToppingName = "Barbecue Chicken",
                Unit = 2,
                PromotionId = promotionId

            },
                        new ToppingUnit()
                        {
                            Id = Guid.NewGuid(),
                            ToppingName = "Pepperoni",
                            PromotionId = promotionId,
                            Unit = 2,
                        });

        });
            base.OnModelCreating(modelBuilder);
        
    }
}
