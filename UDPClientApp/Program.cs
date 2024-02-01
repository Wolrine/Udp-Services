using UDPServerApp;

ClientCon client = new();
int port = 11000;
string host = client.GetLocalIPAddress();

client.ConnectServer(host, port);
Console.Read();