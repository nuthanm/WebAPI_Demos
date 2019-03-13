# WebAPI_Demos
This repository has full of Asp.NET Standard Web API Demos

## WebAPI Important Points to remember 

1. Entry point for Web API is **Application_Start** Event from **Global.asax** File.

2. To know whether controller is a APIController, the only way is to check whether controller is inherited from **APIController** and namespace is for API Controller is **System.Web.Http;**

3. All our Web API related Configurations be in **App_Start** folder and file name is **WebApiConfig.cs**.

4. If client wants data in JSON (**application/json**) or XML (**application/XML**) from server then we add this information **Accept:<mime-type>** in Request Header to Server and server response is the requested format.
  #### **Note :** Can add multiple mime-types with in Accept like below example and If no Accept is added in Request Body then by default Server returns in JSON format.
      ** Example 1: ** Accept: application/json,application/xml and data is shown in Json format
      ** Example 2: ** Accept: application/json;q=0.5,application/xml;q=0.8 and here data is shown in XML because Quality Factor (Q) is high in XML formatter.
 
5. If data is coming in JSON has Pascal Case then if you want to convert into camelCase, Write the following snippet in Register Method inside WebApiConfig.cs file

`**Code Snippet:**`  config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

6. To see JSON data in Raw format with Indent then add the following line in Register method insider WebApiConfig.cs file 
`**Code Snippet:**`  config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

7. In webAPI, by default response is coming in browser is XML and if we want only JSON then add the following code snippet in Register Method inside WebAPiConfig.cs file.
`**Code Snippet:**`  config.Formatters.Remove(config.Formatters.XmlFormatter);
**Note : ** If we set Accept with XML then server response is in JSON only.

8. In webAPI, ViceVersa to XML only then add the following code snippet in Register Method inside WebAPiConfig.cs file.
`**Code Snippet:**`  config.Formatters.Remove(config.Formatters.JsonFormatter);
**Note : ** If we set Accept with JSON then server response is in XML only.

9. If request came from browser then by default server sends in XML format and if we want the response in JSON Format then write the following snippet in Register method inside WebApiConfig.cs file
`**Code Snippet:**`  config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

10. When doing a post operation we include both **Content-Type & Accept as application/json** other wise you may faced the following error.
            "message": "The request entity's media type 'text/plain' is not supported for this resource.",
            
11. Controller method names either start with only Http Verb or Verb<AnyName>
   ** Example : ** Get() or GetEmployees() both are valid.
   ** Note : **  Web API methods are ** case insensitive ** (i.e., Get() or get() => Both are same) and if we not include Get in method name then we get following ** 405 - Method not allowe ** error.
  
                    `{
                        "message": "The requested resource does not support http method 'GET'."
                     }`
     To fix this issue, add [HttpGet] attribute on top of custom named method.
     
12. **Attribute Routing** => Define Routes using [Route] attribute at Controller levels.

In WebApi 2, Attribute Routing is by default enabled. To cross verify, go to WebApiConfig.cs and see whether the below code snippet is there or not.

    config.MapHttpAttributeRoutes()

**Pros** :
1. It has more control to create URI patterns like Hierachies of resources
   - Ex: Employee wants his record
         `[Route("api/employees/id")]`
   - Ex: Employee wants his posts
         `[Route("api/employees/id")]` => This will throw the error because same signature for Getting Id and as well as for getting posts and internal it will confuse and to avoid this we add additional suffix
         `[Route"(api/employees/id/posts")]` => THis code snippet gives us selected employee posts.
2. We use both Attribute Routing and Convention based routing in same WebApi Project.
   If [Route] attribute at controller action then it use Attribute Routing or else use Convention Based Routing.
   
13. **Attribute Routing Constraints**
Same problem we face if two action methods have same signature like we see earlier. To overcome this problem Attribute Routing constraints solves the issue.

`Syntax: paramter:constraint`

Where constraint is nothing but data types : int , alpha, double,....

    Ex:
    [Route("{id:int}"]
    [Route("{name:aplha}"]
    
Allows us to solve the following problem
    [Route("{id:int:min(1)}"] => It means id>=1 and integer type then only map to this URL
Ex: public Employee Get(int id)
    {
      
    }
    [Route("{name:string}") => It means name is string type then only map to this URL.
    public Employee Get(string name)
    {

    }

14. **RoutePrefix** is an attribute is used to specify common route prefix(Ex: api/employees). Earlier we write api/employees/{id} in all the action methods. Using RoutePrefix attributes we eliminate this and add at controller level.

`Ex: [RoutePrefix("api/employees")]`

**Note:** To override RoutePrefix using `"~/"` inside [Route] attribute.
`Ex: [Route("~/api/users")] inside "api/employees"`

15. **API Versioning** in Web API is used for denoting that released API in public and clients started using/consuming services of API. As we all know, Once API is in public there may be changes in future either in terms of issues or new requirements.
    
 ` API Versioning Can be achieve in the following ways `

    1. URI's
       - Versioning either Conventional Based Routing in WebApiConfig.cs or Attribute Routing
       - Ex: AttributeRouting => [Route("api/v1/<ControllerName>)]
       - Ex: Conventional Based Routing 
       - config.Routes.MapHttpRoute(
               name: "Version2",
               routeTemplate: "api/v2/employees/{id}",
               defaults: new { id = RouteParameter.Optional, controller = "EmployeeV2" }
         );
    2. Query Strings
        - Request coming from client in the below format
          /api/employees?v=1
          /api/employees?v=2
          Note: Write custom logic for Query Strings while sending a request. Logic to write to call respective controller.
        
    3. Version Header
       Note: Write custom logic in Version Header while sending a request. Logic to write to call respective controller.
    4. Accept Header => Tell to the server what file format the browser needs the data. We can define the format using MIME Types.
       Note: Write custom logic in Accept Header while sending a request. Logic to write to call respective controller.
       Ex: Accept: application/json;version = 1 
    5. Media Type : Instead of using standard media types we go with custom media type 
       Ex: Accept : application/<anyformat which has version and type of data>

`Reference URL for Web API :`
[https://www.youtube.com/playlist?list=PL6n9fhu94yhW7yoUOGNOfHurUE6bpOO2b](https://www.youtube.com/playlist?list=PL6n9fhu94yhW7yoUOGNOfHurUE6bpOO2b "Web API Youtube Playlist by KudVenkat")
