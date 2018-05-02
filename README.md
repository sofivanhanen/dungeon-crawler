# Dungeon Crawler


A Diablo / Fate -style dungeon crawler game, made with Unity, using C#. Made as part of **OTMK18**.

Builds in the Builds-public folder, and found as releases. Move with arrow keys or WASD, click to attack. Pressing escape exits the game.

## Releases

**Most recent release:**
[v0.9.1](https://github.com/sofivanhanen/dungeon-crawler/releases/tag/v0.9.1)

First pre-release:
[v0.9](https://github.com/sofivanhanen/dungeon-crawler/releases/tag/v0.9)

## Testing

Unit tests are located in the Assets/Editor folder. In order to run them, open the project in Unity, open the Test Runner (Window - Test Runner) and select EditMode.

## Code analysis

I use ReSharper for code analysis. In order to run the analysis, open the project in Unity, select Assets - Open C# Project, open the generated .sln file with an IDE with ReSharper (I use JetBrains Rider), and run code analysis (In Rider, Code - Inspect code). My ReSharper settings are still default.

## Documentation

### Generating html docs from xml tags

You can probably get this done with something like Visual Studio as well. However, [Doxygen](http://www.stack.nl/~dimitri/doxygen/) offers an easy way to do it and it works on Win, Mac and Linux:

[Install Doxygen](http://www.stack.nl/~dimitri/doxygen/download.html) (simple installation for Linux [here](http://xmodulo.com/how-to-generate-documentation-from-source-code-in-linux.html)), navigate to root of project, run command 'doxygen dungeon-crawler.conf', navigate to the generated html folder, run 'firefox index.html' (or use your favourite web browser).

### Files

[Määrittelydokumentti (Specification document)](https://github.com/sofivanhanen/dungeon-crawler/blob/master/dokumentointi/M%C3%A4%C3%A4rittelydokumentti.md)

[Tuntikirjanpito (Time tracking)](https://github.com/sofivanhanen/dungeon-crawler/blob/master/dokumentointi/Tuntikirjanpito.md)

[Arkkitehtuurikuvaus (Architecture / diagrams)](https://github.com/sofivanhanen/dungeon-crawler/blob/master/dokumentointi/Arkkitehtuuri.md)

[Käyttöohje (Install instructions / how to play)](https://github.com/sofivanhanen/dungeon-crawler/blob/master/dokumentointi/K%C3%A4ytt%C3%B6ohje.md)
