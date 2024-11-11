using System;
using System.IO;
using System.Windows.Forms;

/// <summary>
/// DragDropManager class to handle drag-and-drop functionality for files, folders, and text data.
/// This class allows flexible handling by providing customizable handlers for each type of drop.
/// </summary>
public class DragDropManager
{
    private Control targetControl;          // The UI control where drag-and-drop is enabled
    private Action<string> onFileDrop;      // Action to handle file drops
    private Action<string> onFolderDrop;    // Action to handle folder drops
    private Action<string> onTextDrop;      // Action to handle text drops

    /// <summary>
    /// Initializes a new instance of the DragDropManager class.
    /// </summary>
    /// <param name="targetControl">The control to enable drag-and-drop for.</param>
    /// <param name="onFileDrop">Action to handle file drops, receives file path as a string.</param>
    /// <param name="onFolderDrop">Action to handle folder drops, receives folder path as a string.</param>
    /// <param name="onTextDrop">Action to handle text drops, receives dropped text as a string.</param>
    public DragDropManager(Control targetControl, Action<string> onFileDrop = null, Action<string> onFolderDrop = null, Action<string> onTextDrop = null)
    {
        this.targetControl = targetControl;
        this.onFileDrop = onFileDrop;
        this.onFolderDrop = onFolderDrop;
        this.onTextDrop = onTextDrop;
        InitializeDragDrop();
    }

    /// <summary>
    /// Sets up the necessary drag-and-drop events for the target control.
    /// </summary>
    private void InitializeDragDrop()
    {
        targetControl.AllowDrop = true;
        targetControl.DragEnter += TargetControl_DragEnter;
        targetControl.DragDrop += TargetControl_DragDrop;
        targetControl.DragOver += TargetControl_DragOver;
    }

    /// <summary>
    /// Handles the DragEnter event, determining if the dragged data is accepted.
    /// </summary>
    private void TargetControl_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop) || e.Data.GetDataPresent(DataFormats.Text))
        {
            e.Effect = DragDropEffects.Copy;
        }
        else
        {
            e.Effect = DragDropEffects.None;
        }
    }

    /// <summary>
    /// Handles the DragDrop event, identifying whether the dropped item is a file, folder, or text.
    /// </summary>
    private void TargetControl_DragDrop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            // Handle file/folder drops
            string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string path in paths)
            {
                if (Directory.Exists(path))
                {
                    onFolderDrop?.Invoke(path); // Invoke folder drop handler
                }
                else if (File.Exists(path))
                {
                    onFileDrop?.Invoke(path); // Invoke file drop handler
                }
            }
        }
        else if (e.Data.GetDataPresent(DataFormats.Text))
        {
            // Handle text drops
            string text = (string)e.Data.GetData(DataFormats.Text);
            onTextDrop?.Invoke(text); // Use the onTextDrop handler for the text
        }
    }

    /// <summary>
    /// Ensures the copy effect when dragging over the target control.
    /// </summary>
    private void TargetControl_DragOver(object sender, DragEventArgs e)
    {
        e.Effect = DragDropEffects.Copy;
    }
}
