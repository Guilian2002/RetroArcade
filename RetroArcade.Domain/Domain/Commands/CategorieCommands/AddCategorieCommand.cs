using Tools.Cqs.Commands;

namespace RetroArcade.Domain.Domain.Commands.CategorieCommands
{
    public sealed class AddCategorieCommand : ICommandDefinition
    {
        public string Name { get; }

        public AddCategorieCommand(string name)
        {
            Name = name;
        }
    }
}
