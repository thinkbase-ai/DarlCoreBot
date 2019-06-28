
using DarlCoreBot2.Models;
using GraphQL.Client;
using GraphQL.Common.Request;
using Microsoft.ApplicationInsights;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading;
using System.Threading.Tasks;

namespace DarlCoreBot2.Bots
{
    public class DarlBot : ActivityHandler
    {

        GraphQLClient client = null;
        IConfiguration _config;
        string botModelName;


        public DarlBot(IConfiguration config)
        {
            _config = config;
            client = new GraphQLClient(_config["DarlAPIAddress"]);
            var authcode = _config["DarlAPIKey"];
            client.DefaultRequestHeaders.Add("Authorization", $"Basic {authcode}");
            client.Options.JsonSerializerSettings.Converters.Add(new StringEnumConverter());
            botModelName = _config["DarlBotModel"];
        }


        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            try
            {
                var req = new GraphQLRequest()
                {
                    Variables = new { model = botModelName, convId = turnContext.Activity.Conversation.Id, data = new { name = "", Value = turnContext.Activity.Text, dataType = DarlVar.DataType.textual } },
                    Query = @"query Interact($model: String!, $convId: String!, $data: darlVarUpdate!){interact(botModelName: $model, conversationId: $convId, conversationData: $data){ response { value dataType approximate categories } }}",
                    OperationName = "Interact"
                };
                var resp = await client.PostAsync(req);
                var responses = resp.GetDataFieldAs<List<InteractResponse>>("interact");

                foreach (var r in responses)
                {
                    Activity m = MessageFactory.Text("internal error");
                    switch (r.response.dataType)
                    {
                        case DarlVar.DataType.categorical:
                            m = MessageFactory.SuggestedActions(r.response.categories.Select( a => a.Key), r.response.value) as Activity;
                            break;

                        case DarlVar.DataType.link:
                            m = MessageFactory.Text($"[{r.response.value}]({r.response.value})");
                            break;

                        default:
                            if (!string.IsNullOrEmpty(r.response.value))
                            {
                                m = MessageFactory.Text(r.response.value);
                            }
                            break;
                    }
                    await turnContext.SendActivityAsync(m, cancellationToken);
                }
            }
            catch(Exception ex)
            {
                await turnContext.SendActivityAsync(MessageFactory.Text(ex.Message), cancellationToken);
            }
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(_config["initialText"]), cancellationToken);
                }
            }
        }
    }
}
