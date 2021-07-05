using System.Collections.Generic;
using Xenomech.Enumeration;
using Xenomech.Feature.DialogDefinition;
using Xenomech.Service;
using Xenomech.Service.ChatCommandService;

namespace Xenomech.Feature.ChatCommandDefinition
{
    public class DiceChatCommand: IChatCommandListDefinition
    {

        public Dictionary<string, ChatCommandDetail> BuildChatCommands()
        {
            var builder = new ChatCommandBuilder();

            builder.Create("dice")
                .Description("Opens the dice bag menu.")
                .Permissions(AuthorizationLevel.All)
                .Action((user, target, location, args) =>
                {
                    Dialog.StartConversation(user, user, nameof(DiceDialog));
                });

            return builder.Build();
        }
    }
}
