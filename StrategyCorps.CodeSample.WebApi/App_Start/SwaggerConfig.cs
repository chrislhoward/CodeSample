using System.Web.Http;
using StrategyCorps.CodeSample.WebApi;
using Swashbuckle.Application;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace StrategyCorps.CodeSample.WebApi
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            //          GlobalConfiguration.Configuration
            //.EnableSwagger(c => c.SingleApiVersion("v1", "A title for your API"))
            //.EnableSwaggerUi();

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "StrategyCorps.SampleCode.WebApi");
                        //c.IncludeXmlComments(GetXmlCommentsPath());
                        //c.DescribeAllEnumsAsStrings();
                        //c.OperationFilter<AddDefaultResponse>();
                    })
                .EnableSwaggerUi(c =>
                    {
                        //c.InjectStylesheet(containingAssembly, "Swashbuckle.Dummy.SwaggerExtensions.testStyles1.css");
                        //c.InjectJavaScript(thisAssembly, "Swashbuckle.Dummy.SwaggerExtensions.testScript1.js");

                        // Use this option to control how the Operation listing is displayed.
                        // It can be set to "None" (default), "List" (shows operations for each resource),
                        // or "Full" (fully expanded: shows operations and their details).
                        //
                        //c.DocExpansion(DocExpansion.List);

                        // Specify which HTTP operations will have the 'Try it out!' option. An empty paramter list disables
                        // it for all operations.
                        //
                        //c.SupportedSubmitMethods("GET", "HEAD");

                        // Use the CustomAsset option to provide your own version of assets used in the swagger-ui.
                        // It's typically used to instruct Swashbuckle to return your version instead of the default
                        // when a request is made for "index.html". As with all custom content, the file must be included
                        // in your project as an "Embedded Resource", and then the resource's "Logical Name" is passed to
                        // the method as shown below.
                        //
                        //c.CustomAsset("index", containingAssembly, "YourWebApiProject.SwaggerExtensions.index.html");
                        c.DisableValidator();
                        c.DocExpansion(DocExpansion.List);
                    });
        }
    }
}
