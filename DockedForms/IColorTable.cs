using System.Drawing;

namespace DocumentForms
{
    /// <summary>
    /// Defines the color table used in the <see cref="DocumentPanelRenderer"/>.
    /// </summary>
    public interface IColorTable
    {
        /// <summary>
        /// Gets the background color used for the <see cref="DocumentPanel"/> buttons panel.
        /// </summary>
        Color DocumentTabBackground { get; }

        /// <summary>
        /// Gets the background color used for the <see cref="DocumentPanel"/> buttons panel separator line.
        /// </summary>
        Color DocumentTabBottomBackground { get; }

        /// <summary>
        /// Gets the background color for the <see cref="DocumentPanel"/>.
        /// </summary>
        Color DocumentPanelBackground { get; }

        /// <summary>
        /// Gets the background color for the button of the active document.
        /// </summary>
        Color ActiveDocumentTabBackground { get; }

        /// <summary>
        /// Gets the background color for the separator line of the button of the active document.
        /// </summary>
        Color ActiveDocumentTabBottomBackground { get; }

        /// <summary>
        /// Gets the foreground color for the button of the active document.
        /// </summary>
        Color ActiveDocumentTabForeground { get; }

        /// <summary>
        /// Gets the font for the button of the active document.
        /// </summary>
        Font ActiveDocumentTabFont { get; }

        /// <summary>
        /// Gets the background color for the button of an inactive document.
        /// </summary>
        Color InactiveDocumentTabBackground { get; }

        /// <summary>
        /// Gets the background color for the separator line of a button of an inactive document.
        /// </summary>
        Color InactiveDocumentTabBottomBackground { get; }

        /// <summary>
        /// Gets the foreground color for the button of an inactive document.
        /// </summary>
        Color InactiveDocumentTabForeground { get; }

        /// <summary>
        /// Gets the font for the button of an inactive document.
        /// </summary>
        Font InactiveDocumentTabFont { get; }

        /// <summary>
        /// Gets the background color of the close, scroll left, scroll right or menu buttons.
        /// </summary>
        Color ButtonBackground { get; }
        
        /// <summary>
        /// Gets the foreground color of the close, scroll left, scroll right or menu buttons.
        /// </summary>
        Color ButtonForeground { get; }

        /// <summary>
        /// Gets the background color of the close, scroll left, scroll right or menu buttons when the mouse is hovering over the button.
        /// </summary>
        Color ButtonHighlightBackground { get; }

        /// <summary>
        /// Gets the foreground color of the close, scroll left, scroll right or menu buttons when the mouse is hovering over the button.
        /// </summary>
        Color ButtonHighlightForeground { get; }

        /// <summary>
        /// Gets the background color of the close, scroll left, scroll right or menu buttons when it's pressed.
        /// </summary>
        Color ButtonPressedBackground { get; }

        /// <summary>
        /// Gets the foreground color of the close, scroll left, scroll right or menu buttons when it's pressed.
        /// </summary>
        Color ButtonPressedForeground { get; }
    }
}