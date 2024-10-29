# Electricity Billing Application

This repository contains the source code for the Electricity Billing Application.

## Setup and Run Instructions

1. Make sure you have .NET SDK 6.0 or later installed. You can download it from [here](https://dotnet.microsoft.com/download/dotnet/6.0).
2. Clone the repository and navigate into the root directory.
3. Restore the NuGet packages by running `dotnet restore`.
4. Add a `appsettings.json` file in the `ElectricityBill.Api` folder with the following content:

    ```json
    {
      "Email": {
        "Smtp": "your-smtp-server",
        "Port": 587,
        "Username": "your-smtp-username",
        "Password": "your-smtp-password"
      },
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft": "Warning",
          "Microsoft.Hosting.Lifetime": "Information"
        }
      },
      "AllowedHosts": "*"
    }
    ```

    Replace the `Smtp`, `Username`, and `Password` fields with the appropriate email server credentials.

5. Run the application by executing `dotnet run` in the `ElectricityBill.Api` folder.

## Event Handling and Design Decisions

The Electricity Billing Application uses the MediatR library to handle commands and events. Commands and events are used to encapsulate user requests and system notifications. The design aims to provide a clean, modular, and extensible architecture.

The application uses the following design patterns:

- Command: Represents a request to execute a write operation.
- Command Handler: Processes a command and performs the necessary operations.
- Event: Represents a domain notification.
- Event Handler: Processes an event and performs necessary operations, such as sending notifications.

This approach enables a clear separation of concerns and simplifies unit testing.

## SMS Notification Configuration and Usage

The provided solution does not include SMS notifications out of the box. However, you can extend the `NotificationService` and the `IMessageFormatter` to support SMS notifications through an SMS gateway. To do this, follow these steps:

1. Implement a new `SmsMessageFormatter` for formatting SMS messages.
2. Implement a new `ISmsClient` to send SMS messages using the SMS gateway.
3. Update `NotificationService` to support both email and SMS notifications.

For instance, you can use Twilio as your SMS gateway. Check [Twilio's documentation](https://www.twilio.com/docs/sms/quickstart/csharp) for details on how to send SMS messages using C#.

