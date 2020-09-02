# The DARL Bot
This bot interfaces with the [DARL Bot framework](https://darl.ai) to create a bot hosted on Azure. The functionality of the bot is determined by the bot model hosted on [Darl.dev](https://darl.dev) and any rulesets (skills) you attach.

## Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download) version 2.1

  ```bash
  # determine dotnet version
  dotnet --version
  ```

## To try this bot

- Set up the AppSettings.json. Create a free account at [darl.dev](https://darl.dev) and retrieve your API key as described [here](https://darl.dev/docs/GraphQL_examples/#finding-your-api-key).
 You will automatically be given a sample bot model called "thousandquestions.model" which you can insert below.

    ```json
    {
      "MicrosoftAppId": "The app id or empty for local testing",
      "MicrosoftAppPassword": "The app password or empty for local testing",
      "DarlAPIAddress": "https://darl.dev/graphql/",
      "DarlAPIKey": "The DARL API key for your account",
      "DarlBotModel": "The name of the bot model you are using",
      "initialText":  "Hello from DarlBot",
      "endpoint": "The words 'bot' or 'graph'. Use the latter for knowledge graphs "
    }
    ```

- In a terminal, navigate to `DarlCoreBot2`

    ```bash
    # change into project folder
    cd # DarlCoreBot2
    ```

- Run the bot from a terminal or from Visual Studio, choose option A or B.

  A) From a terminal

  ```bash
  # run the bot
  dotnet run
  ```

  B) Or from Visual Studio

  - Launch Visual Studio
  - File -> Open -> Project/Solution
  - Navigate to `DarlCoreBot2` folder
  - Select `DarlCoreBot2.csproj` file
  - Press `F5` to run the project

## Testing the bot using Bot Framework Emulator

[Bot Framework Emulator](https://github.com/microsoft/botframework-emulator) is a desktop application that allows bot developers to test and debug their bots on localhost or running remotely through a tunnel.

- Install the Bot Framework Emulator version 4.3.0 or greater from [here](https://github.com/Microsoft/BotFramework-Emulator/releases)

### Connect to the bot using Bot Framework Emulator

- Launch Bot Framework Emulator
- File -> Open Bot
- Enter a Bot URL of `http://localhost:3978/api/messages`

## Deploy the bot to Azure

To learn more about deploying a bot to Azure, see [Deploy your bot to Azure](https://aka.ms/azuredeployment) for a complete list of deployment instructions.

## Further reading

- [DARL Bot Framework](https://darl.dev/docs)
- [Bot Framework Documentation](https://docs.botframework.com)
- [Azure CLI](https://docs.microsoft.com/cli/azure/?view=azure-cli-latest)
- [Azure Portal](https://portal.azure.com)
- [Channels and Bot Connector Service](https://docs.microsoft.com/en-us/azure/bot-service/bot-concepts?view=azure-bot-service-4.0)
