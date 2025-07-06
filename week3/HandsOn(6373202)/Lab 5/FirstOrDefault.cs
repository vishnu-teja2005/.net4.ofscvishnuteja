var product = await context.Products.FindAsync(1);

Console.WriteLine($"Found: {product?.Name}");
