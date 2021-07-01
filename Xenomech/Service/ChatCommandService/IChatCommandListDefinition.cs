using System.Collections.Generic;

namespace Xenomech.Service.ChatCommandService
{
    public interface IChatCommandListDefinition
    {
        public Dictionary<string, ChatCommandDetail> BuildChatCommands();
    }
}
