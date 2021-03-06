using System;
using System.Threading.Tasks;

namespace Discord.Commands
{
    /// <summary>
    /// Require that the command is invoked by the owner of the bot.
    /// </summary>
    /// <remarks>This precondition will only work if the bot is a bot account.</remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class RequireOwnerAttribute : PreconditionAttribute
    {
        public override async Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            switch (context.Client.TokenType)
            {
                case TokenType.Bot:
                    var application = await context.Client.GetApplicationInfoAsync();
                    if (context.User.Id != application.Owner.Id)
                        return PreconditionResult.FromError("Command can only be run by the owner of the bot");
                    return PreconditionResult.FromSuccess();
                default:
                    return PreconditionResult.FromError($"{nameof(RequireOwnerAttribute)} is not supported by this {nameof(TokenType)}.");                    
            }
        }
    }
}
