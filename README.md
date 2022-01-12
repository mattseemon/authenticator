<h1 align="center">Authenticator</h1>

<p align="center"><img width="200px" height="200px" src="./assets/Logo-256.png" alt="Authenticator" /></p>

<p align="center">Secure, Two Factor Authenticator<br/></p>

<p align="center"><img src="https://img.shields.io/github/license/mattseemon/authenticator?style=for-the-badge" alt="License"/></p>

<p align="center"><br/>[ <a href="#getting_started">Getting Started</a> | <a href="#installation">Installation</a> | <a href="#development">Development</a> | <a href="#contributing">Contributing</a> | <a href="#credits">Credits</a> | <a href="#license">License</a> | <a href="#author">Author</a> ]<br/><br/></p>

<a name="about"></a>
While securing my online accounts with 2 Factor Authentication, I wanted a simple and secure windows desktop application to generate my TOTP codes.

## Getting Started <a name="getting_started"></a>

To run this application locally, you can use the following guide.

1. Clone this repository 
   ```bash
   $ git clone https://github.com/mattseemon/authenticator
   ```
2. Open solution `src/authenticator.sln` in Visual Studio 2022
3. Press `F5` to run application in debug mode or `Ctrl+F5` to run application normally.

## Installation <a name="installation"></a>

Channel | Release
------- | -------
Stable | [![Stable Release](https://img.shields.io/github/v/release/mattseemon/authenticator?label=%20&logo=windows&style=for-the-badge)](https://github.com/mattseemon/authenticator/releases/latest)
Pre Release | [![Pre Release](https://img.shields.io/github/v/release/mattseemon/authenticator?include_prereleases&label=%20&logo=windows&style=for-the-badge)](https://github.com/mattseemon/authenticator/releases)

 * Future application updates are installed directly from within the App.

## Development <a name="development"></a>

Authenticator was developed using the below tools and technologies.
 * [Visual Studio 2022 Community Edition](https://visualstudio.microsoft.com/)
 * [Windows Presentation Foundation](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/introduction-to-wpf?view=netframeworkdesktop-4.8)
 * [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
 * [.NET Framework](https://docs.microsoft.com/en-gb/dotnet/)

### Libraries used

 Library | Version 
 ------- | -------
[MahApps.Metro](https://github.com/MahApps/MahApps.Metro)|2.4.9
[MahApps.Metro.IconPacks.Material](https://github.com/MahApps/MahApps.Metro.IconPacks)|4.11.0
[Microsoft.Extensions.Hosting](https://github.com/dotnet/runtime)|6.0.0
[Microsoft.Extensions.Caching.Memory](https://github.com/dotnet/runtime)|6.0.0
[Microsoft.Toolkit.Mvvm](https://github.com/windows-toolkit/WindowsCommunityToolkit)|7.1.2
[Hardcodet.NotifyIcon.Wpf](https://github.com/hardcodet/wpf-notifyicon)|1.1.0

\* Instead of using the published packages, either included the code as-is or a modified version of the same.

## Contributing <a name="contributing"></a>

If you want to contribute to the project, check out the wiki article [here](https://github.com/mattseemon/authenticator/wiki/Contributing-to-authenticator-Project). 

## Credits <a name="credits"></a>

 * Application Icon from the [Google Suits](https://www.iconfinder.com/iconsets/google-suits-1) icon set by [Chamestudio Pvt Ltd](https://www.iconfinder.com/chamedesign).
 * Full list of [3rd Party Notices](3rd-Party-Notices.md)

## License <a name="license"></a>
The source code in this repository is covered under the MIT License listed [here](LICENSE]). Feel free to use this in your own projects with attribution!

> Copyright (c) 2022 Matt Seemon
>  
> Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
> 
> The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
> 
> THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

## Author <a name="author"></a>

[Matt Seemon](@mattseemon)

<p align="center">Built with <img src="./assets/heart.png" alt="Matt Seemon" /> in Goa, India.</p>
<p align="center"><img src="https://forthebadge.com/images/badges/open-source.svg" alt="Open Source" />&nbsp;
  <img src="https://forthebadge.com/images/badges/you-didnt-ask-for-this.svg" alt="You didn't ask for this" />&nbsp;
  <img src="https://forthebadge.com/images/badges/powered-by-responsibility.svg" alt="Powered By Responsibility"/></p>
