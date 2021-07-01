using System.Collections.Generic;

namespace Xenomech.Service.SnippetService
{
    public interface ISnippetListDefinition
    {
        public Dictionary<string, SnippetDetail> BuildSnippets();
    }
}
