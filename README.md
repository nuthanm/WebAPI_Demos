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
     
