
# DragDropManager for Windows Forms

`DragDropManager` is a C# class designed to easily enable drag-and-drop functionality for Windows Forms applications, handling files, folders, and text. The class provides customizable handlers to define specific actions for each type of dropped item, making it flexible and reusable across different UI controls.

## Features
- Handles drag-and-drop events for files, folders, and text.
- Customizable handlers for each drop type.
- Easily integrated with any Windows Forms control.

## Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/Francois284ModzDev/DragDropManager.git
   ```
2. Add `DragDropManager.cs` to your Windows Forms project.

## Usage
### 1. Initialize `DragDropManager` in Your Form
In your form (e.g., `Form1`), create an instance of `DragDropManager` for any control you want to enable drag-and-drop functionality on. For example, to enable drag-and-drop on a `TextBox`:

```csharp
TextBox myTextBox = new TextBox { Multiline = true, Width = 300, Height = 200 };
DragDropManager dragDropManager = new DragDropManager(
    targetControl: myTextBox,
    onFileDrop: filePath => myTextBox.AppendText($"File dropped: {filePath}{Environment.NewLine}"),
    onFolderDrop: folderPath => myTextBox.AppendText($"Folder dropped: {folderPath}{Environment.NewLine}"),
    onTextDrop: text => myTextBox.AppendText($"Text dropped: {text}{Environment.NewLine}")
);
```

### 2. Custom Handlers
Customize what happens on file, folder, or text drop by providing handlers for `onFileDrop`, `onFolderDrop`, and `onTextDrop`.

Example:
```csharp
DragDropManager dragDropManager = new DragDropManager(
    targetControl: TextBox1,
    onFileDrop: file => TextBox1.AppendText($"File dropped: {file}{Environment.NewLine}"),
    onFolderDrop: folder => TextBox1.AppendText($"Folder dropped: {folder}{Environment.NewLine}"),
    onTextDrop: text => TextBox1.AppendText($"Text dropped: {text}{Environment.NewLine}")
);
```

## Example Code

Here is an example of integrating `DragDropManager` in a Windows Forms project to handle drag-and-drop for a `TextBox` control.

```csharp
public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        // Initialize DragDropManager for TextBox1
        DragDropManager dragDropManager = new DragDropManager(
            targetControl: TextBox1,
            onFileDrop: file => HandleFileDrop(file),
            onFolderDrop: folder => HandleFolderDrop(folder),
            onTextDrop: text => HandleTextDrop(text)
        );
    }

    private void HandleFileDrop(string filePath)
    {
        TextBox1.AppendText($"File dropped: {filePath}{Environment.NewLine}");
    }

    private void HandleFolderDrop(string folderPath)
    {
        TextBox1.AppendText($"Folder dropped: {folderPath}{Environment.NewLine}");
    }

    private void HandleTextDrop(string text)
    {
        TextBox1.AppendText($"Text dropped: {text}{Environment.NewLine}");
    }
}
```

## License
This project is licensed under the MIT License. See `LICENSE` for more details.
