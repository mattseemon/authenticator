# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.1.5-alpha] - 2022-01-12

### Added
	1. Views & ViewModels
		* TaskbarIcon
	2. Services
		* Storage Service
### Updated
	1. Views & ViewModels
		* SettingsPage, ShellWindow, MainPage
	2. General cleanup of References

### Removed
	1. Removed Nuget library for Newtonsoft.Json. Instead using System.Text.Json.
	
## [0.1.4-alpha] - 2022-01-11

### Added
	1. Views & ModelViews
		* CreatePasswordWindow, PasswordWindow, ChangePasswordWindow
	2. Services
		* EncryptionService, PasswordService, PasswordCacheService
	3. Models
		* SecuritySetting, StorageSettings
	4. Controls
		* SecureString Helper for PasswordBox
	6. Various Converters, Helpers and Extensions

### Updated
	1. Services
		* NavigationService, PageService, SystemService, ThemeSelectorService, ApplicationHostService
	2. Views & ModelViews
		* AboutPage, SettingsPage, HomePage, ShellWindow
	3. 3rd-Party-Notices, CHANGELOG.md

## [0.1.2-alpha] - 2022-01-09

### Added
	1. Solution & Base Projects
		- Authenticator - Authenticator User Experience
	2. Implemented combined versioning strategy for all projects.
	3. Included Project Assets (Icons, PNG, etc)
	4. 3rd-Party-Notices.md - Full list of all packages used and licenses information 
	5. CHANGELOG.md
	6. Implemented Core Services
	7. Partially Implemented About and Settings Pages

### Updated
	1. README.md - Updated with project details.