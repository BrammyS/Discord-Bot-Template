<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->

[![Issues][issues-shield]][issues-url]
[![License][license-shield]][license-url]
[![Stargazers][stars-shield]][stars-url]

<!-- PROJECT LOGO -->
<br />
<p align="center">
  <a href="https://github.com/BrammyS/Discord-Bot-Template">
    <img src="https://cdn.brammys.com/2022/02/9c725f64003b63d995003932536c1064.png" style="border-radius: 15%" alt="Logo" width="100">
  </a>

<h3 align="center">Discord Bot Template</h3>

  <p align="center">
    A simple ready to use Discord Bot Template using slash commands and buttons!
    <br />
    <br />
    <a href="https://discord.com/oauth2/authorize?client_id=541336442979483658&permissions=268561494&scope=applications.commands%20bot">View Demo</a>
    ·
    <a href="https://github.com/BrammyS/Discord-Bot-Template/issues">Report Bug</a>
    ·
    <a href="https://github.com/BrammyS/Discord-Bot-Template/issues">Request Feature</a>
  </p>
</p>

<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary><h2 style="display: inline-block">Table of Contents</h2></summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#acknowledgements">Acknowledgements</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->

## About The Project

A simple Discord Bot Template build with
the [Color-Chan.Discord library](https://github.com/Color-Chan/Color-Chan.Discord).
This template includes basic commands including a help command with multiple pages using buttons. Plus a
simple [MongoDB](https://www.mongodb.com/) connection.

- Basic commands
- Persistent command request logging
- Command usage metrics
- MongoDB connection

### Default commands

![Default commands image](https://cdn.brammys.com/file/brammys/2022/02/2BihLcHT0XJRxhIGs5el3xELXgKAjChIEowCdtJXUeigSuUna93f2kjne69KwLoe.png)

### Built With

* [.NET 7](https://dotnet.microsoft.com/download/dotnet/7.0)
* [ASP.NET](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-7.0)
* [Color-Chan.Discord](https://github.com/Color-Chan/Color-Chan.Discord)
* [Serilog.Sinks.Mongodb.TimeSeries](https://github.com/BrammyS/Serilog.Sinks.Mongodb.TimeSeries)

<!-- GETTING STARTED -->

## Getting Started

To get a local copy up and running follow these simple steps.

### Prerequisites

* [.NET 7](https://dotnet.microsoft.com/download/dotnet/7.0)

### Installation

#### Using the template

See
the [Create reposotiry from a template](https://docs.github.com/en/repositories/creating-and-managing-repositories/creating-a-repository-from-a-template)
guide for a more detailed explanation.

1. Navigate to the main page of the repository.
2. Above the file list, click `Use this template`.
   ![Use template button](https://cdn.brammys.com/file/brammys/screenshots/2022/02/use-this-template-button.png)
3. Enter the name for your new repository and choose a visibility option.
4. Click the `Create repositoiry from template` button.

#### Cloning

1. Clone the repo
   ```sh
   git clone https://github.com/BrammyS/Discord-Bot-Template.git
   ```
2. Go to the git folder
   ```sh
   cd Discord-Bot-Template
   ```
3. Run the local dev build
   ```sh
   dotnet build
   ```

### Usage

#### Secrets

To run the Bot you will need to set a couple of secrets first.
You can set these secret either as environment variables or using the [dotnet secret manager](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets#set-a-secret).

- `BOT_TOKEN` : Add your bot token as the value. The bot token van be found [here](https://discord.com/developers/applications/).
- `MONGO_CON_STRING` : Add your MongoDB connection string as the value. You can get a free MongoDB host on [MongoDB Atlas](https://www.mongodb.com/atlas/database).

#### Constants

After this you need to set the `PublicKey` and the `BotId` for your bot. These can be set in
the [Constants.cs](https://github.com/BrammyS/Discord-Bot-Template/blob/dev/src/Bot.Discord/Constants.cs) class in
Bot.Discord.

### URL

Set the interaction end point for your bot. You will need to add this URL to
you [application](https://discord.com/developers/applications/).
The interaction endpoint is located at `https://YOUR_DOMAIN.COM:5001/api/v1/discord/interaction`. Please do keep in mind
that you will need to be able to access this endpoint from outside of your network.
[![interactionUrlSetup](https://cdn.colorchan.com/examples/interactionUrlExample.png)](https://discord.com/developers/applications/)

<!-- CONTRIBUTING -->

## Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any
contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<!-- LICENSE -->

## License

Distributed under the MIT License. See `LICENSE` for more information.



<!-- ACKNOWLEDGEMENTS -->

## Acknowledgements

* [Mongodb](https://www.mongodb.com/)
* [Discord](https://discord.com/developers/docs/intro)

<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->

[stars-shield]: https://img.shields.io/github/stars/BrammyS/Discord-Bot-Template?style=for-the-badge

[stars-url]: https://github.com/BrammyS/Discord-Bot-Template/stargazers

[issues-shield]: https://img.shields.io/github/issues/BrammyS/Discord-Bot-Template?style=for-the-badge

[issues-url]: https://github.com/BrammyS/Discord-Bot-Template/issues

[license-shield]: https://img.shields.io/github/license/BrammyS/Discord-Bot-Template?style=for-the-badge

[license-url]: https://github.com/BrammyS/Discord-Bot-Template/blob/main/LICENSE
