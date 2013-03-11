using System.Drawing;

namespace DocumentForms
{
    /// <summary>
    /// Defines the color table used in the <see cref="DocumentPanelRenderer"/>.
    /// </summary>
    public interface IColorTable
    {
        Color DocumentTabBackground { get; }
        Color DocumentTabBottomBackground { get; }

        Color ActiveDocumentTabBackground { get; }
        Color ActiveDocumentTabBottomBackground { get; }
        Color ActiveDocumentTabForeground { get; }
        Font ActiveDocumentTabFont { get; }

        Color InactiveDocumentTabBackground { get; }
        Color InactiveDocumentTabBottomBackground { get; }
        Color InactiveDocumentTabForeground { get; }
        Font InactiveDocumentTabFont { get; }

        Color ButtonBackground { get; }
        Color ButtonForeground { get; }

        Color ButtonHighlightBackground { get; }
        Color ButtonHighlightForeground { get; }

        Color ButtonPressedBackground { get; }
        Color ButtonPressedForeground { get; }
    }
}