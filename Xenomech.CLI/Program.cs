using Microsoft.Extensions.CommandLineUtils;

namespace Xenomech.CLI
{
    internal class Program
    {
        private static readonly HakBuilder _hakBuilder = new();
        private static readonly PlaceableBuilder _placeableBuilder = new();

        static void Main(string[] args)
        {
            var app = new CommandLineApplication();

            // Set up the options.
            var placeableOption = app.Option(
                "-$|-p |--placeable",
                "Generates uti files in json format for all of the entries found in placeables.2da.",
                CommandOptionType.NoValue);

            var hakBuilderOption = app.Option(
                "-$|-k |--hak",
                "Builds hakpak files based on the hakbuilder.json configuration file.",
                CommandOptionType.NoValue);

            app.HelpOption("-? | -h | --help");

            app.OnExecute(() =>
            {
                if (placeableOption.HasValue())
                {
                    _placeableBuilder.Process();
                }

                if (hakBuilderOption.HasValue())
                {
                    _hakBuilder.Process();
                }

                return 0;
            });

            app.Execute(args);
        }
    }
}
