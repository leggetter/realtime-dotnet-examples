# Real-Time .NET Examples

Examples of real-time frameworks that you can use when building an ASP.NET applications.

The example applications in this repo are real-time chat apps.

The frameworks in the examples are:

* [SignalR](http://www.asp.net/signalr)
* [XSockets](https://xsockets.net/)
* [PubNub](https://www.pubnub.com)
* [Pusher](https://pusher.com)

Samples were written for a talk on **Real-Time Web Apps & .NET - What are Your Options?**

* [Slides](https://leggetter.github.io/realtime-dotnet)
* [Slide source](https://github.com/leggetter/realtime-dotnet)

## Prerequisites

* Visual Studio 2015
* .NET Framework 4.5
* For the Pusher demo you will require a set of Pusher credentials

## Getting Started

Open `ASP.NET MVC5 Realtime Chat.sln` in Visual Studio, run the project and a web page should open showing links to all the examples. It's suggested that you open two windows side-by-side to see each demo in action. Of course, you have to be running the same example in both windows in order to see the real-time updates.

## Developing with WebHooks

I recommend [ngrok](https://ngrok.io) as a localtunnel solution. This will expose your localhost publicly so that Nexmo can make a webhook callback to your localhost server.

```
ngrok http --host-header=localhost 55097
```

If you have subdomain support you can use somethink like:

```
ngrok http --subdomain=realtime --host-header=localhost 55097
```

## Contribution

Bug fixes and generally improvements also appreciated but I'd very much appreciate contribution to demonstrate [using other real-time solutions and frameworks](https://github.com/leggetter/realtime-dotnet-examples/issues?q=is%3Aopen+is%3Aissue+label%3Aframework).

Please see the [full list of issues](https://github.com/leggetter/realtime-dotnet-examples/issues) and see if you can help there too.

I am trying to make sure that PubSub, Evented PubSub and RMI/RPC are demonstrated within the examples. So please consider this before updating an example to use a different communication pattern.

When contributing please try not make large changes to keep commits focused.

## License

MIT - See [LICENSE](LICENSE)
