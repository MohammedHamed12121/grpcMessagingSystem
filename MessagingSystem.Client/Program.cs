// See https://aka.ms/new-console-template for more information
using Grpc.Net.Client;
using MessagingSystem.Client;
using Microsoft.VisualBasic;

Console.WriteLine("Hello, World!");

var channel = GrpcChannel.ForAddress("http://localhost:5236");
var client = new MessagingService.MessagingServiceClient(channel);
Console.WriteLine("WHO ARE YOU?");
var user = Console.ReadLine();


bool running = true;
while (running)
{   
    var command = PrintCommands();
    if (command == "1" || command == "List" || command == "l")
    {
        var response = await client.GetUserMessagesAsync(new GetUserMessagesRequest { UserId = user });

        PrintResponse(response.ToString());

    }else if(command == "send" || command == "s" || command == "2")
    {
        Console.WriteLine("To whom..?");
        var reciever = Console.ReadLine();
        var content = Console.ReadLine();
        
        var response = await client.CreateMessageAsync(new CreateMessageRequest { SenderId = user, RecipientId = reciever , MessageContent= content });

        PrintResponse(response.ToString());

    }else 
    {
        running = false;
    }
}





string PrintCommands()
{
    Console.WriteLine("Type one of those commands......");
    Console.WriteLine("1. list messages(l) \t 2.send(s) \t 3. anything to exit");
    return Console.ReadLine();
}

void PrintResponse(string response)
{
    Console.WriteLine("--------------------------------------");
    Console.WriteLine(response);
    Console.WriteLine("--------------------------------------");
}