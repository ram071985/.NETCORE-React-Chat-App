# The Gab Room Chat  (React.js, C# ASP.NET Core, PostgreSQL, NUnit)

Chat application inspired by the construct of Slack and Facebook.  Users can create an account and chat with other users in the main chat channel.

See my deployed version on Azure - [HERE](https://reidchatapp.azurewebsites.net)

![](src/chat_app.png)

## Summary

Over the span of a couple months I built this app to increase my knowledge of the C# framework .NET Core, client/server-side relationships and management/utilization of a relational SQL database in which I chose the powerful PostgreSQL.  I also have always been fasinated with how chat applicatiions work since my earliest internet experience with AOL chat rooms.   

I used React.js to handle the client-side and React-Router library for handling routhing between React components.  When it came to styling this app, I chose a mix of css and Bootstrap because Bootstrap's library creates a very conveinant solution to screen responsiveness and layout which can come in handy when you are under a time crunch.  I also added several css edits to the Bootstrap for custimization such as container and div sizes and their positioning along with media quiries for screen break points.  

The back-end of my application was built with C# .NET Core which has been a wonderful framework for seprerating client-side and server-side buisiness logic.  HTTP requests are made from the client-side and sent to the internal API endpoints in a controller.  From there, the controller connects to services which then make the appropriate SQL calls to the PostgreSQL requesting or posting data back from and to the front-end.  For the SQL calls I had some help from the open-source data provider Npgsql, which allows access to the PostgreSQL server for programs written in C#.  For unit testing, I used Nunit framework to perform tests on important functionalities such as creating a new user, and sending a new message.



## Installation Instructions

1.) Run `npm i` in the terminal (in the root/API directory)

2.) Push the play button in VS or run `dotnet watch run` in terminal

3.) Open a browser and navigate to `localhost:5001`


## Author 

* **Reid Muchow** - *Full-Stack Web Developer* - [Website](https://www.reidmuchow.com) | [LinkedIn](https://www.linkedin.com/in/reidmuchow/)
