using UDPServerApp;

string host;
int listenPort = 11000;
ServerCon serverCon = new(listenPort);
host = serverCon.GetLocalIPAddress();

Thread thread = new(new ThreadStart(serverCon.StartListener));
thread.Start();
