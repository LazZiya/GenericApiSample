# Generic Api Sample Project
Sample web api project to show the usage of generic api controller.
The Api project is "WebApi" it contains a [GenericBaseController](1) and two inherited controllers [PlayersController](2) and [TreasuresController](3).

## Structure
The solution containes 4 projects

- WebUI : user interface, control panel
- ApiServiceClient : generic Http serivce client to communicate with the API's
- WebAPI : Genreic and non-generic API's
- Infrastructure : common models and interfaces

![Generic API Sample Project Structure](WebUI/wwwroot/files/Generic%20API%20Project%20Structure%20v1.JPG)

If you want to create a new api controller inherit it from the GenericBaseController as below:
````cs
[Route("api/[controller]")]
[ApiController]
public class MyNewController : GenericBaseController<MyModel, int>
{
    public MyNewController(ApplicationDbContext context) : base(context)
    {
    }
}
````

`MyModel` must implement [`IHasId<TKey>`](4) and [`IOrdered`](5) interfaces.

- [`IHasId<TKey>`](4) : Defines the type of the Id of the model, can be int, string or any IEquatable property. 
- [`IOrdered`](5) : defines a `Name` parameter, so the models can be ordered by name

## Overriding methods

All methods in the [GenericBaseController](1) are virtual, so they can be ovderridden when necessary like below:
````cs
[Route("api/[controller]")]
[ApiController]
public class MyNewController : GenericBaseController<MyModel, int>
{
    public MyNewController(ApplicationDbContext context) : base(context)
    {
    }
    
    public override MyModel Get(int Id)
    {
        // custom logic...
        
        return base.Get(id);
    }
}
````
## IDE
This project developed in VS2019 and Asp.Net Core 2.2

## Database installation 

- Open the solution then right click on "WebApi" project then click on "Select as startup project"
- Open "Package Manager Console" window and run update command `update-database`


## Running the project

- First run the WebApi project by selecting the project name from the solution then press "Ctrl + Shift + W"
- Copy the url from the browser e.g. "https://localhost:44376/" and past it into "[Infrastructure\Http\ApiServiceClient.cs](6)" file line:38
`client.BaseAddress = ops.BaseAddress ?? new Uri("https://localhost:44376/api/");`
- Run "WebUI" project and test the Players and Treasures CRUD functionalities.

[1]: https://github.com/LazZiya/GenericApiSample/blob/master/WebApi/Controllers/GenericBaseController.cs
[2]: https://github.com/LazZiya/GenericApiSample/blob/master/WebApi/Controllers/PlayersController.cs
[3]: https://github.com/LazZiya/GenericApiSample/blob/master/WebApi/Controllers/TreasuresController.cs
[4]: https://github.com/LazZiya/GenericApiSample/blob/master/Infrastructure/Models/IHasId.cs
[5]: https://github.com/LazZiya/GenericApiSample/blob/master/Infrastructure/Models/IOrdered.cs
[6]: https://github.com/LazZiya/GenericApiSample/blob/master/Infrastructure/Http/ApiServiceClient.cs#L38
