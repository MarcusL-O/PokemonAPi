
namespace Pokemon
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //här ska du skriva kod

            // vår data
            var pokemons = new List<Pokemon>
            {
                new Pokemon {Id = 1, Name = "bulbasour", Type = "Grass "},
                new Pokemon {Id = 2, Name = "Ivasour", Type = "Grass"},
                new Pokemon {Id = 3, Name = "venosour", Type = "Grass"},
                new Pokemon {Id = 4, Name = "Charmander", Type = "Fire"}
            };

            //create
            app.MapPost("/pokemon", (Pokemon pokemon) => {
                pokemons.Add(pokemon);
                return Results.Ok(pokemon);
            });

            //read all
            app.MapGet("/Pokemons", () =>
            {
                return Results.Ok(pokemons);
            });

            //read by id
            app.MapGet("pokemon/{id}", (int id) =>
            {
                var pokemon = pokemons.Find(p => p.Id == id);

                if (pokemon == null)
                {
                    return Results.NotFound("sorry finns inte");

                }

                return Results.Ok(pokemon);
            });

            //update

            app.MapPut("/pokemon/{id}", (Pokemon updatedPokemon, int id) =>
            {
                var pokemon = pokemons.Find(p => p.Id == id);

                if(pokemon == null)
                {
                    return Results.NotFound("finns inte");
                }

                pokemons[id] = updatedPokemon;

                return Results.Ok(pokemon);
            });

            //delete

            app.MapDelete("/pokemon/{id}", (int id) =>
            {
                var pokemon = pokemons.Find(p => p.Id == id);

                if (pokemon == null)
                {
                    return Results.NotFound("går inte");
                }

                pokemons.Remove(pokemon);
                return Results.Ok("works ");
                
            });

            




            
            app.MapControllers();
            app.Run();

            


        }
    }
}
