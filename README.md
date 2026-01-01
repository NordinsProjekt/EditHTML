# EditHTML

A console-based HTML editor built in C# with syntax highlighting support for HTML tags and attributes.

## Overview

EditHTML is a lightweight, terminal-based HTML editor that provides syntax highlighting for HTML elements and attributes. The project demonstrates console UI manipulation, HTML parsing, and keyboard navigation in a .NET 9 application.

## Features

### ✅ Implemented
- **HTML Syntax Highlighting**: Color-coded HTML tags (green), attributes (yellow), and values (cyan)
- **Keyboard Navigation**: Arrow key support for moving the cursor through the document
- **HTML Tag Validation**: Recognizes valid HTML tags and attributes
- **Console-based Editing**: Full terminal-based editing experience

### 🚧 In Development
- **Undo/Redo**: History tracking for edit operations
- **Line Numbers**: Display and navigation by line number
- **Goto Line Command**: Jump to a specific line in the document
- **Exit Command**: Graceful exit with file save options

## Project Structure

```
EditHTML/
├── EditHTML/                    # Main application entry point
│   ├── Program.cs              # Application entry point and main loop
│   └── EditHTML.csproj         # Project configuration
├── HTMLTagColorer/             # HTML parsing and syntax highlighting
│   ├── HTMLService.cs          # Core HTML parsing and rendering logic
│   ├── HTMLKeywords.cs         # HTML tag and attribute validation
│   └── HTMLTagColorer.csproj   # Project configuration
├── ConsoleEditLogic/           # Console UI and cursor management
│   ├── ConsoleCursorService.cs # Cursor positioning and navigation
│   ├── DisplayService.cs       # Console display utilities
│   └── ConsoleEditLogic.csproj # Project configuration
├── HTMLTagColorerTests/        # Unit tests
│   └── HTMLTagColorerTests.csproj
└── README.md                   # This file
```

## Getting Started

### Prerequisites
- .NET 9 SDK or later
- A console terminal that supports ANSI color codes

### Installation

1. Clone the repository:
```bash
git clone https://github.com/NordinsProjekt/EditHTML.git
cd EditHTML
```

2. Build the project:
```bash
dotnet build
```

3. Run the application:
```bash
cd EditHTML
dotnet run
```

The editor will open the `index.html` file in the output directory with syntax highlighting applied.

## Usage

### Navigation
- **Arrow Keys**: Move the cursor up, down, left, and right through the document
- **Home/End**: Jump to the beginning or end of a line

### Planned Features
- **`:exit`**: Exit the editor (with save prompts)
- **`:goto <line>`**: Jump to a specific line number
- **`Ctrl+Z`**: Undo the last change
- **`Ctrl+Y`**: Redo the last change
- **Line numbers** will be displayed on the left margin

## Color Scheme

- **Green**: Valid HTML tags (e.g., `<div>`, `<h1>`)
- **White**: Invalid/unrecognized HTML tags
- **Yellow**: Valid HTML attributes (e.g., `class`, `id`)
- **Cyan**: Attribute values (e.g., `"example"`)
- **Default**: Regular text content

## Architecture

### HTMLTagColorer
Handles HTML parsing and syntax highlighting:
- `ParseHtml()`: Processes HTML text and writes it to console with appropriate colors
- `HtmlKeywords`: Database of valid HTML tags and attributes

### ConsoleEditLogic
Manages console UI interactions:
- `ConsoleCursorService`: Handles cursor movement and position tracking
- `DisplayService`: Provides console output utilities

### Main Application
- Reads `index.html` into memory
- Renders the HTML with syntax highlighting
- Listens for keyboard input and processes navigation commands

## License

This project is licensed under the **MIT License**. See the LICENSE file for details.

## Status

⚠️ **This project is not finished yet.**

### Planned Improvements
- [ ] Implement undo/redo functionality with command history
- [ ] Add line number display
- [ ] Implement goto line command (`:goto <line>`)
- [ ] Implement exit command with save confirmation
- [ ] Support for editing content (insert, delete, backspace)
- [ ] Search and replace functionality
- [ ] Improved error handling and edge case management
- [ ] Configuration file support for custom color schemes

## Contributing

This is a school project. Feel free to fork and experiment, but please note this is part of an educational curriculum.

## Author

[NordinsProjekt](https://github.com/NordinsProjekt)

---

**Last Updated**: 2026
