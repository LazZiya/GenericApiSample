# Generic Api Sample Project
Sample web api project to show the usage of generic api controller.
The Api project is "WebApi" it contains a [GenericBaseController][1] and two inherited controllers [PlayersController][2] and [TreasuresController][3].

## Structure
The solution containes 4 projects

- WebUI : user interface, control panel
- WebAPI : Genreic and non-generic API's
- Domain : common models and interfaces
- ApiServiceClient : generic Http serivce client to communicate with the API's

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

`MyModel` must implement [`IHasId<TKey>`][4] and [`IOrdered`][5] interfaces.

- [`IHasId<TKey>`][4] : Defines the type of the Id of the model, can be int, string or any IEquatable property. 

- [`IOrdered`][5] : defines a `Name` parameter, so the models can be ordered by name

## Overriding methods

All methods in the [GenericBaseController][1] are virtual, so they can be ovderridden when necessary like below:
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
- Copy the url from the browser e.g. "https://localhost:44376/" and past it into "[WebUI\startup.cs line:45][6]" 
````cs
services.AddHttpClient<GenericApiService>(ops=> {
    ops.BaseAddress = new Uri("https://localhost:44376/api/");
});
````
- Run "WebUI" project and test the Players and Treasures CRUD functionalities.

## References
- [You're using HttpClient wrong and it is destabilizing your software][7]
- [Singleton HttpClient? Beware of this serious behaviour and how to fix it ][8]
- [Use HttpClientFactory to implement resilient HTTP requests][9]
- [Make HTTP requests using IHttpClientFactory in ASP.NET Core][10]


[1]: https://github.com/LazZiya/GenericApiSample/blob/master/WebApi/Controllers/GenericBaseController.cs
[2]: https://github.com/LazZiya/GenericApiSample/blob/master/WebApi/Controllers/PlayersController.cs
[3]: https://github.com/LazZiya/GenericApiSample/blob/master/WebApi/Controllers/TreasuresController.cs
[4]: https://github.com/LazZiya/GenericApiSample/blob/master/Infrastructure/Models/IHasId.cs
[5]: https://github.com/LazZiya/GenericApiSample/blob/master/Infrastructure/Models/IOrdered.cs
[6]: https://github.com/LazZiya/GenericApiSample/blob/master/WebUI/Startup.cs#L45
[7]: https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
[8]: http://byterot.blogspot.com/2016/07/singleton-httpclient-dns.html
[9]: https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
[10]: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-2.2
