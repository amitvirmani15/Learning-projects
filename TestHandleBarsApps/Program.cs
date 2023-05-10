using System;
using System.Collections.Generic;
using HandlebarsDotNet;

namespace TestHandleBarsApps
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            RegisterBlockHelpers();
            
            Console.ReadLine();
        }


        private static void RenderPartial()
        {
            string source =
                @"<h2>Names</h2>
                    {{#names1}}
                      {{> user}}
                    {{/names1}}";

            string partialSource =
                @"<strong>{{name}}</strong>";

            Handlebars.RegisterTemplate("user", partialSource);

            var template = Handlebars.Compile(source);

            var data = new
            {
                names1 = new[] {
                    new {
                        name = "Karen"
                    },
                    new {
                        name = "Jon"
                    }
                }
            };

            var result = template(data);
            Console.Write(result);

            /* Would render:
            <h2>Names</h2>
              <strong>Karen</strong>
              <strong>Jon</strong>
            */
        }

        private static void RenderForeach()
        {
            var animals = new Dictionary<string, string>()
            {
                {"Fluffy", "cat" },
                {"Fido", "dog" },
                {"Chewy", "hamster" }
            };

            var template = "{{#each this}}The animal, {{@key}}, {{@value 'dog'}}is a dog{{else}}is not a dog. \n\r\n{{/each}}";
            var compiledTemplate = Handlebars.Compile(template);
            string templateOutput = compiledTemplate(animals);
            Console.WriteLine(templateOutput);
        }

        private static void RenderLink()
        {
            Handlebars.RegisterHelper("link_to", (writer, context, parameters) =>
            {
                writer.WriteSafeString($"<a href='{context["url"]}'>{context["text"]}</a>");
            });

            string source = @"Click here: {{link_to}}";

            var template = Handlebars.Compile(source);

            var data = new
            {
                url = "https://github.com/rexm/handlebars.net",
                text = "Handlebars.Net"
            };

            var result = template(data);
        }

        private static void RegisterBlockHelpers()
        {
            Handlebars.RegisterHelper("StringEqualityBlockHelper", (output, options, context, arguments) =>
            {
                if (arguments.Length != 2)
                {
                    throw new HandlebarsException("{{#StringEqualityBlockHelper}} helper must have exactly two arguments");
                }

                var left = arguments.At<string>(0);
                var right = arguments[1] as string;
                if (left == right) options.Template(output, context);
                else options.Inverse(output, context);
            });

            var animals = new Dictionary<string, string>()
            {
                {"Fluffy", "cat" },
                {"Fido", "dog" },
                {"Chewy", "hamster" }
            };

            var template = "{{#each this}}The animal, {{@key}}, {{#StringEqualityBlockHelper @value 'dog'}}is a dog{{else}}is not a dog{{/StringEqualityBlockHelper}}.\r\n{{/each}}";
            var compiledTemplate = Handlebars.Compile(template);
            string templateOutput = compiledTemplate(animals);
            Console.WriteLine(templateOutput);

            /* Would render
            The animal, Fluffy, is not a dog.
            The animal, Fido, is a dog.
            The animal, Chewy, is not a dog.
            */
        }
    }
}
